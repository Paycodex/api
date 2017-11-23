<?php

/**
 * 
 * @author koiosoft
 *
 */
class CodexServiceClient {
    /**
     * Setting $setting
     */
    public $setting;
    /**
     * Integer $status
     */
    public $status = 0;
    /**
     * String $sessionToken
     */
    private $sessionToken = null;
    /**
     * Date $sessionExpires
     */
    private $sessionExpires;
    /**
     * Auth $auth
     */
    private $auth;
    
    /**
     * 
     * @return String
     */
    public function getUri()
    {
        return $this->setting->serviceUri . (trim($this->setting->servicePort) == ""?"":":".$this->setting->servicePort);
    }
    

    /**
     * 
     * @param CodexSetting $setting
     */
    public function CodexServiceClient( CodexSetting $setting = null )
    {
        $this->setting = $setting;
        $this->auth = new CodexAuth($setting);
    }
    
    /**
     * Obtiene el Token de Sesion
     * @return string
     */
    public function getSessionToken()
    {
        return $this->sessionToken;
    }
    
    /**
     * Obtiene el objeto para la Firma Digital en Auth 2
     * @return CodexAuth
     */
    private function getAuth()
    {
        if( $this->auth == null )
        {
            $this->auth = new CodexAuth($this->setting);
        }
        return $this->auth;
    }
    
    
    /**
     * void()
     * @throws CodexServiceClientException 
     * @throws Exception 
     * 
     */
    public function connect()
    {           
                try
                {   
                    $session        = $this->parseHttpResponse( $this->executeGetRequest( CodexHttpUtils::URI_SESSION_OPEN ), CodexSession::getClass(), CodexHttpUtils::HTTP_OK ); 
                    $this->sessionToken         = $session->sid;
                    $sessionExpires = new DateTime($session->expires);
                    $this->sessionExpires   = $sessionExpires->format(CodexHttpUtils::DEFAULT_FORMAT_DATE);
                }
                catch( Exception $e )
                {
                    throw new CodexServiceClientException($e->getMessage(), $e->getCode() );
                }
    }
    
    /**
     *  
     * @return BankProfile[]
     * @throws CodexServiceClientException 
     */
    private function getBankProfileList() //throws CodexServiceClientException
    {       
        return $this->parseHttpResponse( $this->executeGetRequest( CodexHttpUtils::URI_BANKPROFILE_LIST ), CodexBankProfile::getArrayClassName(), CodexHttpUtils::HTTP_OK );
    }

    /**
     *  
     * @return BankProfile
     * @throws CodexServiceClientException 
     */
    private function getBankProfileWallet() //throws CodexServiceClientException
    {       
        $bankProfiles = $this->parseHttpResponse( $this->executeGetRequest( CodexHttpUtils::URI_BANKPROFILE_WALLET ), CodexBankProfile::getArrayClassName(), CodexHttpUtils::HTTP_OK );

        return $bankProfiles[0];
    }
    
    
    /**
     * @param Transaction $newTransaction
     * @return Transaction
     * @throws UnsupportedEncodingException 
     * @throws CodexServiceClientException 
     */
    public function openTransaction($newTransaction )
    {
        try {
            
            $bankProfile = $this->getBankProfileWallet();

            $parameters = array();

            $parameters["device"] =  $this->setting->device;  
            
            $parameters["bankProfileId"] = $bankProfile->id;
            
            $parameters["concept"] =  $newTransaction->concept;     
            
            $parameters["number"] =  $newTransaction->amount->number;
            
            $parameters["decimal"] =  $newTransaction->amount->decimal;
        
            return $this->parseHttpResponse( $this->executePostRequest(CodexHttpUtils::URI_TRANSACTION_OPEN, $parameters), CodexTransaction::getClassName(), CodexHttpUtils::HTTP_CREATED ); //  Transaction.class 
            
        } 
        catch (Exception $e) 
        {
            throw new CodexServiceClientException( $e->getMessage(), $e->getCode() );
        }
    }
    
    /**
     * 
     * @param CodexTransaction $transaction
     * @return string
     * @throws CodexCodexServiceClientException 
     */
    public function getQrImage($transaction) {
        try {

            $uriQR = $this->retrieveQrUri(CodexHttpUtils::URI_TRANSACTION_QR . CodexHttpUtils::URI_SEPARATOR . $transaction->token);

            if ($uriQR != null) {
                $response = $this->executeGetImageRequest($uriQR);

                if ($response->getStatusLine()->getStatusCode() == CodexHttpUtils::HTTP_OK) {
                    try {
                        $pathFile = $this->setting->pathImage . $transaction->token . CodexHttpUtils::DEFAULT_IMAGE_EXTENSION;

                        if (file_exists($pathFile)) {
                            unlink($pathFile);
                        }
                        $fp = fopen($pathFile, 'x');
                        fwrite($fp, $response->getEntity()->getContent());
                        fclose($fp);

                        return $pathFile;
                    } catch (Exception $e) {
                        throw new CodexServiceClientException($e->getMessage(), $e->getCode());
                    }
                }
            }

            return null;
        } catch (Exception $e) {
            throw new CodexServiceClientException($e->getMessage(), $e->getCode());
        }
    }

    /**
     *
     * @param string $uri
     * @return string
     */
    public function retrieveQrUri( $uri ) 
    {
        $qrImage = $this->parseHttpResponse( $this->executeGetRequest( $uri ), CodexQrImage::getClassName(), CodexHttpUtils::HTTP_OK );
    
        if( $qrImage != null && $qrImage->qrUrl != null && $qrImage->qrUrl != "" )
        {
            return $qrImage->qrUrl;
        }
    
        return null;
    }   

    /**
     * Obtiene la Lista de Transacciones en base a un Objeto de Consulta 
     * 
     * @param CodexTransactionQuery $query (Objeto de Consulta de Lista de Transacciones)
     * @return CodexTransaction[] (Lista de transaccciones)
     * @throws CodexCodexServiceClientException
     */
    public function listTransactions(CodexTransactionQuery $query )
    {


        try {

            $bankProfile = $this->getBankProfileWallet();
            $query->bankProfileId = $bankProfile->id;

            $parameters = array();
            
            if( $query->device != "" && $query->device != null )
                $parameters[CodexTransactionQuery::FIELD_DEVICE] = $query->device;  
            
            if( $query->bankProfileId != "" && $query->bankProfileId != null )
                $parameters[CodexTransactionQuery::FIELD_BANKPROFILE_ID] = $query->bankProfileId;
            
            if( $query->beginDate != null )
                $parameters[CodexTransactionQuery::FIELD_BEGIN_DATE] = $query->beginDate->getTimestamp() * 1000;
        
            if( $query->endDate != null )
                $parameters[CodexTransactionQuery::FIELD_END_DATE] = $query->endDate->getTimestamp() * 1000;
            if ( $query->deviceId != null )
                $parameters[CodexTransactionQuery::FIELD_DEVICE_ID] = $query->deviceId;
            
            return $this->parseHttpResponse( $this->executePostRequest( CodexHttpUtils::URI_TRANSACTION_LIST, $parameters), CodexTransaction::getArrayClassName(), CodexHttpUtils::HTTP_OK ); 
            
        } 
        catch (Exception $e) 
        {
            throw new CodexServiceClientException( $e->getMessage(), $e->getCode() );
        }
    }
    
    
    /**
     * 
     * @param string $token
     * @return CodexTransaction
     * @throws CodexCodexServiceClientException
     */
    public function retrieveTransaction( $token )
    {
        try 
        {
            return $this->parseHttpResponse( $this->executeGetRequest( CodexHttpUtils::URI_TRANSACTION_GET . CodexHttpUtils::URI_SEPARATOR . $token ), CodexTransaction::getClassName() ,CodexHttpUtils::HTTP_OK );
        } 
        catch (Exception $e) 
        {
            throw new CodexServiceClientException( $e->getMessage(), $e->getCode() );
        }   
    }
    
    
    /**
     * 
     * @param string $token
     * @return CodexTransaction
     * @throws CodexCodexServiceClientException
     */
    public function revertTransaction(CodexTransaction $transaction) {
        try {
            if ($transaction->status == CodexTransaction::STATUS_OK){
                return $this->parseHttpResponse($this->executeGetRequest(CodexHttpUtils::URI_TRANSACTION_REVERT . CodexHttpUtils::URI_SEPARATOR . $transaction->token), CodexTransaction::getClassName(), CodexHttpUtils::HTTP_OK);
            }else{
                return null;
            }
        } catch (Exception $e) {
            throw new CodexServiceClientException($e->getMessage(), $e->getCode());
        }
    }
    
    /**
     * Obtiene la Lista de Transacciones en base a un Objeto de Consulta 
     * 
     * @param CodexTransactionQuery $query (Objeto de Consulta de Lista de Transacciones)
     * @return CodexTransaction[] (Lista de transaccciones)
     * @throws CodexCodexServiceClientException
     */
    public function closingReport(CodexTransactionQuery $query )
    {


        try {
                
            $bankProfile = $this->getBankProfileWallet();
            $query->bankProfileId = $bankProfile->id;

            $parameters = array();
            
            if( $query->device != "" && $query->device != null )
                $parameters[CodexTransactionQuery::FIELD_DEVICE] = $query->device;  
            
            if( $query->bankProfileId != "" && $query->bankProfileId != null )
                $parameters[CodexTransactionQuery::FIELD_BANKPROFILE_ID] = $query->bankProfileId;
            
            if( $query->beginDate != null )
                $parameters[CodexTransactionQuery::FIELD_BEGIN_DATE] = $query->beginDate->getTimestamp() * 1000;
        
            if( $query->endDate != null )
                $parameters[CodexTransactionQuery::FIELD_END_DATE] = $query->endDate->getTimestamp() * 1000;
            
            return $this->parseHttpResponse( $this->executePostRequest( CodexHttpUtils::URI_TRANSACTION_CLOSED_REPORT, $parameters), CodexClosingReport::getClassName(), CodexHttpUtils::HTTP_OK );
            
        } 
        catch (Exception $e) 
        {
            throw new CodexServiceClientException( $e->getMessage(), $e->getCode() );
        }
    }

    /**
     * 
     * @param HttpUriRequest $request
     * @return CodexHttpResponse 
     * @throws ClientProtocolException
     */
    private function executeRequest(CodexHttpUriRequest $request)
    {   
        $httpBuilder = CodexHttpClientBuilder::create()->build();
        return $httpBuilder->execute($request); 
    }
    
    
    /**
     * 
     * @param string $actionRequest
     * @return CodexHttpResponse
     * @throws ClientProtocolException 
     */
    private function executeGetRequest( $actionRequest )
    {
        $request =  new CodexHttpGet ( $this->getUri() . $actionRequest  );
        $this->getAuth()->buildHeader($request, $this->getSessionToken() ); 
        
        return $this->executeRequest($request);
    }
    
    /**
     * 
     * @param string $actionRequest
     * @param List<NameValuePair> $parameters
     * @return CodexHttpResponse
     * @throws ClientProtocolException 
     */
    private function executePostRequest( $actionRequest, $parameters )
    {
        $request = new CodexHttpPost ( $this->getUri() . $actionRequest  );
        $request->setEntity(new UrlEncodedFormEntity($parameters));
        $this->getAuth()->buildHeader( $request, $this->getSessionToken(), $parameters );       
        
        return $this->executeRequest($request);
    }
    
    /**
     * 
     * @param string $imageUri 
     * @return CloseableCodexHttpResponse
     * @throws ClientProtocolException 
     */
    private function executeGetImageRequest( $imageUri )
    {
        $request = new CodexHttpGet($imageUri);     
        return $this->executeRequest($request); 
    }       
    
    /**
     * 
     * @param CodexHttpResponse $response
     * @param Class<T> $classOfT
     * @param int $statusOK
     * @return <T>  T 
     * @return
     * @throws CodexServiceClientException
     */
    private function parseHttpResponse( CodexHttpResponse $response, $classOfT, $statusOK ) 
    {
        if( $response->getStatusLine()->getStatusCode() == $statusOK )
        {
            try
            {
                return $this->createGson()->fromJson( (CodexEntityUtils::toString( $response->getEntity(), CodexHttpUtils::DEFAULT_CHARSET)), $classOfT);
            }       
            catch(Exception $ex)
            {
                throw new CodexServiceClientException( CodexServiceClientException::ERROR_SERVICE_PARSE_FAIL );
            }
        }
        else
        {
            $fail = CodexHttpUtils::processFailResponse( $response );
    
            if( $fail != null )
            {
                throw new CodexServiceClientException( $fail ); 
            }
            else
            {
                throw new CodexServiceClientException( CodexServiceClientException::ERROR_DONT_CAUGHT );
            }           
        }
    }
    
    /**
     * Create GSon Serializator
     * @return CodexGson
     */
    private function createGson()
    {
        return CodexGsonBuilder::setDateFormat(CodexHttpUtils::DEFAULT_FORMAT_DATE)->create();
    }


    /**
     * @return Date (the sessionExpires)
     */
    public function getSessionExpires() 
    {
        return $this->sessionExpires;
    }

}


class CodexHttpClientBuilder{
    
    public static function create(){
        return new CodexHttpClientBuilder();
    }
    
    public function build(){
        return new CodexCloseableHttpClient();
    }
}

?>