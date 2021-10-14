﻿$("#FormRedes").submit(function (e) {
    e.preventDefault();
    $.validator.setDefaults({ ignore: "" });
    var Formulario = $(this);
    if (!Formulario.valid()) {
        return
    }
    var Url = Formulario.attr('action');
    var DatosFormulario = new FormData(Formulario[0]);
    Loading.fire("Guardando...");
    $.ajax({
        type: "POST",
        url: Url,
        data: DatosFormulario,
        cache: false,
        contentType: false,
        processData: false,
        success: function (data) {
            if (data == "success") {
                Toast.fire({
                    icon: 'success',
                    title: 'Red Guardada'
                });
                sleep(2500).then(() => {
                    window.location.href = "../Redes"
                })
            }
            if (data == "Error") {
                Toast.fire({
                    icon: 'error',
                    title: 'Error'
                });
            }
        },
        error: function (xhr, error, status) {
            Toast.fire({
                icon: 'error',
                title: 'Error'
            });
        },
        complete: function () {

        }
    });
});
function sleep(time) {
    return new Promise((resolve) => setTimeout(resolve, time));
}