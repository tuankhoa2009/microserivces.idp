using Microservices.IDP.Persistence;

namespace Microservices.IDP.Infrastructure.Domains
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IdentityContext _context;


        public UnitOfWork(IdentityContext context)
        {
            _context = context;
        }

        public Task<int> CommitAsync()
        {
           return _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
