using System;

namespace EloMatches.Query.Projections.Players
{
    public class PlayerProjection
    {
        public int SequenceId { get; set; }
        public Guid PlayerId { get; set; }
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public DateTime? ActiveSince { get; set; }
        public DateTime? DeactivatedSince { get; set; }
        public DateTime EntryDate { get; set; }
    }
}