﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AutoCareDB
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class AutoCareEntities : DbContext
    {
        public AutoCareEntities()
            : base("name=AutoCareEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Usuario> UsuarioSet { get; set; }
        public virtual DbSet<Vehiculo> VehiculoSet { get; set; }
        public virtual DbSet<Mantenimiento> MantenimientoSet { get; set; }
        public virtual DbSet<Alerta> AlertaSet { get; set; }
    }
}
