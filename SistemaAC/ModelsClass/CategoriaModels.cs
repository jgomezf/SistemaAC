using Microsoft.AspNetCore.Identity;
using SistemaAC.Data;
using SistemaAC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaAC.ModelsClass
{
    public class CategoriaModels
    {
        private ApplicationDbContext context;
        private Boolean estados;
        public CategoriaModels(ApplicationDbContext context)
        {
            this.context = context;
            //filtrarDatos(1, "Android");
        }
        public List<IdentityError> guardarCategoria(string nombre, string descripcion, string estado)
        {
            var errorList = new List<IdentityError>();
            var categoria = new Categoria
            {
                Nombre = nombre,
                Descripcion = descripcion,
                Estado = Convert.ToBoolean(estado)
            };
            context.Add(categoria);
            context.SaveChanges();
            errorList.Add(new IdentityError
            {
                Code = "Save",
                Description = "Save"
            });
            return errorList;
        }
        public List<object[]> filtrarDatos(int numPagina, string valor, string order)
        {
            int count = 0, cant, numRegistros = 0, inicio = 0, reg_por_pagina = 1;
            int can_paginas, pagina;
            string dataFilter = "", paginador = "", Estado = null;
            List<object[]> data = new List<object[]>();
            IEnumerable<Categoria> query;
            List<Categoria> categorias = null;
            switch (order)
            {
                case "nombre":
                    categorias = context.Categoria.OrderBy(c => c.Nombre).ToList();
                    break;
                case "des":
                    categorias = context.Categoria.OrderBy(c => c.Descripcion).ToList();
                    break;
                case "estado":
                    categorias = context.Categoria.OrderBy(c => c.Estado).ToList();
                    break;
            }
            
            numRegistros = categorias.Count;
            if ((numRegistros % reg_por_pagina) > 0)
            {
                numRegistros += 1;
            }
            inicio = (numPagina - 1) * reg_por_pagina;
            can_paginas = (numRegistros / reg_por_pagina);
           
            if (valor == "null")
            {
                query = categorias.Skip(inicio).Take(reg_por_pagina);
            }
            else
            {
                query = categorias.Where(c => c.Nombre.ToLower().StartsWith(valor.ToLower()) || c.Descripcion.ToLower().StartsWith(valor.ToLower())).Skip(inicio).Take(reg_por_pagina);
            }
            cant = query.Count();
            foreach (var item in query)
            {
                if (item.Estado == true)
                {
                    Estado = "<a data-toggle='modal' data-target='#ModaEstado' onclick='editarEstado(" + item.CategoriaID + ',' + 0 + ")' class='btn btn-success'>Activo</a>";
                }
                else
                {
                    Estado = "<a data-toggle='modal' data-target='#ModaEstado' onclick='editarEstado(" + item.CategoriaID + ',' + 0 + ")' class='btn btn-danger'>No activo</a>";
                }
                dataFilter += "<tr>" +
                      "<td>" + item.Nombre + "</td>" +
                      "<td>" + item.Descripcion + "</td>" +
                      "<td>" + Estado + " </td>" +
                      "<td>" +
                      "<a data-toggle='modal' data-target='#modalAC'  onclick='editarEstado(" + item.CategoriaID + ',' + 1 + ")'class='btn btn-success'>Edit</a>" +
                      "</td>" +
                  "</tr>";
            }
            if (valor == "null")
            {
                if (numPagina > 1)
                {
                    pagina = numPagina - 1;
                    paginador += "<a class='btn btn-default' onclick='filtrarDatos(" + 1 + ',' + '"' + order + '"' + ")'> << </a>" +
                    "<a class='btn btn-default' onclick='filtrarDatos(" + pagina + ',' + '"' + order + '"' + ")'> < </a>";
                }
                if (1 < can_paginas)
                {
                    paginador += "<strong class='btn btn-success'>" + numPagina + ".de." + can_paginas + "</strong>";
                }
                if (numPagina < can_paginas)
                {
                    pagina = numPagina + 1;
                    paginador += "<a class='btn btn-default' onclick='filtrarDatos(" + pagina + ',' + '"' + order + '"' + ")'>  > </a>" +
                                 "<a class='btn btn-default' onclick='filtrarDatos(" + can_paginas + ',' + '"' + order + '"' + ")'> >> </a>";
                }
            }
            object[] dataObj = { dataFilter, paginador };
            data.Add(dataObj);
            return data;
        }
        public List<Categoria> getCategorias(int id)
        {
            return context.Categoria.Where(c => c.CategoriaID == id).ToList();
        }
        public List<IdentityError> editarCategoria(int idCategoria, string nombre, string descripcion, Boolean estado, int funcion)
        {
            var errorList = new List<IdentityError>();
            string code = "", des = "";
            switch (funcion)
            {
                case 0:
                    if (estado)
                    {
                        estados = false;
                    }
                    else
                    {
                        estados = true;
                    }
                   
                    break;
                case 1:
                    estados = estado;
                    break;

                
            }
            var categoria = new Categoria()
            {
                CategoriaID = idCategoria,
                Nombre = nombre,
                Descripcion = descripcion,
                Estado = estados
            };
            try
            {
                context.Update(categoria);
                context.SaveChanges();
                code = "Save";
                des = "Save";
            }
            catch (Exception ex)
            {

                code = "error";
                des = ex.Message;
            }

            errorList.Add(new IdentityError
            {
                Code = code,
                Description = des
            });

            return errorList;
        }
    }
}
