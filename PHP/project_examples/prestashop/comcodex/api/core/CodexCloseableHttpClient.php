<?php

/**
 * 
 * @author Koiosoft
 *
 */
class CodexCloseableHttpClient
{

	public function __construct($uri = null)
	{
		$this->uri = $uri;
	}

	/**
	 * 
	 * @param CodexHttpUriRequest $request
	 * @throws Exception
	 * @return mixed|CodexHttpResponse
	 */
	public function execute(CodexHttpUriRequest $request)
	{
		/* @var $request CodexHttpUriRequest */

		$curl_init = curl_init();

		curl_setopt($curl_init, CURLOPT_URL, $request->getURI()->toString() );
		curl_setopt($curl_init, CURLOPT_RETURNTRANSFER, true);
		curl_setopt($curl_init, CURLOPT_BINARYTRANSFER, true);
		
		//
		//  Se establece el metodo de la solicitud
		//
		if ( $request->getMethod() == CodexHttpUtils::METHOD_POST )
		{
			curl_setopt($curl_init, CURLOPT_POST, TRUE);
			
			// se pasan los parametros
			curl_setopt($curl_init, CURLOPT_POSTFIELDS, $request->getEntity()->getParameters() );
		}
		elseif ( $request->getMethod() == CodexHttpUtils::METHOD_GET )
		{
			curl_setopt($curl_init, CURLOPT_HTTPGET, TRUE);
		}
		
		//
		// se construye el header de la solicitud 
		//
		$header_curl = array();
		foreach( $request->getHeaders() as $header_key => $header_value )
		{
			$header_curl[] = $header_key .":" . $header_value;
		}
		curl_setopt($curl_init, CURLOPT_HTTPHEADER, $header_curl);
		
		$result = curl_exec($curl_init);
		
		$httpResponse = new CodexHttpResponse($request);
		$httpResponse->setStatusLine( new CodexStatusLine( curl_getinfo($curl_init, CURLINFO_HTTP_CODE) ) );
		$httpResponse->setEntity( new CodexHttpEntity($result));

		curl_close($curl_init);
		
        if($httpResponse->statusLine->getStatusCode() == 0){
            throw new CodexServiceClientException(CodexServiceClientException::ERROR_STATUS_SERVICE_UNAVAILABLE_MESSAGE, 0 );
        }
		
		return $httpResponse;
	}

	
}