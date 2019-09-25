using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using SistemaAC.Data;
using SistemaAC.Models;

namespace SistemaAC.ModelsClass
{
    public class MisCursosModels : ListObject
    {
        private string dataFilter = string.Empty, paginador = string.Empty, curso = string.Empty;
        private int count = 0, cant, numRegistros=0, inicio = 0, reg_por_pagina = 2;
        private int can_paginas, pagina;
        public string code = string.Empty, desc = string.Empty;
        private List<IdentityError> errorList = new List<IdentityError>();

        public MisCursosModels(ApplicationDbContext context)
        {
            this.context = context;
        }
        public List<object[]> filtrarMisCurso(int numPagina, string valor)
        {
            count = 0;
            var inscripcion = context.Inscripcion.OrderBy(c => c.Fecha).ToList();
            numRegistros = inscripcion.Count;
            if ((numRegistros % reg_por_pagina) > 0)
            {
                numRegistros += 1;
            }
            inicio = (numPagina - 1) * reg_por_pagina;
            can_paginas = (numRegistros / reg_por_pagina);
            if (valor == "null")
            {
                dataInscripcion = inscripcion.Skip(inicio).Take(reg_por_pagina).ToList();
            }
            else
            {
                cursos = getCursos(valor);
                cursos.ForEach(item => {
                    var data = inscripcion.Where(c => c.CursoID == item.CursoID).Skip(inicio).Take(reg_por_pagina).ToList();
                    if (0 < data.Count)
                    {
                        var inscripciones = new Inscripcion
                        {
                            Grado = data[0].Grado,
                            CursoID = data[0].CursoID,
                            EstudienateID = data[0].EstudienateID,
                            Fecha = data[0].Fecha,
                            Pago = data[0].Pago
                        };
                        dataInscripcion.Add(inscripciones);
                    }
                });
            }
            foreach (var item in dataInscripcion)
            {
                if (0 < cursos.Count)
                {
                    curso = cursos[count].Nombre;
                }
                else
                {
                    curso = getCurso(item.CursoID);
                }
                object[] dataCurso = {
                    curso,
                    getEstudiante(item.EstudienateID),
                    getInstructor(getAsignacion(item.CursoID)),
                    item.Grado,
                    item.Pago,
                    item.Fecha
                };
                dataFilter += "<tr>" +
                   "<td>" + curso + "</td>" +
                   "<td>" + getEstudiante(item.EstudienateID) + "</td>" +
                   "<td>" + getInstructor(getAsignacion(item.CursoID)) + "</td>" +
                   "<td>" + item.Grado + "</td>" +
                   "<td>" + '$' + item.Pago + "</td>" +
                   "<td>" + item.Fecha + " </td>" +
                   "<td>" +
                   "<a data-toggle='modal' data-target='#modalMisCS' onclick='getMisCurso(" + JsonConvert.SerializeObject(dataCurso) + ',' + item.InscripcionID + ")'  class='btn btn-success'>Edit</a>" +
                   "</td>" +

               "</tr>";
                count++;
            }
            object[] dataObj = { dataFilter, paginador };
            data.Add(dataObj);
            return data;
        }

        internal List<IdentityError> actualizarMisCurso(DataCurso model)
        {
            var curso = context.Curso.Where(c => c.Nombre.ToLower().Equals(model.Curso.ToLower())).ToList();
            //var estudiantes = model.Estudiante.Split();
            var estudiante = context.Estudiante.Where(c => (c.Nombres.ToLower() + " " + c.Apellidos.ToLower()).Equals(model.Estudiante.ToLower())).ToList();
            var inscripcion = new Inscripcion
            {
                InscripcionID = model.InscripcionID,
                Grado = model.Grado,
                CursoID = curso[0].CursoID,
                EstudienateID = estudiante[0].ID,
                Fecha = model.Fecha,
                Pago = model.Pago
            };

            try
            {
                context.Update(inscripcion);
                context.SaveChanges();
                code = "Save";
                desc = "Save";
            }
            catch (Exception ex)
            {
                code = "error";
                desc = ex.Message;
            }

            errorList.Add(new IdentityError
            {
                Code = code,
                Description = desc
            });

            return errorList;
        }

        internal List<Instructor> getMisDocentes(string query)
        {
            return context.Instructor.Where(c => c.Nombres.ToLower().StartsWith(query.ToLower()) || c.Apellidos.ToLower().StartsWith(query.ToLower())).ToList();
        }

        internal List<Estudiante> getMisEstudiantes(string query)
        {
            return context.Estudiante.Where(c => c.Nombres.ToLower().StartsWith(query.ToLower()) || c.Apellidos.ToLower().StartsWith(query.ToLower())).ToList();
        }

        internal List<Curso> getMisCursos(string query)
        {
            cursos = getCursos(query);
            cursos.ForEach(item =>
            {
                if (getAsignacion2(item.CursoID))
                {
                    misCursos.Add(new Curso
                    {
                        CursoID = item.CursoID,
                        Nombre = item.Nombre
                    });
                }
            });
            return misCursos;
        }

        private bool getAsignacion2(int cursoID)
        {
            var asignacion = context.Asignacion.Where(c => c.CursoID == cursoID).ToList();
            if (0 < asignacion.Count)
            {
                return true;
            }else
                return false;
        }

        public List<Curso> getCursos(string curso)
        {
            return context.Curso.Where(c => c.Nombre.ToLower().StartsWith(curso.ToLower())).ToList();
        }

        public string getCurso(int cursoID)
        {
            var curso = context.Curso.Where(c => c.CursoID == cursoID).ToList();
            return curso[0].Nombre;
        }

        private string getEstudiante(int estudianteId)
        {
            var estudiante = context.Estudiante.Where(c => c.ID == estudianteId).ToList();
            return estudiante[0].Nombres + " " + estudiante[0].Apellidos;
        }
        private int getAsignacion(int id)
        {
            var asignacion = context.Asignacion.Where(c => c.CursoID == id).ToList();
            return asignacion[0].InstructorID;
        }
        private string getInstructor(int id)
        {
            var instructor = context.Instructor.Where(c => c.ID == id).ToList();
            return instructor[0].Nombres + " " + instructor[0].Apellidos;
        }
    }
}
