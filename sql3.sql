DECLARE @sql NVARCHAR(max)=''

SELECT @sql += ' Drop table ' + QUOTENAME(TABLE_SCHEMA) + '.'+ QUOTENAME(TABLE_NAME) + '; '
FROM   INFORMATION_SCHEMA.TABLES
WHERE  TABLE_TYPE = 'BASE TABLE'

Exec Sp_executesql @sql

EXEC sp_msforeachtable "ALTER TABLE ? NOCHECK CONSTRAINT all"



-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 04/19/2021 06:24:30
-- Generated from EDMX file: C:\Users\acer\source\repos\Hotel_Managment\Hotel_Managment\getionhotel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Hotelmanagment];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_OrderFoodFood]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrderFoods] DROP CONSTRAINT [FK_OrderFoodFood];
GO
IF OBJECT_ID(N'[dbo].[FK_RoomOrderFood]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrderFoods] DROP CONSTRAINT [FK_RoomOrderFood];
GO
IF OBJECT_ID(N'[dbo].[FK_RoomReservation]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Reservations] DROP CONSTRAINT [FK_RoomReservation];
GO
IF OBJECT_ID(N'[dbo].[FK_RoomService]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Services] DROP CONSTRAINT [FK_RoomService];
GO
IF OBJECT_ID(N'[dbo].[FK_UserCreditCard]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CreditCards] DROP CONSTRAINT [FK_UserCreditCard];
GO
IF OBJECT_ID(N'[dbo].[FK_UserPremission]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Users] DROP CONSTRAINT [FK_UserPremission];
GO
IF OBJECT_ID(N'[dbo].[FK_UserReservation]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Reservations] DROP CONSTRAINT [FK_UserReservation];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[CreditCards]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CreditCards];
GO
IF OBJECT_ID(N'[dbo].[Foods]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Foods];
GO
IF OBJECT_ID(N'[dbo].[OrderFoods]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OrderFoods];
GO
IF OBJECT_ID(N'[dbo].[Premissions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Premissions];
GO
IF OBJECT_ID(N'[dbo].[Reservations]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Reservations];
GO
IF OBJECT_ID(N'[dbo].[Rooms]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Rooms];
GO
IF OBJECT_ID(N'[dbo].[Services]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Services];
GO
IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'CreditCards'
CREATE TABLE [dbo].[CreditCards] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CardNum] varchar(max)  NOT NULL,
    [Pin] int  NOT NULL,
    [UserId] int  NULL
);
GO

-- Creating table 'Foods'
CREATE TABLE [dbo].[Foods] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FoodName] varchar(max)  NOT NULL,
    [Price] int  NOT NULL,
    [Img] varchar(max)  NOT NULL
);
GO

-- Creating table 'OrderFoods'
CREATE TABLE [dbo].[OrderFoods] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [quantity] int  NOT NULL,
    [RoomId] int  NOT NULL,
    [FoodId] int  NOT NULL,
    [OrderDate] datetime  NOT NULL
);
GO

-- Creating table 'Premissions'
CREATE TABLE [dbo].[Premissions] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [PremissionName] varchar(max)  NOT NULL
);
GO

-- Creating table 'Reservations'
CREATE TABLE [dbo].[Reservations] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [StartDate] datetime  NOT NULL,
    [EndDate] datetime  NOT NULL,
    [UserId] int  NOT NULL,
    [RoomId] int  NOT NULL
);
GO

-- Creating table 'Rooms'
CREATE TABLE [dbo].[Rooms] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Size] tinyint  NOT NULL,
    [Description] varchar(max)  NOT NULL,
    [available] bit  NOT NULL,
    [PriceD] varchar(max)  NOT NULL
);
GO

-- Creating table 'Services'
CREATE TABLE [dbo].[Services] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [TypeService] varchar(max)  NOT NULL,
    [Price] varchar(max)  NOT NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FirstName] varchar(max)  NOT NULL,
    [LastName] varchar(max)  NOT NULL,
    [Email] varchar(max)  NOT NULL,
    [Phone] varchar(max)  NOT NULL,
    [Username] varchar(max)  NOT NULL,
    [Password] varchar(max)  NOT NULL,
    [PremissionId] int  NOT NULL
);
GO

-- Creating table 'OrderServices'
CREATE TABLE [dbo].[OrderServices] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [SOrderDate] datetime  NOT NULL,
    [RoomId] int  NOT NULL,
    [ServiceId] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'CreditCards'
ALTER TABLE [dbo].[CreditCards]
ADD CONSTRAINT [PK_CreditCards]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Foods'
ALTER TABLE [dbo].[Foods]
ADD CONSTRAINT [PK_Foods]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'OrderFoods'
ALTER TABLE [dbo].[OrderFoods]
ADD CONSTRAINT [PK_OrderFoods]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Premissions'
ALTER TABLE [dbo].[Premissions]
ADD CONSTRAINT [PK_Premissions]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Reservations'
ALTER TABLE [dbo].[Reservations]
ADD CONSTRAINT [PK_Reservations]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Rooms'
ALTER TABLE [dbo].[Rooms]
ADD CONSTRAINT [PK_Rooms]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Services'
ALTER TABLE [dbo].[Services]
ADD CONSTRAINT [PK_Services]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'OrderServices'
ALTER TABLE [dbo].[OrderServices]
ADD CONSTRAINT [PK_OrderServices]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [UserId] in table 'CreditCards'
ALTER TABLE [dbo].[CreditCards]
ADD CONSTRAINT [FK_UserCreditCard]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserCreditCard'
CREATE INDEX [IX_FK_UserCreditCard]
ON [dbo].[CreditCards]
    ([UserId]);
GO

-- Creating foreign key on [FoodId] in table 'OrderFoods'
ALTER TABLE [dbo].[OrderFoods]
ADD CONSTRAINT [FK_OrderFoodFood]
    FOREIGN KEY ([FoodId])
    REFERENCES [dbo].[Foods]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OrderFoodFood'
CREATE INDEX [IX_FK_OrderFoodFood]
ON [dbo].[OrderFoods]
    ([FoodId]);
GO

-- Creating foreign key on [RoomId] in table 'OrderFoods'
ALTER TABLE [dbo].[OrderFoods]
ADD CONSTRAINT [FK_RoomOrderFood]
    FOREIGN KEY ([RoomId])
    REFERENCES [dbo].[Rooms]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RoomOrderFood'
CREATE INDEX [IX_FK_RoomOrderFood]
ON [dbo].[OrderFoods]
    ([RoomId]);
GO

-- Creating foreign key on [PremissionId] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [FK_UserPremission]
    FOREIGN KEY ([PremissionId])
    REFERENCES [dbo].[Premissions]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserPremission'
CREATE INDEX [IX_FK_UserPremission]
ON [dbo].[Users]
    ([PremissionId]);
GO

-- Creating foreign key on [RoomId] in table 'Reservations'
ALTER TABLE [dbo].[Reservations]
ADD CONSTRAINT [FK_RoomReservation]
    FOREIGN KEY ([RoomId])
    REFERENCES [dbo].[Rooms]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RoomReservation'
CREATE INDEX [IX_FK_RoomReservation]
ON [dbo].[Reservations]
    ([RoomId]);
GO

-- Creating foreign key on [UserId] in table 'Reservations'
ALTER TABLE [dbo].[Reservations]
ADD CONSTRAINT [FK_UserReservation]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserReservation'
CREATE INDEX [IX_FK_UserReservation]
ON [dbo].[Reservations]
    ([UserId]);
GO

-- Creating foreign key on [RoomId] in table 'OrderServices'
ALTER TABLE [dbo].[OrderServices]
ADD CONSTRAINT [FK_RoomOrderService]
    FOREIGN KEY ([RoomId])
    REFERENCES [dbo].[Rooms]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RoomOrderService'
CREATE INDEX [IX_FK_RoomOrderService]
ON [dbo].[OrderServices]
    ([RoomId]);
GO

-- Creating foreign key on [ServiceId] in table 'OrderServices'
ALTER TABLE [dbo].[OrderServices]
ADD CONSTRAINT [FK_OrderServiceService]
    FOREIGN KEY ([ServiceId])
    REFERENCES [dbo].[Services]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OrderServiceService'
CREATE INDEX [IX_FK_OrderServiceService]
ON [dbo].[OrderServices]
    ([ServiceId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------