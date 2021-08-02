using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication1.Context;

namespace WebApplication1.Service
{
    public interface IUsersManagerService
    {
        Task<IEnumerable<User>> GetUsers();
        Task<IEnumerable<User>> SaveEdits(IEnumerable<User> users);
        Task<double> GetMetrics();
        Task<Dictionary<string, int>> GetLifeCycle();

    }
}
