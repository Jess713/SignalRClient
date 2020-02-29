using System;
using Microsoft.AspNet.SignalR.Client;


namespace SignalRClientNEW
{
    class Program
    {
        //https://docs.microsoft.com/en-us/aspnet/signalr/overview/guide-to-the-api/hubs-api-guide-net-client

        static void Main(string[] args)
        {
            using (var hubConnection = new HubConnection("http://localhost:8089/"))
            {
                IHubProxy myHubProxy = hubConnection.CreateHubProxy("MyHub");
               
                hubConnection.Start().ContinueWith(task =>
                {
                    if (task.IsFaulted)
                    {
                        Console.WriteLine("There was an error opening the connection:{0}", task.Exception.GetBaseException());
                    }
                    else
                    {
                        Console.WriteLine("Connected");
                    }
                }).Wait();

                //myHubProxy.On("notify", data => Console.WriteLine("client: " + data + " was recieved from server"));
               

                myHubProxy.Invoke("Send", "HELLO World ").ContinueWith(task =>
                {
                    if (task.IsFaulted)
                    {
                        Console.WriteLine("There was an error calling send: {0}", task.Exception.GetBaseException());
                    }
                    else
                    {
                        Console.WriteLine("Send Complete.");
                    }
                });
            }






        }
    }
}
