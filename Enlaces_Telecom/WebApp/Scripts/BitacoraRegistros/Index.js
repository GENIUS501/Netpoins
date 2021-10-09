Loading.fire("Cargando Datos..");

$(document).ready(function () {

    $("#GridBitacoraRegistros").DataTable();
    setTimeout(function () {
        Loading.close();
    }, 500)
});