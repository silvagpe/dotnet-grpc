using System;
using System.Net.Http;
using System.Threading.Tasks;
using Grpc.Net.Client;

namespace GrpcGreeterClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var httpHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback =
                    HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };
            // The port number(5001) must match the port of the gRPC server.
            //using var channel = GrpcChannel.ForAddress("https://127.0.0.1:5051");
            
            using var channel = GrpcChannel.ForAddress("https://127.0.0.1:5051", 
                new GrpcChannelOptions { HttpHandler = httpHandler });
            //using var channel = GrpcChannel.ForAddress("http://127.0.0.1:5050");
            //using var channel = GrpcChannel.ForAddress("https://[::]:5001");
            //using var channel = GrpcChannel.ForAddress("https://35.225.123.164:5001");
            var client =  new Greeter.GreeterClient(channel);
            var reply = await client.SayHelloAsync(
                              new HelloRequest { Name = "GreeterClient" });
            Console.WriteLine("Greeting: " + reply.Message);
            Console.WriteLine("Press any key to exit...");
            //Console.ReadKey();
        }
    }
}
