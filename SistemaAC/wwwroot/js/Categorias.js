var localStorage = window.localStorage;
class Categorias {
    constructor(nombre, descripcion, estado, action) {
        this.nombre = nombre;
        this.descripcion = descripcion;
        this.estado = estado;
        this.action = action
    }
    agregarCategoria(id ,funcion) {
        if (this.nombre == "") {
            document.getElementById("Nombre").focus();
        } else {
            if (this.descripcion == "") {
                document.getElementById("Descripcion").focus();
            } else {
                if (this.estado == "0") {
                    document.getElementById("mensaje").innerHTML = "Seleccione un estado";
                } else {
                   
                    var nombre = this.nombre;
                    var descripcion = this.descripcion;
                    var estado = this.estado;
                    var action = this.action;
                    var mensaje = '';
                    $.ajax({
                        type: "POST",
                        url: action,
                        data: {
                           id, nombre, descripcion, estado,funcion
                        },
                        success: (response) => {
                            $.each(response, (index, val) => {
                                mensaje = val.code;

                            });
                            if (mensaje === "Save") {
                                this.restablecer();
                            } else {
                                document.getElementById("mensaje").innerHTML = "No se puede guardar la categoria";
                            }
                            //console.log(response);
                        }
                    });
                }
            }
        }
    }
    filtrarDatos(numPagina, order) {
        var valor = this.nombre;
        var action = this.action;
        if (valor == "") {
            valor = "null";
        }
        $.ajax({
            type: "POST",
            url: action,
            data: { valor, numPagina, order},
            success: (response) =>{
                console.log(response);
                $.each(response,(index, val)=> {

                    $("#resultSearch").html(response[0][0]);
                    $("#paginado").html(response[0][1]);
                });

            }
        });
    }
    qetCategoria(id, funcion) {
        var action = this.action;
        $.ajax({
            type: "POST",
            url: action,
            data: { id },
            success: (response) => {
                console.log(response);
                if (funcion == 0) {
                    if (response[0].estado) {
                        document.getElementById("titleCategoria").innerHTML = "Esta seguro de desactivar la categoría " + response[0].nombre;
                    } else {
                        document.getElementById("titleCategoria").innerHTML = "Esta seguro de habilitar la categoría " + response[0].nombre;
                    }
                } else {
                    document.getElementById("Nombre").value = response[0].nombre;
                    document.getElementById("Descripcion").value = response[0].descripcion;
                    if (response[0].estado) {
                        document.getElementById("Estado").selectedIndex = 1;
                    } else {
                        document.getElementById("Estado").selectedIndex = 2;
                    }
                }
                
                localStorage.setItem("categoria", JSON.stringify(response));
            }
        });
    }
    editarCategoria(id, funcion) {
        var action = this.action;
        var response = JSON.parse(localStorage.getItem("categoria"));
        var nombre = response[0].nombre;
        var descripcion = response[0].descripcion;
        var estado = response[0].estado;
        localStorage.removeItem("categoria");
        $.ajax({
            type: "POST",
            url: action,
            data: { id, nombre, descripcion, estado, funcion },
            success: (response) => {
                console.log(response);
                this.restablecer();
            }
        });

    }
  
    restablecer() {
        document.getElementById("Nombre").value = "";
        document.getElementById("Descripcion").value = "";
        document.getElementById("mensaje").innerHTML = "";
        document.getElementById("Estado").selectedIndex = 0;
        $('#modalAC').modal('hide');
        $('#ModaEstado').modal('hide');
        filtrarDatos(1, "nombre");
    }

}
