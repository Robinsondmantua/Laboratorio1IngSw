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
        [Required(ErrorMessage = "* Nombre de usuario obligatorio")]
        [Display(Name = "Nombre usuario :")]
        [StringLength(10,ErrorMessage = "* Nombre de usuario incorrecto")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "* Contraseña obligatoria")]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña :")]
        public string Password { get; set; }
    }
}