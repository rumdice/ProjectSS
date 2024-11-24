using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

namespace ChatConsoleApp;

class Program
{
    static async Task Main(string[] args)
    {
        // SignalR 허브에 대한 연결 설정
        var connection = new HubConnectionBuilder()
            .WithUrl("http://localhost:5000/MessageHub") // 실제 서버를 띄우고나오는 웹페이지 주소가 아닌 리스닝 주소
            .Build();

        // 허브로부터 메시지를 수신할 때 실행될 이벤트 핸들러 등록
        connection.On<string, string>("ReceiveMessage", (user, message) =>
        {
            Console.WriteLine($"{user}: {message}");
        });

        try
        {
            // 연결 시작
            await connection.StartAsync();
            Console.WriteLine("Connected to the hub.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error connecting to the hub: {ex.Message}");
            return;
        }

        // 메시지 전송 루프
        while (true)
        {
            Console.Write("Enter your name: ");
            var user = Console.ReadLine();

            Console.Write("Enter your message: ");
            var message = Console.ReadLine();

            try
            {
                await connection.InvokeAsync("SendMessage", user, message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending message: {ex.Message}");
            }
        }
    }
}
