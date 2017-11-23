<?php 


/**
 * 
 * @author Koiosoft
 *
 */
class CodexBankProfile {
	
    public $id;
    public $alias;
    public $account;
    public $bank;
    
    
    /**
     * 
     * @return string
     */
    public function toString()
    {
    	return "Id: " . $this->id . ", Alias: " . $this->alias . ", Account: " . $this->account . ", Bank: " .  $this->bank;
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
    
}


?>