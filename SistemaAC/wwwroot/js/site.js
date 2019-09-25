// Write your JavaScript code.
$('#modalEditar').on('shown.bs.modal', function () {
    $('#myInput').focus();
});
$('#modalAC').on('shown.bs.modal', function () {
    $('#Nombre').focus();
});

function getUsuario(id,action) {
    $.ajax({
        type: "POST",
        url: action,
        data: { id },
        success: function (response) {
            mostrarUsuario(response);
        }

    });
}
var items;
var j = 0;
//Variables globales por cada propiedad del usuario
var id;
var userName;
var email;
var phoneNumber;
var role;
var selectRole;

//Otras variables donde almacenaremos los datos del registro, pero estos datos no serán modificados
var accessFailedCount;
var concurrencyStamp;
var emailConfirmed;
var lockoutEnabled;
var lockoutEnd;
var normalizedUserName;
var normalizedEmail;
var passwordHash;
var phoneNumberConfirmed;
var securityStamp;
var twoFactorEnabled;



function mostrarUsuario(response) {
    items = response;
    j = 0;
    for (var i = 0; i < 3; i++) {
        var x = document.getElementById('Select');
        x.remove(i);
    }

    $.each(items, function (index, val) {
        $('input[name=Id]').val(val.id);
        $('input[name=UserName]').val(val.userName);
        $('input[name=Email]').val(val.email);
        $('input[name=PhoneNumber]').val(val.phoneNumber);
        document.getElementById('Select').options[0] = new Option(val.role, val.roleId);

        //Mostrar los detalles del usuario
        $("#dEmail").text(val.email);
        $("#dUserName").text(val.userName);
        $("#dPhoneNumber").text(val.phoneNumber);
        $("#dRole").text(val.role);

        //Mostrar los datos del usuario que deseo eliminar
        $("#eUsuario").text(val.email);
        $('input[name=EIdUsuario]').val(val.id);

    });
}


function getRoles(action) {
    $.ajax({
        type: "POST",
        url: action,
        data: {},
        success: function (response) {
            if (j===0){
                for (var i = 0; i < response.length; i++){
                    document.getElementById('Select').options[i] = new Option(response[i].text, response[i].value);
                    document.getElementById('SelectNuevo').options[i] = new Option(response[i].text, response[i].value);
                }
                j = 1;
            }
        }
    });
}

function editarUsuario(action) {
    //Obtenemos los datos del input respectivo del formulario
    id = $('input[name=Id]')[0].value;
    email = $('input[name=Email]')[0].value;
    phoneNumber = $('input[name=PhoneNumber]')[0].value;
    role = document.getElementById('Select');
    selectRole = role.options[role.selectedIndex].text;


    $.each(items, function (index, val) {
        accessFailedCount = val.accessFailedCount;
        concurrencyStamp = val.concurrencyStamp;
        emailConfirmed = val.emailConfirmed;
        lockoutEnabled = val.lockoutEnabled;
        lockoutEnd = val.lockoutEnd;
        userName = val.userName;
        normalizedUserName = val.normalizedUserName;
        normalizedEmail = val.normalizedEmail;
        passwordHash = val.passwordHash;
        phoneNumberConfirmed = val.phoneNumberConfirmed;
        securityStamp = val.securityStamp;
        twoFactorEnabled = val.twoFactorEnabled;
    });
    $.ajax({
        type: "POST",
        url: action,
        data: {
            id, userName, email, phoneNumber, accessFailedCount,
            concurrencyStamp, emailConfirmed, lockoutEnabled, lockoutEnd,
            normalizedEmail, normalizedUserName, passwordHash, phoneNumberConfirmed,
            securityStamp, twoFactorEnabled,selectRole
        },
        success: function (response) {
            if (response === "Save") {
                window.location.href = "Usuarios";
            }
            else {
                alert("No se puede editar los datos del usuario");
            }
        }
    });


}

function ocultarDetalleUsuario() {
    $("#modalDetalle").modal("hide");
}

function eliminarUsuario(action) {
    var id = $('input[name=EIdUsuario]')[0].value;
    $.ajax({
        type: "POST",
        url: action,
        data: { id },
        success: function (response) {
            if (response === "Delete") {
                window.location.href = "Usuarios";
            }
            else {
                alert("No se puede eliminar el registro");
            }
        }
    });
}

function crearUsuario(action) {
    //Obtener los datos ingresados en los inputs respectivos
    email = $('input[name=EmailNuevo]')[0].value;
    phoneNumber = $('input[name=PhoneNumberNuevo]')[0].value;
    passwordHash = $('input[name=PasswordHashNuevo]')[0].value;
    role = document.getElementById('SelectNuevo');
    selectRole = role.options[role.selectedIndex].text;

    //Vamos a validar ahora que los datos del usuario no estén vacíos
    if (email === "") {
        $('#EmailNuevo').focus();
        alert("Ingrese el email del usuario");
    }
    else {
        if (passwordHash === "") {
            $('#PasswordHashNuevo').focus();
            alert("Ingrese el password del usuario");
        }
        else {
            $.ajax({
                type: "POST",
                url: action,
                data: {
                    email, phoneNumber, passwordHash, selectRole
                },
                success: function (response) {
                    if (response === "Save") {
                        window.location.href = "Usuarios";
                    }
                    else {
                        $('#mensajenuevo').html("No se puede guardar el usuario. <br/>Seleccione un rol. <br/> Ingrese un email correcto. <br/> El password debe tener de 6-100 caracteres, al menos un caracter especial, una letra mayúscula y un número");
                    }
                }
            });
        }
    }

}
$().ready(() => {
    var URLactual = window.location;
    document.getElementById("filtrar").focus();
    switch (URLactual.pathname) {
        case "/Categorias":
            filtrarDatos(1, "nombre");
            break;
        case "/Cursos":
            getCategorias(0, 0);

            filtrarCurso(1, "nombre");
            break;
        case "/Estudiantes":
            filtrarEstudiantes(1, "nombre");
            break;
        case "/Inscripciones":
            filtrarEstudianteInscripcion();
            filtrarCursoInscripcion();
            mostrarCursos();
            break;
        case "/MisCursos":
            filtrarMisCurso(1);
            break;
    }
    
});
$('#modalCS').on('shown.bs.modal', () => {
    $('#Nombre').focus();
});
$('#modalAS').on('shown.bs.modal', () => {
    $('#Codigo').focus();
});
var idCategoria, funcion = 0, idCurso;
var idEstudiante = 0, asignacionID = 0;
/**
 CODIGO DE CATEGORIAS
 */
var agregarCategoria = () => {
    var nombre = document.getElementById("Nombre").value;
    var descripcion = document.getElementById("Descripcion").value;
    var estados = document.getElementById('Estado');
    var estado = estados.options[estados.selectedIndex].value;
    if (funcion === 0) {
        var action = 'Categorias/guardarCategoria';
    } else {
        var action = 'Categorias/editarCategoria';
    }
    var categoria = new Categorias(nombre, descripcion, estado, action);
    categoria.agregarCategoria(idCategoria, funcion);
    funcion = 0;
}
var filtrarDatos = (numPagina, order)=>{
    var valor = document.getElementById("filtrar").value;
    var action = 'Categorias/filtrarDatos';
    var categoria = new Categorias(valor, "", "", action);
    categoria.filtrarDatos(numPagina, order);
}
var editarEstado = (id,fun) => {
    idCategoria = id;
    funcion = fun;
    var action = 'Categorias/getCategorias';
    var categoria = new Categorias("", "", "", action);
    categoria.qetCategoria(id, funcion);
}
var editarCategoria = () => {
    var action = 'Categorias/editarCategoria';
    var categoria = new Categorias("", "", "", action);
    categoria.editarCategoria(idCategoria, funcion);
}
/*
 CODIGO DE CURSOS
 */
var getCategorias = (id, fun) => {
    var action = 'Cursos/getCategorias';
    var cursos = new Cursos("", "", "", "", "", "", "", action);
    cursos.getCategorias(id, fun);
};
var agregarCurso = () => {
    if (funcion === 0) {
        var action = 'Cursos/agregarCurso';
    } else {
        var action = 'Cursos/editarCurso';
    }

    var nombre = document.getElementById("Nombre").value;
    var descripcion = document.getElementById("Descripcion").value;
    var creditos = document.getElementById("Creditos").value;
    var horas = document.getElementById("Horas").value;
    var costo = document.getElementById("Costo").value;
    var estado = document.getElementById("Estado").checked
    var categorias = document.getElementById('CategoriaCursos');
    var categoria = categorias.options[categorias.selectedIndex].value;
    var cursos = new Cursos(nombre, descripcion, creditos, horas, costo, estado, categoria, action);
    cursos.agregarCurso(idCurso, funcion);
    funcion = 0;
};
var filtrarCurso = (numPagina, order) => {
    var valor = document.getElementById("filtrar").value;
    var action = 'Cursos/filtrarCurso';
    var cursos = new Cursos(valor, "", "", "", "", "", "", action);
    cursos.filtrarCurso(numPagina, order);
};
var editarEstadoCurso = (id, fun) => {
    funcion = fun;
    idCurso = id;
    var action = 'Cursos/getCursos';
    var cursos = new Cursos("", "", "", "", "", "", "", action);
    cursos.getCursos(id, fun);
};
var editarEstadoCurso1 = () => {
    var action = 'Cursos/editarCurso';
    var cursos = new Cursos("", "", "", "", "", "", "", action);
    cursos.editarEstadoCurso(idCurso, funcion);
};
var restablecer = () => {
    var cursos = new Cursos("", "", "", "", "", "", "", "");
    cursos.restablecer();
};
var getInstructorCurso = (asignacion, curso, instructor, fun) => {
    idCurso = curso;
    asignacionID = asignacion;
    var action = 'Cursos/getCursos';
    var cursos = new Cursos("", "", "", "", "", "", "", action);
    cursos.getCursos(curso, fun);
    var action = 'Cursos/getInstructors';
    cursos.getInstructors(instructor, fun, action);

};
var instructorCurso = () => {
    let action = 'Cursos/instructorCurso';
    let instructors = document.getElementById('instructorsCursos');
    let instructor = instructors.options[instructors.selectedIndex].value;
    let fecha = document.getElementById("Fecha").value;
    var cursos = new Cursos("", "", "", "", "", "", "", "");
    cursos.instructorCurso(asignacionID, idCurso, instructor, fecha, action);
    asignacionID = 0;
    idCurso = 0;
};
/**
 CODIGO DE ESTUDIANTES
 */
var estudiante = new Estudiantes();
var guardarEstudiante = () => {
    var action = 'Estudiantes/guardarEstudiante';
    var codigo = document.getElementById("Codigo").value;
    var nombre = document.getElementById("Nombre").value;
    var apellido = document.getElementById("Apellidos").value;
    var fecha = document.getElementById("FechaNacimiento").value;
    var documento = document.getElementById("Documento").value;
    var email = document.getElementById("Email").value;
    var telefono = document.getElementById("Telefono").value;
    var direccion = document.getElementById("Direccion").value;
    var estado = document.getElementById("Estado").checked;
    estudiante.guardarEstudiante(idEstudiante, funcion, action, codigo, nombre, apellido, fecha, documento, email, telefono, direccion, estado);
    idEstudiante = 0;
};
var filtrarEstudiantes = (numPagina, order) => {
    var valor = document.getElementById("filtrar").value;
    var action = 'Estudiantes/filtrarEstudiantes';
    estudiante.filtrarEstudiantes(numPagina, valor, order, action);
};
var editarEstudiante = (id, fun) => {
    idEstudiante = id;
    funcion = fun;
    var action = 'Estudiantes/getEstudiante';
    estudiante.getEstudiante(id, fun, action);
};
var deleteEstudiante = (id) => {
    idEstudiante = id;
};
var deleteEstudiantes = () => {
    var action = 'Estudiantes/deleteEstudiante';
    estudiante.deleteEstudiante(idEstudiante, action);
    idEstudiante = 0;
};
var restablecerEstudiantes = () => {
    estudiante.restablecer();
};
/**
 CODIGO DE INSCRIPCIONES
 */
var inscripciones = new Inscripciones();
var filtrarEstudianteInscripcion = () => {
    var action = 'Inscripciones/filtrarEstudiantesIns';
    var valor = document.getElementById("filtrar").value;
    inscripciones.filtrarDataInscripcion(valor, action,1);
};
var getEstudiante = () => {
    let count = 0, id;
    let chk = document.getElementsByName('cboxEstudiante[]');
    for (i = 0; i < chk.length; i++) {
        if (chk[i].checked) {
            id = chk[i].value;
            count++;
        }
    }
    if (1 < count) {
        document.getElementById("mensajeEstudiante").innerHTML = "Seleccione un estudiante";
    } else {
        var action = 'Inscripciones/getEstudiante';
        inscripciones.getData(id, action,1);
    }
};
var filtrarCursoInscripcion = () => {
    var action = 'Inscripciones/filtrarCurso';
    var valor = document.getElementById("filtrarCurso").value;
    inscripciones.filtrarDataInscripcion(valor, action, 2);
};
var getCurso = () => {
    let count = 0, id;
    let chk = document.getElementsByName('cboxCurso[]');
    for (i = 0; i < chk.length; i++) {
        if (chk[i].checked) {
            id = chk[i].value;
            count++;
        }
    }
    if (1 < count) {
        document.getElementById("mensajeCurso").innerHTML = "Seleccione un curso";
    } else {
        var action = 'Inscripciones/getCurso';
        inscripciones.getData(id, action, 2);
    }
};
var addCursos = () => {
    var estudiante = document.getElementById("Estudiante").value;
    var curso = document.getElementById("InscripcionCurso").value;
    var grado = document.getElementById("grado").value;
    var costo = document.getElementById("CostoCurso").value;
    inscripciones.addCursos(estudiante, curso, grado, costo);
};
var restablecerInscripcion = () => {
    inscripciones.deleteData();
};
var mostrarCursos = () => {
    inscripciones.mostrarCursos();
};
var deleteCurso = (id) => {
    inscripciones.deleteCurso(id);
};
var guardarCursos = () => {
    inscripciones.guardarCursos();
};
var filtrarInscripcion = (numPagina, order) => {
    var valor = document.getElementById("filtrar").value;
    var action = 'Inscripciones/filtrarInscripciones';
};
/*
 CODIGO DE MIS CURSOS
 */
var misCursos = new MisCursos();
var filtrarMisCurso = (pagina) => {
    var valor = document.getElementById("filtrar").value;
    misCursos.filtrarMisCurso(pagina, valor);
};
var getMisCurso = (curso, id) => {
    misCursos.getMisCurso(curso, id);
};
$("#Curso").typeahead({
    source: (query, result) => {
        getMisCursos(query, result);
    }
});
var getMisCursos = (query, result) => {
    misCursos.getMisCursos(query, result);
};
$("#Estudiante").typeahead({
    source: (query, result) => {
        getMisEstudiantes(query, result);
    }
});
var getMisEstudiantes = (query, result) => {
    misCursos.getMisEstudiantes(query, result);
};
$("#Docente").typeahead({
    source: (query, result) => {
        getMisDocentes(query, result);
    }
});
var getMisDocentes = (query, result) => {
    misCursos.getMisDocentes(query, result);
};
var actualizarMisCurso = () => {
    misCursos.actualizarMisCurso();
};