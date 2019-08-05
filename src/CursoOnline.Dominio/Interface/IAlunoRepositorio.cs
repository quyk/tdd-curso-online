using CursoOnline.Dominio._Base;
using CursoOnline.Dominio.Entidades.Alunos;

namespace CursoOnline.Dominio.Interface
{
    public interface IAlunoRepositorio : IRepositorio<Aluno>
    {
        Aluno ObterPeloCpf(string cpf);
    }
}
