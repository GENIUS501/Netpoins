﻿
var ClickNew = function () {
    window.location.href = "Roles/Agregar";
}

var ClickUpdate = function (id) {
    window.location.href = "Roles/Edit/" + id;
}

var ClickDelete = function (id) {

    Swal.fire({
        title: 'Estas seguro de desactivar el rol?',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Desactivar Rol!'
    }).then((result) => {
        if (result.isConfirmed) {
            window.location.href = "Roles/Delete/" + id;
        }
    });
}

if (IsLoading) Loading.fire("Cargando Datos..");
$(document).ready(function () {
    $('#GridRoles').DataTable();

    setTimeout(function () {
        if (IsLoading) Loading.close();
    }, 500)
});