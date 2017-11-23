package comcodex;

import java.io.IOException;

import org.apache.http.HttpResponse;
import org.apache.http.ParseException;
import org.apache.http.util.EntityUtils;

import com.google.gson.JsonSyntaxException;


/**
 * Clase utilitaria
 * 
 * @author Koiosoft
 *
 */
public class HttpUtils {
	
	static int 	HTTP_CREATED 	= 201;
	static int 	HTTP_OK 		= 200;
	
	static String 	METHOD_POST 	= "POST";
	static String 	METHOD_PUT 		= "PUT";
	static String 	METHOD_DELETE 	= "DELETE";
	static String 	METHOD_GET 		= "GET";
	static String 	METHOD_OPTION 	= "OPTION";
	static Integer NONCE_LENGTH		= 20;	
	
	static String URI_SESSION_OPEN 			= "/api/session";	
	static String URI_BANKPROFILE_LIST 		= "/api/bank_profile/list";
        static String URI_BANKPROFILE_WALLET 		= "/api/bank_profile/wallet";
	static String URI_TRANSACTION_OPEN 		= "/api/transaction/open";
	static String URI_TRANSACTION_QR 		= "/api/transaction/qrcode";
	static String URI_TRANSACTION_LIST		= "/api/search/transactions";
	static String URI_TRANSACTION_GET		= "/api/transaction";
    static String URI_TRANSACTION_REVERT    = "/api/transaction/cancels";
    static String URI_TRANSACTION_CLOSED_REPORT     = "/api/transaction/generateClosed";
	
	static String URI_SEPARATOR 			= "/";
	
	static String DEFAULT_FORMAT_DATE		= "yyyy-MM-dd'T'HH:mm:ss.SSS";
	static String DEFAULT_CHARSET			= "UTF-8";
	
	static String DEFAULT_IMAGE_EXTENSION 	=  ".png";
	
	/**
	 * Procesa una respuesta fallida HttpResponse
	 * @param response HttpResponse
	 * @return HttpUtils.Fail
	 * @throws ServiceClientException 
	 * @throws org.apache.http.ParseException
	 * @throws IOException
	 */
	public static HttpUtils.Fail processFailResponse(HttpResponse response) throws ServiceClientException 
	{
		String json;
		try {
			
			json = EntityUtils.toString( response.getEntity(), "UTF-8");
			return (new com.google.gson.Gson()).fromJson(json, HttpUtils.Fail.class );
            
		} 
		catch (ParseException e) 
		{
			throw new ServiceClientException( ServiceClientException.ERROR_SERVICE_PARSE_FAIL );
		} 
		catch (IOException e) 
		{
			throw new ServiceClientException( ServiceClientException.ERROR_SERVICE_PARSE_FAIL );
		}
		catch( JsonSyntaxException e) 
		{
			throw new ServiceClientException( ServiceClientException.ERROR_SERVICE_PARSE_FAIL );
		}
	}

	/**
	 * Falla
	 * 
	 * @author Koiosoft
	 *
	 */
	public class Fail
	{
		public String code;
		public String message;
		public FailDetail[] errors;
		
		public String toString()
		{
			String CRLF = "\r\n";
			String message = " Falla" + CRLF;
			message += " Code: " + this.code + CRLF;
			message += " Message: " + this.message + CRLF;
			message += CRLF;
			
			if( this.errors != null )
			{
				for( HttpUtils.FailDetail e: this.errors )
				{
					message += CRLF;
					message += " Parameter: " + e.param + CRLF;
					message += " Messsage: " + e.msg + CRLF;
					message += CRLF;
				}
			}
			return message;
		}
	}
	
	
	/**
	 * Detalle de Falla
	 * @author Koiosoft
	 *
	 */
	public class FailDetail
	{
		public String param;
		public String msg;
	}
	
}
