using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication1.Context;

namespace WebApplication1.Service
{
    public class UsersManagerService : IUsersManagerService
    {
        public double GetMetrics(abTestdbContext db)
        {
            List<User> users = new List<User>();
            using (db)
            {
                users = db.Users.ToList();
            }
            return CalculateMatrics(users);
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
                return result;
            }
            else
            {
                return 0;
            }
        }

        public IEnumerable<User> GetUsers(abTestdbContext db)
        {
            List<User> users = new List<User>();
            try
            {
                using (db)
                {
                    users = db.Users.ToList();
                }
                return users;
            }
            catch
            {
                throw new NotImplementedException();
            }
        }

        public IEnumerable<User> SaveEdits(abTestdbContext db, IEnumerable<User> users)
        {
            try
            {
                using (db)
                {
                    db.Users.UpdateRange(users);
                    db.SaveChanges();
                }
                return users;
            }
            catch
            {
                throw new NotImplementedException();
            }
        }

        public Dictionary<string, int> GetLifeCycle(abTestdbContext db)
        {
            var lifeCycle = new Dictionary<string, int>();
            using (db)
            {
                var users = db.Users.ToList();
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
    }
}