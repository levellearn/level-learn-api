$(document).ready(function () {
    listaCursos()
});

function createCurso() {
    baseEnviaCreate("/Cursos/Create", undefined, undefined, undefined, undefined, function () {
        listaCursos()
    })
}

function updateCurso() {
    baseEnviaUpdate("/Cursos/Update", undefined, undefined, undefined, undefined, function () {
        listaCursos()
    })
}

function carregaCreate() {
    preLoaderAzul("#corpoModal");
    $("#corpoModal").load("/Cursos/CarregaCreate/", function () {
        setFocus("#Nome");
        carregaSelectPicker();
    });
}

function carregaUpdate(id) {
    preLoaderAzul("#corpoModal");
    $("#corpoModal").load("/Cursos/CarregaUpdate/" + id, function () {
        setFocus("#Nome")
        carregaSelectPicker();
    });
}

function listaCursos() {
    preLoaderAmarelo("#listaCursos");
    $("#listaCursos").load("/Cursos/lista/");
}