package comcodex;


import java.util.Comparator;
import java.util.List;
import java.util.ArrayList;
import java.util.Collections;
import java.util.Date;

import org.apache.http.client.methods.HttpGet;
import org.apache.http.client.methods.HttpPost;
import org.apache.http.client.methods.HttpUriRequest;
import org.apache.http.client.utils.URLEncodedUtils;
import org.apache.http.message.BasicNameValuePair;
import org.apache.http.NameValuePair;
 
/**
 * Gestión de autenticación
 * 
 * @author Koiosoft
 *
 */
public class Auth {
	
	public Setting setting;
	private String signature 	= "";
	private String nonce 		= "";
	
	Integer NONCE_LENGTH	= 20;
		
	/**
	 * Constructor
	 * @param setting Setting
	 */
	public Auth( Setting setting )
	{
            this.setting = setting;
	}
	
	
	/**
	 * 
	 * @param request HttpGet
         * @param sessionToken String
	 */
	public void buildHeader( HttpGet request, String sessionToken )
	{
            this.buildHeader(request,  HttpUtils.METHOD_GET, sessionToken );	
	}	
	
	
	/**
	 * 
	 * @param request HttpPost
         * @param sessionToken String
	 */
	public void buildHeader( HttpPost request, String sessionToken )
	{
            this.buildHeader(request,  HttpUtils.METHOD_POST, sessionToken );	
	}	
	
	
	/**
	 * 
	 * @param request HttpPost
         * @param sessionToken String
         * @param postParameters List
	 */
	public void buildHeader( HttpPost request, String sessionToken, List<NameValuePair> postParameters  )
	{
            this.buildHeader(request,  HttpUtils.METHOD_POST, sessionToken, postParameters );	
	}	
	
	
	
	/**
	 * 
	 * @param request HttpUriRequest
         * @param verb String
         * @param sessionToken String
         * @param postParameters List<NameValuePair>
	 */
	private void buildHeader(HttpUriRequest request, String verb, String sessionToken, List<NameValuePair> postParameters  )
	{
            String uri = request.getURI().toString();
            this.buildNonce();

            request.addHeader("oauth_uri", 				uri );		
            request.addHeader("oauth_consumer_key", 	this.getConsumerKey() );
            request.addHeader("oauth_timestamp", 		this.getTimeStamp() );
            request.addHeader("oauth_nonce", 			this.getNonce() );
            request.addHeader("oauth_signature_method", this.getSignatureMethod() );
            request.addHeader("oauth_signature", 		this.getSignature(verb, request, postParameters ) );
            request.addHeader("device", 				this.getDevice() );
            request.addHeader("Accept-Language", 		"es");

            if( sessionToken != "" )
            {
                request.addHeader("oauth_session_token", sessionToken );
            }	
	}	

	/**
	 * 
	 * @param request HttpUriRequest
         * @param verb String
         * @param sessionToken String
	 */
	private void buildHeader(HttpUriRequest request, String verb, String sessionToken )
	{
            this.buildHeader(request,  verb, sessionToken, new ArrayList<NameValuePair>() );
	}
	
	
	/**
	 * 
	 * @return String
	 */
	public String getConsumerKey()
	{
            return this.setting.clientKey;
	}
	
	/**
	 * 
	 * @return String
	 */
	public String getTimeStamp()
	{
            return Double.toString( Math.floor( (new Date()).getTime() / 1000  ) );
	}
	
	
	/**
	 * 
	 */
	public void buildNonce()
	{
            String chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXTZabcdefghiklmnopqrstuvwxyz";
            String result = "";
            for ( Integer i = 0; i < NONCE_LENGTH; ++i) {
                int rnum = (int) Math.floor( Math.random()  * chars.length() );
                result += chars.substring(rnum, rnum+1);
            }
            this.nonce = result;
	}
	
	/**
	 * 
	 * @return String
	 */
	public String getNonce()
	{
            return this.nonce;
	}
	
	
	/**
	 * 
	 * @return String
	 */
	public String getSignatureMethod()
	{
            return "RSA-SHA1";
	}
	
	
	/**
	 * 
	 * @param verb String
	 * @param request HttpUriRequest
	 * @param postParameters List
	 * @return String
	 */
	public String getSignature(String verb, HttpUriRequest request, List<NameValuePair> postParameters)
	{
            this.signature = Signature.hmacSha1(verb.toUpperCase() + "&" + this.normalizeUri( request.getURI() ) + "&" + this.toQuery( request, postParameters ), this.setting.secretPhrase);
            return this.signature;
	}
	
	
	/**
	 * 
	 * @param request HttpUriRequest
	 * @param postParameters List
	 * @return String
	 */
	public String toQuery( HttpUriRequest request, List<NameValuePair> postParameters )
	{
            List<NameValuePair> params = new ArrayList<NameValuePair>();

            params.add( new BasicNameValuePair("oauth_uri", request.getURI().toString() ));
            params.add( new BasicNameValuePair("oauth_consumer_key", this.getConsumerKey() ));
            params.add( new BasicNameValuePair("oauth_timestamp", this.getTimeStamp() ));
            params.add( new BasicNameValuePair("oauth_nonce", this.getNonce() ));
            params.add( new BasicNameValuePair("oauth_signature_method", this.getSignatureMethod()));

            for( NameValuePair postParameter:postParameters )
            {
                params.add( postParameter );
            }

            //Sorting
            Collections.sort(params, new Comparator<NameValuePair>() {
            @Override
                public int compare(NameValuePair  parameter1,NameValuePair  parameter2)
                {
                    return  parameter1.getName().compareTo(parameter2.getName());
                }
            });

            return  this.encodeURIComponent(params);
	}
	
	/**
	 * Obtiene URI normalizada
	 * @param uri URI
	 * @return String
	 */
	public String normalizeUri( java.net.URI uri)
	{	
            String scheme = uri.getScheme();
            String authority = uri.getAuthority();
            int port = uri.getPort();
            boolean dropPort = (scheme == "http" && port == 80)
            || (scheme == "https" && port == 443);
		
            if (dropPort) {
                int index = authority.lastIndexOf(":");
                if (index >= 0) {
                    authority = authority.substring(0, index);
                }
            }

            String path = uri.getPath();

            return scheme + "://" + authority + (path==""?"/":path);
	}	
	
	
	/**
	 * 
	 * @param params List
	 * @return String
	 */
	public String encodeURIComponent( List<NameValuePair> params )   {     
		String result = null;      
		try {       
			result = URLEncodedUtils.format(params, "UTF-8")   .replaceAll("\\%28", "(")                          
                                                                            .replaceAll("\\%29", ")")   		
                                                                            .replaceAll("\\+", "%20")                          
                                                                            .replaceAll("\\%27", "'")   			   
                                                                            .replaceAll("\\%21", "!")
                                                                            .replaceAll("\\%7E", "~");
		}
		catch ( Exception e) {       
			result = null;     
		}      
		return result;   
	} 	
	
	/**
	 * 
	 * @return String
	 */
	public String getDevice()
	{
		return this.setting.device.trim();
	}
}
