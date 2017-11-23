<?php
require_once(_PS_MODULE_DIR_.'/comcodex/comcodex.php');
class comcodextabController extends AdminController {
	
	
	public function __construct() 
	{
        
        
        parent::__construct();
        if (isset($_GET["action"]) && (strtolower($_GET["action"]) == "closingreport")){
            $this->renderClosingReportGrid();
        }elseif (isset($_GET["action"]) && $_GET["action"] == "reportview"){
            $this->generateClosingReport();
        }else{
            
            $this->fields_list = array(
                'id' => array(
                    'title' => $this->l('ID Transaccion'),
                    'width' => 120,
                    'type' => 'text',
                    'orderby' => false,
                    'search' => false
                ),
                'ps_order_id' => array(
                    'title' => $this->l('ID Pedido Prestashop'),
                    'width' => 120,
                    'type' => 'text',
                    'orderby' => false,
                    'search' => false
                ),
                'ktoken' => array(
                    'title' => $this->l('Token'),
                    'width' => 120,
                    'type' => 'text',
                    'orderby' => false,
                    'search' => false
                ),
                'concept' => array(
                    'title' => $this->l('Concepto'),
                    'width' => 140,
                    'type' => 'text',
                    'orderby' => false,
                    'search' => false
                )
                ,
                'amount' => array(
                    'title' => $this->l('Monto'),
                    'width' => 140,
                    'type' => 'decimal',
                    'orderby' => false,
                    'search' => false
                ),
                'date' => array(
                    'title' => $this->l('Fecha'),
                    'width' => 140,
                    'type' => 'datetime',
                    'orderby' => false,
                    'search' => false
                ),
                'title' => array(
                    'title' => $this->l('Estatus'),
                    'width' => 160,
                    'type' => 'text',
                    'orderby' => false,
                    'search' => false,
                    'color' => 'color',
                    'class' => 'kstatus'
                )
            );

            $this->shopLinkType = '';
            //$this->module = $this->module;
            $this->list_simple_header = true;
            $this->identifier = 'ktoken';
            //$this->list_no_link = true;
            $this->show_toolbar = false;
            $this->lang = false;
            $this->path = _MODULE_DIR_ . "comcodex";

            $this->actions = array('reverted');

            
            $this->tpl_folder = false;
        }

        if (isset($_GET["action"]) && $_GET["action"] == "reverted") {
            $revert = $this->getReverted($_GET['id']);
            exit();
        }
        
 
    }
	
	/**
	 * Muestra la grilla de transacciones
	 */
	public function renderList() 
	{
	    $helper = new KHelperList();
	    $this->setHelperDisplay($helper);
        $data = array();
        
        if (isset($_GET["action"]) && (strtolower($_GET["action"]) == "closingreport")){
            $data = $this->getTransactionClosingReport();
        }else{
            $data = $this->getTransactionData();
        }
        
        $this->context->smarty->assign(array('link_closing_operation_report'=>'index.php?controller=comcodextab&action=closingreport&token='.$_GET['token']));
        
		return $helper->generateList($data, $this->fields_list);
	}
    
    public function getTransactionClosingReport(){

        $db = Db::getInstance();
        $sql = "SELECT ct.id, ct.ps_order_id, ct.token AS ktoken, ct.concept, ct.amount, ch.date, cs.title, cs.color FROM comcodex_transactions ct
                    INNER JOIN comcodex_historical_transactions ch ON (ct.id = ch.comcodex_transactions_id)
                    INNER JOIN comcodex_status_transaction cs ON (cs.ccodex_id = ch.comcodex_status_transaction_id)
                    WHERE ch.comcodex_status_transaction_id = 5 OR ch.comcodex_status_transaction_id = 2 AND ct.bank_profile_id = ".(int)Configuration::get('comcodex_BANK_PROFILE_ACTIVE');
        
        $result = $db->executeS($sql);
        return $result;
        
    }
    
    public function generateClosingReport(){
        
        
        //$this->context->smarty->setTemplete('detail_report.tpl');
        
        $beginDate      = explode("-",date("Y-m-d",strtotime($_POST["beginDate"])));
        $beginHour      = explode(':', date('H:i:s',strtotime($_POST["beginHour"])));
        $endDate        = explode("-",date("Y-m-d",strtotime($_POST["endDate"])));
        $endHour        = explode(':', date('H:i:s',strtotime($_POST["endHour"])));
        
        // solicitamos la data del reporte.
        
        $transactionQuery                   = new CodexTransactionQuery();
        
        // fecha y hora de inicio..
        
        $transactionQuery->beginDate        = new DateTime();
        $transactionQuery->beginDate->setDate((int)$beginDate[0], (int)$beginDate[1], (int)$beginDate[2]);
        $transactionQuery->beginDate->setTime((int)$beginHour[0], (int)$beginHour[1], (int)$beginHour[2]);
        
        // fecha y hora final
        
        $transactionQuery->endDate          = new DateTime();           
        $transactionQuery->endDate->setDate((int)$endDate[0], (int)$endDate[1], (int)$endDate[2]);
        $transactionQuery->endDate->setTime((int)$endHour[0], (int)$endHour[1], (int)$endHour[2]);
        $transactionQuery->bankProfileId    = Comcodex::getBankProfilesActiveId();
        
        $response   = $this->getDetailReportData($transactionQuery);
        
        
        $this->context->smarty->assign(array(
            "detail"        => Comcodex::getDetailClosingReportByCard($response),
            "transactions"  =>  Comcodex::detailTransactionsClosingReport($response->transaction)
        ));

        
        
        
        
    }
    
    private function getDetailReportData($transactionQuery){
        
        $client = Comcodex::getClientApi();
        $client->connect();
        $closingReportData = $client->closingReport($transactionQuery);
        
        return $closingReportData;
        
    }
    
	/**
	 * Obtiene de la base de datos las transacciones ordenadas por su ultimo estatus
	 */
	private function getTransactionData() 
	{
	    /**
	     * todo:  mejorar el query contra un procedimiento almacenado para que sea mas eficiente el filtro del historico
	     */
		return  Db::getInstance()->executeS('
			SELECT ct.id, ct.ps_order_id, ct.token AS ktoken, ct.concept, ct.amount, ch.date, cs.title, cs.color
                FROM comcodex_transactions ct
                INNER JOIN ps_orders o ON (o.id_order = ct.ps_order_id )
                INNER JOIN (		    
    		          SELECT ct.*  
    		          FROM  comcodex_historical_transactions ct 
                      INNER JOIN (SELECT comcodex_transactions_id , MAX(id) as id FROM comcodex_historical_transactions GROUP BY comcodex_transactions_id ) filter
                      ON ( ct.id = filter.id ) 
	            ) ch ON (ct.id = ch.comcodex_transactions_id)		    
	            INNER JOIN comcodex_status_transaction cs ON (cs.ccodex_id = ch.comcodex_status_transaction_id)
                WHERE ct.bank_profile_id = '.(int)Configuration::get('comcodex_BANK_PROFILE_ACTIVE').'
                GROUP BY ch.comcodex_transactions_id ORDER BY ch.comcodex_transactions_id DESC;
		    ');
	}
	
	public function postProcess()
	{
	    try {
	        if ($this->ajax) {
	            return $this->ajaxProcess();
	        }
	        if (Tools::isSubmit('updateconfiguration') && ($token = Tools::getValue('ktoken'))) {
	            $row = $this->getPsTransactionByToken($token);
	            if(count($row) > 0) {
	                //var_dump($row[0]['ps_order_id']);
	                Tools::redirectAdmin(
	                   'index.php?controller=AdminOrders&id_order='.$row[0]['ps_order_id'].'&vieworder&token='.Tools::getAdminTokenLite('AdminOrders')
	                );
	            }
	        }
	    } catch (PrestaShopException $e) {
	        $this->errors[] = $e->getMessage();
	    };
	    return false;
	}
	
	public function ajaxProcess()
	{
	    if ($token = $_GET['id']) {
	        $client = Comcodex::getClientApi();
	        $ccodexTransaction = Comcodex::getTransactionApi($client, $token);
	        $psTransaction = $this->getPsTransactionByToken($token);
	        $this->setNewStatusTransaction($psTransaction[0]['id'], $ccodexTransaction);
	    }
	}
	
	/**
	 * Obtiene un id transaccion filtrada por token de la bd prestashop
	 * @param string $token
	 * @return array
	 */
	private function getPsTransactionByToken($token) {
        
        $SQL = '
			SELECT id, ps_order_id
			FROM comcodex_transactions
	        WHERE token = "'.pSQL($token).'"
	    ';
        
	    return Db::getInstance()->executeS ($SQL);
	}

	/**
	 * Persiste un nuevo estatus de la transaccion solo si no esta en la bd de prestashop
	 * @param int $psTransactionId
	 * @param obj $ccodexTransaction
	 */
	private function setNewStatusTransaction($psTransactionId, $ccodexTransaction) {
	    $db = Db::getInstance();
	    $row = $db->executeS('
			SELECT id, comcodex_status_transaction_id
			FROM comcodex_historical_transactions
	        WHERE comcodex_transactions_id = '.(int)$psTransactionId.' 
	        ORDER BY id DESC
            LIMIT 1
	    ');
	    
	    $response = array();
	    
	    if($row[0]['comcodex_status_transaction_id'] != $ccodexTransaction->status) {
            
	        $date = DateTime::createFromFormat(CodexHttpUtils::DEFAULT_FORMAT_DATE,$ccodexTransaction->payedDate);
	        $datetime = $date->format('Y-m-d G:i:s');
	        $result = $db->insert('comcodex_historical_transactions', array(
	            'date'=> $datetime,
	            'comcodex_status_transaction_id'  => (int)$ccodexTransaction->status,
	            'comcodex_transactions_id'  => (int)$psTransactionId,
	        ), false, true, Db::INSERT, false);
            
	        $response['updated'] = true;
	        switch ($ccodexTransaction->status) {
	            case 0:
	                $response['bg'] = '#8ea6ec';
	                $response['color'] = '#383838';
	                $response['text'] = 'Comcodex - Pago en espera';
	                break;
	            case 1:
	                $response['bg'] = '#8ea6ec';
	                $response['color'] = '#383838';
	                $response['text'] = 'Comcodex - Pago en proceso';
	                break;
	            case 2:
	                $response['bg'] = '#284d28';
	                $response['color'] = '#fff';
	                $response['text'] = 'Comcodex - Pago aceptado';
	                break;
	            case 3:
	                $response['bg'] = '#dc143c';
	                $response['color'] = '#fff';
	                $response['text'] = 'Comcodex - Pago cancelado';
	                break;
	            case 4:
	                $response['bg'] = '#8f0621';
	                $response['color'] = '#fff';
	                $response['text'] = 'Comcodex - Pago inválido';
	                break;
                case 5:
	                $response['bg'] = '#dc143c';
	                $response['color'] = '#fff';
	                $response['text'] = 'Comcodex - Pago Revertido';
	                break;
	        }
	    }
	    
	    
	    
	    echo json_encode($response);
	}
    
    public function getReverted($token){
        
        $client                 = Comcodex::getClientApi();
        $transaction            = Comcodex::getTransactionApi($client, $token);
        $revertTransaction      = $client->revertTransaction($transaction);
        
        // buscamos la transaccion local para cambiarle el status
        
        $transactionLocal   = $this->getPsTransactionByToken($token);
        
        if (count($transactionLocal) > 0){
            
            $id = $transactionLocal[0]["id"];
            $date = DateTime::createFromFormat(CodexHttpUtils::DEFAULT_FORMAT_DATE,$revertTransaction->payedDate);
	        $datetime = $date->format('Y-m-d G:i:s');
            
            
            $fields = array(
	            'date'=> $datetime,
	            'comcodex_status_transaction_id'  => $revertTransaction->status,
	            'comcodex_transactions_id'  => (int)$id,
	        );
            
            Db::getInstance()->insert('comcodex_historical_transactions',$fields, false, true, Db::INSERT, false);
            
        }
        
        
    }
    
    public function renderClosingReportGrid(){
        
        $this->fields_list = array(
                'id' => array(
                    'title' => $this->l('ID Transaccion'),
                    'width' => 120,
                    'type' => 'text',
                    'orderby' => false,
                    'search' => false
                ),
                'ps_order_id' => array(
                    'title' => $this->l('ID Pedido Prestashop'),
                    'width' => 120,
                    'type' => 'text',
                    'orderby' => false,
                    'search' => false
                ),
                'ktoken' => array(
                    'title' => $this->l('Token'),
                    'width' => 120,
                    'type' => 'text',
                    'orderby' => false,
                    'search' => false
                ),
                'concept' => array(
                    'title' => $this->l('Concepto'),
                    'width' => 140,
                    'type' => 'text',
                    'orderby' => false,
                    'search' => false
                )
                ,
                'amount' => array(
                    'title' => $this->l('Monto'),
                    'width' => 140,
                    'type' => 'decimal',
                    'orderby' => false,
                    'search' => false
                ),
                'date' => array(
                    'title' => $this->l('Fecha'),
                    'width' => 140,
                    'type' => 'datetime',
                    'orderby' => false,
                    'search' => false
                ),
                'title' => array(
                    'title' => $this->l('Estatus'),
                    'width' => 160,
                    'type' => 'text',
                    'orderby' => false,
                    'search' => false,
                    'color' => 'color',
                    'class' => 'kstatus'
                )
            );
        
        
        $this->shopLinkType = '';
        $this->list_simple_header = true;
        $this->identifier = 'ktoken';
        $this->show_toolbar = false;
        $this->lang = false;
        $this->path = _MODULE_DIR_ . "comcodex";
        $this->tpl_folder = false;
        $this->actions = null;
        
                
        
    }
	
	public function setMedia()
	{
  		$this->addJquery();
  		$this->addJS($this->path."/js/comcodex.js");
  		parent::setMedia();
	}
	
}
?>