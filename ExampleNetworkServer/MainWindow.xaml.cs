using Lidgren.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace server
{
    public partial class MainWindow : Window
    {
        private NetServer server;
        private const string messageStop = "stop";
        private const string messageMoveLeft = "move_left";
        private const string messageMoveUp = "move_up";
        private const string messageMoveRight = "move_right";
        private const string messageMoveDown = "move_down";
        private const string name = "game";
        private const int port = 12345;
        private const string statusNoConnections = "no connections";

        public MainWindow()
        {
            InitializeComponent();

            var config = new NetPeerConfiguration(name) { Port = port };
            server = new NetServer(config);
            server.Start();
        }

        //region buttons clicks listeners
        private void ButtonClickSendMessage(object sender, RoutedEventArgs e)
        {
            sendMessage(tb.Text);
        }

        private void send_stop(object sender, RoutedEventArgs e)
        {
            sendMessage(messageStop);
        }

        private void send_move_left(object sender, RoutedEventArgs e)
        {
            sendMessage(messageMoveLeft);
        }

        private void send_move_up(object sender, RoutedEventArgs e)
        {
            sendMessage(messageMoveUp);
        }

        private void send_move_right(object sender, RoutedEventArgs e)
        {
            sendMessage(messageMoveRight);
        }

        private void send_move_down(object sender, RoutedEventArgs e)
        {
            sendMessage(messageMoveDown);
        }
        //endregion

        private void sendMessage(string messageText)
        {
            if (server.ConnectionsCount == 0)
            {
                tbStatus.Text = statusNoConnections;
                return;
            }
            tbStatus.Text = "";
            var message = server.CreateMessage();
            message.Write(messageText);
            server.SendMessage(message, server.Connections, NetDeliveryMethod.ReliableOrdered, 0);
        }
    }
}
