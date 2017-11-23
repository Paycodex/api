<?php 

/**
 * 
 * @author Koiosoft
 *
 */
class CodexDecimal {
	
	public $number;
	public $decimal;
	
	/**
	 * 
	 * @param number
	 * @param decimal
	 */
	public function CodexDecimal( $number, $decimal )
	{
		$this->number = $number;
		$this->decimal = $decimal;
	}
	
	/**
	 * 
	 * @return
	 */
	public function toDouble()
	{	
		return floatval($this->__toString());
	}
	
	
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
	
	
	/**
	 * 
	 * @return string
	 */
	public function __toString()
	{
		return $this->number . "." . $this->decimal;
	}
	
	
}

?>