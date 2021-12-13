
var ClickNew = function () {
    Loading.fire("Cargando...");
    window.location.href = "Proveedores/Agregar";
}

var ClickUpdate = function (id) {
    Loading.fire("Cargando...");
    window.location.href = "Proveedores/Edit/" + id;
}

var ClickDelete = function (id) {

    Swal.fire({
        title: 'Estas seguro de Eliminar el proveedor?',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Eiminar proveedor!'
    }).then((result) => {
        if (result.isConfirmed) {
            window.location.href = "Proveedores/Delete/" + id;
        }
    });
}

if (IsLoading) Loading.fire("Cargando Datos..");
$(document).ready(function () {
    $('#GridProveedores').DataTable({
        language: {
            sLengthMenu: "_MENU_",
            decimal: "",
            emptyTable: "No hay información",
            info: "Mostrando _START_ a _END_ de _TOTAL_",
            infoEmpty: "Mostrando 0 de 0",
            infoPostFix: "",
            search: "Buscar",
            thousands: ",",
            loadingRecords: "Cargando...",
            processing: "<div class='lds-dual-ring'></div>",
            zeroRecords: "No se encontraron resultados",
            paginate: {
                "first": "Primero",
                "last": "Último",
                "next": "Siguiente",
                "previous": "Anterior"
            },
        }
    });

    setTimeout(function () {
        if (IsLoading) Loading.close();
    }, 500)
});