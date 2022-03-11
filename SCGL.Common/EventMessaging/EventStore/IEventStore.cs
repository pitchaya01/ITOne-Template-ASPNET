using Lazarus.Common.Nexus.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lazarus.Common.EventMessaging.EventStore
{
    public interface IEventStore
    {
        LogEventConsumer ConsumeSuccess(string logEventStoreId, string consumerGroupName, string val, string eventName, int offset, int parition, string ConsumerBy = "", int? exectime = null);
        LogEventConsumer ConsumeFail(string logEventStoreId, string consumerGroupName, string val, string eventName, string messageEror, int? offset = null);
        bool IsCommited(string consumerGroup, string topic, int offset, int partition);
        void Persist<TAggregate>(TAggregate aggregate) where TAggregate : IntegrationEvent;
        bool IsCommited(string aggId);
        void Commit(string id, string MachinesName);
        void CommitedFail(string id, string msg);
        //TAggregate GetById<TAggregate, TEvent>(Guid id) where TAggregate : IAggregateRoot, new() where TEvent : class, IEvent;

    }
}
