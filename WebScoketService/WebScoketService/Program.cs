using Fleck;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebScoketNet
{
    class Program
    {
        static void Main(string[] args)
        {

            var service = new WebSocketServer("ws://127.0.0.1:8282");

            service.Start(OnServerStarted);

            Console.ReadLine();

        }


        private static void OnServerStarted(IWebSocketConnection conn) {
            conn.OnOpen = () =>
            {
                Console.WriteLine("conned");
                for (;;) {
                    conn.Send("hello unity");
                }
            };

            conn.OnMessage = (message) =>
            {
                Console.WriteLine(message);
            };
        }

    }
}
