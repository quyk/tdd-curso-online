using System.Collections.Generic;
using System.Linq;

namespace CursoOnline.Dominio._Base
{
    public class ValidadorDeRegra
    {
        private readonly IList<string> _mensagemDeErro;

        private ValidadorDeRegra()
        {
            _mensagemDeErro = new List<string>();
        }

        public static ValidadorDeRegra Novo()
        {
            return new ValidadorDeRegra();
        }

        public ValidadorDeRegra Quando(bool temErro, string mensagemDeErro)
        {
            if(temErro)
            {
                _mensagemDeErro.Add(mensagemDeErro);
            }

            return this;
        }

        public void DispararExcessaoSeExistir()
        {
            if (_mensagemDeErro.Any())
            {
                throw new ExcecaoDeDominio(_mensagemDeErro); 
            }
        }
    }
}
