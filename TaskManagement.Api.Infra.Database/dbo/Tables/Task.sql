CREATE TABLE [dbo].[Task]
(
	[Id]	        INT	            NOT NULL    IDENTITY(1,1),
    [Title]         VARCHAR(50)     NOT NULL,
    [Description]   VARCHAR(200),
    [Priority]      INT             NOT NULL,
    [DueDate]       DATETIME        NOT NULL,
    [Status]        INT             NOT NULL,
    CONSTRAINT [PK_Task] PRIMARY KEY CLUSTERED ([Id] ASC)
)
