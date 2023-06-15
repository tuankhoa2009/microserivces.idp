using Microservices.IDP.Infrastructure.Repositories;
using Microservices.IDP.Infrastructure.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Microservices.IDP.Presentation.Controllers
{

    [ApiController]
    [Route("/api/[controller]/roles/{roleId}")]
    [Authorize("Bearer")]
    public class PermissonController : ControllerBase
    {

        private readonly IRepositoryManager _repository;

        public PermissonController(IRepositoryManager repository)
        {
            _repository = repository;
        }


        [HttpGet]
        public async Task<IActionResult> GetPermissions(string roleId)
        {
            var result = await _repository.Permission.GetPermissionByRole(roleId);


            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(PermissionViewModel),(int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreatePermission(string roleId, [FromBody] PermissionAddModel model)
        {
            var result = await _repository.Permission.CreatePermission(roleId, model);

            return result != null ? Ok(result) : NoContent();

        }


        [HttpDelete("function/{function}/command/{command}")]
        [ProducesResponseType(typeof(PermissionViewModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeletePermission(string roleId, [Required] string function, [Required] string command)
        {
            await _repository.Permission.DeletePermission(roleId, function, command);
            return NoContent();
        }


        [HttpPost("update-permissions")]
        [ProducesResponseType(typeof(NoContentResult), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdatePermissions(string roleId, [FromBody] IEnumerable<PermissionAddModel> permissions)
        {
            await _repository.Permission.UpdatePermissionsByRoleId(roleId, permissions);
            return NoContent();
        }

    }
}
