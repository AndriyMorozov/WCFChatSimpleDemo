using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace ChatLibrary
{
    [ServiceContract(CallbackContract = typeof(IChatServiceCallback))]
    public interface IChatService
    {
        [OperationContract]
        int Connect(string username);
        [OperationContract(IsOneWay = true)]
        void Disconnect(int id);
        [OperationContract(IsOneWay = true)]
        void SendMessage(string message);
    }

    [ServiceContract]
    public interface IChatServiceCallback
    {
        [OperationContract(IsOneWay = true)]
        void SendMessageToClient(string message);
    }

    public class ChatUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public OperationContext Context { get; set; }
    }

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class ChatService : IChatService
    {
        List<ChatUser> usersList = new List<ChatUser>();
        int nextUserId = 1;
        public int Connect(string username)
        {
            ChatUser user = new ChatUser()
            {
                Id = nextUserId++,
                Name = username,
                Context = OperationContext.Current,
            };
            usersList.Add(user);
            return user.Id;
        }

        public void Disconnect(int id)
        {
            var user = usersList.FirstOrDefault(x => x.Id == id);
            if (user != null)
                usersList.Remove(user);
        }

        public void SendMessage(string message)
        {
            foreach (ChatUser user in usersList)
            {
                user.Context.GetCallbackChannel<IChatServiceCallback>().SendMessageToClient(message);
            }
        }
    }
}