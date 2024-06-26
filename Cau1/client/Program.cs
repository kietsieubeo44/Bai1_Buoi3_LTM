﻿using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class UDPClient
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        // Tạo Server EndPoint
        IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5000);

        // Tạo Client Socket
        Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

        while (true)
        {
            // Nhập dữ liệu từ người dùng
            Console.Write("Nhập dữ liệu: ");
            string message = Console.ReadLine();

            // Chuyển đổi dữ liệu sang mảng byte
            byte[] buffer = Encoding.ASCII.GetBytes(message);

            // Gửi dữ liệu tới Server
            clientSocket.SendTo(buffer, serverEndPoint);

            // Kiểm tra nếu người dùng gửi lệnh "exit"
            if (message.Trim().ToLower() == "exit")
            {
                Console.WriteLine("Đã gửi lệnh exit. Đóng client...");
                clientSocket.Close();
                return;
            }
            // Kiểm tra nếu người dùng gửi lệnh "exit all"
            else if (message.Trim().ToLower() == "exit all")
            {
                Console.WriteLine("Đã gửi lệnh exit all. Đóng client và thoát...");
                clientSocket.Close();
                return;
            }
        }
    }
}
