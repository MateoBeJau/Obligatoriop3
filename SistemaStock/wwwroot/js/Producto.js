﻿let datatable;
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
            "url": "/Admin/Producto/ObtenerTodos"
        },
        "columns": [

            { "data": "numeroProducto" },
            { "data": "descripcion" },
            { "data": "categoria.nombre" },
            { "data": "marca.nombre" },
            {
                "data": "precio", "className": "text-end",

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
            {
                "data": "estado",
                "render": function (data) {
                    if (data == true) {
                        return "Activo";
                    }
                    else {
                        return "Inactivo";
                    }
                }
            },
            {
                "data": "id",
                "render": function (data) {
                    return `
                      <div class="text-center">
                        <a href="/Admin/Producto/Upsert/${data}") class="btn btn-success text-white" style="cursor:pointer">
                            <i class="bi bi-pencil-square"></i>
                        </a>
                        <a onclick=Delete("/Admin/Producto/Delete/${data}") class=" btn btn-danger text-white" style="cursor:pointer" >
                            <i class="bi bi-trash3-fill"></i>
                        </a>
                      </div>
                    `;
                }, "white": "20%"
            }
        ]
    });
}

function Delete(url) {
    swal({
        title: "Estas seguro de eliminar EL PRODUCTO?",
        text: "Sera eliminada de la bdd",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((borrar) => {
        if (borrar) {
            $.ajax({
                type: "POST",
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        datatable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}