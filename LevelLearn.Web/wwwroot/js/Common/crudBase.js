function baseEnviaCreate(url, form, textoBotao, btn, fechaModel, callback) {

    var formId = "#formCreate";
    if (form != undefined)
        formId = form

    var botaoId = "#btnCreate";
    if (btn != undefined)
        botaoId = btn

    var objeto = $(formId).serialize();

    var botao = $(botaoId);
    desabilitarBotao(botao);
    $.ajax({
        url: url,
        type: "POST",
        data: objeto,
        datatype: "json",
        success: function (data) {

            if (textoBotao == undefined)
                habilitarBotao(botao, 'Adicionar');
            else
                habilitarBotao(botao, textoBotao);

            if (data.MensagemSucesso != undefined) {
                notificaSucesso(data.MensagemSucesso)

                if (fechaModel == undefined || fechaModel == true) {
                    $("#corpoModal").html("");
                    $('#myModal').modal('hide')
                }

                if (callback != undefined)
                    callback(data);

            } else {
                notificaFalha(data.MensagemErro)
            }
        }
    });
}

function baseEnviaUpdate(url, form, textoBotao, btn, fechaModel, callback) {
    var formId = "#formUpdate";
    if (form != undefined)
        formId = form

    var botaoId = "#btnUpdate";
    if (btn != undefined)
        botaoId = btn

    var objeto = $(formId).serialize();

    var botao = $(botaoId);
    desabilitarBotao(botao);
    $.ajax({
        url: url,
        type: "POST",
        data: objeto,
        datatype: "json",
        success: function (data) {

            if (textoBotao == undefined)
                habilitarBotao(botao, 'Atualizar');
            else
                habilitarBotao(botao, textoBotao);

            if (data.MensagemSucesso != undefined) {
                notificaSucesso(data.MensagemSucesso)

                if (fechaModel == undefined || fechaModel == true) {
                    $("#corpoModal").html("");
                    $('#myModal').modal('hide')
                }

                if (callback != undefined)
                    callback(data);

            } else {
                notificaFalha(data.MensagemErro)
            }
        }
    });
}


jQuery(document.body).on('keypress', '#formCreate', function (e) {
    if (e.keyCode === 13) {
        e.preventDefault();
        $("#btnCreate").trigger("click");
    }
});

jQuery(document.body).on('keypress', '#formUpdate', function (e) {
    if (e.keyCode === 13) {
        e.preventDefault();
        $("#btnUpdate").trigger("click");
    }
});

function setFocus(elemento) {
    setTimeout(function () { $(elemento).focus(); }, 400)
}

function onlyNumbers(id) {
    jQuery(document.body).on('keydown', id, function (e) {
        console.log(e.keyCode);
        if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 188, 190]) !== -1 ||
            (e.keyCode == 65 && (e.ctrlKey === true || e.metaKey === true)) ||
            (e.keyCode == 67 && (e.ctrlKey === true || e.metaKey === true)) ||
            (e.keyCode == 88 && (e.ctrlKey === true || e.metaKey === true)) ||
            (e.keyCode >= 35 && e.keyCode <= 39)) {
            return;
        }
        if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
            e.preventDefault();
        }
    });
}