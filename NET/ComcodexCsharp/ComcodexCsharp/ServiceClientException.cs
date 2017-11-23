/*
 * Created by SharpDevelop.
 * User: koiosoft
 * Date: 17/09/2014
 * Time: 11:42
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Net;

namespace Comcodex
{
 
	/// <summary>
	/// Gestión de Excepciones.
	/// </summary>
	public class ServiceClientException : Exception
	{
		
		/// <summary>
		/// Error no capturado.
		/// </summary>
		public const  string ERROR_DONT_CAUGHT= "This error was not caught";
		/// <summary>
		/// Error en el parseo de la respuesta del servicio.
		/// </summary>
		public const  string ERROR_SERVICE_PARSE_FAIL= "Error parsing response from service";
		/// <summary>
		/// Error credenciales incorrectas.
		/// </summary>
		public const string ERROR_STATUS_UNAUTHORIZED_MESSAGE = "These credentials are invalid or unauthorized";
		/// <summary>
		/// Error conexión con el servidor.
		/// </summary>
		public const string ERROR_STATUS_SERVICE_UNAVAILABLE_MESSAGE = "Could not connect to server";
		
		/// <summary>
		/// Serial.
		/// </summary>
		public const  long serialVersionUID = 1L;
		/// <summary>
		/// Código token de sesión invalido.
		/// </summary>
		public const int ERROR_CODE_SESSION_TOKEN_INVALID = 1617;
		/// <summary>
		/// Status no autorizado.
		/// </summary>
		public const int ERROR_STATUS_UNAUTHORIZED = 401;
		
		

		
		private HttpUtils.Fail fail = new HttpUtils.Fail();
		
		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="message"></param>
		public  ServiceClientException ( String message ): base (message)
		{
			if(message == ServiceClientException.ERROR_STATUS_UNAUTHORIZED_MESSAGE){
				this.fail.code = ServiceClientException.ERROR_STATUS_UNAUTHORIZED.ToString();
			}
			
			this.fail.message = message;
		}
		
		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="fail"></param>
		public  ServiceClientException ( HttpUtils.Fail fail ): this( ServiceClientException.buildMessageFromFail(fail))
		{

		}	
		
		
		/// <summary>
		/// Constructor de mensaje fallido.
		/// </summary>
		/// <param name="fail"></param>
		/// <returns></returns>
		private static String buildMessageFromFail( HttpUtils.Fail fail )
		{
			string CRLF = "\r\n";
			string message = " Falla" + CRLF;
			message += " Code: " + fail.code + CRLF;
			message += " Message: " + fail.message + CRLF;
			message += " ------ Detalle ------ " + CRLF;
			
			if( fail.errors != null )
			{
				foreach( HttpUtils.FailDetail e in fail.errors )
				{
					message += " Parameter: " + e.param + CRLF;
					message += " Messsage: " + e.msg + CRLF;
					message += CRLF;
				}
			}
			return message;
		}
		
		/// <summary>
		/// Obtiene el código de la falla.
		/// </summary>
		/// <param name="fail"></param>
		/// <returns></returns>
		public int getCode()
		{
			if( this.fail != null ) 
				return Convert.ToInt16(this.fail.code);
			else 
				return 0;
		}

		
	
	}
	
	
}
