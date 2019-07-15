using CursoOnline.Dominio.Entidades.Cursos;

namespace CursoOnline.Dominio.Interface
{
    public interface ICursoRepositorio
    {
        void Adicionar(Curso curso);
        void Atualizar(Curso curso);
        Curso ObterPeloNome(string nome);
    }
}