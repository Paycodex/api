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

<div id="main-container-comcodex">
{if $status == '2'}
    <div id="container-icon-status">
        
        <img id="icon-status" src="modules/comcodex/icons/aproved.png">
    </div>
	<div id="container-checkPaymentStatus">
		<span id="checkPaymentStatus">{$messageStatus}</span>
		<br>
	</div>
{else}
    <div id="container-icon-status">
        
        <img id="icon-status" src="modules/comcodex/icons/error.png">
    </div>
    <div id="container-checkPaymentStatus">
        <span id="checkPaymentStatus" class="checkPaymentStatus-background-color-error">{$messageStatus}</span>
        <br>
    </div>
{/if}
</div>

<style>

#main-container-comcodex{
	margin-bottom: 40px;
	margin-top: 45px;
}

#container-icon-status{
    text-align: center;
    margin-bottom: 38px;
}

#icon-status{
    display: inline-block;
}

#container-details-comcodex{
	float: left;
}

#container-checkPaymentStatus{
	text-align: center;
    font-size: 21px;
}

#link-check-status{
	color: blue;
	font-weight: bold;
	display: none;
}

#checkPaymentStatus{
    background-color: #4e892f;
    color: white;
    padding-left: 30px;
    padding-right: 30px;
    border-radius: 4px;
    font-size: large;
    padding-top: 8px;
    padding-bottom: 8px;
}

.checkPaymentStatus-background-color-error{
    background-color: #df0028 !important;
}
</style>