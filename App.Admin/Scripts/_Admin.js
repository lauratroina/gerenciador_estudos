init.push(function () {

    $(".cb-parceiro").change(function () {
        location.href = $(this).val();
        return false;
    });

    $('.switcher-example-default').switcher();

    $("body").on("click", "#modal-view-copy", function (e) {
        e.preventDefault();
        clipboard.copy($(this).attr("href")).then(
            function () { notifica('Link copiado com sucesso', 'sucesso'); },
            function (err) { notifica('Erro ao copiar o link', 'erro'); }
        );
    });

    $("body").on("click", ".lnk-view", function (e) {
        e.preventDefault();
        var lnk = $(this);
        var href = lnk.attr("href");
        var valido = true;
        if (lnk.attr("data-form") !== undefined) {
            var form = $(lnk.attr("data-form"));
            valido = form.validationEngine("validate");
            
            if (href.indexOf("?") < 0) {
                href += "?" + form.serialize();
            } else {
                href += "&" + form.serialize();
            }

            form.find("input[type=file]").each(function () {
                if ($(this).attr("data-getname") !== undefined) {
                    try {
                        href += "&" + $(this).attr("data-getname") + "=" + escape(URL.createObjectURL(this.files[0]));
                    }
                    catch (e) { }
                }
            });
        }
        if (valido) {
            $("#modal-view").modal();
            $("#modal-view-link").attr("href", href);
            $("#modal-view-copy").attr("href", href);
            window.open(href, "desktop");
            window.open(href, "mobile");
            window.open(href, "tablet");
            $(".width-zoom").each(function () {
                var elem = $(this);
                elem.find("iframe").css({ "opacity": 0 });
                setTimeout(function () {
                    if (elem.hasClass("zoom-calculado")) {
                        elem.find("iframe").css({ "opacity": 1 });
                    } else {
                        var w_disponivel = elem.find(".frame").width() - 2;
                        var w_atual = elem.find("iframe").width();
                        var h_atual = elem.find(".frame").height();
                        if (w_atual > w_disponivel) {
                            var scale = w_disponivel / w_atual;
                            elem.find(".frame").height(h_atual * scale);
                            elem.find("iframe").css({
                                "transform": "scale(" + scale + ")",
                                "transform-origin": "0",
                                "-webkit-transform": "scale(" + scale + ")",
                                "-webkit-transform-origin": "0 0",
                                "opacity": 1
                            });
                        } else {
                            elem.find("iframe").css({ "opacity": 1 });
                        }
                    }
                    elem.addClass("zoom-calculado");
                }, 1500);
            });
        } else {
            notifica("É preciso preencher os campos obrigatórios para visualizar", 'erro');
        }
    });

});

function notifica(mensagem, tipo) {
    if (tipo == undefined) {
        tipo = "info";
    }
    if (tipo == "info") {
        $.growl({ title: "Opa!", message: mensagem });
    }
    if (tipo == "sucesso") {
        $.growl.notice({ title: "OK!", message: mensagem });
    }
    if (tipo == "erro") {
        $.growl.error({ title: "Erro :(", message: mensagem });
    }
}

$('.switch1').switcher({
    theme: 'square',
    on_state_content: '<span class="fa fa-check"></span>',
    off_state_content: '<span class="fa fa-times"></span>'
});


var dataTableLang = {
    "sEmptyTable": "Nenhum registro encontrado",
    "sInfo": "Mostrando de _START_ até _END_ de _TOTAL_ registros",
    "sInfoEmpty": "Mostrando 0 até 0 de 0 registros",
    "sInfoFiltered": "(Filtrados de _MAX_ registros)",
    "sInfoPostFix": "",
    "sInfoThousands": ".",
    "sLengthMenu": "_MENU_ itens",
    "sLoadingRecords": "Carregando...",
    "sProcessing": "Processando...",
    "sZeroRecords": "Nenhum registro encontrado",
    "sSearch": "",
    "oPaginate": {
        "sNext": "Próximo",
        "sPrevious": "Anterior",
        "sFirst": "Primeiro",
        "sLast": "Último"
    },
    "oAria": {
        "sSortAscending": ": Ordenar colunas de forma ascendente",
        "sSortDescending": ": Ordenar colunas de forma descendente"
    }
};