<?php


/**
 * 
 * @author Koiosoft
 *
 */
class CodexTransaction {

	public $id;
	public $openDate;
	public $device;
	public $bankProfileId;
	public $amount;
	public $concept;
	public $token;
	public $payed;
	public $payedDate;
	public $payedCardHolder;
	public $payedCardNumber;
	public $payedIdentity;
    public $deviceId;
    public $gateWayResponse;
	public $status;
    
    
    const STATUS_REVERT = 5;
    const STATUS_REJECTED = 3;
    const STATUS_FAIL = 4;
    const STATUS_OK = 2;
    const STATUS_WAITTING = 0;


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
    
    public function setGateWayResponse($value){
        $this->gateWayResponse = $value;
    }
    
    /**
     * 
     * @return string
     */
    public function getGateWayResponse(){
        return $this->gateWayResponse;
    }
    
    
		
}

?>