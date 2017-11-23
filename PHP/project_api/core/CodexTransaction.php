<?php


/**\brief Transacción.
 *
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
    
    /**
     * Transacción revertida
    */
    const STATUS_REVERT = 5;
    /**
     * Pago rechazado
    */
    const STATUS_REJECTED = 3;
    /**
     * Pago fallido
    */
    const STATUS_FAIL = 4;
    /**
     * Pago exitoso
    */
    const STATUS_OK = 2;
    /**
     * Transacción en espera
    */
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
    
    /**
     * Asigna la respuesta del gateway
     * @param string $value
     */
    public function setGateWayResponse($value){
        $this->gateWayResponse = $value;
    }
    
    /**
     * Obtiene la respuesta del gateway
     * @return string
     */
    public function getGateWayResponse(){
        return $this->gateWayResponse;
    }
    
    
		
}

?>