Loading.fire("Cargando Datos..");

$(document).ready(function () {

    $("#GridReporteUbicacion").DataTable();
    setTimeout(function () {
        Loading.close();
    }, 500)
});