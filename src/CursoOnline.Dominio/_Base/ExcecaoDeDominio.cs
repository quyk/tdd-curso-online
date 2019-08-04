using System;
using System.Collections.Generic;

namespace CursoOnline.Dominio._Base
{
    public class ExcecaoDeDominio : ArgumentException
    {
        public IList<string> MensagensDeErro { get; set; }


        public ExcecaoDeDominio(IList<string> mensagensDeErro)
        {
            MensagensDeErro = mensagensDeErro;
        }
    }
}