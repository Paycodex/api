package comcodex;


/**
 * Gestión de Excepciones
 * @author Koiosoft
 *
 */
public class ServiceClientException extends Exception {
	
        /**
         * Error no capturado
        */
	public static final String ERROR_DONT_CAUGHT= "This error was not caught";
        /**
         * Error en el parseo de la respuesta del servicio
        */
	public static final String ERROR_SERVICE_PARSE_FAIL = "Error parsing response from service";
        /**
         * Código token de sesión invalido
        */
	public static final Integer ERROR_CODE_SESSION_TOKEN_INVALID = 1617;
        /**
         * Status no autorizado
        */
	public static final Integer ERROR_STATUS_UNAUTHORIZED = 401;
        /**
         * Error credenciales incorrectas
        */
        public static final String  ERROR_STATUS_UNAUTHORIZED_MESSAGE = "These credentials are invalid or unauthorized";
    
	private HttpUtils.Fail fail;
	
	/**
	 * 
	 */
	private static final long serialVersionUID = 1L;
	
	
	/**
	 * Constructor
	 * @param message String
	 */
	public  ServiceClientException ( String message )
	{
		super( message );
		this.fail = null;
	}
	
	/**
	 * Constructor
	 * @param fail HttpUtils.Fail
	 */
	public  ServiceClientException ( HttpUtils.Fail fail )
	{
		this( buildMessageFromFail(fail) );
		this.fail = fail;
	}	
	
	
	/**
	 * Constructor de mensaje fallido
	 * @param fail HttpUtils.Fail
	 * @return
	 */
	private static String buildMessageFromFail( HttpUtils.Fail fail )
	{
		String CRLF = "\r\n";
		String message = " Falla" + CRLF;
		message += " Code: " + fail.code + CRLF;
		message += " Message: " + fail.message + CRLF;
		message += CRLF;
		
		if( fail.errors != null )
		{
			for( HttpUtils.FailDetail e: fail.errors )
			{
				message += CRLF;
				message += " Parameter: " + e.param + CRLF;
				message += " Messsage: " + e.msg + CRLF;
				message += CRLF;
			}
		}
		return message;
	}
	
	
	/**
	 * Obtiene el objeto fail
	 * @return HttpUtils.Fail
	 */
	public HttpUtils.Fail getFail()
	{
		return this.fail;
	}
	
	
	/**
	 * Obtiene el código de la falla
	 * @return Integer
	 */
	public Integer getCode()
	{
            if( this.fail != null ) 
                return Integer.parseInt(this.fail.code);
            else 
                return 0;
	}
	

}
