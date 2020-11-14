USE Metrics
GO

PRINT 'QueryMetrics - Creating table'

CREATE TABLE met.QueryMetrics
( Id                      INT IDENTITY(1,1) NOT NULL
, Identifier              UNIQUEIDENTIFIER  NOT NULL
, TimeStarted             DATETIME          NOT NULL
, TimeFinished            DATETIME          NOT NULL
, TotalMilliseconds       INT               NOT NULL
, QueryTypeName           VARCHAR(256)      NOT NULL
, EntryDate               DATETIME          NOT NULL DEFAULT GETDATE()
)
GO

ALTER TABLE met.QueryMetrics
ADD CONSTRAINT PK_QueryMetrics PRIMARY KEY CLUSTERED (Id);
GO

PRINT 'QueryMetrics - Table created'


PRINT 'CommandMetrics - Creating table'

CREATE TABLE met.CommandMetrics
( Id                      INT IDENTITY(1,1) NOT NULL
, Identifier              UNIQUEIDENTIFIER  NOT NULL
, TimeStarted             DATETIME          NOT NULL
, TimeFinished            DATETIME          NOT NULL
, TotalMilliseconds       INT               NOT NULL
, CommandTypeName         VARCHAR(256)      NOT NULL
, EntryDate               DATETIME          NOT NULL DEFAULT GETDATE()
)
GO

ALTER TABLE met.CommandMetrics
ADD CONSTRAINT PK_CommandMetrics PRIMARY KEY CLUSTERED (Id);
GO

PRINT 'CommandMetrics - Table created'