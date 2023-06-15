using AutoMapper;
using Microservices.IDP.Infrastructure.Domains;
using Microservices.IDP.Infrastructure.Entities;
using Microservices.IDP.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage;

namespace Microservices.IDP.Infrastructure.Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IdentityContext _dbContext;
        private readonly Lazy<IPermissionRepository> _permissionRepository;
        private readonly IMapper _mapper;


        public RepositoryManager(IdentityContext dbContext, IUnitOfWork unitOfWork, UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _dbContext = dbContext;
            _unitOfWork = unitOfWork;
            UserManager = userManager;
            RoleManager = roleManager;
            _mapper = mapper;
            _permissionRepository = new Lazy<IPermissionRepository>(() => new PermissionRepository(_dbContext, _unitOfWork, UserManager, _mapper));
        }
        public IPermissionRepository Permission => _permissionRepository.Value;

        public UserManager<User> UserManager { get; }

        public RoleManager<IdentityRole> RoleManager { get; }

        public Task<int> SaveAsync()
            => _unitOfWork.CommitAsync();

        public Task<IDbContextTransaction> BeginTransactionAsync()
            => _dbContext.Database.BeginTransactionAsync();

        public Task EndTransactionAsync()
            => _dbContext.Database.CommitTransactionAsync();

        public void RollbackTransaction()
            => _dbContext.Database.RollbackTransactionAsync();
    }
}
