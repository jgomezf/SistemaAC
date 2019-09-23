class Inscripciones
{
    constructor() {

    }

    filtrarEstudiantes(valor, action) {
        valor = (valor === "") ? "null" : valor;
        $.post(
            action,
            { valor },
            (response) => {
                $("#resultSearchEstudiante").html(response);
            }
        );
    }

    getEstudiante(id, action) {
        $.post(
            action,
            { id },
            (response) => {
                console.log(response);
                document.getElementById("Estudiante").value = response[0].apellidos + " " + response[0].nombres;
                this.restablecer();
            }
        );
    }
    restablecer() {
        document.getElementById("filtrar").value = "";
        $('#modalEstudiante').html('hide');
    }
}