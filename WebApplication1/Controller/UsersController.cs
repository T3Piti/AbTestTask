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
        private IUsersManagerService _usersManager;

        //Get users List
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(await _usersManager.GetUsers());
        }

        //Save users changes
        [Route("SaveEdits")]
        [HttpPost]
        public async Task<IActionResult> SaveUsers(IEnumerable<User> users)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _usersManager.SaveEdits(users));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        //get RollingRetation
        [Route("GetMetrics")]
        [HttpGet]
        public async Task<IActionResult> GetMetrics()
        {
            return Ok(await _usersManager.GetMetrics());
        }

        //Get users lifecycle
        [Route("GetLifeCycle")]
        [HttpGet]
        public async Task<IActionResult> GetLifeCycle()
        {
            return Ok(await _usersManager.GetLifeCycle());
        }

        public UsersController(IUsersManagerService usersManager, abTestdbContext dbContext)
        {
            this._usersManager = usersManager;
        }
    }
}
