using System.IO;
using System.IO.Pipes;

namespace WindowsClient.Utils
{
    public static class InstanceCommunicator
    {
        private const string PipeName = "ArqanumPipe";

        public static void SendMessageToMainInstance(string message)
        {
            using var client = new NamedPipeClientStream(".", PipeName, PipeDirection.Out);
            try
            {
                client.Connect(1000);
                using var writer = new StreamWriter(client) { AutoFlush = true };
                writer.WriteLine(message);
            }
            catch
            {
            }
        }

        public static void StartListening(Action<string> onMessageReceived, CancellationToken token)
        {
            Task.Run(() =>
            {
                while (!token.IsCancellationRequested)
                {
                    try
                    {
                        using var server = new NamedPipeServerStream(PipeName, PipeDirection.In);
                        server.WaitForConnection();
                        using var reader = new StreamReader(server);
                        var line = reader.ReadLine();
                        if (line != null)
                        {
                            onMessageReceived?.Invoke(line);
                        }
                    }
                    catch
                    {
                    }
                }
            }, token);
        }
    }
}
