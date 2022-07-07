using Client.ChatServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public class ChatServerConnector : IChatServiceCallback
    {      
        private static ChatServerConnector Instance = null;
        private ChatServiceClient Client;
        private int UserId;
        private ChatServerConnector()
        {
            Client = new ChatServiceClient(new System.ServiceModel.InstanceContext(this));
        }
        static ChatServerConnector()
        {
            if (Instance == null)
                Instance = new ChatServerConnector();
        }
        public static ChatServerConnector GetInstance()
        {
            if (Instance == null)
                Instance = new ChatServerConnector();
            return Instance;
        }

        internal static void Connect(string text)
        {
            Instance.UserId = Instance.Client.Connect(text);
        }
        public void SendMessageToServer(string message)
        {
            GetInstance().Client.SendMessage(message);
        }
        public void SendMessageToClient(string message)
        {
            (Application.OpenForms["Form1"] as Form1).listBox1.Items.Add(message);
        }
    }
}
