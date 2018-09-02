$(document).ready(function () {
    listaTurmas();
});

function createTurma() {
    baseEnviaCreate("/Turmas/Create", undefined, undefined, undefined, undefined, function () {
        listaTurmas();
    });
}

function updateTurma() {
    baseEnviaUpdate("/Turmas/Update", undefined, undefined, undefined, undefined, function () {
        listaTurmas();
    });
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
        setFocus("#Nome");
    });
}

function listaTurmas() {
    preLoaderAmarelo("#listaTurmas");
    $("#listaTurmas").load("/Turmas/lista/");
}

function carregaAlunos(id) {
    if (id === undefined || id === "" || id === 0) {
        $('#AlunoIds').empty().val(0).trigger("change");
        return false;
    }

    $.getJSON("/Usuarios/GetAlunosCurso/" + id, function (result) {
        $('#AlunoIds').empty();
        $.each(result, function (i, item) {
            var newOption = new Option(item.Nome, item.PessoaId, true, true);
            $('#AlunoIds').append(newOption);
        });
        $('#AlunoIds').val(0).trigger("change");
    });
}