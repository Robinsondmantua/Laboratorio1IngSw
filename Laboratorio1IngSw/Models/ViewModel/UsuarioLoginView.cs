using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Laboratorio1IngSw.Models.ViewModel
{
    public class UsuarioLoginView
    {
        [Key]
        public int IDUsuario { get; set; }
        [Required(ErrorMessage = "*")]
        [Display(Name = "Nombre usuario :")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "*")]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña :")]
        public string Password { get; set; }
    }
}