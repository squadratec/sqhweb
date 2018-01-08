using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SQH.Entities.Models
{
    public class Login
    {
        [Required(ErrorMessage = "* Campo Obrigatório")]
        public String usuario { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "* Campo Obrigatório")]
        public String senha { get; set; }

        public bool lembrar { get; set; }
    }
}
