<?php

/**\brief Entidad HTTP.
 *
 * 
 * @author Koiosoft
 *
 */
class CodexHttpEntity
{

	/** @var $content string */
	private $content;
	
	
	/**
	 * 
	 * @param strinig $content
	 */
	public function __construct($content)
	{
		$this->content = $content;
	}

	
	/**
	 * 
	 * @return string
	 */
	public function getContent()
	{
		return $this->content;
	}
	
	
	/**
	 * 
	 * @param string $value
	 */
	public function setContent($value)
	{
		$this->content = $value;
	}
}