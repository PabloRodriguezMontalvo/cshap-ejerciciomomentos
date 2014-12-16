using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Security;

namespace MvcMomentos.Seguridad
{
    public class ProveedorIdentidad:IIdentity
    {
        public string Name {
            get { return Identity.Name; }
             }
        public string AuthenticationType {
            get { return Identity.AuthenticationType; } 
        }
        public bool IsAuthenticated {
            get { return Identity.IsAuthenticated; }
        }
        public IIdentity Identity { get; set; }
    
        public int IdUsuario { get; set; }
     


        public ProveedorIdentidad(IIdentity identity)
        {
            Identity = identity;
            var usuario = 
                (UsuarioProveedorIdentidad)Membership.GetUser(identity.Name,false);

            
            IdUsuario = usuario.IdUsuario;
           
        }


    }
}