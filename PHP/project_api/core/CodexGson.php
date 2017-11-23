<?php



/**\brief Convertidor de objetos de Java y Json.
 *
 * 
 * @author Koiosoft
 *
 */
class CodexGson
{
	
	const DATETIME_CLASS = "DateTime";
	
	
	/**
	 * 
	 * @param string $object
	 * @return mixed (CodexSession|CodexTransaction)
	 */
	public function fromJson( $object, $classOfT )
	{
		
		switch( $classOfT )
		{
			case CodexSession::getClass():
				try 
				{
					$response_array = json_decode($object, true);
					
					$session = new CodexSession();
					
					$session->sid = $response_array["sid"];
					$session->expires = $response_array["expires"];
					$session->maxAge = $response_array["maxAge"];
					$session->now = $response_array["now"];
					
					return $session;
				}
				catch(Exception $ex )
				{
					throw new CodexServiceClientException( $ex->getMessage() );
				}

				break;
			case  CodexBankProfile::getArrayClassName():
				
				$response_array = json_decode($object, true);
				
				$resultFromJson = array();
				
				foreach(  $response_array as $key => $value )
				{
					$bankProfile = new CodexBankProfile();
					$bankProfile->account 	= $value["account"];
					$bankProfile->alias 	= $value["alias"];
					$bankProfile->bank 		= $value["bank"];
					$bankProfile->id 		= $value["id"];
					$resultFromJson[] = $bankProfile;
				}
				
				return $resultFromJson;
				
				break;
				
			case CodexTransaction::getArrayClassName():
				
				$object_decode = json_decode($object, true);
				
				$resultFromJson = array();
				
				foreach(  $object_decode as $key => $value )
				{
					
					$transaction = new CodexTransaction();
					$transaction->amount 			= self::getValue($value, "amount", CodexDecimal::getClassName());
					$transaction->bankProfileId 	= self::getValue($value,"bankProfileId");
					$transaction->concept 			= self::getValue($value,"concept");
					$transaction->device 			= self::getValue($value,"device");
					$transaction->id 				= self::getValue($value,"id");
					$transaction->openDate 			= self::getValue($value,"openDate");
					$transaction->payed 			= self::getValue($value,"payed");
					$transaction->payedCardHolder 	= self::getValue($value,"payedCardHolder");
					$transaction->payedCardNumber 	= self::getValue($value,"payedCardNumber");
					$transaction->payedDate 		= self::getValue($value,"payedDate");
					$transaction->payedIdentity 	= self::getValue($value,"payedIdentity");
					$transaction->status 			= self::getValue($value,"status");
					$transaction->token 			= self::getValue($value,"token");
                    $transaction->gateWayResponse   = self::getGatewayResponseDecode(self::getValue($value, "gateWayResponse"));

					$resultFromJson[] = $transaction;
				}
				
				return $resultFromJson;
				
				break;
				
			case CodexTransaction::getClassName():

				$object_decode = json_decode($object, true);
				
				$transaction = new CodexTransaction();
				
				$transaction->amount 			= self::getValue($object_decode,"amount", CodexDecimal::getClassName());
				$transaction->bankProfileId 	= self::getValue($object_decode,"bankProfileId");
				$transaction->concept 			= self::getValue($object_decode,"concept");
				$transaction->device 			= self::getValue($object_decode,"device");
				$transaction->id 				= self::getValue($object_decode,"id");
				$transaction->openDate 			= self::getValue($object_decode,"openDate", self::DATETIME_CLASS ); 
				$transaction->payed 			= self::getValue($object_decode,"payed");
				$transaction->payedCardHolder 	= self::getValue($object_decode,"payedCardHolder");
				$transaction->payedCardNumber 	= self::getValue($object_decode,"payedCardNumber");
				$transaction->payedDate 		= self::getValue($object_decode,"payedDate", self::DATETIME_CLASS); 
				$transaction->payedIdentity 	= self::getValue($object_decode,"payedIdentity");
				$transaction->status 			= self::getValue($object_decode,"status");
				$transaction->token 			= self::getValue($object_decode,"token");
                $transaction->gateWayResponse   = self::getValue($object_decode, "gateWayResponse",CodexGatewayResponse::getClassName());
				
				return $transaction;
				
				break;
				
			case CodexQrImage::getClassName():
				
				$object_decode = json_decode($object, true);
				
				$qrImage = new CodexQrImage();
				$qrImage->qrUrl = self::getValue($object_decode,"qrUrl");

				return $qrImage;
				
				break;
            
            case CodexClosingReport::getClassName():
                
                $object_decode = json_decode($object,true);
                
                $closingReport = new CodexClosingReport();
                $closingReport->hourInit            = self::getValue($object_decode, "hourInit");
                $closingReport->hourFinish          = self::getValue($object_decode, "hourFinish");
                $closingReport->accountNumber       = self::getValue($object_decode, "accountNumber");
                $closingReport->transaction         = self::getValue($object_decode, "transaction",  CodexTransaction::getArrayClassName());
                $closingReport->detail              = json_decode(self::getValue($object_decode, "detail"),true);
                $closingReport->totalPurchases      = self::getValue($object_decode, "totalPurchases");
                $closingReport->totalReverted       = self::getValue($object_decode, "totalReverted");
                $closingReport->totalByCard         = json_decode(self::getValue($object_decode, "totalByCard"));
                $closingReport->globalTotal         = self::getValue($object_decode, "globalTotal");
                
                return $closingReport;
                
                break;
				
			case CodexFail::getClassName():
				
				$object_decode = json_decode($object, true);
				
				$fail = new CodexFail();
				$fail->code     = self::getValue($object_decode,"code");
				$fail->message  = self::getValue($object_decode,"message");
				$fail->errors   = self::getValue($object_decode,"errors", CodexFailDetail::getArrayClassName());

				return $fail; 
				
				break;
		}
		
	}
    
	/**
	 *
	 * @param unknown $object
	 * @param unknown $key
	 * @return Ambigous <NULL, unknown>
	 */
	public static function getValue($object, $key, $className=null)
	{
		$value = null;
		
		switch($className)
		{
			case CodexDecimal::getClassName():
				$object = $object[$key];	
				$value = new CodexDecimal($object["number"], $object["decimal"]);
				break;
				
			case CodexDecimal::getClassName():
				if(isset($object[$key]))
				{
					$value = DateTime::createFromFormat( CodexHttpUtils::DEFAULT_FORMAT_DATE,$object[$key]);	
				}				
				break;
            case CodexGatewayResponse::getClassName():
                
                $gateWayResponse = new CodexGateWayResponse();
                if (is_null($object[$key])) {
                    $value = $gateWayResponse;
                } else {
                    
                    $jsonGateWayResponse            = json_decode($object[$key],true);
                    
                    $gateWayResponse->gateWay       = $jsonGateWayResponse["gateWay"];
                    $gateWayResponse->source        = $jsonGateWayResponse["source"];
                    $gateWayResponse->orderNumber   = $jsonGateWayResponse["orderNumber"];
                    $gateWayResponse->reference     = $jsonGateWayResponse["reference"];
                    $gateWayResponse->statusCode    = $jsonGateWayResponse["statusCode"];
                    $gateWayResponse->message       = $jsonGateWayResponse["message"];
                    $gateWayResponse->status        = $jsonGateWayResponse["status"];

                    $value = $gateWayResponse;
                }

                break;
                
            case CodexTransaction::getArrayClassName():
                
                $transactionDecode      = json_decode($object[$key],true);
                $transactionList        = Array();
                
                foreach ($transactionDecode as $key=>$transactionRow){
                    
                    $transaction                        = new CodexTransaction();
                    $transaction->amount                = self::getValue($transactionRow, "amount", CodexDecimal::getClassName());
                    $transaction->bankProfileId         = self::getValue($transactionRow, "bankProfileId");
                    $transaction->concept               = self::getValue($transactionRow, "concept");
                    $transaction->device                = self::getValue($transactionRow, "device");
                    $transaction->id                    = self::getValue($transactionRow, "id");
                    $transaction->openDate              = self::getValue($transactionRow, "openDate", self::DATETIME_CLASS);
                    $transaction->payed                 = self::getValue($transactionRow, "payed");
                    $transaction->payedCardHolder       = self::getValue($transactionRow, "payedCardHolder");
                    $transaction->payedCardNumber       = self::getValue($transactionRow, "payedCardNumber");
                    $transaction->payedDate             = self::getValue($transactionRow, "payedDate", self::DATETIME_CLASS);
                    $transaction->payedIdentity         = self::getValue($transactionRow, "payedIdentity");
                    $transaction->status                = self::getValue($transactionRow, "status");
                    $transaction->token                 = self::getValue($transactionRow, "token");
                    $transaction->gateWayResponse       = self::getValue($transactionRow, "gateWayResponse", CodexGatewayResponse::getClassName());
                    
                    $transactionList[] = $transaction;
                    
                }

                $value = $transactionList;
                break;
				
			case CodexFailDetail::getArrayClassName():
				$object = $object[$key];
				$value = array();
				foreach( $object as $error )
				{
					
					$failDetail = new CodexFailDetail();
					$failDetail->msg = $error["msg"];
					$failDetail->param = $error["param"];
					$value[] = $failDetail;
				}

				break;
			
			default:
				if(isset($object[$key]))
					$value = $object[$key];				
				break;
		}		
		
		return $value;
	}
	
}