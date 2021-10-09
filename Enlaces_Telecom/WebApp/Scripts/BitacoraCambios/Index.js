Loading.fire("Cargando Datos..");

$(document).ready(function () {

    $("#GridBitacoraCambios").DataTable();
    setTimeout(function () {
        Loading.close();
    }, 500)
});