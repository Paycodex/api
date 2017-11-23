<?php

/**
 * 
 * @author koiosoft
 *
 */
class CodexQrImage {
	
	public $qrUrl;
	

	/**
	 * @return string
	 */
	public static function getClassName()
	{
		return __CLASS__;
	}
	
	/**
	 * @return string
	 */
	public static function getArrayClassName()
	{
		return self::getClassName() . "[]";
	}	

}
?>