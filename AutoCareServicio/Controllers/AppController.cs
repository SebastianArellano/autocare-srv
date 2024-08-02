using AutoCareDB;
using Comun;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace AutoCareServicio.Controllers
{

    public class AppController : ApiController
    {
        [Route("api/Usuario/ObtenerTodos")]
        [HttpGet]
        public List<Usuario> ObtenerTodosUsuario() 
        {
            DominioApp dominio = new DominioApp();
            return dominio.ObtenerUsuario();
        }

        [Route("api/Usuario/Registrar")]
        [HttpPost]
        public string RegistrarUsuario(Usuario usuario)
        {
            DominioApp dominio = new DominioApp();
            return dominio.RegistrarUsuario(usuario);
        }

        [Route("api/Usuario/Login")]
        [HttpPost]
        public RespuestaLogin Login(Usuario usuario)
        {
            DominioApp dominio = new DominioApp();
            return dominio.Login(usuario);
        }

        [Route("api/Vehiculo/Registrar")]
        [HttpPost]
        public bool Registrar(Vehiculo vehiculo)
        {
            DominioApp dominio = new DominioApp();
            return dominio.RegistrarVehiculo(vehiculo);
        }

        [Route("api/Home/ObtenerInformacion")]
        [HttpGet]
        public HomeInformation ObtenerInformacion(int usuarioId)
        {
            DominioApp dominio = new DominioApp();
            return dominio.ObtenerInformacionDelHome(usuarioId);
        }

        [Route("api/Mantenimiento/Registrar")]
        [HttpPost]
        public bool RegistrarMantenimiento(Mantenimiento mantenimiento)
        {
            DominioApp dominio = new DominioApp();
            return dominio.RegistrarMantenimiento(mantenimiento);
        }

        [Route("api/Mantenimiento/Historial")]
        [HttpGet]
        public List<HistorialMantenimiento> ObtenerHistorialMantenimiento(int vehiculoId)
        {
            DominioApp dominio = new DominioApp();
            return dominio.ObtenerHistorialMantenimiento(vehiculoId);
        }
    }

    
}
