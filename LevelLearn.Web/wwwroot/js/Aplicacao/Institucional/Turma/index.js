$(document).ready(function () {
    listaTurmas()
});

function createTurma() {
    baseEnviaCreate("/Turmas/Create", undefined, undefined, undefined, undefined, function () {
        listaTurmas()
    })
}

function updateTurma() {
    baseEnviaUpdate("/Turmas/Update", undefined, undefined, undefined, undefined, function () {
        listaTurmas()
    })
}

function carregaCreate() {
    preLoaderAzul("#corpoModal");
    $("#corpoModal").load("/Turmas/CarregaCreate/", function () {
        setFocus("#Nome");
        carregaSelectPicker();
    });
}

function carregaUpdate(id) {
    preLoaderAzul("#corpoModal");
    $("#corpoModal").load("/Turmas/CarregaUpdate/" + id, function () {
        setFocus("#Nome")
    });
}

function listaTurmas() {
    preLoaderAmarelo("#listaTurmas");
    $("#listaTurmas").load("/Turmas/lista/");
}