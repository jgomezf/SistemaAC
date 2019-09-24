using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            return estudiante[0].Nombres;
        }
        private int getAsignacion(int id)
        {
            var asignacion = context.Asignacion.Where(c => c.CursoID == id).ToList();
            return asignacion[0].InstructorID;
        }
        private string getInstructor(int id)
        {
            var instructor = context.Instructor.Where(c => c.ID == id).ToList();
            return instructor[0].Nombres;
        }
    }
}
