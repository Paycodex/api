<?php

/**\brief HTTP GET method.
 *
 * @author Koiosoft
 *
 */
class CodexHttpGet extends CodexHttpUriRequest
{

	/**
	 * 
	 * @param unknown $uri
	 */
	public function __construct($uri )
	{
		parent::__construct($uri);
		parent::setMethod( CodexHttpUtils::METHOD_GET );
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