<?php

/**\brief Solicitud HTTP.
 * 
 * 
 * @author koiosoft
 *
 */
class CodexHttpUriRequest
{

	private $entity;
	private $uri;
	private $method;
	private $headers;
	private $contentType;


	/**
	 * 
	 * @param unknown $uri
	 */
	public function __construct($uri)
	{

		$this->uri = new CodexURI($uri);
		$this->headers = array();
	}

	
	/**
	 * 
	 * @return CodexURI
	 */
	public function getURI()
	{
		return  $this->uri;
	}

	
	/**
	 * 
	 * @return string
	 */
	public function getMethod()
	{
		return $this->method;
	}

	/**
	 * 
	 * @param string $value
	 */
	public function setMethod($value)
	{
		$this->method = $value;
	}
	
	
	/**
	 * 
	 * @return CodexURLEncodedUtils
	 */
	public function getEntity()
	{
		return $this->entity;
	}
	
	
	/**
	 * 
	 * @param CodexUrlEncodedFormEntity $value
	 */
	public function setEntity(CodexUrlEncodedFormEntity $value )
	{
		$this->entity = $value;
	}

	
	/**
	 * 
	 * @param string $headerName
	 * @param string $headerValue
	 */
	public function addHeader( $headerName, $headerValue )
	{
		$this->headers[$headerName] = $headerValue;
	}
	
	/**
	 * 
	 * @return array <multitype:, string>
	 */
	public function getHeaders()
	{
		return $this->headers;
	}
	
	
	/**
	 * 
	 * @return string
	 */
	public static function getClassName()
	{
		return __CLASS__;
	}	

}