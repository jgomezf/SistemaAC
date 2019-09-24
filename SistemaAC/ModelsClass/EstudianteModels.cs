using Microsoft.AspNetCore.Identity;
using SistemaAC.Data;
using SistemaAC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaAC.ModelsClass
{
    public class EstudianteModels
    {
        private ApplicationDbContext context;
        private List<IdentityError> identityError;
        private string code = "", des = "";
        private Boolean estados;
        public EstudianteModels(ApplicationDbContext context)
        {
            this.context = context;
            identityError = new List<IdentityError>();
        }

        public List<Estudiante> getEstudiante(int id)
        {
            return context.Estudiante.Where(c => c.ID == id).ToList();
        }

        public List<IdentityError> guardarEstudiante(List<Estudiante> response, int funcion)
        {
            switch (funcion)
            {
                case 0:
                    if (response[0].Estado)
                        estados = false;
                    else
                        estados = true;
                    break;
                case 1:
                    estados = response[0].Estado;
                    break;
            }

            var estudiante = new Estudiante
            {
                ID = response[0].ID,
                Codigo = response[0].Codigo,
                Apellidos = response[0].Apellidos,
                Nombres = response[0].Nombres,
                FechadNacimeinto = response[0].FechadNacimeinto,
                Documento = response[0].Documento,
                Email = response[0].Email,
                Telefono = response[0].Telefono,
                Direccion = response[0].Direccion,
                Estado = estados
            };

            try
            {
                context.Update(estudiante);
                context.SaveChanges();
                code = "1";
                des = "Save";
            }
            catch (Exception ex)
            {
                code = "0";
                des = ex.Message;
            }

            identityError.Add(new IdentityError
            {
                Code = code,
                Description = des
            });

            return identityError;
        }

        public List<object[]> filtrarEstudiantes(int numPagina, string valor, string order)
        {
            int cant, numRegistros = 0, inicio = 0, reg_por_pagina = 1;
            int can_paginas, pagina = 0, count = 1;
            string dataFilter = "", paginador = "", Estado = null;
            List<object[]> data = new List<object[]>();
            IEnumerable<Estudiante> query;
            List<Estudiante> estudiantes = null;

            estudiantes = context.Estudiante.OrderBy(p => p.Nombres).ToList();
            numRegistros = estudiantes.Count;

            if ((numRegistros % reg_por_pagina) > 0)
            {
                numRegistros += 1;
            }
            inicio = (numPagina - 1) * reg_por_pagina;
            can_paginas = (numRegistros / reg_por_pagina);

            if (valor == "null")
            {
                query = estudiantes.Skip(inicio).Take(reg_por_pagina);
            }
            else
            {
                query = estudiantes.Where(p => p.Documento.ToLower().StartsWith(valor.ToLower()) || p.Nombres.ToLower().StartsWith(valor.ToLower()) || p.Apellidos.ToLower().StartsWith(valor.ToLower())).Skip(inicio).Take(reg_por_pagina);
            }
            cant = query.Count();
            foreach (var item in query)
            {
                if (item.Estado == true)
                {
                    Estado = "<a onclick='editarEstudiante(" + item.ID + ',' + 0 + ")' class='btn btn-success'>Activo</a>";
                }
                else
                {
                    Estado = "<a onclick='editarEstudiante(" + item.ID + ',' + 0 + ")' class='btn btn-danger'>No activo</a>";
                }

                dataFilter += "<tr>" +
                    "<td>" + item.Codigo + "</td>" +
                    "<td>" + item.Documento + "</td>" +
                    "<td>" + item.Nombres + "</td>" +
                    "<td>" + item.Apellidos + "</td>" +
                    "<td>" + item.FechadNacimeinto + "</td>" +
                    "<td>" + item.Telefono + "</td>" +
                    "<td>" + item.Email + "</td>" +
                    "<td>" + item.Direccion + "</td>" +
                    "<td>" + Estado + " </td>" +
                    "<td>" +
                    "<a data-toggle='modal' data-target='#modalAS' onclick='editarEstudiante(" + item.ID + ',' + 1 + ")'  class='btn btn-success'>Edit</a>" +
                    "</td>" +
                    "<td>" +
                    "<a data-toggle='modal' data-target='#modalDeleteAS' onclick='deleteEstudiante(" + item.ID + ")'  class='btn btn-danger'>Delete</a>" +
                    "</td>" +
                "</tr>";
            }
            if (valor == "null")
            {
                if (numPagina > 1)
                {
                    pagina = numPagina - 1;
                    paginador += " <a class='btn btn-default' onclick='filtrarEstudiantes(" + 1 + ',' + '"' + order + '"' + ")'> << </a>" +
                    "<a class='btn btn-default' onclick='filtrarEstudiantes(" + pagina + ',' + '"' + order + '"' + ")'> < </a> ";
                }

                if (1 < can_paginas)
                {
                    //paginador += "<strong class='btn btn-success'>" + numPagina + ".de." + can_paginas + "</strong>";
                    for (int i = numPagina; i <= can_paginas; i++)
                    {
                        paginador += " <strong class='btn btn-success' onclick='filtrarEstudiantes(" + i + ',' + '"' + order + '"' + ")'>" + i + "</strong> ";
                        if (count == 5)
                        {
                            break;
                        }
                        count++;
                    }
                }
                if (numPagina < can_paginas)
                {
                    pagina = numPagina + 1;
                    paginador += "<a class='btn btn-default' onclick='filtrarEstudiantes(" + pagina + ',' + '"' + order + '"' + ")'>  > </a>" +
                                 "<a class='btn btn-default' onclick='filtrarEstudiantes(" + can_paginas + ',' + '"' + order + '"' + ")'> >> </a>";
                }
            }
            object[] dataObj = { dataFilter, paginador };
            data.Add(dataObj);
            return data;
        }

        internal List<IdentityError> deleteEstudiante(int id)
        {
            try
            {
                var estudiante = context.Estudiante.SingleOrDefault(s => s.ID == id);
                if (estudiante == null)
                {
                    code = "0";
                    des = "Not";
                }
                else
                {
                    context.Estudiante.Remove(estudiante);
                    context.SaveChanges();
                    code = "1";
                    des = "delete";
                }
            }
            catch (Exception ex)
            {
                code = "0";
                des = ex.Message;
            }

            identityError.Add(new IdentityError
            {
                Code = code,
                Description = des
            });

            return identityError;
        }
    }
}
