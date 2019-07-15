using CursoOnline.Dominio._Base;
using CursoOnline.Dominio.Entidades.Cursos;

namespace CursoOnline.Dominio.Interface
{
    public interface ICursoRepositorio : IRepositorio<Curso>
    {
        Curso ObterPeloNome(string nome);
    }
}
