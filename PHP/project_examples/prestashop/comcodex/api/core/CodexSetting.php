<?php

/**
 * 
 * @author Koiosoft
 *
 */
class CodexSetting {
	
	public $pathImage;
	public $proxyPort;
	public $proxyHost;
	public $proxyUser;
	public $proxyPassword;
	public $serviceUri;
	public $servicePort;
	public $clientKey;
	public $secretPhrase;
	public $key;
	public $qrServiceUri;
	public $device;
	
	
	/**
	 * 
	 * @param file
	 */
	public function Setting( $file = null )
	{
		if (!is_null($file)) {
			$this->load( $file );
		}
	}
	
	
	/**
	 * 
	 * @param String fileConfig
	 */
	public function load($file)
	{
		// Pendiente por completar

	}
	
}

?>