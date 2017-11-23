<?php


/**\brief Una entidad compuesta de una lista de pares de url codificadas.
 * Esto suele ser útil cuando se envía una solicitud HTTP POST.
 * 
 * @author koiosoft
 *
 */
class CodexUrlEncodedFormEntity
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