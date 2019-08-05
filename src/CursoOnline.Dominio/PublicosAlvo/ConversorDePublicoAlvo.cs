using CursoOnline.Dominio._Base;
using CursoOnline.Dominio.Enums;
using CursoOnline.Dominio.Interface;
using System;

namespace CursoOnline.Dominio.PublicosAlvo
{
    public class ConversorDePublicoAlvo : IConversorDePublicoAlvo
    {
        public PublicoAlvo Converter(string publicoAlvo)
        {
            ValidadorDeRegra.Novo()
                .Quando(!Enum.TryParse<PublicoAlvo>(publicoAlvo, out var publicoAlvoConvertido), Resource.PublicoAlvoInvalido)
                .DispararExcessaoSeExistir();

            return publicoAlvoConvertido;
        }
    }
}
