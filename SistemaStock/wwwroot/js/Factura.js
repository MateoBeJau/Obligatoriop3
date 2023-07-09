let datatable;
$(document).ready(function () {
    loadDataTable();

});


function loadDataTable() {
    datatable = $('#tblDatos').DataTable({
        "language": {
            "lengthMenu": "Mostrar _MENU_ Registros Por Pagina",
            "zeroRecords": "Ningun Registro",
            "info": "Mostrar page _PAGE_ de _PAGES_",
            "infoEmpty": "no hay registros",
            "infoFiltered": "(filtered from _MAX_ total registros)",
            "search": "Buscar",
            "paginate": {
                "first": "Primero",
                "last": "Último",
                "next": "Siguiente ",
                "previous": "Anterior",
            }
        },
        "ajax": {
            "url": "/Inventario/Factura/ObtenerTodos"
        },
        "columns": [

            { "data": "economato.nombre" },
            { "data": "producto.descripcion" },
            
            {
                "data": "producto.costo", "className": "",
            },
            {
                "data": "moneda",
                "render": function (data) {
                    if (data == true) {
                        return "USD";
                    }
                    else {
                        return "UYU";
                    }
                }
            },
            { "data": "cantidad" },

        ]
    });
}
