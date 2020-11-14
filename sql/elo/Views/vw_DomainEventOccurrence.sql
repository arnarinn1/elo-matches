USE Elo_Matches
GO

CREATE OR ALTER View elo.vw_DomainEventOccurrence
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
 FROM elo.DomainEventOccurrence d
GO