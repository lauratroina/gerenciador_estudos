var habilitarCustomType = false;

$(document).ready(function () {
	bindMascaras($("body"));
    bindValidacao($("body"));
});

function bindValidacao(obj) {
	addTypeValidation(obj,"date");
	addTypeValidation(obj,"email");
	addTypeValidation(obj,"cpf");
	addTypeValidation(obj,"cnpj");
	addTypeValidation(obj,"int");
	addTypeValidation(obj,"number");
	obj.find("form").validationEngine({
		showPrompts: false,
		scroll: false,
		focusFirstField:false,
		validateAttribute:"validate",
		exclusiveAttribute: true,
		onFieldFailure: function (field, promptText, error) {
		    if (field.attr("validateMessage") != undefined) {
		        eval('var message = ' + field.attr("validateMessage") + ';');
		        promptText = "";
		        for (var type in error) {
		            if (message[type] != undefined) {
		                promptText += message[type] + "<br />";
		            } else {
		                promptText += error[type] + "<br />";
		            }
		        }
		    }
		    var container = field.parent();
            container.removeClass("has-success");
		    container.addClass("has-error");
		    container.addClass("has-feedback");
		    var ico = container.find(".fa");
		    var text = container.find(".help-block");
		    if (ico.length > 0) {
		        ico.removeClass("fa-check-circle");
		        ico.addClass("fa-times-circle");
		    }
		    else {
		        container.append('<span class="fa fa-times-circle form-control-feedback"></span>');
		    }
		    if (text.length > 0)
		    {
		        text.html(promptText);
		    }
		    else
		    {
		        container.append('<p class="help-block">' + promptText + '</p>');
		    }
		},
		onFieldSuccess: function(field){

		    var container = field.parent();
		    container.removeClass("has-error");
		    container.addClass("has-feedback");
		    container.addClass("has-success");
		    var ico = container.find(".fa");
		    var text = container.find(".help-block");
		    if (ico.length > 0) {
		        ico.removeClass("fa-times-circle");
		        ico.addClass("fa-check-circle");
		    }
		    else {
		        container.append('<span class="fa fa-check-circle form-control-feedback"></span>');
		    }
		    if (text.length > 0) {
		        text.remove();
		    }
        }
	});
} 

function addTypeValidation(obj, type){
	obj.find("input[tipo='"+type+"']").each(function(){
		var validate = $(this).attr("validate");
		$(this).attr("validate", type+(((validate==undefined)||($.trim(validate)==""))?"":","+validate));
	});
}

function bindMascaras(obj) {
    var dateTypeEnable = false;
    if (habilitarCustomType) {
        if (checkInput("tel")) {
            obj.find("input[tipo=cpf], input[typo=cnpj], input[tipo=digito], input[tipo=int], input[tipo=number]").attr("type", "tel");
        }
        if (checkInput("email")) {
            obj.find("input[tipo=email]").attr("type", "email");
        }
        if (checkInput("date")) {
            obj.find("input[tipo=date]").attr("type", "date");
            dateTypeEnable = true;
        }   
    }
    if (!dateTypeEnable) {
        obj.find("input[tipo=date]").each(function () {
            if ($(this).val()) {
                var dataYmd = ($(this).val()).split("-");
                if (dataYmd.length == 1) {
                    dataYmd = ($(this).val()).split("/");
                }
                if (dataYmd[0].length > 2) {
                    $(this).val(dataYmd[2] + "/" + dataYmd[1] + "/" + dataYmd[0]);
                }
                else {
                    $(this).val(dataYmd[0] + "/" + dataYmd[1] + "/" + dataYmd[2]);
                }
            }
        });
        obj.find("input[tipo=date]").mask("99/99/9999");
        // obj.find("input[tipo=date]").datepicker({ format: 'dd/mm/yyyy' });
    }
    obj.find("input[tipo=tel]").mask("(99) 9999-9999?9");
    obj.find("input[tipo=cpf]").mask("999.999.999-99");
    obj.find("input[tipo=cnpj]").mask("99.999.999/9999-99");
    obj.find("input[tipo=number]").maskNumber();
    obj.find("input[tipo=int]").maskNumber({ decimals: 0 });
    obj.find("input[mascara]").each(function () {
        $(this).mask($(this).attr("mascara"));
    });
}

function checkInput(type) {
    var input = document.createElement("input");
    input.setAttribute("type", type);
    return input.type == type;
}