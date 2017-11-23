<?php


/**\brief This class defines common routines for generating authentication signatures
 * for AWS requests.
 * 
 */
class CodexSignature {
    
    const HMAC_SHA1_ALGORITHM = "sha1";
    const CHARSET_UTF8 = "UTF-8";
	    /**
    	 * 
    	 * @param string value
    	 * @param string key
    	 * @return string
    	 */
    public static function hmacSha1($value, $key) {
    	try 
    	{
    	    //  Covert array of Hex bytes to a String
    	    return hash_hmac( CodexSignature::HMAC_SHA1_ALGORITHM, $value, $key, false); 
    	} 
    	catch (Exception $e) 
    	{
    	    throw new Exception($e);
    	}
    }
}

?>