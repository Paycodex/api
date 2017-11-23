<?php

 
/**
 * 
 * @author Koiosoft
 *
 */
class CodexAuth {
    
    private $setting;
    private $signature  = "";
    private $nonce      = "";
    /**
     * Integer NONCE_LENGTH
     */
    const NONCE_LENGTH = 20;
    
    
    /**
     * 
     *  @param Setting $setting
     */
    public function codexAuth( $setting )
    {
        $this->setting = $setting;
    }
    
    
    
    /**
     * 
     * @param string $request
     * @param string $sessionToken
     * @param array $parameters
     * @return NULL
     */
    public function buildHeader( $request = null, $sessionToken = null, $parameters = array() )
    {
        if (get_class($request ) == CodexHttpGet::getClassName() ) 
        {
            $this->buildHeaderGet( $request, $sessionToken, $parameters );
            return null;
        }
        elseif (get_class($request ) == CodexHttpPost::getClassName() ) 
        {
            $this->buildHeaderPost( $request, $sessionToken, $parameters );
            return null;
        }
    }
    
    /**
     * 
     * @param unknown $request
     * @param string $sessionToken
     */
    public function buildHeaderGet( $request, $sessionToken)
    {
        $this->buildHeaderHURequestParams($request, CodexHttpUtils::METHOD_GET, $sessionToken, array() );
    }   
    
    
    /**
     * 
     * @param unknown $request
     * @param string $sessionToken
     * @param array $postParameters
     */
    public function buildHeaderPost( $request, $sessionToken, $postParameters  = array() )
    {
        $this->buildHeaderHURequestParams($request,  CodexHttpUtils::METHOD_POST, $sessionToken, $postParameters );
    }   
    
    
    /**
     * 
     * @param CodexHttpUriRequest $request
     * @param string $verb
     * @param string $sessionToken
     * @param string $postParameters
     */
    private function buildHeaderHURequestParams( $request, $verb, $sessionToken, $postParameters  )
    {
        
        $uri = $request->getURI()->toString();
        $this->buildNonce();
        
        $request->addHeader("oauth_uri",                $uri );     
        $request->addHeader("oauth_consumer_key",       $this->getConsumerKey() );
        $request->addHeader("oauth_timestamp",          $this->getTimeStamp() );
        $request->addHeader("oauth_nonce",              $this->getNonce() );
        $request->addHeader("oauth_signature_method",   $this->getSignatureMethod() );
        $request->addHeader("oauth_signature",          $this->getSignature($verb, $request, $postParameters ) );
        $request->addHeader("device",                   $this->getDevice( ) );
        $request->addHeader("Accept-Language",          "es");
        
        if( $sessionToken != "" )
        {
            $request->addHeader("oauth_session_token", $sessionToken );
        }
    }   

    /**
     * 
     * @param HttpUriRequest $request
     * @param string $verb
     * @param string sessionToken
     */
    private function buildHeaderHURequest( $request, $verb, $sessionToken )
    {
        $this->buildHeader($request,  $verb, $sessionToken, array() ); 
    }
    
    
    /**
     * 
     * @return string
     */
    public function getConsumerKey()
    {
        return $this->setting->clientKey;
    }
    
    /**
     * 
     * @return string
     */
    public function getTimeStamp()
    {
        return  strtotime("now");
    }
    
    
    /**
     * 
     * @return string
     */
    public function buildNonce()
    {
        $chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXTZabcdefghiklmnopqrstuvwxyz";
        $chars_length = strlen($chars); 
        $result = "";
        for ( $i = 0; $i < self::NONCE_LENGTH; ++$i) {
            $rnum = rand(0,$chars_length -1);
            $result .= substr($chars, $rnum, 1);
        }
        $this->nonce = $result;
        return $this->nonce;
    }
    
    /**
     * 
     * @return string
     */
    public function getNonce()
    {
        return $this->nonce;
    }
    
    
    /**
     * 
     * @return
     */
    public function getSignatureMethod()
    {
        return "RSA-SHA1";
    }
    
    
    /**
     * 
     * @param string $verb
     * @param HttpUriRequest $request
     * @param array $postParameters
     * @return
     */
    public function getSignature( $verb, $request, $postParameters)
    {

        $this->signature =  CodexSignature::hmacSha1(strtoupper($verb) . "&" . $this->normalizeUri( $request->getURI() ) . "&" . $this->toQuery( $request, $postParameters ), $this->setting->secretPhrase);
        
        return $this->signature;
    }
    
    
    /**
     * 
     * @param CodexHttpUriRequest $request
     * @param List<NameValuePair> $postParameters
     * @return
     */
    public function toQuery( CodexHttpUriRequest $request, $postParameters ) 
    {
        $params = array();
        
        $params["oauth_uri"] = $this->normalizeUri( $request->getURI() );
        $params["oauth_consumer_key"] = $this->getConsumerKey();
        $params["oauth_timestamp"] = $this->getTimeStamp();
        $params["oauth_nonce"] = $this->getNonce();
        $params["oauth_signature_method"] = $this->getSignatureMethod();
        
        foreach( $postParameters as $index => $postParameter )
        {
            $params[$index]= $postParameter;
        }
        
        ksort($params);
        
        return  $this->encodeURIComponent($params); 
    }
    
    /**
     * 
     * @param codexURI $uri
     * @return string
     */
    public function normalizeUri( $uri)
    {   
        
        $defaultSchemes = array("http" => 80, "https" => 443);
        $normalizedUrl = "";
        $url = parse_url($uri->toString());
        if (isset($url['scheme'])) {
            $url['scheme'] = strtolower($url['scheme']);
            // Strip scheme default ports
            if (isset($defaultSchemes[$url['scheme']]) && isset($url['port']) && $defaultSchemes[$url['scheme']] == $url['port']){
                unset($url['port']);
            }
            $normalizedUrl .= "{$url['scheme']}://";
        } else {
            $normalizedUrl .= "http://";
        }
        if (isset($url['host'])) {
            $url['host'] = strtolower($url['host']);
            // Seems like a valid domain, properly validation should be made in higher layers.
            if (preg_match("/[a-z]+\Z/", $url['host'])) {
                if (preg_match("/^www\./", $url['host']) && gethostbyname($url['host']) == gethostbyname(str_replace("www.", "", $url['host']))){
                    $normalizedUrl .= str_replace("www.", "", $url['host']); 
                }
                else{
                    $normalizedUrl .= $url['host'];
                }
            } else
              $normalizedUrl .= $url['host'];
        }
        if (isset($url['port'])){
            $normalizedUrl .= ":{$url['port']}";
        }
        if (isset($url['path'])) {
            // Case normalization
            $url['path'] = @preg_replace('/(%([0-9abcdef][0-9abcdef]))/ex', "'%'.strtoupper('\\2')", $url['path']);
            //Strip duplicate slashes
            while (preg_match("/\/\//", $url['path'])){
                $url['path'] = preg_replace("/\/\//", "/", $url['path']);
            }
            /*
               * Decode unreserved characters, http://www.apps.ietf.org/rfc/rfc3986.html#sec-2.3
               * Heavily rewritten version of urlDecodeUnreservedChars() in Glen Scott's url-normalizer.
               */
            $unReservedCharacters = array();
            for ($i = 65; $i <= 90; $i++){
                $unReservedCharacters[] = dechex($i);
            }
            for ($i = 97; $i <= 122; $i++){
                $unReservedCharacters[] = dechex($i);
            }
            for ($i = 48; $i <= 57; $i++){
                $unReservedCharacters[] = dechex($i);
            }
            $characters = array('-', '.', '_', '~');
            foreach ($characters as $char){
                $unReservedCharacters[] = dechex(ord($char));
            }
            $url['path'] = preg_replace_callback(array_map(create_function('$str', 'return "/%" . strtoupper($str) . "/x";'), $unReservedCharacters), create_function('$matches', 'return chr(hexdec($matches[0]));'), $url['path']);
            // Remove directory index
            $defaultIndexes = array("/default\.aspx/" => "default.aspx", "/default\.asp/" => "default.asp", "/index\.html/" => "index.html", "/index\.htm/" => "index.htm", "/default\.html/" => "default.html", "/default\.htm/" => "default.htm", "/index\.php/" => "index.php", "/index\.jsp/" => "index.jsp");
            foreach ($defaultIndexes as $index => $strip) {
              if (preg_match($index, $url['path']))
                $url['path'] = str_replace($strip, "", $url['path']);
            }
            /**
             * Path segment normalization.
             */
            $newPath = '';
            while (!empty($url['path'])) {
                if(preg_match('!^(\.\./|\./)!x', $url['path']))
                    $url['path'] = preg_replace('!^(\.\./|\./)!x', '', $url['path']); elseif (preg_match('!^(/\./)!x', $url['path'], $matches) || preg_match('!^(/\.)$!x', $url['path'], $matches))
                    $url['path'] = preg_replace("!^" . $matches[1] . "!", '/', $url['path']); elseif (preg_match('!^(/\.\./|/\.\.)!x', $url['path'], $matches)) {
                    $url['path'] = preg_replace('!^' . preg_quote($matches[1], '!') . '!x', '/', $url['path']);
                    $newPath = preg_replace('!/([^/]+)$!x', '', $newPath);
                }elseif (preg_match('!^(\.|\.\.)$!x', $url['path'])) {
                    $url['path'] = preg_replace('!^(\.|\.\.)$!x', $url['path']);
                }else {
                    if (preg_match('!(/*[^/]*)!x', $url['path'], $matches)) {
                        $firstPathSegment = $matches[1];
                        $url['path'] = preg_replace('/^' . preg_quote($firstPathSegment, '/') . '/', '', $url['path'], 1);
                        $newPath .= $firstPathSegment;
                    }
                }
            }
            $normalizedUrl .= $newPath;
        }
        if (isset($url['fragment'])){
            unset($url['fragment']);
        }
        // Sort GET params alphabetically
        if (isset($url['query'])) {
            if (preg_match("/&/", $url['query'])) {
                $s = explode("&", $url['query']);
                $url['query'] = "";
                sort($s);
                foreach ($s as $z){
                    $url['query'] .= "{$z}&";
                }
                $url['query'] = preg_replace("/&\Z/", "", $url['query']);
            }
            $normalizedUrl .= "?{$url['query']}";
        }
        
        return $normalizedUrl;

    }   
    
    
    /**
     * 
     * @param List<NameValuePair> params 
     * @return
     */
    public function encodeURIComponent( $params )
    {     
        try {        
            $encode =  CodexURLEncodedUtils::format($params, "UTF-8");      
            $encode = preg_replace( "/%29/", ")", $encode);
            $encode = preg_replace( "/\+/", "%20", $encode);
            $encode = preg_replace( "/%21/", "!", $encode);
            $encode = preg_replace( "/%7E/", "~", $encode);
            return $encode;
        }
        catch ( Exception $e) 
        {       
            new CodexServiceClientException( $ex->getMessage(),$e->getCode() );     
        }      
    }   
    
    /**
     * 
     * @return string
     */
    public function getDevice( )
    {
        return trim($this->setting->device);
    }
}
?>