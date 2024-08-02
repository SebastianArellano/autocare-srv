using AutoCareDB;
using Comun;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class DominioApp
    {
        DbContext db;

        public DominioApp()
        {
            this.db = new AutoCareEntities();
        }

        public List<Usuario> ObtenerUsuario()
        {
            Repositorio<Usuario> repoUsuario = new Repositorio<Usuario>(db);
            return repoUsuario.Todos();
        }

        public string RegistrarUsuario(Usuario usuario)
        {
            Repositorio<Usuario> repoUsuario = new Repositorio<Usuario>(db);
            try
            {
                bool usuarioExiste = repoUsuario.Algunos(u => u.Correo == usuario.Correo).ToList().Count > 0;

                if (usuarioExiste)
                {
                    return "CORREOEXISTE";
                }

                usuario.FechaRegistro = DateTime.Now;
                repoUsuario.Crear(usuario);
                db.SaveChanges();
                return "CREADO";

            }
            catch (Exception)
            {
                return "ERROR";
            }
        }

        public RespuestaLogin Login(Usuario usuario)
        {
            Repositorio<Usuario> repoUsuario = new Repositorio<Usuario>(db, new[] { "Vehiculo" });
            try
            {
                Usuario usuarioExistente = repoUsuario.Algunos(u => u.Correo == usuario.Correo).FirstOrDefault();
                RespuestaLogin respuesta = new RespuestaLogin();

                if (usuarioExistente == null)
                {
                    respuesta.VehiculoResgistrado = false;
                    respuesta.Respuesta = "NOEXISTEUSUARIO";
                    return respuesta;
                }

                if (usuarioExistente.Contraseña == usuario.Contraseña)
                {
                    respuesta.UsuarioId = usuarioExistente.Id;
                    respuesta.VehiculoResgistrado = usuarioExistente.Vehiculo.Count > 0;
                    respuesta.Respuesta = "LOGEADOCORRECTO";
                    return respuesta;
                }
                else
                {
                    respuesta.VehiculoResgistrado = false;
                    respuesta.Respuesta = "CREDENCIALESINCORRECTAS";
                    return respuesta;
                }

            }
            catch (Exception)
            {
                RespuestaLogin respuesta = new RespuestaLogin
                {
                    VehiculoResgistrado = false,
                    Respuesta = "ERROR"
                };
                return respuesta;
            }
        }

        public bool RegistrarVehiculo(Vehiculo vehiculo) 
        {
            Repositorio<Vehiculo> repoVehiculo = new Repositorio<Vehiculo>(db);
            try
            {
                repoVehiculo.Crear(vehiculo);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw;
            }
        }

        public HomeInformation ObtenerInformacionDelHome(int usuarioId) 
        {
            Repositorio<Vehiculo> repoVehiculo = new Repositorio<Vehiculo>(db);
            Repositorio<Mantenimiento> repoMantenimiento = new Repositorio<Mantenimiento>(db);

            Vehiculo vehiculo = repoVehiculo.Algunos(v => v.UsuarioId == usuarioId).FirstOrDefault();
            DateTime fechaActual = DateTime.Now;
            Mantenimiento ultimoMantenimiento = repoMantenimiento.Algunos(m => m.VehiculoId == vehiculo.Id && m.Fecha < fechaActual).OrderBy(m => m.Fecha).FirstOrDefault();
            Mantenimiento proximoMantenimiento = repoMantenimiento.Algunos(m => m.VehiculoId == vehiculo.Id && m.Fecha >= fechaActual).OrderBy(m => m.Fecha).FirstOrDefault();

            HomeInformation information = new HomeInformation
            {
                Imagen = vehiculo.Imagen,
                Kilometraje = vehiculo.Kilometraje,
                Modelo = vehiculo.Modelo,
                Año = vehiculo.Año,
                Titulo = $"{vehiculo.Marca} {vehiculo.Modelo}",
                ProximoMantenimiento = proximoMantenimiento != null ? proximoMantenimiento.Fecha.ToString("dd/MM/yyyy") : "--/--/----",
                UltimoMantenimiento = ultimoMantenimiento != null ? ultimoMantenimiento.Fecha.ToString("dd/MM/yyyy") : "--/--/----",
                VehiculoId = vehiculo.Id
            };

            return information;

        }

        public bool RegistrarMantenimiento(Mantenimiento mantenimiento) 
        {
            Repositorio<Mantenimiento> repoMantenimiento = new Repositorio<Mantenimiento>(db);

            try
            {
                repoMantenimiento.Crear(mantenimiento);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw;
            }
        }

        public List<HistorialMantenimiento> ObtenerHistorialMantenimiento(int vehiculoId) 
        {
            Repositorio<Mantenimiento> repoMantenimiento = new Repositorio<Mantenimiento>(db);
            List<Mantenimiento> mantenimientos = repoMantenimiento.Algunos(m => m.VehiculoId == vehiculoId).ToList();

            List<HistorialMantenimiento> historial = mantenimientos.Select(m => new HistorialMantenimiento
            {
                Id = m.Id,
                Fecha = m.Fecha.ToString("dd/MM/yyyy"),
                Mantenimiento = m.TipoMantenimiento,
                Costo = $"${m.Costo}"
            }).ToList();

            return historial;
        }
    }
}
