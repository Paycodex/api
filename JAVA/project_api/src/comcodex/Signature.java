package comcodex;


import javax.crypto.Mac;
import javax.crypto.spec.SecretKeySpec;

import org.apache.commons.codec.binary.Hex;


/**
 * This class defines common routines for generating authentication signatures
 * for AWS requests.
 */
public class Signature {
	private static final String HMAC_SHA1_ALGORITHM = "HmacSHA1";
	private static final String CHARSET_UTF8 = "UTF-8";

	public static String hmacSha1(String value, String key) {
        try {

            // Get an hmac_sha1 key from the raw key bytes
            byte[] keyBytes = key.getBytes();           
            SecretKeySpec signingKey = new SecretKeySpec(keyBytes, Signature.HMAC_SHA1_ALGORITHM);

            // Get an hmac_sha1 Mac instance and initialize with the signing key
            Mac mac = Mac.getInstance(Signature.HMAC_SHA1_ALGORITHM);
            mac.init(signingKey);

            // Compute the hmac on input data bytes
            byte[] rawHmac = mac.doFinal(value.getBytes());

            // Convert raw bytes to Hex
            byte[] hexBytes = new Hex().encode(rawHmac);

            //  Covert array of Hex bytes to a String
            return new String(hexBytes, Signature.CHARSET_UTF8);
            
            //return Base64.encodeBase64String(hexBytes);
        } catch (Exception e) {
            throw new RuntimeException(e);
        }
    }
}