
var inscripcionId = 0;
class MisCursos {
    constructor() {

    }
    filtrarMisCurso(numPagina, valor) {
        if (valor === "")
            valor = "null";
        $.post(
            "MisCursos/filtrarMisCurso",
            { valor, numPagina },
            (response) => {
                console.log(response);
                $("#resultMisCursos").html(response[0][0]);
            }
        );
    }
    getMisCursos(query, result) {
        $.ajax({
            type: "POST",
            url: "MisCursos/getMisCursos",
            data: { query },
            success: (response) => {
                result($.map(response, (item) => {
                    return item.nombre;
                }));
            }
        });
    }

    getMisEstudiantes(query, result) {
        $.ajax({
            type: "POST",
            url: "MisCursos/getMisEstudiantes",
            data: { query },
            success: (response) => {
                result($.map(response, (item) => {
                    return item.nombres + " " + item.apellidos;
                }));
            }
        });
    }

    getMisDocentes(query, result) {
        $.ajax({
            type: "POST",
            url: "MisCursos/getMisDocentes",
            data: { query },
            success: (response) => {
                result($.map(response, (item) => {
                    return item.nombres + " " + item.apellidos;
                }));
            }
        });
    }

    actualizarMisCurso() {
        let curso = document.getElementById("Curso").value;
        let estudiante = document.getElementById("Estudiante").value;
        let docente = document.getElementById("Docente").value;
        let grado = document.getElementById("Grado").value;
        let pago = document.getElementById("Pago").value;
        let fecha = document.getElementById("Fecha").value;
        if (curso === "") {
            document.getElementById("Curso").focus();
        } else {
            if (estudiante === "") {
                document.getElementById("Estudiante").focus();
            } else {
                if (docente === "") {
                    document.getElementById("Docente").focus();
                } else {
                    if (grado === "") {
                        document.getElementById("Grado").focus();
                    } else {
                        if (pago === "") {
                            document.getElementById("Pago").focus();
                        } else {
                            if (fecha === "") {
                                document.getElementById("Fecha").focus();
                            } else {
                                let listCursos = new Array({
                                    inscripcionId,
                                    curso,
                                    estudiante,
                                    docente,
                                    grado,
                                    pago,
                                    fecha
                                });
                                let data = JSON.stringify(listCursos);
                                $.post(
                                    "MisCursos/actualizarMisCurso",
                                    { data },
                                    (response) => {
                                        if (response[0].code === "Save") {
                                            this.restablecer();
                                        }
                                        console.log(response);
                                    }
                                );
                            }
                        }
                    }
                }
            }
        }
    }

    getMisCurso(curso, id) {
        document.getElementById("Curso").value = curso[0];
        document.getElementById("Estudiante").value = curso[1];
        document.getElementById("Docente").value = curso[2];
        document.getElementById("Grado").value = curso[3];
        document.getElementById("Pago").value = curso[4];
        document.getElementById("Fecha").value = curso[5];
        inscripcionId = id;
    }

    restablecer() {
        $('#modalMisCS').modal('hide');
        this.filtrarMisCurso(1,"");
    }
}