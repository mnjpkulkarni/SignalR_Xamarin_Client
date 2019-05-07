using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Microsoft.AspNet.SignalR.Client;
using System.Collections.ObjectModel;

namespace SignalR_Client
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(true)]
    public partial class MainPage : ContentPage
    {
        HubConnection hubConnection;
        IHubProxy hubProxy;

        ObservableCollection<MessageModel> messages= new ObservableCollection<MessageModel>();

        string nameToHub = "Manoj";
        string messageToHub = "Hi HUB!!";

        public MainPage()
        {
            InitializeComponent();

            hubConnection = new HubConnection("http://psalsignalr.azurewebsites.net/");
            hubProxy = hubConnection.CreateHubProxy("MyHub");
            hubConnection.Start();

            chatList.ItemsSource = messages;

            hubProxy.On<string, string>("broadcastMessage", (nameFromHub, messageFromHub) =>
            {

                Device.BeginInvokeOnMainThread(() => {
                    var messageItem = new MessageModel(nameFromHub, messageFromHub);
                    messages.Add(messageItem);
                });

            });

        }

        void Handle_Clicked_1(object sender, System.EventArgs e)
        {
            nameToHub = nameEntry.Text;
            nameEntry.Text = "";
        }

        void Handle_Clicked(object sender, System.EventArgs e)
        {
            messageToHub = messageEntry.Text;
            messageEntry.Text = "";
            connectToHub();
        }


        async void connectToHub()
        {
            try
            {
                await hubConnection.Start();
            }
            catch(Exception exception)
            {
                System.Diagnostics.Debug.Write("Hub Start Error " + exception);
            }

            await hubProxy.Invoke("Send", new object[] { nameToHub, messageToHub });

        }

    }
}
