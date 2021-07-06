using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Curso_Arquitetura_Backend.Models
{
    public class ValidaCampoViewModelOutPut
    {
        public IEnumerable<string> Erros { get; private set; }

        public ValidaCampoViewModelOutPut(IEnumerable<string> erros)
        {
            Erros = erros;
        }
    }
}
