using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class UDPServer
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        // Tạo Server EndPoint
        IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5000);

        // Tạo Server Socket
        Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

        // Bind Server Socket với Server EndPoint
        serverSocket.Bind(serverEndPoint);

        Console.WriteLine("UDP Server đã khởi động...");

        byte[] buffer = new byte[1024];
        EndPoint remote = new IPEndPoint(IPAddress.Any, 0);

        while (true)
        {
            // Nhận dữ liệu từ client
            int receivedBytes = serverSocket.ReceiveFrom(buffer, ref remote);
            string dataReceived = Encoding.ASCII.GetString(buffer, 0, receivedBytes);

            Console.WriteLine($"{dataReceived}");

            // Hiện thông tin kết nối
            Console.WriteLine($"Kết nối từ: {((IPEndPoint)remote).Address}:{((IPEndPoint)remote).Port}");
            // Kiểm tra nếu client gửi lệnh "exit all"
            if (dataReceived.Trim().ToLower() == "exit all")
            {
                Console.WriteLine("Đã nhận lệnh exit all. Đóng server và thoát...");
                serverSocket.Close();
                return;
            }
            // Phản hồi lại client
            string responseMessage = "helo tu UDP Server";
            byte[] responseData = Encoding.ASCII.GetBytes(responseMessage);
            serverSocket.SendTo(responseData, remote);
        }
    }
}
