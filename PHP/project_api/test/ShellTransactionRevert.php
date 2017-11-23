<?php

/* 
 * test revertir pago..
 */

include '../Codex.inc.php';

echo "inicia la prueba\n";

$setting = new CodexSetting();
$setting->serviceUri = "https://qa-api.paycodex.com";
$setting->servicePort = "";
$setting->device = "PC_Juan";
$setting->secretPhrase = "SECRET-276700-A8FBB7-8C789C-A3978F";
$setting->clientKey = "CCODEX-5915FB-61EC74-FE5304-00000D";
$setting->qrServiceUri = "https://qa.paycodex.com/image/qrImage/:token.png";
$setting->pathImage = "/home/jcarrillo/Documents/";
$client = new CodexServiceClient($setting);


try {

    $client->connect();
    $bankProfile = $client->getBankProfileList();
    $bankProfileId = "";

    foreach ((array) $bankProfile as $key => $value) {
        $bankProfileId = $value->id;
        echo $value->id;
        break;
    }
	die;
    // solicitamos la transaccion segun el bankprofile

    $query                       = new CodexTransactionQuery();
    $query->bankProfileId        = $bankProfileId;
    $transaction                 = $client->listTransactions($query);
    $transactionToken            = "";

    foreach ((array) $transaction as $transactionKey => $transactionValue) {
        if ($transactionValue->status == 2) {
            $transactionToken = $transactionValue;
            break;
        }
    }

    $transactionRevert = $client->revertTransaction($transactionToken);
    if ($transactionRevert->status == 5) {
        echo "Transaccion revertida satisfactoriamente\n";
    } else {
        echo "No se pudo revertir la transaccion\n";
    }
} catch (CodexServiceClientException $err) {
    var_dump($err);
}
