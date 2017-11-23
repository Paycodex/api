<?php

/**\brief Respuesta Gateway.
 * 
 *
 * @author ebrainc
 */
class CodexGatewayResponse {
    
    public $gateWay;
    public $source;
    public $orderNumber;
    public $reference;
    public $statusCode;
    public $message;
    public $status;
    
    /*
     * @return string
     */

    public static function getClassName() {
        return __CLASS__;
    }

    /**
     * @return string
     */
    public static function getArrayClassName() {
        return self::getClassName() . "[]";
    }

}
