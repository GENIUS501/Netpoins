
var ClickNew = function () {
    window.location.href = "Usuarios/Agregar";
}

var ClickUpdate = function (id) {
    window.location.href = "Usuarios/Edit/" + id;
}

var ClickDelete = function (id) {

    Swal.fire({
        title: 'Estas seguro de desactivar el usuario?',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Desactivar Usuario!'
    }).then((result) => {
        if (result.isConfirmed) {
            window.location.href = "Usuarios/Delete/" + id;
        }
    });
}

if (IsLoading) Loading.fire("Cargando Datos..");
$(document).ready(function () {
    $('#GridUsuarios').DataTable({
        buttons: [
            {
                "extend": 'excelHtml5',
                "text": '<i class="icon-line-awesome-file-excel-o"><span>Exportar a excel</span></i>',
                "titleAttr": 'Excel',
                "className": '',
                "exportOptions": {
                    "columns": ":visible",

                },
                filename: 'Cursos INA',
                title: 'Cursos INA \n Nombre: ',
                "autoFilter": true,
                "sheetName": 'Datos exportado ANE'
            },
            {
                "extend": 'print',
                "text": '<i class= "icon-feather-printer"><span>Imprimir</span></i>',
                "titleAttr": 'Imprimir',
                "exportOptions": {
                    "columns": ":visible"
                },
                title: 'Cursos INA \n Nombre:',
                "className": ''
            },
            {
                "extend": 'pdfHtml5',
                "text": '<i class="icon-line-awesome-file-pdf-o"><span>Exportar a PDF</span></i>',
                "titleAttr": 'pdf',
                "className": '',
                "exportOptions": {
                    "columns": ":visible",

                },
                filename: 'Cursos INA',
                title: 'Cursos INA \n Nombre:',
                "autoFilter": true,
                "sheetName": 'Datos exportado ANE'
            },
        ]});

    setTimeout(function () {
        if (IsLoading) Loading.close();
    }, 500)
});