namespace CS.Email
{
    public class MockSender : IRichMessageEmailSender
    {
        public virtual void Send(string from, string to, string subject, string messageText)
        {
        }

        public virtual void Send(Message message)
        {
        }

        public virtual void Send(Message[] messages)
        {
        }
    }
}