using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Context;

namespace WebApplication1.Service
{
    public class UsersManagerService : IUsersManagerService
    {
        private abTestdbContext _dbContext;

        public async Task <double> GetMetrics()
        {
            List<User> users = new List<User>();
            using (_dbContext)
            {
                users =await _dbContext.Users.ToListAsync();
            }
            var metricsResult = await Task.Run(()=> CalculateMatrics(users));
            return metricsResult;
        }
        private double CalculateMatrics(List<User> users)
        {
            int days = 7;
            int returnLater = 0;
            int registrateUsers = 0;
            bool isRegistrate = false;
            bool isReturnLater = false;
            foreach (var user in users)
            {
                isRegistrate = ((DateTime.Now - user.DateRegistrate).TotalDays) >= days;
                if (isRegistrate)
                {
                    registrateUsers++;
                    isReturnLater = ((user.DateLastActive - user.DateRegistrate).TotalDays) >= days;
                    if (isReturnLater)
                    {
                        returnLater++;
                    }
                }
            }
            int multiplier = 100;
            if (registrateUsers != 0)
            {
                double result = ((double)returnLater / (double)registrateUsers) * multiplier;
                result = Math.Round(result, 2);
                return result;
            }
            else
            {
                return 0;
            }
        }
        public async Task<IEnumerable<User>> GetUsers()
        {
            List<User> users = new List<User>();
            try
            {
                using (_dbContext)
                {
                    users = await _dbContext.Users.ToListAsync();
                }
                return users;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<IEnumerable<User>> SaveEdits(IEnumerable<User> users)
        {
            try
            {
                using (_dbContext)
                {
                    _dbContext.Users.UpdateRange(users);
                    await _dbContext.SaveChangesAsync();
                }
                return users;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<Dictionary<string, int>> GetLifeCycle()
        {
            var lifeCycle = new Dictionary<string, int>();
            using (_dbContext)
            {
                var users = await _dbContext.Users.ToListAsync();
                string dictinoraryKey = "";
                foreach (var user in users)
                {
                    int daysQuantity = (int)(user.DateLastActive - user.DateRegistrate).TotalDays;
                    dictinoraryKey = $"day(s) {daysQuantity}";
                    if (lifeCycle.ContainsKey(dictinoraryKey))
                    {
                        lifeCycle[dictinoraryKey]++;
                    }
                    else
                    {
                        lifeCycle.Add(dictinoraryKey, 1);
                    }
                }
            }
            return lifeCycle;
        }
        public UsersManagerService(abTestdbContext dbContext)
        {
            this._dbContext = dbContext;
        }
    }
}