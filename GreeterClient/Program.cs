// Copyright 2015 gRPC authors.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Core.Utils;
using Helloworld;

namespace GreeterClient
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            Channel channel = new Channel("localhost:50051", ChannelCredentials.Insecure);

            var client = new Greeter.GreeterClient(channel);
            string firstName = "David";
            string lastName = "Huet";
            
            var reply = client.Hello(new HelloRequest { Name = firstName, Lastname = lastName });
            Console.WriteLine("HelloRequest: " + reply.Message);
            var reply2 = client.GoodBye(new HelloRequest { Name = firstName, Lastname = lastName });
            Console.WriteLine("GoodByeRequest: " + reply2.Message);

            var call = client.AsyncHello(new HelloRequest { Name = firstName, Lastname = lastName });
            while (await call.ResponseStream.MoveNext())
            {
              Console.WriteLine("AsyncHello: " + call.ResponseStream.Current.Message);
            }
            
            channel.ShutdownAsync().Wait();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
