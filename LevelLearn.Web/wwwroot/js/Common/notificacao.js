function notificaSucesso(texto) {
    PNotify.success({
        title: 'Sucesso!',
        text: texto
    });
}

function notificaFalha(texto) {
    PNotify.error({
        title: 'Falha!',
        text: texto
    });
}

function notificaInfo(texto) {
    PNotify.info({
        title: 'Aviso!',
        text: texto
    });
}