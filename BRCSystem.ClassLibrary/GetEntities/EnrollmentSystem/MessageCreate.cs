using BRCSystem.ClassLibrary.EnrollmentSystem.Entities;

namespace BRCSystem.ClassLibrary.GetEntities.EnrollmentSystem
{
    public static class MessageCreate
    {
        public static Message Get()
        {
            return new Message
            {
                DateTime = DateTime.UtcNow,
                Read = false,
                UserId = 1,
                MessageText = "Message 1"
            };
        }

        public static List<Message> GetList()
        {
            var messageList = new List<Message>();

            for (int i = 0; i < 10; i++)
            {
                var message = Get();
                message.MessageText = $"Message {i + 1}";
                messageList.Add(message);
            }

            return messageList;
        }
    }
}
