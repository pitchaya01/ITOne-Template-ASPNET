using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lazarus.Common.Nexus.Database
{
    public class  LogEventStore
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string AggregateId { get; set; }
         public string EventName { get; set; }
         public string Val { get; set; }
         public string  Status { get; set; }
        public string Message { get; set; }
         public DateTime CreateDate { get; set; }
         public DateTime? CommitDateTime { get; set; }
    }
    public class LogEventConsumer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string ConsumerGroupName { get; set; }
        public string EventName { get; set; }
        public string Status { get; set; }

        public string Val { get; set; }
        public int? LogEventStoreId { get; set; }
        public string MessageError { get; set; }
        public string ConsumeBy { get; set; }
        public int? ExecutionTime { get; set; }
        public DateTime CreateDate { get; set; }
        public int? OffSet { get; set; }
        public int Partition { get; set; }

    }
}
