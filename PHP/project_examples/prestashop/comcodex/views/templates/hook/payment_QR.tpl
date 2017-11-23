{*
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
*}

{if $status == 'ok'}
<div id="main-container-comcodex">
	<div id="container-details-comcodex">
	<p>		
			{$concept}.
			<br /><br /> 	
			{l s='Please use the QR image to execute the payment with the Comcodex system' mod='comcodex'}
			<br /><br /> 
			<strong>{l s='Your order will be sent as soon as the payment is completed.' mod='comcodex'}</strong>
			<br /><br />
			{l s='If you have questions, comments or concerns, please contact our' mod='comcodex'} <a href="{$link->getPageLink('contact', true)|escape:'html'}">{l s='expert customer support team. ' mod='comcodex'}</a>.
		</p>
	</div>
	<div id="container-qr">
	<strong>QR</strong>
			<br /><br />
		<img src="{$pathImageQR}" alt="Imagen QR" style="max-height: 50%;">
	</div>	
</div>
<div id="container-check-payment-status">
	<strong>Estado: </strong>  <span id="check-payment-status"> En espera</span>
	<br>
    <img id="loading-icon" src="modules/comcodex/icons/ajax-loader2.gif">
	<br>
	<a href="" id="link-check-status">Check Transaction Status</a>
</div>



<script lang="javascript">
var token = "{$token}"; 
var id_order = "{$id_order}";
myInterval = setInterval(function () {
   $( "#link-check-status" ).click();
}, 3000);


$( "#link-check-status" ).click(function( event ) {
	var query = $.ajax({
			type: 'POST',
			url: baseDir + 'modules/comcodex/ajaxCheckTransactions.php',
			data: 'method=checkStatusQR&token=' + token+'&id_order=' + id_order,
			dataType: 'json',
			success: function(json) {
                
				if (json['status']!=1){
                    $(location).attr('href',window.location.href+'&status='+json['status']+'&message-status='+json['message']);
					window.clearInterval(myInterval);
				}else{
					jQuery('#check-payment-status').html(json['message']);
				}
			},
			error: function(err){
				console.log(err);
			}
		});
	return false;
			
});

</script>

<style>

#main-container-comcodex{
	display: flex;
}

#container-details-comcodex{
	float: left;
}

#container-qr{
	float: right;
	text-align: center;
	font-size: medium;
	margin-left: 20px;
}

#container-check-payment-status{
	text-align: center;
    font-size: 21px;	
}

#loading-icon{
    margin-top: 14px;
}

#link-check-status{
	color: blue;
	font-weight: bold;
	display: none;
}
</style>

{else}
	<p class="warning">
		{l s='We noticed a problem with your order. If you think this is an error, feel free to contact our' mod='comcodex'} 
		<a href="{$link->getPageLink('contact', true)|escape:'html'}">{l s='expert customer support team. ' mod='comcodex'}</a>.
	</p>
{/if}
