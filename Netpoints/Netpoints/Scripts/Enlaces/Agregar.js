function ChangeOficina() {
    console.log(UrlOficina);
    if ($('#IdOficina').val() != "") {
        $.ajax({
            type: "GET",
            url: UrlOficina,
            cache: false,
            contentType: false,
            processData: false,
            success: function (data) {
                Loading.fire("Cargando...");
                if (data == "Error") {
                    Loading.close();
                    Toast.fire({
                        icon: 'error',
                        title: 'La identificación ya se encuentra registrada!'
                    });
                }
                if (data != "") {
                    //debugger
                    Loading.close();
                    $('#UE').val(data['UE']);
                    $('#Provincia').val(data['Provincia']);
                    $('#Comentario').val(data['Comentario']);
                }
            },
            error: function (xhr, error, status) {
                Loading.close();
                Toast.fire({
                    icon: 'error',
                    title: 'Error'
                });
            },
        });
    } else {
        $('#UE').val("");
        $('#Provincia').val("");
        $('#Comentario').val("");
    }
}