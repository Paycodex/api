<?php

class ComcodexPaymentModuleFrontController extends ModuleFrontController
{
	/**
	 * @see FrontController::initContent()
	 */
	public function initContent()
	{
		// se crea la transaccion
		
		$this->display_column_left = false;
		parent::initContent();

		$cart = $this->context->cart;
		//var_dump($cart);
		
		$this->context->smarty->assign(array(
			'nbProducts' => $cart->nbProducts(),
			'cust_currency' => $cart->id_currency,
			'total' => $cart->getOrderTotal(true, Cart::BOTH),
		));

		$this->setTemplate('payment_execution.tpl');
	}
}
