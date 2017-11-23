/*
 * Created by SharpDevelop.
 * User: koiosoft
 * Date: 18/09/2014
 * Time: 10:27
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Net;
using System.Collections.Generic;
using System.Collections;
using System.Web;
using System.Text.RegularExpressions;

namespace Comcodex
{
	/// <summary>
	/// Gestión de autenticación.
	/// </summary>
	public class Auth
	{


		public 	Setting 		setting		= null;
		private string 		signature 	= "";
		private string 		nonce 		= "";
		
		const int NONCE_DEFAULT_LENGTH	= 20;
		
			
		/// <summary>
		/// 
		/// </summary>
		/// <param name="setting"></param>
		public Auth( Setting setting )
		{
			this.setting = setting;
		}
		
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="request"></param>
		/// <param name="sessionToken"></param>
		public void buildHeader( HttpGet request, string sessionToken )
		{
			this.buildHeader(request,  HttpUtils.METHOD_GET, sessionToken );	
		}	
		
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="request"></param>
		/// <param name="sessionToken"></param>
		public void buildHeader( HttpPost request, string sessionToken )
		{
			this.buildHeader(request,  HttpUtils.METHOD_POST, sessionToken );	
		}	
		
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="request"></param>
		/// <param name="sessionToken"></param>
		/// <param name="postParameters"></param>
		public void buildHeader( HttpPost request, string sessionToken, List<NameValuePair> postParameters  )
		{
			this.buildHeader(request,  HttpUtils.METHOD_POST, sessionToken, postParameters );	
		}	
		
		
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="request"></param>
		/// <param name="verb"></param>
		/// <param name="sessionToken"></param>
		/// <param name="postParameters"></param>
		private void buildHeader(HttpUriRequest request, String verb, String sessionToken, List<NameValuePair> postParameters  )
		{
			string uri = request.Uri.ToString();
			this.buildNonce();
	
			request.addHeader("oauth_uri", 				uri );		
			request.addHeader("oauth_consumer_key", 	this.getConsumerKey() );
			request.addHeader("oauth_timestamp", 		this.getTimeStamp() );
			request.addHeader("oauth_nonce", 			this.getNonce() );
			request.addHeader("oauth_signature_method", this.getSignatureMethod() );
			request.addHeader("oauth_signature", 		this.getSignature(verb, request, postParameters ) );
            request.addHeader("device",                 this.getDevice());
			request.addHeader("Accept-Language", 		"es");

			if( sessionToken != "" )
			{
				request.addHeader("oauth_session_token", sessionToken );
			}	
		}	
	
		/// <summary>
		/// 
		/// </summary>
		/// <param name="request"></param>
		/// <param name="verb"></param>
		/// <param name="sessionToken"></param>
		private void buildHeader(HttpUriRequest request, string verb, string sessionToken )
		{
			this.buildHeader(request,  verb, sessionToken, new List<NameValuePair>() );
		}
		
		
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public String getConsumerKey()
		{
			return this.setting.clientKey;
		}
		 
		
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public String getTimeStamp()
		{
			long ticks = DateTime.UtcNow.Ticks - DateTime.Parse("01/01/1970 00:00:00").Ticks;
			ticks /= 10000000;
			return ticks.ToString();
		}
		
		
		/// <summary>
		/// 
		/// </summary>
		public void buildNonce()
		{

			Random r = new Random();
			string chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXTZabcdefghiklmnopqrstuvwxyz";
			string result = "";
	        for ( int i = 0; i < NONCE_DEFAULT_LENGTH; ++i) {
				int rnum = (int)( r.NextDouble()  * chars.Length );
	            result += chars.Substring(rnum, 1);
	        }
	        this.nonce = result;
		}
		
		
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public String getNonce()
		{
			return this.nonce;
		}
		
		
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public String getSignatureMethod()
		{
			return "RSA-SHA1";
		}
		
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="verb"></param>
		/// <param name="request"></param>
		/// <param name="postParameters"></param>
		/// <returns></returns>
		public String getSignature(String verb, HttpUriRequest request, List<NameValuePair> postParameters)
		{
			this.signature = Signature.hmacSha1(verb.ToUpper() + "&" + this.normalizeUri( request.Uri ) + "&" + this.toQuery( request, postParameters ), this.setting.secretPhrase);
			return this.signature;
		}
		
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="request"></param>
		/// <param name="postParameters"></param>
		/// <returns></returns>
		public String toQuery( HttpUriRequest request, List<NameValuePair> postParameters )
		{
			List<NameValuePair> parameters = new List<NameValuePair>();		
			try
			{
				parameters.Add( new NameValuePair("oauth_uri", 				request.Uri.ToString() ));
				parameters.Add( new NameValuePair("oauth_consumer_key", 		this.getConsumerKey() ));
				parameters.Add( new NameValuePair("oauth_timestamp", 		this.getTimeStamp() ));
				parameters.Add( new NameValuePair("oauth_nonce", 			this.getNonce() ));
				parameters.Add( new NameValuePair("oauth_signature_method", 	this.getSignatureMethod()));
				
				foreach( NameValuePair postParameter in postParameters )
				{
					parameters.Add( new NameValuePair( postParameter.Name, postParameter.Value ) );
				}		
				parameters.Sort();			
				
			}
			catch(ServiceClientException ex )
			{
				throw new ServiceClientException( ex.Message );
			}
			catch(Exception)
			{
				throw new ServiceClientException( "Fail building parameters query to Digital Sign." );
			}
			
			return  this.encodeURIComponent(parameters);
		}
		
		
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="uri"></param>
		/// <returns></returns>
		public string normalizeUri( Uri uri)
		{	
			string authority = uri.Authority;
			bool dropPort = (uri.Scheme == "http" && uri.Port == 80) || (uri.Scheme == "https" && uri.Port == 443);
	        if (dropPort) {
	            int index = authority.LastIndexOf(":");
	            if (index >= 0) {
	                authority = authority.Substring(0, index);
	            }
	        }
	        return uri.Scheme + "://" + authority + ( uri.AbsolutePath ==""?"/":uri.AbsolutePath);
		}	
		
		
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="?"></param>
		/// <returns></returns>
		public String encodeURIComponent( List<NameValuePair> parameters )   
		{ 	
			try 
			{
				string result = "";
				
				foreach( NameValuePair parameter in parameters )
				{
					result += (result==""?"":"&") + parameter.Name + "=" + HttpUtility.UrlEncode(parameter.Value, System.Text.Encoding.UTF8);
				}
				result = Regex.Replace(result, "(%[0-9a-f][0-9a-f])", c => c.Value.ToUpper());
				result = Regex.Replace(result,	"\\%28",  	"(");
				result = Regex.Replace(result, 	"\\%29",  	")");
				result = Regex.Replace(result, 	"\\+", 		"%20");
				result = Regex.Replace(result, 	"\\%27", 	"'");
				result = Regex.Replace(result, 	"\\%21", 	"!");
				result = Regex.Replace(result, 	"\\%7E", 	"~");
				
				return result;
			}
			catch ( Exception ) 
			{
				throw new ServiceClientException("Fail encoding parameters to request");
			}     
		} 			
		
        /// <summary>
        /// 
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public String getDevice()
        {
            return this.setting.device.Trim();
        }
	}
}
