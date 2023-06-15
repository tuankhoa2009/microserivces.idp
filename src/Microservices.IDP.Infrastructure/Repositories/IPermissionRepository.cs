using Microservices.IDP.Infrastructure.Domains;
using Microservices.IDP.Infrastructure.Entities;
using Microservices.IDP.Infrastructure.ViewModels;

namespace Microservices.IDP.Infrastructure.Repositories
{
    public interface IPermissionRepository : IRepositoryBase<Permission,long>
    {
        Task<IEnumerable<PermissionViewModel>> GetPermissionByRole(string roleId);


        Task<PermissionViewModel> CreatePermission(string roleId, PermissionAddModel model);

        Task DeletePermission(string roleId, string function, string command);

        Task UpdatePermissionsByRoleId(string roleId, IEnumerable<PermissionAddModel> permissionCollection);

        Task<IEnumerable<PermissionUserViewModel>> GetPermissionByUser(User user);



    }
}
