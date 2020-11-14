USE Metrics
GO

CREATE OR ALTER Procedure met.CreateCommandMetrics
( @CorrelationId           UNIQUEIDENTIFIER
, @TimeStarted             DATETIME
, @TimeFinished            DATETIME
, @TotalMilliseconds       INT
, @CommandTypeName         VARCHAR(128)
, @ExceptionMessage        VARCHAR(256)
)
AS 

INSERT INTO met.CommandMetrics
( CorrelationId
, TimeStarted
, TimeFinished
, TotalMilliseconds
, CommandTypeName
, ExceptionMessage
)
VALUES
( @CorrelationId
, @TimeStarted
, @TimeFinished
, @TotalMilliseconds
, @CommandTypeName
, @ExceptionMessage
)
GO