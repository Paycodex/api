<?php

/**
 * Código encargado de la gestión de las peticiones Ajax para checkear las transacciones.
 * 
 */

require_once(dirname(__FILE__).'../../../config/config.inc.php');
require_once(dirname(__FILE__).'../../../init.php');
require_once(dirname(__FILE__).'/comcodex.php');

switch (Tools::getValue('method')){
	case 'checkStatusQR':
		// Se optiene la direccion fisica del autoloader.
		$pathDirname = dirname(__FILE__);
		
		if(isset($_POST['token'])){
			$token = $_POST['token'];
			$idOrder = $_POST['id_order'];
		}else{
			die(Tools::jsonEncode( array('status'=>4,"message"=>"Ocurrió un error, intentelo nuevamente")));
		}
		$client = Comcodex::getClientApi();
		//
		//	Se consulta la transacción
		//
		$transaction = Comcodex::getTransactionApi($client,$token);
	
		
		if(is_null($transaction)){
			//setCancelState($idOrder, Configuration::get('comcodex_STATUS_3'));
			die(Tools::jsonEncode( array('status'=>4,"message"=>"Ocurrió un error, intentelo nuevamente")));
		}elseif(is_null($transaction->status)){
			die(Tools::jsonEncode( array('status'=>4,"message"=>"Ocurrió un error, intentelo nuevamente")));
		}
			// Obtiene el ultimo status registrado en prestashop
			$oldStatus = getStatusTransaction($transaction->id);
				
			// Compara el estatus registrado en prestashop con el ultimo registrado en comcodex
			error_log(" oldStatus = ".$oldStatus." newstatus= ".(int)$transaction->status);
			if($oldStatus != (int)$transaction->status){

				if (!addStatusTransaction($transaction->status,$transaction->id)) {
					// Handle error;
				};
				updateOrderStatus($idOrder, $transaction->status);
			}				

				switch ($transaction->status){
					case 0:
						die( Tools::jsonEncode( array('status'=>1,"message"=>"En espera")));
						break;
					case 1:
						die( Tools::jsonEncode( array('status'=>1,"message"=>"El pago esta en proceso de ser aprobado")));
						break;
					case 2:
						die( Tools::jsonEncode( array('status'=>2,"message"=>"El pago se realizó exitosamente")));
						break;
					case 3:
						die( Tools::jsonEncode( array('status'=>3,"message"=>"El pago no pudo ser procesado")));
						break;
					case 4:
						die( Tools::jsonEncode( array('status'=>3,"message"=>"El pago no pudo ser procesado")));
						break;
				}
				
	    break;
	default:
		exit;
}

/**
 * Asigna Status registrado de la transaccion
 * 
 * @param int $status
 * @param int $idTransaction
 * @return boolean
 */
function addStatusTransaction($status,$idTransaction){
	
	#id_pedido_prestashop, #nombre_tienda, #cant_productos
	
	// Se instancia el objeto encargado de ejecutar las Peticiones SQL
	$db = Db::getInstance();
	$date = date("Y-m-d H-i-s");
	//
	// Se obtiene el id del registro en la tabla 'comcodex_transactions'
	//
	$sql = "SELECT `id`
	FROM  `comcodex_transactions`
	WHERE  `transaction_id` LIKE '$idTransaction' LIMIT 1;";
	
	$arrayResult = $db->ExecuteS($sql);
	foreach ($arrayResult as $element){
		$idTransactionInt = $element['id'];
		break;
	}
	
	// Se instancia el objeto encargado de ejecutar las Peticiones SQL
	$db = Db::getInstance();
	
	$values = "NULL, '$date',$status,'$idTransactionInt'";
	$sql = "
	INSERT INTO `comcodex_historical_transactions`
	(`id`, `date`, `comcodex_status_transaction_id`, `comcodex_transactions_id`)
	VALUES ($values);";
	
	$result = $db->Execute($sql);
	if(!$result){
		//echo ("Ocurrio un Error SQL al tratar de agregar registro en la tabla 'comcodex_historical_transactions', Mensage:".$db->getMsgError()." ------ SQL: ".$sql."<br><br>");
		//echo "----VALUES---- = ".$values;
		return false;
	}
	return true;
}


/**
 * Obtiene el status de una transacción registrada
 * 
 * @param int $idTransaction
 */
function getStatusTransaction($idTransaction){
	// Se instancia el objeto encargado de ejecutar las Peticiones SQL
	$db = Db::getInstance();
	
	//
	// Se obtiene el id del registro en la tabla 'comcodex_transactions'
	//
	$sql = "SELECT historical.comcodex_status_transaction_id
	FROM  `comcodex_transactions`
	
	INNER JOIN `comcodex_historical_transactions` historical 
		ON (historical.comcodex_transactions_id = `comcodex_transactions`.id)
			
		WHERE  `transaction_id` LIKE '$idTransaction' 
		ORDER BY historical.id DESC	LIMIT 1;";
	
	$arrayResult = $db->ExecuteS($sql);
	if(isset($arrayResult[0]['comcodex_status_transaction_id'])){
		return $arrayResult[0]['comcodex_status_transaction_id'];
	}else{
		return NULL;
	}
	
}

/**
 * 
 * @param int $idOrder
 */
function updateOrderStatus($idOrder, $status){
	
	//setCancelState($idOrder, Configuration::get('comcodex_STATUS_3'));
	//
	//	Obtener id del status
	//		
	switch ($status){
		case 0:
			$idStatus = Configuration::get('comcodex_STATUS_0');
			break;
		case 1:
			$idStatus = Configuration::get('comcodex_STATUS_1');
			break;
		case 2:
			$idStatus = Configuration::get('comcodex_STATUS_2');
			break;
		case 3:
			$idStatus = Configuration::get('comcodex_STATUS_3');
			break;
		case 4:
			$idStatus = Configuration::get('comcodex_STATUS_4');
			break;
		default:
			break;
		
	}
	
	$order = new Order($idOrder);
	/* Change order state, add a new entry in order history and send an e-mail to the customer if needed */
	if (isset($order))
	{
		
			$order_state = new OrderState($idStatus);
	
			if (!Validate::isLoadedObject($order_state))
				error_log('The new order status is invalid.');
			else
			{
				$current_order_state = $order->getCurrentOrderState();
				if ($current_order_state->id != $order_state->id)
				{
					
					// Update order
					Db::getInstance()->Execute("INSERT INTO "._DB_PREFIX_."order_history (`id_employee`, `id_order`, `id_order_state`, `date_add`) VALUES ('0', '".$order->id."', '". (int)$order_state->id . "', NOW())");
					
					// Create new OrderHistory
					$history = new OrderHistory();
					$history->id_order = $order->id;

					/*
					 * obtener id_employee
					 */
					//$history->id_employee = (int)$this->context->employee->id;
					$use_existings_payment = false;
					if (!$order->hasInvoice())
						$use_existings_payment = true;
					$history->changeIdOrderState((int)$order_state->id, $order, $use_existings_payment);
	
					// Send email
					$extraVars = array();
					$history->addWithemail(true, $extraVars);
				}
				else{
					error_log('The order has already been assigned this status.');
				}
			}
		
	}
	
}


?>