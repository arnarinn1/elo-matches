USE Elo_Matches
GO

CREATE OR ALTER View vw_DomainEventOccurrence
( Id
, AggregateId
, AggregateType
, TypeName
, SerializedData
, OccurrenceDate
, TransactionId
)
AS
SELECT d.Id
     , d.AggregateId
     , d.AggregateType
     , d.TypeName
     , d.SerializedData
     , d.OccurrenceDate
     , d.TransactionId
 FROM DomainEventOccurrence d
GO