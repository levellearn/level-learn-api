function carregaDataTable(id) {
    return $(document).ready(function () {
        $(id).DataTable({
            "language": {
                "zeroRecords": "Nenhum registro encontrado",
                "paginate": {
                    "first": "Primeiro",
                    "last": "Último",
                    "next": "Próximo",
                    "previous": "Anterior"
                },
                "search": "Busque:",
                "info": "",
                "lengthMenu": "Exibindo _MENU_ registros",
                "infoEmpty": "",
                "infoFiltered": "",
            },
            "lengthMenu": [[15, 50, 100, -1], [15, 50, 100, "Todos"]]
        });
    });
}