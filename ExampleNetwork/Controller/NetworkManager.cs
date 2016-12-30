using ExampleNetwork.Model;
using Lidgren.Network;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using ExampleNetwork.Controller;

namespace GameHologram.classes.xna
{
    class NetworkManager
    {
        private const string serverIp = "127.0.0.1";
        private const int port = 12345;
        private const string name = "game";

        private const string message_up = "move_up";
        private const string message_left = "move_left";
        private const string message_right = "move_right";
        private const string message_down = "move_down";
        private const string message_stop = "stop";

        public static NetworkManager Instance
        {
            get
            {
                if (instance == null) instance = new NetworkManager();
                return instance;
            }
        }

        private static NetworkManager instance;

        private NetClient client;

        private MessageControl? stringToControl(string message)
        {
            if (message == message_left) return MessageControl.MOVE_LEFT;
            if (message == message_up) return MessageControl.MOVE_UP;
            if (message == message_right) return MessageControl.MOVE_RIGHT;
            if (message == message_down) return MessageControl.MOVE_DOWN;
            if (message == message_stop) return MessageControl.STOP;

            return null;
        }
        private NetworkManager()
        {
            var config = new NetPeerConfiguration(name);
            client = new NetClient(config);
            client.Start();
            client.Connect(serverIp, port);
        }

        public void Update()
        {
            NetIncomingMessage message;
            while ((message = client.ReadMessage()) != null)
            {
                switch (message.MessageType)
                {
                    case NetIncomingMessageType.Data:
                        var data = message.ReadString();
                        ControllerPlayer.Instance.setState(stringToControl(data));
                        break;

                    case NetIncomingMessageType.StatusChanged:
                        //switch (message.SenderConnection.Status)
                        //{
                        //}
                        break;

                    default:
                        break;
                }
            }
        }

    }
}
