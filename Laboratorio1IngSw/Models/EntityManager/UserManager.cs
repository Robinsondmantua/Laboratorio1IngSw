using Laboratorio1IngSw.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Laboratorio1IngSw.Models.EntityManager
{
    public class UserManager
    {
        public byte[] ObtenerContraseñaUsuario(string nombreUsuario)
        {
            using (TestPlataformaEntities db = new TestPlataformaEntities())
            {
                var usuario = db.Usuarios.Where(o => o.Nombre.ToLower().Equals(nombreUsuario));
                if (usuario.Any())
                {
                    return usuario.FirstOrDefault().Password;  
                } 
                else
                {
                    return null;
                }
            }
        }
    }
}