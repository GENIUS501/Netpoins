
var ClickNew = function () {
    window.location.href = "Usuarios/Edit";
}

var ClickUpdate = function (id) {
    window.location.href = "Usuarios/Edit/" + id;
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
            window.location.href = "Usuarios/Delete/" + id;
        }
    });
}

if (IsLoading) Loading.fire("Cargando Datos..");
$(document).ready(function () {
    $('#GridUsuarios').DataTable();

    setTimeout(function () {
        if (IsLoading) Loading.close();
    }, 500)
});