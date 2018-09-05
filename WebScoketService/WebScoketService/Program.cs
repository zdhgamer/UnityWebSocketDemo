using Fleck;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[ProtoContract]
public class WebSocketMsgDataTest
{
    [ProtoMember(1)]
    public int idd;
}

namespace MyWebScoketNet
{
    class Program
    {

        static IWebSocketConnection conn;

        static void Main(string[] args)
        {

            int id = 1010;

            byte[] data = Encoding.ASCII.GetBytes(id+"");

            Console.WriteLine(data);

            var service = new WebSocketServer("ws://127.0.0.1:8282");

            service.Start(OnServerStarted);

            Console.ReadLine();

        }


        private static void OnServerStarted(IWebSocketConnection conn) {

            Program.conn = conn;

            conn.OnBinary = OnBinary;

            conn.OnOpen = () =>
            {

                Console.WriteLine("conned");
            };

            conn.OnMessage = (message) =>
            {

            };
        }

        /// <summary>
        /// 收到二进制消息
        /// </summary>
        /// <param name="array"></param>
        private static void OnBinary(byte[] array) {
            Console.WriteLine(array.ToString());
            int msgId = int.Parse(Encoding.Default.GetString(array, 0, 4));
            Console.WriteLine("收到消息id:" + msgId);
            byte[] receive = new byte[array.Length - 4];
            Array.Copy(array, 4, receive, 0, array.Length - 4);
            using (MemoryStream ms = new MemoryStream(receive))
            {
                WebSocketMsgDataTest data = Serializer.Deserialize<WebSocketMsgDataTest>(ms);
                Console.Write("收到消息内容:" + data.idd);
            }


            for (int i = 0; i < 1; i++)
            {
                WebSocketMsgDataTest test = new WebSocketMsgDataTest();
                test.idd = 11111;

                byte[] mid = System.Text.Encoding.Default.GetBytes(1010 + "");
                using (MemoryStream ms = new MemoryStream())
                {
                    Serializer.Serialize(ms, test);
                    byte[] bytes = ms.ToArray();
                    byte[] send = new byte[4 + bytes.Length];
                    Array.Copy(mid, send, 4);
                    Array.Copy(bytes, 0, send, mid.Length, bytes.Length);
                    Program.conn.Send(send);
                }
            }

        }

    }
}
