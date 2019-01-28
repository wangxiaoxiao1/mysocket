using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MySocket.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            int port = 6000;
            string host = "127.0.0.1";//服务器端ip地址

            IPAddress ip = IPAddress.Parse(host);
            IPEndPoint ipe = new IPEndPoint(ip, port);
            Parallel.For(0, 10, (t) =>
            {
                Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                clientSocket.Connect(ipe);

                //send message
                string sendStr = @"send to server : hellaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa
aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa
aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa
aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa
aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaao,ni hao " +t;
                byte[] sendBytes = Encoding.ASCII.GetBytes(sendStr);
                //clientSocket.Send(sendBytes);
                clientSocket.BeginSend(sendBytes, 0, sendBytes.Length, 0, new AsyncCallback(SendCallback), clientSocket);

                //receive message
                string recStr = "";
                byte[] recBytes = new byte[4096];
                int bytes = clientSocket.Receive(recBytes, recBytes.Length, 0);
                recStr += Encoding.ASCII.GetString(recBytes, 0, bytes);
                Console.WriteLine(recStr);
            });
          

            //clientSocket.Close();

            //clientSocket.Dispose();
            Console.WriteLine("End");
            Console.ReadKey();
        }
        private static void SendCallback(IAsyncResult ar)
        {
            try
            {
                Socket handler = (Socket)ar.AsyncState;
                handler.EndSend(ar);
            }
            catch (SocketException ex)
            { }
        }
    }
}
