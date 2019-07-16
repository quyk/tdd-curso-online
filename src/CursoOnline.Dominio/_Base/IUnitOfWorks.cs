using System.Threading.Tasks;

namespace CursoOnline.Dominio._Base
{
    public interface IUnitOfWorks
    {
        Task Commit();
    }
}
