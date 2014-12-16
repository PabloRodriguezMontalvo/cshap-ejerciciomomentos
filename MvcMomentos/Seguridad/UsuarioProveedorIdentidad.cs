using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using MvcMomentos.Models;


namespace MvcMomentos.Seguridad
{
    public class UsuarioProveedorIdentidad:MembershipUser
    {
      
        public int IdUsuario { get; set; }
        
        public UsuarioProveedorIdentidad(Usuario usuario)
        {
           
            IdUsuario = usuario.id;
           
        }
    }
}