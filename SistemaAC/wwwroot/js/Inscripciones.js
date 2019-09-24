var localStorage = window.localStorage;
var idEstudiante, idCurso, idCategoria;
var list = new Array();
var fecha = new Date();
class Inscripciones
{
    constructor() {

    }

    filtrarDataInscripcion(valor, action,fun) {
        valor = (valor === "") ? "null" : valor;
        $.post(
            action,
            { valor },
            (response) => {
                switch (fun) {
                    case 1:
                        $("#resultSearchEstudiante").html(response);
                        break;
                    case 2:
                        $("#resultSearchCurso").html(response);
                        break;                    
                }                
            }
        );
    }

    getData(id, action, fun) {
        $.post(
            action,
            { id },
            (response) => {
                console.log(response);
                switch (fun) {
                    case 1:
                        if (0 < response.length) {
                            document.getElementById("Estudiante").value = response[0].apellidos + " " + response[0].nombres;
                            idEstudiante = response[0].id;
                        }                        
                        break;
                    case 2:
                        if (0 < response.length) {
                            document.getElementById("InscripcionCurso").value = response[0].nombre;
                            document.getElementById("CostoCurso").value = response[0].costo;
                            idCurso = response[0].cursoID;
                            idCategoria = response[0].categoriaID;
                        }
                        
                        break;
                }
                
                this.restablecer();
            }
        );
    }

    addCursos(estudiante, curso, grado, costo) {
        if (estudiante === "") {
            document.getElementById("Estudiante").focus();
        } else {
            if (curso === "") {
                document.getElementById("InscripcionCurso").focus();
            }
            else {
                if (grado === "") {
                    document.getElementById("grado").focus();
                } else {
                    if (costo === "") {
                        document.getElementById("CostoCurso").focus();
                    } else {
                        var listCurso = new Array({
                            idEstudiante: idEstudiante,
                            estudiante: estudiante,
                            idCurso: idCurso,
                            curso: curso,
                            idCategoria: idCategoria,
                            grado: grado,
                            costo: costo,
                            fecha: fecha
                        });

                        var cursos = JSON.parse(localStorage.getItem("cursos"));
                        if (null !== cursos) {
                            if (0 < cursos.length) {
                                for (var i = 0; i <= cursos.length; i++) {
                                    if (i === cursos.length) {                                        
                                        list.push(listCurso);
                                    } else {
                                        if (idCurso !== cursos[i][0].idCurso) {
                                            var listCursos = new Array({
                                                idEstudiante: cursos[i][0].idEstudiante,
                                                estudiante: cursos[i][0].estudiante,
                                                idCurso: cursos[i][0].idCurso,
                                                curso: cursos[i][0].curso,
                                                idCategoria: cursos[i][0].idCategoria,
                                                grado: cursos[i][0].grado,
                                                costo: cursos[i][0].costo,
                                                fecha: cursos[i][0].fecha
                                            });
                                            list.splice(i, 1, listCursos);
                                            //list.push(listCurso);
                                        } else {
                                            alert("Este curso ya está en la lista");
                                            break;
                                        }
                                    }
                                }
                            } else {
                                list.push(listCurso);
                            }
                        } else {
                            list.push(listCurso);
                        }
                        console.log(list);
                        localStorage.setItem("cursos", JSON.stringify(list));
                        this.mostrarCursos();
                    }
                }
            }
        }
    }

    mostrarCursos() {
        var dataFilter;
        var pago = 0;
        var cursos = JSON.parse(localStorage.getItem("cursos"));
        if (null !== cursos) {
            if (0 <= cursos.length) {
                for (var i = 0; i < cursos.length; i++) {
                    //var categoria = this.getCategorias(cursos[i][0].idCategoria);
                    dataFilter += "<tr>" +
                        "<td>" + cursos[i][0].curso + "</td>" +
                        "<td>" + cursos[i][0].idCategoria + "</td>" +
                        "<td>" + cursos[i][0].grado + "</td>" +
                        "<td>" + cursos[i][0].costo + "</td>" +
                        "<td>" + cursos[i][0].fecha + "</td>" +
                        "<td>" +
                        "<a  onclick='deleteCurso(" + i + ")' class='btn btn-success'>Eliminar</a>" +
                        "</td>" +
                        "</tr>";
                    pago += parseFloat(cursos[i][0].costo);
                }
            }
        }
        document.getElementById("pagosCursos").innerHTML = "$ " + pago;
        $("#resultCursos").html(dataFilter);
    }
    
    guardarCursos() {
        let listCursos = new Array();
        let cursos = JSON.parse(localStorage.getItem("cursos"));
        if (null !== cursos) {
            if (0 < cursos.length) {
                for (var i = 0; i < cursos.length; i++) {
                    listCursos.push({
                        grado: cursos[i][0].grado,
                        cursoID: cursos[i][0].idCurso,
                        estudienateID: cursos[i][0].idEstudiante,
                        fecha: cursos[i][0].fecha,
                        pago: cursos[i][0].costo
                    });
                }
            }
        }
        $.post(
            'Inscripciones/guardarCursos',
            { listCursos },
            (response) => {
                if (response[0].code === "Save") {
                    this.deleteData();
                }
                console.log(response);
            }
        );
    }

    deleteData() {
        localStorage.removeItem("cursos");
        $("#resultCursos").html("");
        this.restablecer();
    }

    deleteCurso(id) {        
        let cursos = JSON.parse(localStorage.getItem("cursos"));
        if (null !== cursos) {
            if (0 <= cursos.length) {
                for (var i = 0; i <= cursos.length; i++) {
                    if (i === id) {
                        cursos.splice(i, 1);
                        if (i === cursos.length) {
                            $("#resultCursos").html("");
                        } else {
                            break;
                        }
                    }
                }
                localStorage.setItem("cursos", JSON.stringify(cursos));
            }
        }
        this.mostrarCursos();
    }

    getCategorias(id) {
        var action = 'Inscripciones/getCategorias';
        $.post(
            action,
            { id },
            (response) => {
                return response[0];
            }
        );
    }

    restablecer() {
        document.getElementById("filtrar").value = "";
        document.getElementById("filtrarCurso").value = "";
        $('#modalEstudiante').modal('hide');
        $('#modalCurso').modal('hide');
        
    }
}