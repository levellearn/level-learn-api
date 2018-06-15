function desabilitarBotao(botao, textoBotao) {
    botao.attr('disabled', 'disabled');

    if (textoBotao != undefined && textoBotao != "")
        botao.val(textoBotao);
    else
        botao.val('Enviando...');
}

function habilitarBotao(botao, textoBotao) {
    botao.removeAttr('disabled');
    botao.val(textoBotao);
}