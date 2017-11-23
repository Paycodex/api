<?php

/**\brief Gestión de Excepciones.
 * 
 * @author Koiosoft
 *
 */
class CodexServiceClientException extends Exception {
	
        /**
         * Error no capturado
        */
	const ERROR_DONT_CAUGHT = "This error was not caught";
        /**
         * Error en el parseo de la respuesta del servicio
        */
	const ERROR_SERVICE_PARSE_FAIL = "Error parsing response from service";
        /**
         * Error no se pudo conectar al servicio
        */
	const ERROR_STATUS_SERVICE_UNAVAILABLE_MESSAGE = "Could not connect to server";
        /**
         * Error credenciales incorrectas
        */
	const ERROR_STATUS_UNAUTHORIZED_MESSAGE = "These credentials are invalid or unauthorized";
        /**
         * Status no autorizado
        */
	const ERROR_STATUS_UNAUTHORIZED = 401;
        /**
         * Código token de sesión invalido
        */
	const ERROR_CODE_SESSION_TOKEN_INVALID = 1617;
	
	/**
	 * Constructor
         * 
	 * @param String $message
         * @param Integer $code
	 */
	public function CodexServiceClientException ( $message, $code = null )
	{
		if ($message instanceof CodexFail) 
		{
			parent::__construct( CodexServiceClientException::buildMessageFromFail($message), is_null($code)?$message->code:$code  );
		}
		elseif (is_string($message)) 
		{
			parent::__construct($message, $code);
		}
		
	}	
	
	
	/**
	 * Construye el mensaje en base al objeto fail
         * 
	 * @param CodexHttpUtils::Fail $fail
	 * @return string
	 */
	public static function buildMessageFromFail( $fail )
	{
		$CRLF = "\r\n";
		$message = " Falla" . $CRLF;
		$message .= " Code: " . $fail->code . $CRLF;
		$message .= " Message: " . $fail->message . $CRLF;
		$message .= $CRLF;
		
		if( !is_null($fail->errors))
		{
			foreach( $fail->errors as $e )
			{
				$message .= $CRLF;
				$message .= " Parameter: " . $e->param . $CRLF;
				$message .= " Messsage: " . $e->msg . $CRLF;
				$message .= $CRLF;
			}
		}
		return $message;
	}
	

}
