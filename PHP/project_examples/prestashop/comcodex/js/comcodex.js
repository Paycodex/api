function action_check(row_id, controller, token, action, params)
{
	var id = action+'_'+row_id;
	var current_element = $('#check_'+id);
	current_element.find('img').attr('src', '../modules/comcodex/icons/ajax-loader.gif');
	if(current_element.hasClass('processing')) {
        return false;
    }
	current_element.addClass('processing');
	var ajax_params = {
		'id' : row_id,
		'controller' : controller,
		'token' : token,
		'action' : action,
		'ajax' : true
	};
	$.ajax({
		url: 'index.php',
		data: ajax_params,
		dataType: 'json',
		cache: false,
		context: current_element
	}).done(function(response){
		if(response.updated)
			current_element.parent().siblings('.kstatus').children().html(response.text).css({'background-color':response.bg, 'color':response.color});
	}).always(function(){
		current_element.find('img').attr('src', '../modules/comcodex/icons/database_gear.gif');
		current_element.removeClass('processing');
	});
}

function action_reverted(id,controller,token,action){
    
    var linkId = action + '_' + id;
    var current_element = $('#reverted_' + linkId);
    current_element.find('img').attr('src', '../modules/comcodex/icons/ajax-loader.gif');
    if (current_element.hasClass('processing')) {
        return false;
    }
    current_element.addClass('processing');
    
    var ajax_params = {
        "id"            :id,
        "controller"    :controller,
        "token"         :token,
        "action"        :action
    }
    
    if (confirm("Esta seguro que desea realizar esta operación")){
        $.ajax({
           url:'index.php',
           data:ajax_params,
           cache:false,
           
        }).done(function(response){
            alert("Operación exitosa");
        });
    }
    
    return false;
    
}

function action_closing_report(){
    
    var form = $('#filter-closing-report');
    if ($("#beginDate").val() === ""){
        alert('El campo Fecha inicio es obligatorio');
        return false;
    }
    
    if ($("#beginHour").val() === ""){
        alert('El campo Hora inicio es obligatorio');
        return false;
    }
    
    if ($("#endDate").val() === "") {
        alert('El campo Fecha final es obligatorio');
        return false;
    }
    
    if ($("#endHour").val() === "") {
        alert('El campo Hora inicio es obligatorio');
        return false;
    }
    
    form.submit();
    
}