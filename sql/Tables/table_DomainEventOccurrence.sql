USE Elo_Matches
GO

CREATE TABLE elo.DomainEventOccurrence
( Id                      INT IDENTITY(1,1) NOT NULL
, AggregateId             VARCHAR(64)       NOT NULL
, AggregateType           VARCHAR(64)       NOT NULL
, TypeName                VARCHAR(128)      NOT NULL
, SerializedData          VARCHAR(1024)     NOT NULL
, OccurrenceDate          DATETIME          NOT NULL
, TransactionId           UNIQUEIDENTIFIER      NULL
)
GO

ALTER TABLE elo.DomainEventOccurrence
ADD CONSTRAINT PK_DomainEvent PRIMARY KEY CLUSTERED (Id);
GO

CREATE NONCLUSTERED INDEX Idx_DomainEvent_AggregateId ON elo.DomainEventOccurrence (AggregateId, OccurrenceDate)
GO