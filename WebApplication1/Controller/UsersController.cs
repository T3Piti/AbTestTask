using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication1.Context;
using WebApplication1.Service;

namespace WebApplication1.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private abTestdbContext _abTestdbContext;
        private IUsersManagerService _usersManager;

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(await Task.Run(() => _usersManager.GetUsers(_abTestdbContext)));
        }

        [Route("SaveEdits")]
        [HttpPost]
        public async Task<IActionResult> SaveUsers(IEnumerable<User> users)
        {
            if (ModelState.IsValid)
            {
                return Ok(await Task.Run(() => _usersManager.SaveEdits(_abTestdbContext, users)));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [Route("GetMetrics")]
        [HttpGet]
        public async Task<IActionResult> GetMetrics()
        {
            return Ok(await Task.Run(() => _usersManager.GetMetrics(_abTestdbContext)));
        }

        [Route("GetLifeCycle")]
        [HttpGet]
        public async Task<IActionResult> GetLifeCycle()
        {
            return Ok(await Task.Run(() => _usersManager.GetLifeCycle(_abTestdbContext)));
        }

        public UsersController(IUsersManagerService usersManager, abTestdbContext dbContext)
        {
            this._usersManager = usersManager;
            this._abTestdbContext = dbContext;
        }
    }
}
