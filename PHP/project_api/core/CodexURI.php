<?php

/**\brief Representa a la referencia del Identificador de Recursos Uniforme (URI).
 *
 * 
 * @author Koiosoft
 *
 */
class CodexURI {

	private $url;
	private $authority;
	private $port;
	private $path;
	private $scheme;
	private $host;

	
	/**
	 * 
	 * @param string $uri
	 */
	public function __construct($url)
	{
		$this->url = $url;
		$this->scheme = parse_url($url, PHP_URL_SCHEME);
		$this->port = parse_url($url, PHP_URL_PORT);
		$this->path = parse_url($url, PHP_URL_PATH);
		$this->host = parse_url($url, PHP_URL_HOST);
		$this->authority = "";
	}

	
	/**
	 * 
	 *  @return mixed
	 */
	public function toString()
	{
		return $this->url;
	}

	
	/**
	 * 
	 * @return mixed
	 */
	public function getScheme()
	{
		return $this->scheme;
	}
	

	/**
	 * 
	 * @return mixed
	 */
	public function getAuthority()
	{
		return $this->authority;
	}

	/**
	 * 
	 * @return mixed
	 */
	public function getPort()
	{
		return $this->port;
	}

	
	/**
	 * 
	 * @return mixed
	 */
	public function getPath()
	{
		return $this->path;
	}
	
	
	/**
	 * 
	 * @return string
	 */
	public function getHost()
	{
		return $this->host;
	}

}
