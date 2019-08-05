using CursoOnline.Dominio.Enums;

namespace CursoOnline.Dominio.Interface
{
    public interface IConversorDePublicoAlvo
    {
        PublicoAlvo Converter(string publicoAlvo);
    }
}
