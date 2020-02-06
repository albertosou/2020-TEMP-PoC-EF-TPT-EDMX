DROP TABLE IF EXISTS dbo.OperationFile, dbo.OperationImport, dbo.OperationExport, dbo.Operation

GO
CREATE TABLE [dbo].[Operation] (
    [Id]         UNIQUEIDENTIFIER CONSTRAINT [DF_Operation] DEFAULT (newid()) NOT NULL,
    [CreateDate] DATETIME         CONSTRAINT [DF_CreateDate] DEFAULT (getdate()) NOT NULL,
    [Active]     BIT              CONSTRAINT [DF_Active] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_Operation] PRIMARY KEY CLUSTERED ([Id] ASC)
);

GO

CREATE TABLE [dbo].[OperationExport] (
    [Id]             UNIQUEIDENTIFIER NOT NULL,
    [ExportBasePath] VARCHAR (100)    NOT NULL,
    CONSTRAINT [PK_OperationExport] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_OperationExport_Operation] FOREIGN KEY ([Id]) REFERENCES [dbo].[Operation] ([Id])
);
GO

CREATE TABLE [dbo].[OperationImport] (
    [Id]                UNIQUEIDENTIFIER NOT NULL,
    [OperationExportId] UNIQUEIDENTIFIER NULL,
    [ImportBasePath]    VARCHAR (100)    NOT NULL,
    CONSTRAINT [PK_OperationImport] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_OperationImport_OperationExport] FOREIGN KEY ([OperationExportId]) REFERENCES [dbo].[OperationExport] ([Id]),
    CONSTRAINT [FK_OperationImport_Operation] FOREIGN KEY ([Id]) REFERENCES [dbo].[Operation] ([Id])
);
GO
CREATE TABLE [dbo].[OperationFile] (
    [Id]          UNIQUEIDENTIFIER CONSTRAINT [DF_OperationFile] DEFAULT (newid()) NOT NULL,
    [OperationId] UNIQUEIDENTIFIER NOT NULL,
    [FilePath]    VARCHAR (100)    NOT NULL,
    CONSTRAINT [PK_OperationFile] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_OperationFile_Operation] FOREIGN KEY ([OperationId]) REFERENCES [dbo].[Operation] ([Id])
);
GO