
var ClickNew = function () {
    window.location.href = "Proveedores/Agregar";
}

var ClickUpdate = function (id) {
    window.location.href = "Proveedores/Edit/" + id;
}

var ClickDelete = function (id) {

    Swal.fire({
        title: 'Estas seguro de Eliminar el proveedor?',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Borra proveedor!'
    }).then((result) => {
        if (result.isConfirmed) {
            window.location.href = "Proveedores/Delete/" + id;
        }
    });
}

if (IsLoading) Loading.fire("Cargando Datos..");
$(document).ready(function () {
    $('#GridProveedores').DataTable();

    setTimeout(function () {
        if (IsLoading) Loading.close();
    }, 500)
});