
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

    getMisCurso(curso, id) {
        document.getElementById("Curso").value = curso[0];
        document.getElementById("Estudiante").value = curso[1];
        document.getElementById("Docente").value = curso[2];
        document.getElementById("Grado").value = curso[3];
        document.getElementById("Pago").value = curso[4];
        document.getElementById("Fecha").value = curso[5];
        inscripcionId = id;
    }
}