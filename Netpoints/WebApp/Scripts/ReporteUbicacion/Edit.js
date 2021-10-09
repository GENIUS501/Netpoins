
var Entity = $("model").data("entity");

var reporteUbicacionEdit = new Vue({
    //Data
    data: {
        model: Entity,
        formulario: "#FormReporteUbicacion",
    },
    //Metodos
    methods: {

        Selection: function () {

            if (BValidateData(this.formulario)) {
                Loading.fire("Guardando...");
                axios.post("Oficinas", this.model).then(function (get) {
                    Loading.close();
                    var result = get.data;

                    if (result.CodeError == 0) {
                        Toast.fire({
                            icon: 'success',
                            title: 'Registro Guardado'
                        });
                        setTimeout(function () {
                            window.location.href = "../ReporteUbicacion"
                        }, 1500)
                    } else {
                        Toast.fire({
                            icon: 'error',
                            title: result.MsgError
                        });
                    }
                });

            } else {

                Toast.fire({
                    icon: "error",
                    title: "Porfavor Complete los campos requeridos!"
                });
            }
        }
    },

    mounted: function () {
        CreateValidator(this.formulario);
    }
    //create
});

reporteUbicacionEdit.$mount("#ReporteUbicacionEdit");