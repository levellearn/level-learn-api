﻿function Entrar() {
    var objeto = $("#formLogar").serialize();
    var botao = $("#btnLogar");
    desabilitarBotao(botao, "ENTRANDO");
    $.ajax({
        url: "/Usuarios/Logar",
        type: "POST",
        data: objeto,
        datatype: "json",
        success: function (data) {

            habilitarBotao(botao, 'ENTRAR');

            if (data.MensagemSucesso != undefined) {
                localStorage.removeItem("Imagem");
                localStorage.setItem("Imagem", JSON.stringify(data.Retorno.Imagem))
                mensagemLogin(data.MensagemSucesso, true)
                window.location.href = "/Home";
            } else {
                mensagemLogin(data.MensagemErro)
            }
        }
    });
}

jQuery(document.body).on('keypress', "#Email", function (event) {
    if (event.which == 13) {
        $("#Senha").focus();
        event.preventDefault();
    }
});

jQuery(document.body).on('keypress', "#Senha", function (event) {
    if (event.which == 13) {
        Entrar();
        event.preventDefault();
    }
});


function mensagemLogin(texto, isSucesso) {
    if (isSucesso) {
        $("#msgUsuarioNaoEncontrado").removeClass("alert-danger").addClass("alert-success");
        $("#msgUsuarioNaoEncontrado").html(texto);
        $("#msgUsuarioNaoEncontrado").show();

    } else {
        $("#msgUsuarioNaoEncontrado").removeClass("alert-success").addClass("alert-danger");
        $("#msgUsuarioNaoEncontrado").html(texto);
        $("#msgUsuarioNaoEncontrado").show();
    }
}

jQuery(document.body).on('keypress', '.field-login', function (event) {
    $("#msgUsuarioNaoEncontrado").hide();
});