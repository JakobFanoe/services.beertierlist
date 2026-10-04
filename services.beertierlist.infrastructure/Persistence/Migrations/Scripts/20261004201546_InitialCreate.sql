IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT[PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
CREATE TABLE[TierlistEntries] (
    [Id] uniqueidentifier NOT NULL,
    [UserId] nvarchar(50) NOT NULL,
    [TierId] nvarchar(max) NULL,
    [ImageUri] nvarchar(max) NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    CONSTRAINT[PK_TierlistEntries] PRIMARY KEY ([Id])
);

CREATE TABLE[Tiers] (
    [Id] uniqueidentifier NOT NULL,
    [UserId] nvarchar(50) NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    CONSTRAINT[PK_Tiers] PRIMARY KEY ([Id])
);

CREATE TABLE[WheelOptions] (
    [Id] uniqueidentifier NOT NULL,
    [UserId] nvarchar(50) NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    CONSTRAINT[PK_WheelOptions] PRIMARY KEY ([Id])
);

INSERT INTO[__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES(N'20261004201546_InitialCreate', N'10.0.12');

COMMIT;
GO