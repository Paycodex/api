<?php

/**
 * 
 * @author Koiosoft
 *
 */
class CodexServiceClientException extends Exception {
	
	/**
	 * final String 
	 */
	const ERROR_DONT_CAUGHT = "This error was not caught";
	const ERROR_SERVICE_PARSE_FAIL = "Error parsing response from service";
	const ERROR_STATUS_SERVICE_UNAVAILABLE_MESSAGE = "Could not connect to server";
	const ERROR_STATUS_UNAUTHORIZED_MESSAGE = "These credentials are invalid or unauthorized";
	const ERROR_STATUS_UNAUTHORIZED = 401;
	const ERROR_CODE_SESSION_TOKEN_INVALID = 1617;
	
	/**
	 * 
	 * @param CodexHttpUtils::Fail $fail
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
			foreach( $fail->errors as $e ) 		// $e => FailDetail
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
