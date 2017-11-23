<?php

/* 
 * test Operation Closing
 */

include '../Codex.inc.php';

echo "inicia la prueba\n";

$setting = new CodexSetting();
$setting->serviceUri = "http://192.168.0.119";
$setting->servicePort = "8082";
$setting->secretPhrase = "SECRET-39B6E2-69E352-6ACC59-A77A56";
$setting->clientKey = "CCODEX-57DAA2-AE6FC9-A67575-00000E";
$setting->qrServiceUri = "http://comcodex.local/image/qrImage/:token.png";
$setting->pathImage = "/home/jcarrillo/Documents/qr/";
$setting->device = "PC_API_TEST";

$client = new CodexServiceClient($setting);


try {

    $client->connect();
    //$bankProfile = $client->getBankProfileList();
    //$bankProfileId = "";
    /*
    foreach ((array) $bankProfile as $key => $value) {
        $bankProfileId = $value->id;
        break;
    }
    */
    // solicitamos la transaccion segun el bankprofile

    $query = new CodexTransactionQuery();
    $query->beginDate  = new DateTime();
    $query->beginDate->setDate(2016, 06, 08);
    
    $query->endDate = new DateTime();
    $query->endDate->setDate(2016, 07, 25);
    //$query->bankProfileId        = $bankProfileId;
    /*
    $transaction                 = $client->listTransactions($query);
    $transactionToken            = "";
    
    foreach($transaction as $key=>$value){
        print (is_string($value->gateWayResponse))?"si":"no";
        die("fin");
    }
    */
    
    $closingReport = $client->closingReport($query);
    var_dump($closingReport);
    
    
    
} catch (CodexServiceClientException $err) {
    //var_dump($err);
    echo $err->getMessage();
}