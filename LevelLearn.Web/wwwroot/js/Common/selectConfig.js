function carregaSelectPicker(id, callback) {
    setTimeout(function () {
        var seletor = "";
        if (id === undefined || id === "" || id === 0)
            seletor = ".my-select";
        else
            seletor = id;

        $(seletor).select2({
            language: "pt-BR",
            dropdownParent: $('#myModal'),
            placeholder: 'Selecione varios'
        });

        if (callback !== undefined)
            callback();
    }, 200);
}

function carregaSelectPickerSemModal(id, callback) {
    var seletor = "";
    if (id === undefined || id === "" || id === 0)
        seletor = ".my-select";
    else
        seletor = id;

    $(seletor).select2({
        language: "pt-BR",
        placeholder: 'Selecione varios'
    });

    if (callback !== undefined)
        callback();
}