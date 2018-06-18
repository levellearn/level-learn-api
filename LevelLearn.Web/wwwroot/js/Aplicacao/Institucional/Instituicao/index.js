$(document).ready(function () {
    listaInstituicoes()
});

function createInstituicao() {
    baseEnviaCreate("/Instituicoes/Create", undefined, undefined, undefined, undefined, function () {
        listaInstituicoes()
    })
}

function updateInstituicao() {
    baseEnviaUpdate("/Instituicoes/Update", undefined, undefined, undefined, undefined, function () {
        listaInstituicoes()
    })
}

function carregaCreate() {
    escrevePreLoader("#corpoModal");
    $("#corpoModal").load("/Instituicoes/CarregaCreate/", function () {
        setFocus("#Nome");
        carregaSelectPicker();
    });
}

function carregaUpdate(id) {
    escrevePreLoader("#corpoModal");
    $("#corpoModal").load("/Instituicoes/CarregaUpdate/" + id, function () {
        setFocus("#Nome")
    });
}

function listaInstituicoes() {
    escrevePreLoader("#listaInstituicoes");
    $("#listaInstituicoes").load("/Instituicoes/lista/");
}