using System;
using System.ComponentModel.DataAnnotations;
using WebApplication1.Context.ValidationsAttributes;

#nullable disable

namespace WebApplication1.Context
{
    [UserValidation]
    public partial class User
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "Не указана дата регистрации")]
        public DateTime DateRegistrate { get; set; }


        [Required(ErrorMessage = "Не указана дата последней активности")]
        public DateTime DateLastActive { get; set; }
    }
}
