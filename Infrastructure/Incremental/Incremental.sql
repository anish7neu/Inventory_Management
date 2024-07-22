IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [AspNetRoles] (
    [Id] uniqueidentifier NOT NULL,
    [Description] nvarchar(max) NULL,
    [Name] nvarchar(256) NULL,
    [NormalizedName] nvarchar(256) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AspNetUsers] (
    [Id] uniqueidentifier NOT NULL,
    [Address] nvarchar(max) NULL,
    [IsActive] bit NOT NULL,
    [IsQRScanned] bit NOT NULL,
    [UserType] tinyint NOT NULL,
    [UserName] nvarchar(256) NULL,
    [NormalizedUserName] nvarchar(256) NULL,
    [Email] nvarchar(256) NULL,
    [NormalizedEmail] nvarchar(256) NULL,
    [EmailConfirmed] bit NOT NULL,
    [PasswordHash] nvarchar(max) NULL,
    [SecurityStamp] nvarchar(max) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    [PhoneNumber] nvarchar(max) NULL,
    [PhoneNumberConfirmed] bit NOT NULL,
    [TwoFactorEnabled] bit NOT NULL,
    [LockoutEnd] datetimeoffset NULL,
    [LockoutEnabled] bit NOT NULL,
    [AccessFailedCount] int NOT NULL,
    CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Products] (
    [Id] uniqueidentifier NOT NULL,
    [ProductName] nvarchar(max) NOT NULL,
    [ProductDescription] nvarchar(max) NOT NULL,
    [ProductCategory] tinyint NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatedBy] nvarchar(max) NULL,
    [LastModified] datetime2 NULL,
    [LastModifiedBy] nvarchar(max) NULL,
    CONSTRAINT [PK_Products] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Vendors] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(30) NOT NULL,
    [Address] nvarchar(50) NOT NULL,
    [PAN] nvarchar(max) NOT NULL,
    [PhoneNumber] nvarchar(10) NOT NULL,
    [Email] varchar(100) NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatedBy] nvarchar(max) NULL,
    [LastModified] datetime2 NULL,
    [LastModifiedBy] nvarchar(max) NULL,
    CONSTRAINT [PK_Vendors] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AspNetRoleClaims] (
    [Id] int NOT NULL IDENTITY,
    [RoleId] uniqueidentifier NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [ApplicationRoleApplicationUser] (
    [RolesId] uniqueidentifier NOT NULL,
    [UsersId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_ApplicationRoleApplicationUser] PRIMARY KEY ([RolesId], [UsersId]),
    CONSTRAINT [FK_ApplicationRoleApplicationUser_AspNetRoles_RolesId] FOREIGN KEY ([RolesId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_ApplicationRoleApplicationUser_AspNetUsers_UsersId] FOREIGN KEY ([UsersId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserClaims] (
    [Id] int NOT NULL IDENTITY,
    [UserId] uniqueidentifier NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserLogins] (
    [LoginProvider] nvarchar(128) NOT NULL,
    [ProviderKey] nvarchar(128) NOT NULL,
    [ProviderDisplayName] nvarchar(max) NULL,
    [UserId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
    CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserRoles] (
    [UserId] uniqueidentifier NOT NULL,
    [RoleId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
    CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserTokens] (
    [UserId] uniqueidentifier NOT NULL,
    [LoginProvider] nvarchar(128) NOT NULL,
    [Name] nvarchar(128) NOT NULL,
    [Value] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
    CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AvailableStocks] (
    [Id] uniqueidentifier NOT NULL,
    [Action] tinyint NOT NULL,
    [TotalQuantity] int NOT NULL,
    [ProductId] uniqueidentifier NOT NULL,
    [ChangerId] uniqueidentifier NOT NULL,
    [SRId] uniqueidentifier NULL,
    [Created] datetime2 NOT NULL,
    [CreatedBy] nvarchar(max) NULL,
    [LastModified] datetime2 NULL,
    [LastModifiedBy] nvarchar(max) NULL,
    CONSTRAINT [PK_AvailableStocks] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AvailableStocks_AvailableStocks_SRId] FOREIGN KEY ([SRId]) REFERENCES [AvailableStocks] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_AvailableStocks_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [OrderRequests] (
    [Id] uniqueidentifier NOT NULL,
    [Quantity] int NOT NULL,
    [Remarks] nvarchar(100) NOT NULL,
    [Status] tinyint NOT NULL,
    [ProductId] uniqueidentifier NOT NULL,
    [UserId] nvarchar(max) NOT NULL,
    [UserName] nvarchar(max) NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatedBy] nvarchar(max) NULL,
    [LastModified] datetime2 NULL,
    [LastModifiedBy] nvarchar(max) NULL,
    CONSTRAINT [PK_OrderRequests] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_OrderRequests_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AdminPurchaseLogs] (
    [Id] uniqueidentifier NOT NULL,
    [Quantity] int NOT NULL,
    [Price] float NOT NULL,
    [VendorId] uniqueidentifier NOT NULL,
    [ProductId] uniqueidentifier NOT NULL,
    [IsAddedToStock] bit NOT NULL,
    [Created] datetime2 NOT NULL,
    [CreatedBy] nvarchar(max) NULL,
    [LastModified] datetime2 NULL,
    [LastModifiedBy] nvarchar(max) NULL,
    CONSTRAINT [PK_AdminPurchaseLogs] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AdminPurchaseLogs_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AdminPurchaseLogs_Vendors_VendorId] FOREIGN KEY ([VendorId]) REFERENCES [Vendors] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_AdminPurchaseLogs_ProductId] ON [AdminPurchaseLogs] ([ProductId]);
GO

CREATE INDEX [IX_AdminPurchaseLogs_VendorId] ON [AdminPurchaseLogs] ([VendorId]);
GO

CREATE INDEX [IX_ApplicationRoleApplicationUser_UsersId] ON [ApplicationRoleApplicationUser] ([UsersId]);
GO

CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
GO

CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;
GO

CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
GO

CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
GO

CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
GO

CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
GO

CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;
GO

CREATE INDEX [IX_AvailableStocks_ProductId] ON [AvailableStocks] ([ProductId]);
GO

CREATE UNIQUE INDEX [IX_AvailableStocks_SRId] ON [AvailableStocks] ([SRId]) WHERE [SRId] IS NOT NULL;
GO

CREATE INDEX [IX_OrderRequests_ProductId] ON [OrderRequests] ([ProductId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240429082621_Initial_Migration', N'8.0.3');
GO

COMMIT;
GO

