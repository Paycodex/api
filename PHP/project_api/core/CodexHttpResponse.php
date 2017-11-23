<?php


/**\brief Respuesta HTTP.
 * 
 * @author Koiosoft
 *
 */
class CodexHttpResponse
{
	/* @var $requestLine CodexStatusLine */
	public $statusLine;
	public $entity;

	
	/**
	 * 
	 * @param string $resultClass
	 */
	public function  __construct( $resultClass = null)
	{
		$this->resultClass = $resultClass;
	}
	
	/**
	 * 
	 * @return CodexStatusLine
	 */
	public function getStatusLine()
	{  
		return $this->statusLine;
	}
	
	
	/**
	 * 
	 * @param CodexStatusLine $value
	 */
	public function setStatusLine(CodexStatusLine $value)
	{
		$this->statusLine = $value;
	}
	
	
	
	/**
	 * 
	 * @return CodexHttpEntity
	 */
	public function getEntity()
	{
		return $this->entity;
	}
	
	
	/**
	 * 
	 * @param CodexHttpEntity $value
	 */
	public function setEntity($value)
	{
		$this->entity = $value;
	}

	
}