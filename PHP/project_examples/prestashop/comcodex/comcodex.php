<?php
if (!defined('_PS_VERSION_'))
    exit;

include(_PS_MODULE_DIR_.'comcodex/KHelperList.php');
require(dirname(__FILE__).'/api/Codex.inc.php');
class Comcodex extends PaymentModule 
{
	/**
	 * Almacena el contenido a mostrar en la configuración del modulo
	 * @var string
	 */
	private $_html = '';
	
	/**
	 * Almacena los bankprofiles que se obtienen desde la API
	 * @var array
	 */
	private $bankProfiles = array();
	
	/**
	 * Almacena un objeto HelperList
	 * @var HelperList
	 */
	private $list = null;
	
	private $image_path;
	
	/**
	 * Almacena los estatus de la transaccion
	 * @var array
	 */
	private $orderState = array();

    private $errorApi = "";
	
    public function __construct()
    {
        $this->name = 'comcodex';
        $this->tab = 'payments_gateways';
        $this->version = '1.0';
        $this->author = 'Koiosoft';
        $this->ps_versions_compliancy = array('min' => '1.5', 'max' => '1.5.6.3');
     	
        parent::__construct();
        $this->displayName = $this->l('Comcodex');
        $this->description = $this->l('Módulo de Pago Comcodex');
        $this->confirmUninstall = $this->l('Are you sure you want to uninstall?');
        //$this->tab = 'Admin';
        $this->tabClassName = 'comcodextab';
        $this->tabParentName = 'AdminOrders';

    }
    
    public function install()
    {
        
    	$this->createTables ();
		if (
			!parent::install () ||
			!$this->registerHook ( 'displayPayment' ) ||
			!$this->registerHook('paymentReturn') ||
		    !Configuration::updateValue( 'comcodex_BANK_PROFILE_ACTIVE', '' ) ||
			!Configuration::updateValue ( 'comcodex_TRANSACTION_CONCEPT', '' ) ||
		    !Configuration::updateValue ( 'comcodex_SERVICE_PORT', '' ) ||
		    !Configuration::updateValue ( 'comcodex_SERVICE_URI', '' ) ||
		    !Configuration::updateValue ( 'comcodex_SECRET_PHRASE', '' ) ||
		    !Configuration::updateValue ( 'comcodex_CLIENT_KEY', '' ) ||
		    !Configuration::updateValue ( 'comcodex_QR_SERVICE_URI', '' ) ||
		    !Configuration::updateValue ( 'comcodex_PATH_IMAGE', '' ) ||
            !Configuration::updateValue ( 'comcodex_DEVICE', '' )
        )
			return false;
       
        if (!Configuration::get('comcodex_STATUS_1'))
            $this->createStates();
        
        $this->addTab(); 
		return true;
    }
    
    public function uninstall()
    {
    	
		if (!parent::uninstall())
			return false;
		$this->removeTab();
		
		$a = Db::getInstance()->update(
			'comcodex_bank_profile', array(
			'active' => 0
			),
			'',
			$limit = 0, $null_values = false, $use_cache = true, $add_prefix = false
		);
		
		
		return true;
    }
    
    public function createTables() 
    {
    	$db = Db::getInstance();
    	$sql= "

			
			-- -----------------------------------------------------
			-- Table `comcodex_transactions`
			-- -----------------------------------------------------
			CREATE TABLE IF NOT EXISTS `comcodex_transactions` (
			  `id` INT NOT NULL AUTO_INCREMENT,
			  `token` TEXT NULL,
			  `transaction_id` TEXT NULL,
			  `concept` TEXT NULL,
			  `amount` DECIMAL NULL,
			  `payed` TINYINT(1) NULL,
			  `payed_card_holder` VARCHAR(255) NULL,
			  `payed_card_number` VARCHAR(255) NULL,
			  `payed_identity` VARCHAR(45) NULL,
			  `payed_date` TIMESTAMP NULL,
			  `ps_order_id` INT NULL,
			  `bank_profile_id` INT NULL,
			  PRIMARY KEY (`id`),
			  INDEX `fk_comcodex_transactions_1_idx` (`bank_profile_id` ASC))
			ENGINE = InnoDB
			DEFAULT CHARACTER SET = utf8
			COLLATE = utf8_bin;
			
    	    -- -----------------------------------------------------
            -- Table `comcodex_status_transaction`
            -- -----------------------------------------------------
            CREATE TABLE IF NOT EXISTS `comcodex_status_transaction` (
              `id` INT NOT NULL AUTO_INCREMENT,
              `ccodex_id` INT NOT NULL,
              `title` VARCHAR(45) NOT NULL,
              `color` VARCHAR(45) NOT NULL,
              PRIMARY KEY (`id`),
              INDEX `INDEX` (`ccodex_id` ASC))
            ENGINE = InnoDB
            DEFAULT CHARACTER SET = utf8
            COLLATE = utf8_bin;
    	    
			-- -----------------------------------------------------
            -- Table `comcodex_historical_transactions`
            -- -----------------------------------------------------
            CREATE TABLE IF NOT EXISTS `comcodex_historical_transactions` (
              `id` INT NOT NULL AUTO_INCREMENT,
              `date` TIMESTAMP NULL,
              `comcodex_transactions_id` INT NOT NULL,
              `comcodex_status_transaction_id` INT NOT NULL,
              PRIMARY KEY (`id`),
              INDEX `fk_comcodex_historical_transactions_comcodex_transactions1_idx` (`comcodex_transactions_id` ASC),
              INDEX `fk_comcodex_historical_transactions_comcodex_status_transac_idx` (`comcodex_status_transaction_id` ASC),
              CONSTRAINT `fk_comcodex_historical_transactions_comcodex_transactions1`
                FOREIGN KEY (`comcodex_transactions_id`)
                REFERENCES `comcodex_transactions` (`id`)
                ON DELETE NO ACTION
                ON UPDATE NO ACTION,
              CONSTRAINT `fk_comcodex_historical_transactions_comcodex_status_transacti1`
                FOREIGN KEY (`comcodex_status_transaction_id`)
                REFERENCES `comcodex_status_transaction` (`ccodex_id`)
                ON DELETE NO ACTION
                ON UPDATE NO ACTION)
            ENGINE = InnoDB
            DEFAULT CHARACTER SET = utf8
            COLLATE = utf8_bin;
    			
        ";

    	if(!$result=Db::getInstance()->Execute($sql)){
    		error_log($sql);
    		return false;
    	}
		return true;
    	
    }

    
    /**
     * Muestra la configuracion del modulo
     * @return string
     */
    public function getContent()
    {
    	// formulario
    	if (!empty($_POST['comcodex_TRANSACTION_CONCEPT'])) {
    		Configuration::updateValue('comcodex_TRANSACTION_CONCEPT', $_POST['comcodex_TRANSACTION_CONCEPT']);
    	}
    	if (!empty($_POST['comcodex_SERVICE_URI'])) {
    	    Configuration::updateValue('comcodex_SERVICE_URI', $_POST['comcodex_SERVICE_URI']);
    	}
        if (isset($_POST['comcodex_SERVICE_PORT'])) {
            Configuration::updateValue('comcodex_SERVICE_PORT', $_POST['comcodex_SERVICE_PORT']);
        }
    	if (!empty($_POST['comcodex_SECRET_PHRASE'])) {
    	    Configuration::updateValue('comcodex_SECRET_PHRASE', $_POST['comcodex_SECRET_PHRASE']);
    	}
    	if (!empty($_POST['comcodex_CLIENT_KEY'])) {
    	    Configuration::updateValue('comcodex_CLIENT_KEY', $_POST['comcodex_CLIENT_KEY']);
    	}
    	if (!empty($_POST['comcodex_QR_SERVICE_URI'])) {
    	    Configuration::updateValue('comcodex_QR_SERVICE_URI', $_POST['comcodex_QR_SERVICE_URI']);
    	}
    	if (!empty($_POST['comcodex_PATH_IMAGE'])) {
    	    Configuration::updateValue('comcodex_PATH_IMAGE', $_POST['comcodex_PATH_IMAGE']);
    	}
        if (!empty($_POST['comcodex_DEVICE'])) {
            Configuration::updateValue('comcodex_DEVICE', $_POST['comcodex_DEVICE']);
        }
    	
    	$this->displayForm();
    	return $this->_html;
    }
    
    /**
     * Crea los estatus de la transaccion que se muestran en el detalle de una compra
     * y en el listado de transacciones comcodex
     */
	public function createStates() 
    {
        $this->order_state = array(
            array('8ea6ec', '00100', 'Comcodex - Pago en espera', ''),
            array('8ea6ec', '00100', 'Comcodex - Pago en proceso', ''),
            array('284d28', '11110', 'Comcodex - Pago aceptado', 'payment'),
            array('dc143c', '11110', 'Comcodex - Pago rechazado', 'order_canceled'),
            array('8f0621', '11110', 'Comcodex - Pago inválido', 'payment_error')
        );
        $languages = Db::getInstance()->ExecuteS('
    		SELECT `id_lang`, `iso_code` FROM `' . _DB_PREFIX_ . 'lang`
		');
        foreach ($this->order_state as $key => $value) {
            Db::getInstance()->Execute('
                INSERT INTO `' . _DB_PREFIX_ . 'order_state`
			         ( `invoice`, `send_email`, `color`, `unremovable`, `logable`, `delivery`)
			     VALUES
			         (' . $value[1][0] . ', ' . $value[1][1] . ', \'#' . $value[0] . '\', ' . $value[1][2] . ', ' . $value[1][3] . ', ' . $value[1][4] . ');
		    ');
            $sql = 'SELECT MAX(id_order_state) FROM ' . _DB_PREFIX_ . 'order_state';
            $this->figura = Db::getInstance()->getValue($sql);
            foreach ($languages as $language_atual) {
                Db::getInstance()->Execute('
			         INSERT INTO `' . _DB_PREFIX_ . 'order_state_lang`
			             (`id_order_state`, `id_lang`, `name`, `template`)
			         VALUES
			             (' . $this->figura . ', ' . $language_atual['id_lang'] . ', \'' . $value[2] . '\', \'' . $value[3] . '\');
		        ');
            }
            $file = (dirname(__file__) . "/icons/$key.gif");
            $newfile = (dirname(dirname(dirname(__file__))) . "/img/os/$this->figura.gif");
            if (! copy($file, $newfile)) {
                return false;
            }
            Configuration::updateValue("comcodex_STATUS_$key", $this->figura);
            
            // inserto la data en la tablas de comcodex
            Db::getInstance()->Execute("
                INSERT INTO `comcodex_status_transaction` (`id`, `ccodex_id`, `title`, `color`) VALUES
                    (1, 0, 'Comcodex - Pago en espera', '#8ea6ec'),
                    (2, 1, 'Comcodex - Pago en proceso', '#8ea6ec'),
                    (3, 2, 'Comcodex - Pago aceptado', '#284d28'),
                    (4, 3, 'Comcodex - Pago rechazado', '#dc143c'),
                    (5, 4, 'Comcodex - Pago inválido', '#8f0621'),
                    (6, 5, 'Comcodex - Pago revertido', '#dc143c');
		    ");
            
        }
        return true;
    }

    /**
     * Configura un objeto HelperList y lo asigna a  $list
     */
    private function initlist() 
    {
    	$this->list = new KHelperList();
    	$this->list->name_controller = 'comcodex';
		$this->list->title = 'Listado de Bank Profiles';
		$this->list->shopLinkType = '';
		$this->list->module = $this->name;
		$this->list->show_toolbar = false;
		$this->list->simple_header = true;
		$this->list->module = $this;
		$this->list->identifier = 'id';
		$this->list->token = Tools::getAdminTokenLite('AdminModules');
		$this->list->currentIndex = AdminController::$currentIndex.'&configure='.$this->name;
		$this->list->no_link = true;
	}
	
	/**
	 * Obtiene el id del bankprofile activo
	 */
	public static function getBankProfilesActiveId()
	{
		$data = Configuration::get('comcodex_BANK_PROFILE_ACTIVE');

		if(isset($data) && !is_null($data)){
			return $data;
		}else{
			return NULL;
		}
		
	}

	
	/**
	 * Sincroniza la data bankprofile en las tablas locales
	 */
	private function syncBankProfiles()
	{

	    foreach($this->bankProfiles as $bankProfile) {
	    	Configuration::updateValue('comcodex_BANK_PROFILE_ACTIVE', $bankProfile->id);
	    	break;
	    }
	}
	
	/**
	 * Muestra el formulario de edicion de concepto de transacción
	 */
    private function displayForm()
    {
    	$transaction_concept = Configuration::get('comcodex_TRANSACTION_CONCEPT');
    	$port = Configuration::get('comcodex_SERVICE_PORT');
    	$service_uri = Configuration::get('comcodex_SERVICE_URI');
    	$secret_phrase = Configuration::get('comcodex_SECRET_PHRASE');
    	$client_key = Configuration::get('comcodex_CLIENT_KEY');
    	$qr_uri = Configuration::get('comcodex_QR_SERVICE_URI');
    	$path_image = Configuration::get('comcodex_PATH_IMAGE');
        $device = Configuration::get('comcodex_DEVICE');
        $message = "";
		
		$error_wallet_account = "";
    	$bankprofileID = Configuration::get('comcodex_BANK_PROFILE_ACTIVE');
    	// Verify the wallet account
		if ($service_uri != "" && ( !isset($bankprofileID) || is_null($bankprofileID) || $bankprofileID == "") ) {
    		$message = '<span style="color:red;">No se pudo registrar los datos de la Cuenta Wallet, verifique sus datos.</span>';
		}

        if($this->errorApi != ""){
            $message = '<span style="color:red;">'.$this->errorApi.'</span>';
        }else if($message == "" && isset($_POST)){
            $message = '<span style="color:darkgreen;"> Conexión exitosa con el servicio paycodex.</span>';
        }

    	$transaction_concept_update = isset($POST['comcodex_TRANSACTION_CONCEPT']) ? $POST['comcodex_TRANSACTION_CONCEPT'] : !empty($transaction_concept) ? $transaction_concept : '';
    	$port_update = isset($POST['comcodex_SERVICE_PORT']) ? $POST['comcodex_SERVICE_PORT'] : !empty($port) ? $port : '';
    	$service_uri_update = isset($POST['comcodex_SERVICE_URI']) ? $POST['comcodex_SERVICE_URI'] : !empty($service_uri) ? $service_uri : '';
    	$secret_phrase_update = isset($POST['comcodex_SECRET_PHRASE']) ? $POST['comcodex_SECRET_PHRASE'] : !empty($secret_phrase) ? $secret_phrase : '';
    	$client_key_update = isset($POST['comcodex_CLIENT_KEY']) ? $POST['comcodex_CLIENT_KEY'] : !empty($client_key) ? $client_key : '';
    	$qr_uri_update = isset($POST['comcodex_QR_SERVICE_URI']) ? $POST['comcodex_QR_SERVICE_URI'] : !empty($qr_uri) ? $qr_uri : '';
    	$path_image_update = isset($POST['comcodex_PATH_IMAGE']) ? $POST['comcodex_PATH_IMAGE'] : !empty($path_image) ? $path_image : '';
        $device_update = isset($POST['comcodex_DEVICE']) ? $POST['comcodex_DEVICE'] : !empty($device) ? $device : '';


    	
    	$this->_html .=
    	'   '.$message.'    <form action="'.Tools::htmlentitiesUTF8($_SERVER['REQUEST_URI']).'" method="post">
			<fieldset>
			<legend><img src="../img/admin/contact.gif" />'.$this->l('Configuración de concepto de compra por Comcodex').'</legend>
				<table border="0" width="750" cellpadding="0" cellspacing="0" id="form">
					<tr>
						<td colspan="2">'.
							'<br/>'.
							$this->l('Puede utilizar los comodines <b>#id_pedido_prestashop</b>, <b>#nombre_tienda</b>, <b>#cant_productos</b>').
							'<br/><br/>'.
						'</td>
					</tr>
					<tr>
						<td width="130" style="vertical-align: top;">'.$this->l('Concepto de Compra').'</td>
						<td style="padding-bottom:15px;">
							<textarea name="comcodex_TRANSACTION_CONCEPT" rows="5" cols="80">'. htmlentities($transaction_concept_update, ENT_COMPAT, 'UTF-8') .'</textarea>
							<p>'.$this->l('Ejemplo: #nombre_tienda - Pedido: #id_pedido_prestashop - Número de productos: #cant_productos').'</p>
						</td>
					</tr>
				    <tr>
						<td width="130" style="vertical-align: top;">'.$this->l('Uri Servicio').'</td>
						<td style="padding-bottom:15px;">
							<input size="80" type="text" name="comcodex_SERVICE_URI" value="'.htmlentities($service_uri_update, ENT_COMPAT, 'UTF-8').'">
						</td>
					</tr>
                    <tr>
                        <td width="130" style="vertical-align: top;">'.$this->l('Puerto Servicio').'</td>
                        <td style="padding-bottom:15px;">
                            <input size="80" type="text" name="comcodex_SERVICE_PORT" value="'.htmlentities($port_update, ENT_COMPAT, 'UTF-8').'">
                        </td>
                    </tr>
				    <tr>
						<td width="130" style="vertical-align: top;">'.$this->l('Frase Secreta').'</td>
						<td style="padding-bottom:15px;">
							<input size="80" type="text" name="comcodex_SECRET_PHRASE" value="'.htmlentities($secret_phrase_update, ENT_COMPAT, 'UTF-8').'">
						</td>
					</tr>
				    <tr>
						<td width="130" style="vertical-align: top;">'.$this->l('Llave de Cliente').'</td>
						<td style="padding-bottom:15px;">
							<input size="80" type="text" name="comcodex_CLIENT_KEY" value="'.htmlentities($client_key_update, ENT_COMPAT, 'UTF-8').'">
						</td>
					</tr>
				    <tr>
						<td width="130" style="vertical-align: top;">'.$this->l('Uri QR').'</td>
						<td style="padding-bottom:15px;">
							<input size="80" type="text" name="comcodex_QR_SERVICE_URI" value="'.htmlentities($qr_uri_update, ENT_COMPAT, 'UTF-8').'">
						</td>
					</tr>
			        <tr>
                        <td width="130" style="vertical-align: top;">'.$this->l('Nombre de dispositivo').'</td>
                        <td style="padding-bottom:15px;">
                            <input size="80" type="text" name="comcodex_DEVICE" value="'.htmlentities($device_update, ENT_COMPAT, 'UTF-8').'">
                        </td>
                    </tr>

					<tr><td colspan="2" align="center"><input class="button" name="btnSubmit" value="'.$this->l('Actualizar').'" type="submit" /></td></tr>
				</table>
			</fieldset>
		</form>';
    }
    
    /**
     * Agrega una pestaña hija con el nombre "Comcodex" en el menu Order del backend prestashop
     */
    private function addTab()
    {
    	$tab = new Tab();
    	$tab->class_name = $this->tabClassName;
    	$tab->id_parent = Tab::getIdFromClassName($this->tabParentName);
    	$tab->module = $this->name;
    	$languages = Language::getLanguages();
    	foreach ($languages as $language)
    		$tab->name[$language['id_lang']] = $this->displayName;
    	$tab->add();
    }
    
    /**
     * Elimina la pestaña el nombre "Comcodex" en el menu Order del backend prestashop
     */
    private function removeTab()
    {
    	$id_tab = Tab::getIdFromClassName($this->tabClassName);
    	if ($id_tab) {
    		$tab = new Tab($id_tab);
    		$tab->delete();
    	}
    }
    
    /**
     * Data de prueba
     */
    private function installFixtures()
    {
        $this->bankProfiles = array(
            array(
                'alias'=> 'BANKPROFILE#1',
                'ccodex_bankProfile_id' => '5353535353535aa',
                'account'=>'14424242',
                'bank'=>'BANCARIBE'
            ),
            array(
                'alias'=> 'BANKPROFILE#2',
                'ccodex_bankProfile_id' => 'dsdasd6456575',
                'account'=>'123456675',
                'bank'=>'BANESCO'
            )
        );
    }
    
    /**
     * Retorna el concepto de la transaccion
     * @param int $psOrderId
     * @param int $productQuantity
     * @return String
     */
    private function getTransactionConcept($psOrderId, $productQuantity) 
    {
        $concept = Configuration::get('comcodex_TRANSACTION_CONCEPT');
        
        if(empty($concept))
            return $this->l('Comcodex');
        
        $search = array('#nombre_tienda', '#id_pedido_prestashop', '#cant_productos');
        $replace = array(Configuration::get('PS_SHOP_NAME'), $psOrderId, $productQuantity);
        return str_replace($search, $replace, $concept);
    }
    
    
    /**
     * Retorna el concepto de la transaccion
     * @param int $psOrderId
     * @param int $productQuantity
     * @return String
     */
    public static function getOrderConcept($psOrderId, $productQuantity)
    {
    	$concept = Configuration::get('comcodex_TRANSACTION_CONCEPT');
    
    	if(empty($concept))
    		return 'Comcodex';
    
    	$search = array('#nombre_tienda', '#id_pedido_prestashop', '#cant_productos');
    	$replace = array(Configuration::get('PS_SHOP_NAME'), $psOrderId, $productQuantity);
    	return str_replace($search, $replace, $concept);
    }
    
    
    /**
     * Retorna los parametros de conexion de la Api 
     * que fueron previamente guardados en la configuración del modulo 
     * @return array
     */
    public static function getParametersApi()
    {
        return array(
            'serviceUri' => Configuration::get('comcodex_SERVICE_URI'),
            'servicePort' => Configuration::get('comcodex_SERVICE_PORT'),
            'secretPhrase' => Configuration::get('comcodex_SECRET_PHRASE'),
            'clientKey' => Configuration::get('comcodex_CLIENT_KEY'),
            'qrUri' => Configuration::get('comcodex_QR_SERVICE_URI'),
            'pathImage' => dirname(__FILE__).'/qr/',
            'device' => Configuration::get('comcodex_DEVICE'),
        );
    }
    
	/**
	 * 
	 * @return CodexServiceClient
	 */
    public static function getClientApi(){
    	//
    	//	Se obtiene la configuracion para emplear el servicio comcodex
    	//
    	$paramsAPI = self::getParametersApi();

    	$setting = new CodexSetting();
    	$setting->serviceUri   = $paramsAPI['serviceUri'];
		$setting->servicePort  = $paramsAPI['servicePort'];
    	$setting->secretPhrase = $paramsAPI['secretPhrase'];
    	$setting->clientKey	 = $paramsAPI['clientKey'];
    	$setting->qrServiceUri = $paramsAPI['qrUri'];//"http://web.comcodex.net/image/qrImage/:token.png";
		// Se optiene la direccion del directorio de la carpeta que contenerá las imagenes QR
    	$setting->pathImage 	 = $paramsAPI['pathImage'];
        $setting->device        = $paramsAPI['device'];
    	
    	//
    	// Se crea la instancia del cliente en comcodex con los parametros de la configuración
    	// del servicio
    	$client = new CodexServiceClient($setting);
    	return $client; 
    }
    
    /**
     * 
     * @param CodexServiceClient $setting
     * @param string $token
     * @return CodexTransaction|NULL
     */
    public static function getTransactionApi($client,$token){
    			
		//
		// Se establece la conexion
		//
		$client->connect();
		
		try {
			//
			// Se consulta la transacción
			//
			$transaction = $client->retrieveTransaction($token);
			return $transaction;
		}
		catch (CodexServiceClientException $ex)
		{
			if( $ex->getCode() == CodexServiceClientException::ERROR_CODE_SESSION_TOKEN_INVALID )
			{
				$client->connect();
				$transaction = $client->retrieveTransaction($token);
				return $transaction;
	
			}else{ // Si ocurre un error no previsto
				return NULL;
			}
		}
    }
	
    /**
     * 
     * @param unknown $params
     */
	public function hookdisplayPayment($params)
    {

    	$error = false;
    	
		$settingsParams = self::getParametersApi();
    		
    	foreach ($settingsParams as $key => $param) {
    		$paramValue = trim($param);
    		if((is_null($paramValue) || $paramValue == "") && $key != "servicePort"){
    			$error = true;
                
    			break;
    		}
    	}


    	if(!$error){
    		
    		$this->smarty->assign('iconPath', 'modules/comcodex/icons/');
    		return $this->display(__FILE__, 'payment.tpl');
    	}else{
            
    		return NULL;
        }
    }
    
    /**
     * 
     * @param unknown $params
     */
    public function hookPaymentReturn($params)
    {
    	if (!$this->active)
    		return;
    	
    	if(isset($_GET['status'])) {
    		
    		$this->smarty->assign(array(
    				'total_to_pay' => Tools::displayPrice($params['total_to_pay'], $params['currencyObj'], false),
    				'concept' => $this->getTransactionConcept($params['objOrder']->id, $quantityProducts),
    				'status' => $_GET['status'],
    				'messageStatus'=> $_GET['message-status']
    		));

    		return $this->display(__FILE__, 'payment_show_message.tpl');    		
    		
    	}else{
    		$existTransaction = true;
	    	$state = $params['objOrder']->getCurrentState();
	    	
	    	// Se instancia el objeto encargado de ejecutar las Peticiones SQL
	    	$db = Db::getInstance();
	    	
	    	//
	    	// Se Verifica que existe registrado una transaccion para la orden indicada
	    	//
	    	$sql = "SELECT *
	    	FROM  `comcodex_transactions`
	    	WHERE  `ps_order_id` LIKE '".$params['objOrder']->id."' LIMIT 1;";
	    	
	    	$data = $db->ExecuteS($sql);
	    	foreach ($data as $row){
	    		$existTransaction = false;
	    	}
	    	
	    	if (!$existTransaction)
	    	{
	    		// Get transaction data.
	    		foreach ($data as $row){
	    			$token = $row['token'];
	    			$codexTransactionId = $row['transaction_id'];
	    		}
	    		// Set QR image path
	    		$pathImageQR = "modules/comcodex/qr/".$token.".png";
	    		$order = new Order($params['objOrder']->id);
	    		$cart = new Cart($order->id_cart);
		    	// 	Se obtiene la cantidad de productos
				$quantityProducts = 0;
				foreach ($cart->getProducts() as $product){
					$quantityProducts = $quantityProducts + (int)$product['cart_quantity'];
				}
	    		$this->smarty->assign(array(
	    				'total_to_pay' => Tools::displayPrice($params['total_to_pay'], $params['currencyObj'], false),
	    				'concept' => $this->getTransactionConcept($params['objOrder']->id, $quantityProducts),
	    				'pathImageQR' => $pathImageQR,
	    				'token'=>$token,
	    				'status' => 'ok',
	    				'id_order' => $params['objOrder']->id
	    		));
	    		if (isset($params['objOrder']->reference) && !empty($params['objOrder']->reference))
	    			$this->smarty->assign('reference', $params['objOrder']->reference);
	    	}
	    	else
	    		$this->smarty->assign('status', 'failed');
	    	return $this->display(__FILE__, 'payment_QR.tpl');
	
    	}
    }
    
    public static function getDetailClosingReportByCard($value){ //reporte detalle
        
        
        
        $date = date("Y-m-d",$value->hourInit);
        echo $date;
        $html = '
        <table style="font-size: 12pt; margin: 0 auto;">
            <tr>
                <td>
                    <b>Reporte De Cierre</b>
                </td>
            </tr>
        </table>
        <br>
        <table style="font-size: 12pt; margin: 0 auto;">
            <tr>
                <td>
                    <b>Desde:</b> '.date("d-m-Y h:i:s",((int)$value->hourInit / 1000)).' <b>Hasta:</b> '.date("d-m-Y h:i:s",($value->hourFinish / 1000)).' <b>Cuenta:</b> '.$value->accountNumber.'
                </td>
            </tr>
        </table>
        <table class="table-codex-grid" style="min-width:80% !important; margin: 0 auto;">
            <thead>
                <tr>
                    <th style="border-left: 1px solid #A5A2A2; width:25%;">
                       	&nbsp; 
                    </th>';
                    
                    foreach($value->detail as $element=>$detailValue){
                        $html .= '<th style="border-left: 1px solid #A5A2A2; width:25%;">';
                        $html .= $element;
                        $html .= '</th>';
                    }
                    
                    $html .= '<th style="border-left: 1px solid #A5A2A2; width:25%;">
                        Totales Generales
                    </th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td style="border-left: 1px solid #A5A2A2;">
                        Compras
                    </td>';
                    
                    foreach($value->detail as $element=>$detailValue){
                        $html .= '<td style="border-left: 1px solid #A5A2A2; text-align: center;">';
                        $html .= ((int)$detailValue["totalBuy"] > 0)?'+':'';
                        $html .= $detailValue["totalBuy"];
                        $html .= '</td>';
                    }
                    
                     
                    $html.= '
                    <td style="border-left: 1px solid #A5A2A2; text-align: center;">';
                    $html .= ((int)str_replace('"','',$value->totalPurchases) > 0)?'+':'';
                    $html .= str_replace('"','',$value->totalPurchases)." Bs.";
                     
                    $html .= '</td>
                </tr>
                
                <tr>
                    <td style="border-left: 1px solid #A5A2A2;">
                        Anuladas
                    </td>';
                    foreach ($value->detail as $element=>$detailValue){
                        $html .= '<td style="border-left: 1px solid #A5A2A2; text-align: center;">';
                        $html .= ((int)$detailValue["totalRevert"] > 0)?'-':'';
                        $html .= $detailValue["totalRevert"];
                        $html .= '</td>';
                        
                    }
                    
                    $html .= '<td style="border-left: 1px solid #A5A2A2; text-align: center;">';
                    $html .= ((int)str_replace('"','',$value->totalReverted) > 0)?'-':'';
                    $html .= str_replace('"','',$value->totalReverted);    //- <%=totalRevert%> Bs.
                    $html .= '</td>
                </tr>
                <tr>
                    <td style="border-left: 1px solid #A5A2A2;">
                        Total
                    </td>';
                    
                    foreach ((array)$value->totalByCard as $element=>$totalByCardValue){
                        $html .= '<td style="border-left: 1px solid #A5A2A2; text-align: center;">';
                        $html .= $value->totalByCard->{$element};
                        $html .= '</td>';
                        
                    
                    }
                    
                    $html .= '<td style="border-left: 1px solid #A5A2A2; text-align: center;">';
                    $html .= str_replace('"','',$value->globalTotal)." Bs.";
                    $html .= '</td>
                </tr>
            </tbody>
        </table>';
        
        
        return $html;
        
    }
    
    public static function detailTransactionsClosingReport($value){ // reporte general
        
        $html = ' <table style="font-size: 12pt; margin: 0 auto;">
            <tr>
                <td>
                    <b>Reporte Detallado</b>
                </td>
            </tr>
        </table>
        <br/>
        
        <table class="table-codex-grid" >
            <thead>
                <tr>
                    <th style="border-left: 1px solid #A5A2A2;">
                        Codex
                    </th>
                    <th>
                        Fecha
                    </th>
                    <th>
                        Hora
                    </th>
                     <th>
                        Id
                    </th>
                    <th>
                        Id de tarjeta
                    </th>
                    <th>
                        Tipo de tarjeta
                    </th>
                    <th style="width:200px !important;">
                        Concepto
                    </th>
                    <th>
                        Nº Ref.
                    </th>
                    <th>
                        Monto
                    </th>
                </tr>
            </thead>
            <tbody>
         ';
        
        
        foreach ($value as $element=>$object){
            $operator = ($object->status == 2)?'+':'-';
            $html .= '<tr><td>'.
                            substr($object->id,-8).
                    '</td>
                    <td>
                        '.date("d-m-Y",strtotime($object->openDate)).'
                    </td>
                    <td>
                        '.date("h:i:s",strtotime($object->openDate)).'<!--<%=transaction[i].created_hour%>-->
                    </td>
                    <td>
                        '.substr(json_decode($object->gateWayResponse->source)->id,-8).'<!--<%=transaction[i].gateway_response.source.id.substr(-8);%>-->
                    </td>
                    <td>
                        '.$object->payedCardNumber.'<!--<%=transaction[i].identification_card.substr(-7);%>-->
                    </td>
                    <td>
                         '.mb_strtoupper($object->payedCardHolder,'UTF-8').'<!--<%=transaction[i].credit_card_type.toLocaleUpperCase()%>-->
                    </td>
                    <td>
                        '.mb_strtoupper($object->concept,'UTF-8').'<!--<%=transaction[i].alias.toLocaleUpperCase()%>-->
                    </td>
                    <td>
                        '.mb_strtoupper($object->gateWayResponse->reference,'UTF-8').'<!--<%=transaction[i].gateway_response.source.reference%>-->
                    </td>
                    <td>
                        '.$operator.number_format($object->amount->number.".".$object->amount->decimal,2,',','.').'<!--+<%=transaction[i].amount_formated%>-->
                    </td></tr>';
        }
        $html .= '   </tbody>
        </table>';
        
        return $html;
        
    }
    
}
?>