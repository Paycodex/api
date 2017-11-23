<?php

/**\brief Estado de la respuesta HTTP.
 *
 * 
 * @author koiosoft
 *
 */

class CodexStatusLine extends CodexBasicRequestLine
{

	/** @var $statusCode integer */
	private $statusCode;	
	
	/**
	 * 
	 * @param unknown $statusCode
	 */
	public function __construct($statusCode)
	{
		$this->statusCode = $statusCode;
		
	}


	
	/**
	 * return integer
	 */
	public function getStatusCode()
	{
		return $this->statusCode;
	}
	
	
	/**
	 * 
	 * @param integer $value
	 */
	public function setStatusCode($value)
	{
		$this->statusCode = $value;
	}
}