using CursoOnline.Dominio._Base;
using System.Threading.Tasks;

namespace CursoOnline.Dados.Contextos
{
    public class UnitOfWorks : IUnitOfWorks
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWorks(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }
    }
}
