
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 07/31/2024 23:46:32
-- Generated from EDMX file: C:\Users\Sebas\source\repos\AutoCare\AutoCareDB\AutoCareDB.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [AutoCare];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_UsuarioVehiculo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[VehiculoSet] DROP CONSTRAINT [FK_UsuarioVehiculo];
GO
IF OBJECT_ID(N'[dbo].[FK_VehiculoMantenimiento]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MantenimientoSet] DROP CONSTRAINT [FK_VehiculoMantenimiento];
GO
IF OBJECT_ID(N'[dbo].[FK_VehiculoAlerta]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AlertaSet] DROP CONSTRAINT [FK_VehiculoAlerta];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[UsuarioSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UsuarioSet];
GO
IF OBJECT_ID(N'[dbo].[VehiculoSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[VehiculoSet];
GO
IF OBJECT_ID(N'[dbo].[MantenimientoSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MantenimientoSet];
GO
IF OBJECT_ID(N'[dbo].[AlertaSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AlertaSet];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'UsuarioSet'
CREATE TABLE [dbo].[UsuarioSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Nombre] nvarchar(max)  NOT NULL,
    [Correo] nvarchar(max)  NOT NULL,
    [Contraseña] nvarchar(max)  NOT NULL,
    [FechaRegistro] datetime  NOT NULL,
    [Apellido] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'VehiculoSet'
CREATE TABLE [dbo].[VehiculoSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Marca] nvarchar(max)  NOT NULL,
    [Modelo] nvarchar(max)  NOT NULL,
    [Año] nvarchar(max)  NOT NULL,
    [Kilometraje] int  NOT NULL,
    [Imagen] nvarchar(max)  NULL,
    [UsuarioId] int  NOT NULL,
    [Observacion] nvarchar(max)  NULL
);
GO

-- Creating table 'MantenimientoSet'
CREATE TABLE [dbo].[MantenimientoSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [TipoMantenimiento] nvarchar(max)  NOT NULL,
    [Fecha] datetime  NOT NULL,
    [Costo] decimal(18,0)  NOT NULL,
    [VehiculoId] int  NOT NULL,
    [Observacion] nvarchar(max)  NULL,
    [Taller] nvarchar(max)  NULL,
    [Horario] nvarchar(max)  NULL
);
GO

-- Creating table 'AlertaSet'
CREATE TABLE [dbo].[AlertaSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [TipoAlerta] nvarchar(max)  NOT NULL,
    [FechaAlerta] datetime  NOT NULL,
    [Kilometraje] int  NOT NULL,
    [VehiculoId] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'UsuarioSet'
ALTER TABLE [dbo].[UsuarioSet]
ADD CONSTRAINT [PK_UsuarioSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'VehiculoSet'
ALTER TABLE [dbo].[VehiculoSet]
ADD CONSTRAINT [PK_VehiculoSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'MantenimientoSet'
ALTER TABLE [dbo].[MantenimientoSet]
ADD CONSTRAINT [PK_MantenimientoSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'AlertaSet'
ALTER TABLE [dbo].[AlertaSet]
ADD CONSTRAINT [PK_AlertaSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [UsuarioId] in table 'VehiculoSet'
ALTER TABLE [dbo].[VehiculoSet]
ADD CONSTRAINT [FK_UsuarioVehiculo]
    FOREIGN KEY ([UsuarioId])
    REFERENCES [dbo].[UsuarioSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UsuarioVehiculo'
CREATE INDEX [IX_FK_UsuarioVehiculo]
ON [dbo].[VehiculoSet]
    ([UsuarioId]);
GO

-- Creating foreign key on [VehiculoId] in table 'MantenimientoSet'
ALTER TABLE [dbo].[MantenimientoSet]
ADD CONSTRAINT [FK_VehiculoMantenimiento]
    FOREIGN KEY ([VehiculoId])
    REFERENCES [dbo].[VehiculoSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_VehiculoMantenimiento'
CREATE INDEX [IX_FK_VehiculoMantenimiento]
ON [dbo].[MantenimientoSet]
    ([VehiculoId]);
GO

-- Creating foreign key on [VehiculoId] in table 'AlertaSet'
ALTER TABLE [dbo].[AlertaSet]
ADD CONSTRAINT [FK_VehiculoAlerta]
    FOREIGN KEY ([VehiculoId])
    REFERENCES [dbo].[VehiculoSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_VehiculoAlerta'
CREATE INDEX [IX_FK_VehiculoAlerta]
ON [dbo].[AlertaSet]
    ([VehiculoId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------