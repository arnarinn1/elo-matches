USE Metrics
GO

CREATE OR ALTER Procedure met.CreateQueryMetrics
( @CorrelationId           UNIQUEIDENTIFIER
, @TimeStarted             DATETIME
, @TimeFinished            DATETIME
, @TotalMilliseconds       INT
, @QueryTypeName           VARCHAR(128)
, @ExceptionMessage        VARCHAR(256)
)
AS 

INSERT INTO met.QueryMetrics
( CorrelationId
, TimeStarted
, TimeFinished
, TotalMilliseconds
, QueryTypeName
, ExceptionMessage
)
VALUES
( @CorrelationId
, @TimeStarted
, @TimeFinished
, @TotalMilliseconds
, @QueryTypeName
, @ExceptionMessage
)
GO