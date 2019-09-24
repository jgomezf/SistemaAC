using Microsoft.AspNetCore.Identity;
using SistemaAC.Data;
using SistemaAC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaAC.ModelsClass
{
    public class ListObject
    {
        public List<Object[]> data = new List<object[]>();
        public ApplicationDbContext context;
        public List<Inscripcion> dataInscripcion = new List<Inscripcion>();
        public List<Curso> cursos = new List<Curso>();
        public List<Curso> misCursos = new List<Curso>();
        public List<IdentityError> errorList = new List<IdentityError>();

    }
}
