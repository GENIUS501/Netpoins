var UsuariosEdit = new Vue({
    data: {
        Model: {
            Usuario: null,
            Contrasena: null,
            Nombre: null
        },//fin Model
        formulario: "#FormUsuarios"

    },//fin Data

    methods: {

        Save: function () {
            if (BValidateData(this.formulario)) {
                Loading.fire("Registrando...");

                axios.post("/Usuarios/Edit", this.Model).then(function (get) {
                    var result = get.data;
                    Loading.close();

                    if (result.CodeError == 0) {
                        Toast.fire({
                            icon: 'success',
                            title: "Su usuario se creo sastifactoriamente!"
                        }).then(function () {
                            window.location.href = "/Usuario";
                        });
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
                    title: "Por favor Complete los campos requeridos!"
                });
            }
        },//fin LoginUsuario
    },//fin methods
    mounted: function () {
        CreateValidator(this.formulario);
    },//fin Mounted
});
UsuariosEdit.$mount("#UsuariosEdit");