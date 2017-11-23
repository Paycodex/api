<?php

/**
 * 
 * @author koiosoft
 *
 */
class CodexURLEncodedUtils
{
	
	/**
	 * 
	 * @param array $parameters
	 * @param string $encode
	 */
	public static function format( $parameters, $encode )
	{
		return http_build_query($parameters, null, "&");
	}
	
}