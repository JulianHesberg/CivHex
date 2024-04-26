using Fleck;


var server = new WebSocketServer("ws://0.0.0.0:8888");

server.Start(wsConnection =>
{
    wsConnection.OnMessage = message =>
    {
        wsConnection.Send("You just sent " + message);
        Console.WriteLine(message);
    };
});
    WebApplication.CreateBuilder(args).Build().Run();