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

{if !$simple_header}

	<script type="text/javascript">
		$(document).ready(function() {
			$('table.{$list_id} .filter').keypress(function(event){
				formSubmit(event, 'submitFilterButton{$list_id}')
			})
		});
	</script>
	{* Display column names and arrows for ordering (ASC, DESC) *}
	{if $is_order_position}
		<script type="text/javascript" src="../js/jquery/plugins/jquery.tablednd.js"></script>
		<script type="text/javascript">
			var token = '{$token}';
			var come_from = '{$list_id}';
			var alternate = {if $order_way == 'DESC'}'1'{else}'0'{/if};
		</script>
		<script type="text/javascript" src="../js/admin-dnd.js"></script>
	{/if}

	<script type="text/javascript">
		$(function() {
			if ($("table.{$list_id} .datepicker").length > 0)
				$("table.{$list_id} .datepicker").datepicker({
					prevText: '',
					nextText: '',
					dateFormat: 'yy-mm-dd'
				});
		});
	</script>


{/if}{* End if simple_header *}

{if $show_toolbar}
	{include file="toolbar.tpl" toolbar_btn=$toolbar_btn toolbar_scroll=$toolbar_scroll title=$title}
{/if}

{if !$simple_header}
	<div class="leadin">{block name="leadin"}{/block}</div>
{/if}

{block name="override_header"}{/block}


{hook h='displayAdminListBefore'}
{if isset($name_controller)}
	{capture name=hookName assign=hookName}display{$name_controller|ucfirst}ListBefore{/capture}
	{hook h=$hookName}
{elseif isset($smarty.get.controller)}
	{capture name=hookName assign=hookName}display{$smarty.get.controller|ucfirst|htmlentities}ListBefore{/capture}
	{hook h=$hookName}
{/if}

<style type="text/css">
	.close_operation_ccodex{
		font-size: 14px; font-weight: bold; color: #00529b; text-decoration: underline;
	}
</style>

{if !$simple_header}

<form method="post" action="{$action}" class="form">

	{block name="override_form_extra"}{/block}

	<input type="hidden" id="submitFilter{$list_id}" name="submitFilter{$list_id}" value="0"/>
{/if}
<table class="table_grid" name="list_table">
    <tr>
        <td>
            <ul class="cc_button">
                <li>
                    {if empty($smarty.get.action)}
                        <a class="toolbar_btn close_operation_ccodex" href="{$link_closing_operation_report}" style="" >
                            Cierre de operaciones
                        </a>
                    {else}
                        <form id="filter-closing-report" method="POST" action="index.php?controller=comcodextab&action=reportview&token={$token}">
                        <table>
                            <tr>
                                <td>Fecha Inicio</td>
                                <td><input name="beginDate" id="beginDate" type="date"/></td>
                                <td>Hora Inicio</td>
                                <td><input name="beginHour"  id="beginHour" type="text"/></td>
                                <td>
                                    <a class="toolbar_btn" href="#" onclick="action_closing_report(); " >
                                        Generar reporte
                                    </a>
                                </td>
                            </tr>
                            <tr>
                                <td>Fecha Final</td>
                                <td><input name="endDate" id="endDate" type="date"/></td>
                                <td>Hora Final</td>
                                <td><input name="endHour" id="endHour" type="text"/></td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                        </form>
                    {/if}
                </li>
            </ul>
        </td>
    </tr>
</table>
