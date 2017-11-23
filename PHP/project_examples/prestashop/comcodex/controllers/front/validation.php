<?php
/*
* 2007-2013 PrestaShop
*
* NOTICE OF LICENSE
*
* This source file is subject to the Academic Free License (AFL 3.0)
* that is bundled with this package in the file LICENSE.txt.
* It is also available through the world-wide-web at this URL:
* http://opensource.org/licenses/afl-3.0.php
* If you did not receive a copy of the license and are unable to
* obtain it through the world-wide-web, please send an email
* to license@prestashop.com so we can send you a copy immediately.
*
* DISCLAIMER
*
* Do not edit or add to this file if you wish to upgrade PrestaShop to newer
* versions in the future. If you wish to customize PrestaShop for your
* needs please refer to http://www.prestashop.com for more information.
*
*  @author PrestaShop SA <contact@prestashop.com>
*  @copyright  2007-2013 PrestaShop SA
*  @license    http://opensource.org/licenses/afl-3.0.php  Academic Free License (AFL 3.0)
*  International Registered Trademark & Property of PrestaShop SA
*/


/**
 * @since 1.5.0
 */
class ComcodexValidationModuleFrontController extends ModuleFrontController
{
	/**
	 * @see FrontController::postProcess()
	 */
	public function postProcess()
	{	
		$error = false;
		// Se optiene la direccion fisica del autoloader.
		$pathDirname = dirname(__FILE__);
		$cart = $this->context->cart;
		$comcodex = new Comcodex();
		$total = (float)$cart->getOrderTotal(true, Cart::BOTH);
		$totalAmount = explode(".",$total);
				
		if ($cart->id_customer == 0 || $cart->id_address_delivery == 0 || $cart->id_address_invoice == 0 || !$this->module->active)
			Tools::redirect('index.php?controller=order&step=1');
		
		// Check that this payment option is still available in case the customer changed his address just before the end of the checkout process
		$authorized = false;
		foreach (Module::getPaymentModules() as $module)
			if ($module['name'] == 'comcodex')
			{
				$authorized = true;
				break;
			}
		if (!$authorized)
			die($this->module->l('This payment method is not available.', 'validation'));
		
		$customer = new Customer($cart->id_customer);
		if (!Validate::isLoadedObject($customer))
			Tools::redirect('index.php?controller=order&step=1');
		
		$currency = $this->context->currency;

		
		//
		// Se optiene la instancia del cliente en comcodex 
		// 
		$client = Comcodex::getClientApi();

		//
		// Se establece la conexion
		//
		try {
				$client->connect();
			}
			catch (CodexServiceClientException $ex)
			{
				if( $ex->getCode() == CodexServiceClientException::ERROR_CODE_SESSION_TOKEN_INVALID )
				{
					$client->connect();
										
				}else{
					$error = true;
				}
			}

		//
		// Se valida la Orden
		//
		if (!$error) {
			$this->module->validateOrder($cart->id, Configuration::get('comcodex_STATUS_0'), $total, $this->module->displayName,NULL, array(), NULL, false, $customer->secure_key, null);
			// Se obtiene el id de la orden
			$idOrder = $this->module->currentOrder;
			// Se obtiene la cantidad de productos
			$quantity = 0;
			foreach ($cart->getProducts() as $product){
					$quantity = $quantity + (int)$product['cart_quantity'];
			}
	
		}else{
			die('Error creating the transaction, please try again.');
		}
		
		
		try {
			//
			// Se crea la nueva transacción en el servicio comcodex
			//


			$newTransaction = new CodexTransaction();
			$newTransaction->amount 			= new CodexDecimal($totalAmount[0], $totalAmount[1]);
			$newTransaction->device 			= "PrestaShop";
			$newTransaction->concept 			= Comcodex::getOrderConcept($idOrder, $quantity);
			$newTransaction = $client->openTransaction($newTransaction);
		}
		catch (CodexServiceClientException $ex)
		{
			if( $ex->getCode() == CodexServiceClientException::ERROR_CODE_SESSION_TOKEN_INVALID )
			{
				$client->connect();
				$newTransaction = new CodexTransaction();
				$newTransaction->amount 			= new CodexDecimal($totalAmount[0], $totalAmount[1]);
				$newTransaction->device 			= "PrestaShop";
				$newTransaction->concept 			= Comcodex::getOrderConcept($idOrder, $quantity);
				$newTransaction = $client->openTransaction($newTransaction);
				
			}else{
				error_log($ex->getMessage());
				$error = true;
			}
		}
		
		if ($error) {
			die ('Ocurrio un error en la generacion de la transaccion con el servicio comcodex.');
			$error = true;
		}elseif (!$this->saveNewTransaction($newTransaction,$idOrder)){
			die('Ocurrio un error SQL.');
		}
		//
		// Se optiene el QR de la transacción
		//
		try {
			$pathImageQR = $client->getQrImage($newTransaction);
		}
		catch (CodexServiceClientException $ex)
		{ 
			echo "CodexServiceClientException Arrojada, codigo: ".$ex->getCode();
			if( $ex->getCode() == CodexServiceClientException::ERROR_CODE_SESSION_TOKEN_INVALID )
			{
				$pathImageQR = $client->getQrImage($newTransaction);
			}else{
				$error = true;
			}
		}
		
		if (!file_exists($pathImageQR)) {
			die('No se generó la imagen QR');
		}
		
		Tools::redirect('index.php?controller=order-confirmation&id_cart='.$cart->id.'&id_module='.$this->module->id.'&id_order='.$this->module->currentOrder.'&key='.$customer->secure_key);
	}
	
	
	/**
	 * 
	 * @param CodexTransaction $transaction
	 * @param Int $orderId
	 * @param number $idBankProfile
	 * @return boolean
	 */
	public function saveNewTransaction($transaction,$orderId = "NULL", $idBankProfile=1){
		/* @var $transaction CodexTransaction */
		/* @var $transaction->amount CodexDecimal */
		/* @var $db Db */
		$date = date("Y-m-d H-i-s");
		
		$transaction_id = $transaction->id;
		$token = $transaction->token;
		$concept = $transaction->concept;

		$amount = $transaction->amount->toDouble();
		$payed = ($transaction->payed ==!"")?$transaction->payed:"FALSE";
		$payedCardHolder = ($transaction->payedCardHolder !== "")? :"NULL";;
		$payedCardNumber = ($transaction->payedCardNumber !== "")? :"NULL";
		$payedIdentity = ($transaction->payedIdentity !== "")?$transaction->payedIdentity:"NULL";;
		$payedDate = ($transaction->payedDate !== "")?$transaction->payedDate:"NULL";;
		$orderId = ($orderId !== "")?$orderId:"NULL";
		
		//$status = ($transaction->status != "")?$transaction->status:"0";
		$status = $transaction->status;
		
		$values = "NULL, '$token','$transaction_id', '$concept', $amount, $payed, '$payedCardHolder', '$payedCardNumber', '$payedIdentity', '$payedDate', $orderId, '$idBankProfile'";

		$sql = "
			INSERT INTO `comcodex_transactions`
				 (`id`, `token`, `transaction_id`, `concept`, `amount`, `payed`, `payed_card_holder`, `payed_card_number`, `payed_identity`, `payed_date`, `ps_order_id`, `bank_profile_id`) 
				VALUES 
				($values);";
		// Se instancia el objeto encargado de ejecutar las Peticiones SQL
		$db = Db::getInstance();

		$result = $db->Execute($sql);
		 
		if(!$result){
			echo ("Ocurrio un Error SQL al tratar de agregar registro en la tabla 'comcodex_transactions', Mensaje:".$db->getMsgError()." ------ SQL: ".$sql);
			return false;
		}

		// Se instancia el objeto encargado de ejecutar las Peticiones SQL
		$db = Db::getInstance();

		//
		// Se obtiene el id del registro en la tabla 'comcodex_transactions'
		//
		$sql = "SELECT `id` 
		FROM  `comcodex_transactions` 
		WHERE  `transaction_id` LIKE '$transaction_id' LIMIT 1;";
		
		$arrayResult = $db->ExecuteS($sql);
		foreach ($arrayResult as $element){
			$idTransaction = $element['id'];
			break;
		}
		
		// Se instancia el objeto encargado de ejecutar las Peticiones SQL
		$db = Db::getInstance();
		
		$values = "NULL, '$date',$status,'$idTransaction'";
		$sql = "
		INSERT INTO `comcodex_historical_transactions` 
		(`id`, `date`, `comcodex_status_transaction_id`, `comcodex_transactions_id`) 
		VALUES ($values);";
		
		$result = $db->Execute($sql);
		if(!$result){
		  echo ("Ocurrio un Error SQL al tratar de agregar registro en la tabla 'comcodex_historical_transactions', Mensaje:".$db->getMsgError()." ------ SQL: ".$sql."<br><br>");
			echo "----VALUES---- = ".$values;
			return false;
		}
		return true;
		
	}
	
}
