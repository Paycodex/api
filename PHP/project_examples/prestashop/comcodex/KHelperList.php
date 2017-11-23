<?php

class KHelperList extends HelperListCore
{
    
    public function __construct()
	{
		parent::__construct ();
		$this->base_folder = _PS_MODULE_DIR_.'comcodex/views/templates/admin/';
	}
	
	/**
	 * Fetch the template for action enable
	 *
	 * @param string $token
	 * @param int $id
	 * @param int $value state enabled or not
	 * @param string $active status
	 * @param int $id_category
	 * @param int $id_product
	 * @return string
	 */
	public function displayEnableLink($token, $id, $value, $active, $id_category = null, $id_product = null)
	{
		$tpl_enable = $this->createTemplate('list_action_enable.tpl');
		$tpl_enable->assign(array(
			'enabled' => (bool)$value,
			'url_enable' => Tools::safeOutput($this->currentIndex.'&'.$this->identifier.'='.(int)$id.'&'.$active.'='.$value.'&token='.($token != null ? $token : $this->token))
		));
		return $tpl_enable->fetch();
	}
	
	/**
	 * Display view action link
	 */
	public function displayCheckLink($token = null, $id, $name = null)
	{
		$tpl = $this->createTemplate('list_action_check.tpl');
	    
	    $ajax_params = $this->ajax_params;
	    if (!is_array($ajax_params) || !isset($ajax_params['action']))
	        $ajax_params['action'] = 'check';
	
	    $tpl->assign(array(
	        'id' => Tools::safeOutput($id),
	        'controller' => str_replace('Controller', '', get_class($this->context->controller)),
	        'token' => Tools::safeOutput($token != null ? $token : $this->token),
	        'action' => 'Check',
	        'params' => $ajax_params,
	        'json_params' => Tools::jsonEncode($ajax_params)
	    ));
	    return $tpl->fetch();

	}
    
    /**
     * Display view action link
     */
    public function displayRevertedLink($token = null, $id, $name = null) {
        
        $tpl = $this->createTemplate('list_action_reverted.tpl');

        if (!is_array($ajax_params) || !isset($ajax_params['action']))
            $ajax_params['action'] = 'reverted';
        
        $tpl->assign(array(
            'id' => Tools::safeOutput($id),
            'controller' => str_replace('Controller', '', get_class($this->context->controller)),
            'token' => Tools::safeOutput($token != null ? $token : $this->token),
            'action' => 'reverted'
        ));
        return $tpl->fetch();
    }
    
    

}