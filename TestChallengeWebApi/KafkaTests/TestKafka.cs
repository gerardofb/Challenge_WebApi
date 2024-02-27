using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Confluent.Kafka;

namespace TestChallengeWebApi.KafkaTests
{
    public class TestKafka
    {
        public static void handler(DeliveryReport<Null, string> delivery)
        {
            System.Diagnostics.Debug.WriteLine(String.Format("Salida del handler {0} {1} {3}", delivery.Error, delivery.Topic, delivery.Value));
        }
        private ProducerConfig configKafka;
        [SetUp]
        public void Setup()
        {
            configKafka = new ProducerConfig
            {
                BootstrapServers = "localhost:9094",
            };
        }
        [Test]
        public async Task Test()
        {
            using (var producer = new ProducerBuilder<Null, string>(configKafka).Build())
            {
                var result = await producer.ProduceAsync("logKafka", new Message<Null, string> { Value = "Un mensaje" });
                //producer.Produce("logKafka", new Message<Null, string> { Value = "Un mensaje más" }, handler);
                Assert.IsTrue(result.Value != "");
            }
        }
    }
}
