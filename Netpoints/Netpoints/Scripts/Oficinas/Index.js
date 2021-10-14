
var ClickNew = function () {
    window.location.href = "Oficinas/Agregar";
}

var ClickUpdate = function (id) {
    window.location.href = "Oficinas/Edit/" + id;
}

var ClickDelete = function (id) {

    Swal.fire({
        title: 'Estas seguro de Eliminar el registro?',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Borra Registro!'
    }).then((result) => {
        if (result.isConfirmed) {
            window.location.href = "Oficinas/Delete/" + id;
        }
    });
}

if (IsLoading) Loading.fire("Cargando Datos..");
$(document).ready(function () {
    $('#GridOficinas').DataTable();

    setTimeout(function () {
        if (IsLoading) Loading.close();
    }, 500)
});