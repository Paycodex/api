/*
 * Created by SharpDevelop.
 * User: koiosoft
 * Date: 17/09/2014
 * Time: 18:21
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Comcodex
{
 
	/// <summary>
	/// Clase utilitaria.
	/// </summary>
	public class HttpUtils 
	{
		
		public const int 	HTTP_CREATED 			= 201;
		public const int 	HTTP_OK 				= 200;
		public const int 	HTTP_NONE 				= 0;
		
		public const string 	METHOD_POST 			= "POST";
		public const string 	METHOD_PUT 				= "PUT";
		public const string 	METHOD_DELETE 			= "DELETE";
		public const string 	METHOD_GET 				= "GET";
		public const string 	METHOD_OPTION 			= "OPTION";
		public const int 	NONCE_LENGTH				= 20;	
		
		public const string URI_SESSION_OPEN 		= "/api/session";	
		public const string URI_BANKPROFILE_LIST 	= "/api/bank_profile/list";
		public const string URI_TRANSACTION_OPEN 	= "/api/transaction/open";
		public const string URI_TRANSACTION_QR 		= "/api/transaction/qrcode";
		public const string URI_TRANSACTION_LIST		= "/api/search/transactions";
		public const string URI_TRANSACTION_GET		= "/api/transaction";
        public const string URI_TRANSACTION_REVERT		= "/api/transaction/cancels";
        public const string URI_TRANSACTION_CLOSED_REPORT		= "/api/transaction/generateClosed";

		public const string URI_SEPARATOR 			= "/";
		
		public const string DEFAULT_FORMAT_DATE		= "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fffZ";
		public const string DEFAULT_CHARSET			= "UTF-8";
		
		public const string DEFAULT_IMAGE_EXTENSION 	=  ".png";
		private static readonly DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
		
		public static Int64 ConvertToTimestamp(DateTime value)
		{
		    TimeSpan elapsedTime = value - Epoch;
		    string stringMilliseconds = elapsedTime.TotalMilliseconds.ToString();
		    Int64 intMilliseconds =  Convert.ToInt64(stringMilliseconds);
		    return intMilliseconds;
		}
		
		/// <summary>
		/// Procesa una respuesta fallida HttpResponse.
		/// </summary>
		public static HttpUtils.Fail processFailResponse(HttpResponse response) 
		{
	
			Gson gson = (new GsonBuilder()).SetDateFormat(HttpUtils.DEFAULT_FORMAT_DATE).Create();
			return gson.FromJson<HttpUtils.Fail>( EntityUtils.ToString( response.Entity, HttpUtils.DEFAULT_CHARSET) );
		}
	
		/// <summary>
		/// Falla.
		/// </summary>
		public class Fail
		{
			public String code;
			public String message;
			public FailDetail[] errors;
		}
		
		
		/// <summary>
		/// Detalle de Falla.
		/// </summary>
		public class FailDetail
		{
			public String param;
			public String msg;
		}
		
	}

	
}
