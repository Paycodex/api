<?php

/**
 * 
 * @author jcarrillo
 *
 */
class CodexHttpPost extends CodexHttpUriRequest
{

	/**
	 * 
	 * @param unknown $uri
	 */
	public function __construct($uri)
	{
		parent::__construct($uri);
		parent::setMethod( CodexHttpUtils::METHOD_POST );
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