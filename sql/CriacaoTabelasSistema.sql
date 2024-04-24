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

CREATE TABLE [Users] (
    [Id] uniqueidentifier NOT NULL,
    [Name] varchar(100) NOT NULL,
    [EmailAddress] varchar(100) NOT NULL,
    [UserType] int NOT NULL,
    [RegistrationDate] datetime NOT NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Products] (
    [Id] uniqueidentifier NOT NULL,
    [OwnerId] uniqueidentifier NULL,
    [Name] varchar(100) NOT NULL,
    [Description] varchar(100) NOT NULL,
    [Value] decimal(18,2) NOT NULL,
    [ExpirationDate] datetime NOT NULL,
    [RegistrationDate] datetime NOT NULL,
    [IsActive] bit NOT NULL,
    [IsAvailableForSell] bit NOT NULL,
    CONSTRAINT [PK_Products] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Products_Users_OwnerId] FOREIGN KEY ([OwnerId]) REFERENCES [Users] ([Id])
);
GO

CREATE TABLE [Transactions] (
    [Id] uniqueidentifier NOT NULL,
    [UserId] uniqueidentifier NOT NULL,
    [ProductId] uniqueidentifier NOT NULL,
    [TransactionType] int NOT NULL,
    [ProductValue] decimal(18,2) NOT NULL,
    [TransactionTimeStamp] datetime NOT NULL,
    CONSTRAINT [PK_Transactions] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Transactions_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]),
    CONSTRAINT [FK_Transactions_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id])
);
GO

CREATE INDEX [IX_Products_OwnerId] ON [Products] ([OwnerId]);
GO

CREATE INDEX [IX_Transactions_ProductId] ON [Transactions] ([ProductId]);
GO

CREATE INDEX [IX_Transactions_UserId] ON [Transactions] ([UserId]);
GO

CREATE UNIQUE INDEX [IX_Users_EmailAddress] ON [Users] ([EmailAddress]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240424012702_Initial', N'6.0.29');
GO

COMMIT;
GO

