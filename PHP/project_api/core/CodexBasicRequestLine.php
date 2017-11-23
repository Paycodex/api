<?php

/**\brief La primera línea de una peticion  Http Request.
 *
 * Contiene el método, URI y versión HTTP de la solicitud.
 * 
 * @author Koiosoft
 *
 */
class CodexBasicRequestLine
{

	private $method;
	private $protocolVersion;
	private $uri;
	
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
	 * @return string
	 */	
	public function getProtocolVersion() 
	{
		return $this->protocolVersion;
	}

	/**
	 * 
	 * @return string
	 */
	public function getUri()
	{
		return $this->uri;
	}

	/**
	 *  @return string
	 */
	public function toString(){

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
	 * @param string $value
	 */
	public function setProtocolVersion($value) 
	{
		$this->protocolVersion = $value;
	}
	
	/**
	 * 
	 * @param CodexURI $value
	 */
	public function setUri($value)
	{
		$this->uri = $value;
	}

	
}