{capture name=path}{l s='Comcodex payment.' mod='comcodex'}{/capture}
{include file="$tpl_dir./breadcrumb.tpl"}
<!-- code to hide the right_column. -->
<script lang="javascript"> 
$("#right_column").ready(function() 
{		 
	$("#right_column").css({ 'display': "none" });
});	
</script>

<h2>{l s='Order summary' mod='comcodex'}</h2>

{assign var='current_step' value='payment'}
{include file="$tpl_dir./order-steps.tpl"}

{if $nbProducts <= 0}
	<p class="warning">{l s='Your shopping cart is empty.' mod='comcodex'}</p>
{else}

<h3>{l s='Comcodex payment.' mod='comcodex'}</h3>
<form action="{$link->getModuleLink('comcodex', 'validation', [], true)|escape:'html'}" method="post">
<p>
	{l s='You have chosen to pay by Comcodex' mod='comcodex'}
	<br/><br />
	{l s='Here is a short summary of your order:' mod='comcodex'}
</p>
<p style="margin-top:20px;">
	- {l s='The total amount of your order is' mod='comcodex'}
	<span id="amount" class="price">{displayPrice price=$total}</span>
	{if $use_taxes == 1}
    	{l s='(tax incl.)' mod='comcodex'}
    {/if}
</p>

<p>
	{l s='Image QR will be displayed on the next page.' mod='comcodex'}
	<br /><br />
	<b>{l s='Please confirm your order by clicking "Place my order."' mod='comcodex'}.</b>
</p>
<p class="cart_navigation" id="cart_navigation">
	<input type="submit" value="{l s='Place my order' mod='comcodex'}" class="exclusive_large" />
	<a href="{$link->getPageLink('order', true, NULL, "step=3")|escape:'html'}" class="button_large">{l s='Other payment methods' mod='comcodex'}</a>
</p>
</form>
{/if}
