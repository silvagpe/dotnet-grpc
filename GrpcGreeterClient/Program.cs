﻿using System;
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
            
            using var channel = GrpcChannel.ForAddress("https://34.72.245.183:5001", 
                new GrpcChannelOptions { HttpHandler = httpHandler });
            
            var client =  new Greeter.GreeterClient(channel);
            var reply = await client.SayHelloAsync(
                              new HelloRequest { Name = "GreeterClient" });
            Console.WriteLine("Greeting: " + reply.Message);
            Console.WriteLine("Press any key to exit...");
            //Console.ReadKey();
        }
    }
}
