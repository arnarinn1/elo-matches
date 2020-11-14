USE Metrics
GO

CREATE TABLE met.CommandMetrics
( Id                      INT IDENTITY(1,1) NOT NULL
, CorrelationId           UNIQUEIDENTIFIER  NOT NULL
, TimeStarted             DATETIME          NOT NULL
, TimeFinished            DATETIME          NOT NULL
, TotalMilliseconds       INT               NOT NULL
, CommandTypeName         VARCHAR(128)      NOT NULL
, ExceptionMessage        VARCHAR(256)          NULL
, EntryDate               DATETIME          NOT NULL DEFAULT GETDATE()
)
GO

ALTER TABLE met.CommandMetrics
ADD CONSTRAINT PK_CommandMetrics PRIMARY KEY CLUSTERED (Id);
GO