<?php
/**
 * Description of CodexClosingReport
 *
 * @author ebrainc
 */
class CodexClosingReport {
    
    public $hourInit;
    public $hourFinish;
    public $accountNumber;
    public $transaction;
    public $detail;
    public $totalPurchases;
    public $totalReverted;
    public $totalByCard;
    public $globalTotal;
    
    /* 
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