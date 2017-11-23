<?php

/**
 * 
 * @author Koiosoft
 *
 */
class CodexSession
{
	public $sid;
	public $maxAge;
	public $expires;
	public $now;
	
	/**
	 * 
	 * @return string
	 */
	public function toString()
	{
		return " Token: " . $this->sid . " Time:" . $this->maxAge . " Expires:" . $this->expires . "  Created:" . $this->now;
	}		
	
	
	/**
	 * 
	 * @return string
	 */
	public static function getClass()
	{
		return __CLASS__;
	}
}


?>