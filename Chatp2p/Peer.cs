using System.Net.Sockets;
using System.Formats.Asn1;
using System.Net;
using System.Text;
namespace Chatp2p;

public class Peer{
    private readonly TcpListener _listener;
    private TcpClient? _client;
    private const int Port = 8080;

    public Peer ()=> _listener = new TcpListener(IPAddress.Any, Port);
    public async Task ConectToPeer(string ipAddress, string port)
    {
        try
        {
            _client = new TcpClient(ipAddress, Convert.ToInt32(port));
            Console.WriteLine("Connected");
            var receiveTask = ReceiveMessage();
            await SendMessage();
            await receiveTask;
        }catch(Exception ex)
        {
            Console.WriteLine("Conection closed " + ex.Message);
        }
    }

    public async Task StartListening()
    {
        try
        {
            _listener.Start();
            Console.WriteLine("Listening");
            _client = await _listener.AcceptTcpClientAsync();
            Console.WriteLine("Client connected");

            var receiveTask = ReceiveMessage();
            await SendMessage();
            await receiveTask;
        }catch(Exception ex)
        {
            Console.WriteLine("Error" + ex.Message);
        }
    }

private async Task ReceiveMessage(){
    try{
        var stream = _client!.GetStream();
        var reader = new StreamReader(stream, Encoding.UTF8);

        string? message;
        while ((message = await reader.ReadLineAsync()) != null){
            Console.WriteLine($"Peer message: {message}");
        }
    }
    catch (Exception ex){
        Console.WriteLine($"Error receiving message: {ex.Message}");
    }
    finally{
        close();
    }
}

private async Task SendMessage(){
    try{
        var stream = _client!.GetStream();
        var writer = new StreamWriter(stream, Encoding.UTF8) { AutoFlush = true };

        string? message;
        while ((message = Console.ReadLine()) != null){
            await writer.WriteLineAsync(message);
        }
    }
    catch (Exception ex){
        Console.WriteLine($"Error sending messages: {ex.Message}");
    }
    finally{
        close();
    }
}
private void close ()
{
    _client?.Close();
    _listener.Stop();
}
}