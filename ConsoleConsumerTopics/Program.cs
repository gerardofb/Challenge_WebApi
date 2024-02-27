using System.Collections.Generic;
using Confluent.Kafka;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Inicio de la aplicación de consumo de mensajes de la cola de mensajes en Kafka Broker");
var config = new ConsumerConfig
{
    BootstrapServers = "localhost:9094",
    GroupId = "newgroup",
    AutoOffsetReset = AutoOffsetReset.Earliest
};
Console.WriteLine("Escriba un número de segundos a esperar para terminar la ejecución, o bien escriba Enter para continuar");
string? nuevaCadena = Console.ReadLine();
int numerosegundos;
bool secondsset = int.TryParse(nuevaCadena, out numerosegundos);
Console.WriteLine("¿Desea continuar  y/n? En cualquier momento presione X para terminar");
using (var consumer = new ConsumerBuilder<Ignore, string>(config).Build())
{
    consumer.Subscribe("logKafka");
    bool cancelled = false;
    DateTime fechaactual = DateTime.Now;
    while (!cancelled)
    {

        if (secondsset)
        {
            DateTime newdate = DateTime.Now;
            int segundoselapsed = (int)newdate.Subtract(fechaactual).TotalSeconds;
            if (segundoselapsed > numerosegundos)
                cancelled = true;
            var consumeResult = consumer.Consume(numerosegundos);
            if (consumeResult != null)
            {
                Console.WriteLine(consumeResult.Value);
            }
        }
        else
        {
            var consumeResult = consumer.Consume();
            if (consumeResult != null)
            {
                Console.WriteLine(consumeResult.Value);
            }
        }

        
    }

    consumer.Close();
}
