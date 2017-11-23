<?php

/**
 * 
 * @author koiosoft
 *
 */
class CodexGsonBuilder
{
	/** @var $dateFormat string */
	private $dateFormat;
	
	
	public function __construct($dateFormat)
	{
		
	}
	
	
	/**
	 * 
	 * @param string $dateFormat
	 * @return CodexGsonBuilder
	 */
	public static function setDateFormat($dateFormat)
	{
		return new CodexGsonBuilder($dateFormat);
	}
	
	
	/**
	 * @return string;
	 */
	public static function getDateFormat()
	{
		return self::$dateFormat;
	}
	
	
	/**
	 * 
	 * @return CodexGson
	 */
	public function create()
	{
		return new CodexGson();
	}
}