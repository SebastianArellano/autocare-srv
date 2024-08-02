using AutoCareDB;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Servicio.Controllers
{
    public class AppController : ApiController
    {
        [Route("api/Usuario/ObtenerTodos")]
        [HttpGet]
        public List<Usuario> ObtenerTodos()
        {
            DominioApp dominio = new DominioApp();
            return dominio.ObtenerUsuario();
        }
    }
}
