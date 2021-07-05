using System.Collections.Generic;
using WebApplication1.Context;

namespace WebApplication1.Service
{
    public interface IUsersManagerService
    {
        IEnumerable<User> GetUsers(abTestdbContext db);
        IEnumerable<User> SaveEdits(abTestdbContext db, IEnumerable<User> users);
        double GetMetrics(abTestdbContext db);
        Dictionary<string, int> GetLifeCycle(abTestdbContext db);

    }
}
