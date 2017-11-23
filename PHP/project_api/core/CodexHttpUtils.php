<?php


/** \brief Clase utilitaria.
 * 
 * @author Koiosoft
 *
 */
class CodexHttpUtils {
	
	const HTTP_CREATED 	= 201;
	const HTTP_OK 		= 200;
	
	const METHOD_POST 	= "POST";
	const METHOD_PUT 		= "PUT";
	const METHOD_DELETE 	= "DELETE";
	const METHOD_GET 		= "GET";
	const METHOD_OPTION 	= "OPTION";
	const NONCE_LENGTH		= 20;	
	
	const URI_SESSION_OPEN 			= "/api/session";	
	const URI_BANKPROFILE_LIST 		= "/api/bank_profile/list";
	const URI_BANKPROFILE_WALLET	= "/api/bank_profile/wallet";
	const URI_TRANSACTION_OPEN 		= "/api/transaction/open";
	const URI_TRANSACTION_QR 		= "/api/transaction/qrcode";
	const URI_TRANSACTION_LIST		= "/api/search/transactions";
	const URI_TRANSACTION_GET		= "/api/transaction";
    const URI_TRANSACTION_REVERT    = "/api/transaction/cancels";
    const URI_TRANSACTION_CLOSED_REPORT = "/api/transaction/generateClosed";
	
	const URI_SEPARATOR 			= "/";
	
	const DEFAULT_FORMAT_DATE		= "Y-m-d\TH:i:s.uP";
	const DEFAULT_CHARSET			= "UTF-8";
	
	const DEFAULT_IMAGE_EXTENSION 	=  ".png";
	
	
	/**
	 * 
	 * @param HttpResponse $response
	 * @return null
	 * @throws CodexServiceClientException
	 */
	public static function processFailResponse(codexHttpResponse $response)
	{
		$fail = new CodexFail();
		try 
		{
			$fail->code = json_decode($response->getStatusLine()->getStatusCode());

		}catch (Exception $e) 
		{	
			$fail->code = "";
		} 

		$json = CodexEntityUtils::toString( $response->getEntity(), "UTF-8");
		try 
		{
			
			if($response->getStatusLine()->getStatusCode() == CodexServiceClientException::ERROR_STATUS_UNAUTHORIZED){
				
				$fail->message = CodexServiceClientException::ERROR_STATUS_UNAUTHORIZED_MESSAGE;
				return $fail;
			}else{
				
				$gson = CodexGsonBuilder::setDateFormat(CodexHttpUtils::DEFAULT_FORMAT_DATE)->create();	
				return $gson->fromJson($json, CodexFail::getClassName() ); 
			}
			
		} 
		catch (Exception $e) 
		{	
			throw new CodexServiceClientException( $json, $fail->code);
		} 
		catch (IOException $e) 
		{ 
			throw new CodexServiceClientException( $e->getMessage(), $fail->code );
		}
		return  null;
	}
	
}


	/**\brief Falla.
	 * 
	 * Falla.
	 * 
	 * @author Koiosoft
	 *
	 */
	class CodexFail
	{
		public $code;
		public $message;
		/**
		 * FailDetail[] errors
		 */
		public $errors = array();
		
		
		/**
		 * 
		 * @return string
		 */
		public static function getClassName()
		{
			return __CLASS__;
		}
		
		
		/**
		 * 
		 */
		public function __toString()
		{
			return $this->code . " " . $this->message;
		}
	}
	
	
	/**\brief Detalle de Falla.
	 * 
	 * Detalle de Falla
	 * @author Koiosoft
	 *
	 */
	class CodexFailDetail
	{
		public $param;
		public $msg;
		
		/**
		 *
		 * @return string
		 */
		public static function getClassName()
		{
			return __CLASS__;
		}
		

		/**
		 * @return string
		 */
		public static function getArrayClassName()
		{
			return self::getClassName() . "[]";
		}		

		/**
		 *
		 */
		public function __toString()
		{
			return $this->param . " " . $this->msg;
				
		}
		
		
	}

?>