$(document).ready(function () {
    listaCursos();
});

function createCurso() {
    baseEnviaCreate("/Cursos/Create", undefined, undefined, undefined, undefined, function () {
        listaCursos();
    });
}

function updateCurso() {
    baseEnviaUpdate("/Cursos/Update", undefined, undefined, undefined, undefined, function () {
        listaCursos();
    });
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
        setFocus("#Nome");
        carregaSelectPicker();
    });
}

function listaCursos() {
    preLoaderAmarelo("#listaCursos");
    $("#listaCursos").load("/Cursos/lista/");
}

function carregaPessoas(id) {
    if (id === undefined || id === "" || id === 0) {
        $('#Alunos').empty().val(0).trigger("change");
        $('#Professores').empty().val(0).trigger("change");
        return false;
    }

    $.getJSON("/Usuarios/GetAlunosInstituicao/" + id, function (result) {
        $('#Alunos').empty();
        $.each(result, function (i, item) {
            var newOption = new Option(item.Nome, item.PessoaId, true, true);
            $('#Alunos').append(newOption);
        });
        $('#Alunos').val(0).trigger("change");
    });

    $.getJSON("/Usuarios/GetProfessoresInstituicao/" + id, function (result) {
        $('#Professores').empty();
        $.each(result, function (i, item) {
            var newOption = new Option(item.Nome, item.PessoaId, true, true);
            $('#Professores').append(newOption);
        });
        $('#Professores').val(0).trigger("change");
    });

}