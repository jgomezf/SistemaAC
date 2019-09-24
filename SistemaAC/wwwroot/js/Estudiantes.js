var idEstudiante = 0;
class Estudiantes {
    constructor() {

    }
    guardarEstudiante(id,funcion,...data) {
        var action = data[0];
        var response = new Array({
            apellidos: data[3], codigo: data[1], direccion: data[8], documento: data[5], email: data[6], estado: data[9], fechadNacimeinto: data[4], nombres: data[2], id: id, telefono: data[7]
        });
        if (data[1] === "") {
            document.getElementById("Codigo").focus();
        } else {
            if (data[2] === "") {
                document.getElementById("Nombre").focus();
            } else {
                if (data[3] === "") {
                    document.getElementById("Apellidos").focus();
                } else {
                    if (data[4] === "") {
                        document.getElementById("FechaNacimiento").focus();
                    } else {
                        if (data[5] === "") {
                            document.getElementById("Documento").focus();
                        } else {
                            if (data[6] === "") {
                                document.getElementById("Email").focus();
                            } else {
                                if (data[7] === "") {
                                    document.getElementById("Telefono").focus();
                                } else {
                                    if (data[8] === "") {
                                        document.getElementById("Direccion").focus();
                                    } else {
                                        $.post(
                                            action,
                                            {
                                                response, funcion
                                            },
                                            (response) => {
                                                if ("1" === response[0].code) {
                                                    this.restablecer();
                                                } else {
                                                    document.getElementById("mensaje").innerHTML = "No se puede guardar el estudiante";
                                                }
                                            }
                                        );
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    filtrarEstudiantes(numPagina, valor, order, action) {
        valor = (valor === "") ? "null" : valor;
        $.post(
            action,
            { valor, numPagina, order },
            (response) => {
                console.log(response);
                $("#resultSearch").html(response[0][0]);
                $("#paginado").html(response[0][1]);
            });
    }
    getEstudiante(id, funcion, action) {
        $.post(
            action,
            {
                id
            },
            (response) => {
                console.log(response);
                if (funcion === 1) {
                    idEstudiante = response[0].id;
                    document.getElementById("Codigo").value = response[0].codigo;
                    document.getElementById("Nombre").value = response[0].nombres;
                    document.getElementById("Apellidos").value = response[0].apellidos;
                    document.getElementById("FechaNacimiento").value = response[0].fechadNacimeinto;
                    document.getElementById("Documento").value = response[0].documento;
                    document.getElementById("Email").value = response[0].email;
                    document.getElementById("Telefono").value = response[0].telefono;
                    document.getElementById("Direccion").value = response[0].direccion;
                    document.getElementById("Estado").checked = response[0].estado;
                }
                var action = 'Estudiantes/guardarEstudiante';
                this.editarEstudiante(response, funcion, action);
            });
    }
    editarEstudiante(response, funcion, action) {
        $.post(
            action,
            {
                response, funcion
            },
            (response) => {
                if (funcion === 0) {
                    this.restablecer();
                }
                console.log(response);

            });
    }

    deleteEstudiante(id, action) {
        $.post(
            action,
            {
                id
            },
            (response) => {
                console.log(response);
                this.restablecer();

            });
    }

    restablecer() {
        document.getElementById("Codigo").value = "";
        document.getElementById("Nombre").value = "";
        document.getElementById("Apellidos").value = "";
        document.getElementById("FechaNacimiento").value = "";
        document.getElementById("Documento").value = "";
        document.getElementById("Email").value = "";
        document.getElementById("Telefono").value = "";
        document.getElementById("Direccion").value = "";
        document.getElementById("Estado").checked = false;
        document.getElementById("Direccion").value = "";
        filtrarEstudiantes(1, "nombre");
        $('#modalAS').modal('hide');
        $('#modalDeleteAS').modal('hide');

    }
}