using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Context.ValidationsAttributes
{
    public class UserValidationAttribute : ValidationAttribute
    {

        public override bool IsValid(object value)
        {
            User user = value as User;
            if (user.DateRegistrate > user.DateLastActive)
            {
                this.ErrorMessage = "Registration date must be <= than the Last Activity Date";
                return false;
            }

            if (user.DateRegistrate > DateTime.Now)
            {
                this.ErrorMessage = "Registration date must be <= than Current Date";
                return false;
            }

            if (user.DateLastActive > DateTime.Now)
            {
                this.ErrorMessage = "Last Activity Date must be <= than Current Date";
                return false;
            }

            return true;
        }
    }
}
