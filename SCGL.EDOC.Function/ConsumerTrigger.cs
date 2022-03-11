using Autofac;
using Lazarus.Common.EventMessaging;
using Lazarus.Common.EventMessaging.EventStore;
using Lazarus.Common.Nexus.Database;
using Lazarus.Common.Utilities;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Kafka;
using Microsoft.Extensions.Logging;
using SCGL.Common.EventMessaging.Command;
using SCGL.EDOC.Api.Infrastructure;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SCGL.EDOC.Function
{
    public class ConsumerTrigger
    {
        const string URL_KAFKA = "10.101.7.45";
        const string CONSUMER_GROUP = "EDOC_CONSUMER";
        const string PREFIX_ENV = "TEST";
 
        const int MAX_RETRY = 1;
        const bool IS_API_MODE = false;
        const bool DISABLE_CONSUME = false;

        TimeSpan PAUSE_BETWEEN_FAILURES = TimeSpan.FromSeconds(2);
        
        public ILifetimeScope container;
        public IEventStore _EventStore;
        public NexusDataContext _dbNexus;
        public DbDataContext _db;
        public ConsumerTrigger()
        {
            var dbNexus = new NexusDataContext(SCGL.EDOC.Function.Infrastructure.DbContextOptionsFactory.GetNexus());
            
            _db = new DbDataContext(SCGL.EDOC.Function.Infrastructure.DbContextOptionsFactory.GetDbContext());
            _EventStore = new EventStore(dbNexus);
            _dbNexus = dbNexus;
            container = new DependencyConfig().SetUp();

        }
        [FunctionName(nameof(DnUpdatedEvent))]
        public async Task DnUpdatedEvent(
[KafkaTrigger(URL_KAFKA, PREFIX_ENV + "DNUpdatedEvent", ConsumerGroup = CONSUMER_GROUP, Protocol = BrokerProtocol.Plaintext)] KafkaEventData<string> kafkaEvent,
Microsoft.Extensions.Logging.ILogger logger)
        {

            if (_EventStore.IsCommited(CONSUMER_GROUP, "DNUpdatedEvent", (int)kafkaEvent.Offset, (int)kafkaEvent.Partition)) return;
            var json = kafkaEvent.Value.ToString().Replace("'", "");
            var model = json.ToObject<DNUpdatedEvent>();
            try
            {

        
                   await   container.Resolve<IIntegrationEventHandler<DNUpdatedEvent>>().Handle(model);
                    _EventStore.ConsumeSuccess("", CONSUMER_GROUP, model.ToJSON(), "DNUpdatedEvent", (int)kafkaEvent.Offset, (int)kafkaEvent.Partition);
    


            }
            catch (Exception e)
            {
                _EventStore.ConsumeFail("", CONSUMER_GROUP, model.ToJSON(), "DNUpdatedEvent", e.GetMessageError());
                Console.WriteLine(e.GetMessageError());
            }

        }
    }
}
