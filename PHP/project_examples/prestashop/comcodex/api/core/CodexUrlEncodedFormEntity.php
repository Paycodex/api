<?php


/**
 * 
 * @author koiosoft
 *
 */
class UrlEncodedFormEntity
{
	private $parameters;
	
	
	/**
	 * 
	 * @param unknown $parameters
	 */
	public function __construct($parameters)
	{
		$this->parameters =$parameters;
	}
	
	
	/**
	 * @return array 
	 */
	public function getParameters()
	{
		return $this->parameters; 
	}
}