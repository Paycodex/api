/*
 * Created by SharpDevelop.
 * User: koiosoft
 * Date: 17/09/2014
 * Time: 11:35
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Security.Cryptography;
using System.Text;
using System.IO;

namespace Comcodex
{


	/// <summary>
	/// This class defines common routines for generating authentication signatures
	/// </summary>
	public class Signature {
		
		public const string HMAC_SHA1_ALGORITHM = "HmacSHA1";
		public const string CHARSET_UTF8 = "UTF-8";
	
		/// <summary>
		/// 
		/// </summary>
		/// <param name="value"></param>
		/// <param name="key"></param>
		/// <returns></returns>
		public static String hmacSha1(string message, string key)
		{				
			using (var hmac = new HMACSHA1(Encoding.UTF8.GetBytes(key)))
            {	
                return  ByteToString( hmac.ComputeHash(Encoding.UTF8.GetBytes(message)));
            }
	    }
		
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="buff"></param>
		/// <returns></returns>
		public static string ByteToString(byte[] buff)
	    {
	        string sbinary = "";
	
	        for (int i = 0; i < buff.Length; i++)
	        {
	            sbinary += buff[i].ToString("x2"); // hex format
	        }
	        return (sbinary);
	    }
			
		
	}
	
}
