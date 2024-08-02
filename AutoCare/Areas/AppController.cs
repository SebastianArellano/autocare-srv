using AutoCareDB;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace AutoCare.Areas
{
    public class AppController : ApiController
    {
        [Route("api/Usuario/Obtener")]
        [HttpGet]
        public List<Usuario> ObtenerTodosUsuarios()
        {
            DominioApp dominio = new DominioApp();
            return dominio.ObtenerUsuario();
        }
    }
}