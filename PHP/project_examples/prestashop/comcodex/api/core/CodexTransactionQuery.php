<?php

/**
 * 
 * @author Koiosoft
 *
 */
class CodexTransactionQuery {
	
	public $device;
	public $beginDate;
	public $endDate;
	public $bankProfileId;
    public $deviceId;
	
	const FIELD_DEVICE          = "device";
	const FIELD_BEGIN_DATE      = "beginDate";
	const FIELD_END_DATE        = "endDate";
	const FIELD_BANKPROFILE_ID      = "bankProfileId";
    const FIELD_DEVICE_ID       = "deviceId"; 
    const FIELD_STATUS          = "status";
    
    const STATUS_APPOVED        = 2;
    const STATUS_REVERT         = 5;
	
	
}

?>