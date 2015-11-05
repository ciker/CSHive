using System;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Mail;

namespace CS.Email
{
    /// <summary>
    ///     Uses Smtp to send emails.
    /// </summary>
    public class SmtpSender : IRichMessageEmailSender
    {
        private readonly NetworkCredential _credentials = new NetworkCredential();
        private readonly SmtpClient _smtpClient;
        private bool _configured;

        /// <summary>
        ///     This service implementation
        ///     requires a host name in order to work
        /// </summary>
        /// <param name="hostname">The smtp server name</param>
        public SmtpSender(string hostname)
        {
            Hostname = hostname;
            _smtpClient = new SmtpClient(hostname);
        }

        /// <summary>
        ///     Gets or sets the port used to
        ///     access the SMTP server
        /// </summary>
        public int Port { get; set; } = 25;

        /// <summary>
        ///     Gets the hostname.
        /// </summary>
        /// <value>The hostname.</value>
        public string Hostname { get; }

        /// <summary>
        /// </summary>
        public bool EnableSsl { get; set; }

        /// <summary>
        ///     Gets or sets a value which is used to
        ///     configure if emails are going to be sent asyncrhonously or not.
        /// </summary>
        public bool AsyncSend { get; set; } = false;

        /// <summary>
        ///     Gets or sets a value that specifies
        ///     the amount of time after which a synchronous Send call times out.
        /// </summary>
        public int Timeout
        {
            get { return _smtpClient.Timeout; }
            set { _smtpClient.Timeout = value; }
        }

        /// <summary>
        ///     Gets or sets the domain.
        /// </summary>
        /// <value>The domain.</value>
        public string Domain
        {
            get { return _credentials.Domain; }
            set { _credentials.Domain = value; }
        }

        /// <summary>
        ///     Gets or sets the name of the user.
        /// </summary>
        /// <value>The name of the user.</value>
        public string UserName
        {
            get { return _credentials.UserName; }
            set { _credentials.UserName = value; }
        }

        /// <summary>
        ///     Gets or sets the password.
        /// </summary>
        /// <value>The password.</value>
        public string Password
        {
            get { return _credentials.Password; }
            set { _credentials.Password = value; }
        }

        /// <summary>
        ///     Gets a value indicating whether credentials were informed.
        /// </summary>
        /// <value>
        ///     <see langword="true" /> if this instance has credentials; otherwise, <see langword="false" />.
        /// </value>
        protected bool HasCredentials => _credentials.UserName != null && _credentials.Password != null ? true : false;

        /// <summary>
        ///     Sends a message.
        /// </summary>
        /// <exception cref="ArgumentNullException">If any of the parameters is null</exception>
        /// <param name="from">From field</param>
        /// <param name="to">To field</param>
        /// <param name="subject">e-mail's subject</param>
        /// <param name="messageText">message's body</param>
        public void Send(string from, string to, string subject, string messageText)
        {
            if (from == null) throw new ArgumentNullException(nameof(@from));
            if (to == null) throw new ArgumentNullException(nameof(to));
            if (subject == null) throw new ArgumentNullException(nameof(subject));
            if (messageText == null) throw new ArgumentNullException(nameof(messageText));

            Send(new Message(from, to, subject, messageText));
        }

        /// <summary>
        ///     Sends a message.
        /// <remarks>
        /// TODO:异步发送时无法发送成功
        /// </remarks>
        /// </summary>
        /// <exception cref="ArgumentNullException">If the message is null</exception>
        /// <param name="message">Message instance</param>
        public void Send(Message message)
        {
            if (message == null) throw new ArgumentNullException(nameof(message));

            ConfigureSender(message);

            if (AsyncSend)
            {
                // The MailMessage must be diposed after sending the email.
                // The code creates a delegate for deleting the mail and adds
                // it to the smtpClient.
                // After the mail is sent, the message is disposed and the
                // eventHandler removed from the smtpClient.
                var msg = CreateMailMessage(message);
                //var msgGuid = new Guid();//ERROR
                var msgGuid = Guid.NewGuid();
                SendCompletedEventHandler sceh = null;
                sceh = delegate(object sender, AsyncCompletedEventArgs e)
                {
                    if (msgGuid == (Guid) e.UserState)
                        msg.Dispose();
                    // The handler itself, cannot be null, test omitted
                    _smtpClient.SendCompleted -= sceh;
                };
                _smtpClient.SendCompleted += sceh;
                _smtpClient.SendAsync(msg, msgGuid);
            }
            else
            {
                using (var msg = CreateMailMessage(message))
                {
                    _smtpClient.Send(msg);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="messages"></param>
        public void Send(Message[] messages)
        {
            foreach (var message in messages)
            {
                Send(message);
            }
        }

        /// <summary>
        ///     Converts a message from Castle.Components.Common.EmailSender.Message  type
        ///     to System.Net.Mail.MailMessage
        /// </summary>
        /// <param name="message">The message to convert.</param>
        /// <returns>The converted message .</returns>
        private static MailMessage CreateMailMessage(Message message)
        {
            var mailMessage = new MailMessage(message.From, message.To.Replace(';', ','));

            if (!string.IsNullOrEmpty(message.Cc))
            {
                mailMessage.CC.Add(message.Cc);
            }

            if (!string.IsNullOrEmpty(message.Bcc))
            {
                mailMessage.Bcc.Add(message.Bcc);
            }

            mailMessage.Subject = message.Subject;
            mailMessage.Body = message.Body;
            mailMessage.BodyEncoding = message.Encoding;
            mailMessage.IsBodyHtml = (message.Format == Format.Html);
            mailMessage.Priority = message.Priority;
            if (message.ReplyTos != null)
            {
                foreach (var replyTo in message.ReplyTos)
                {
                    mailMessage.ReplyToList.Add(replyTo);
                }
            }

            foreach (DictionaryEntry entry in message.Headers)
            {
                mailMessage.Headers.Add((string) entry.Key, (string) entry.Value);
            }

            foreach (var mailAttach in message.Attachments.Select(attachment => attachment.Stream != null
                ? new Attachment(attachment.Stream, attachment.FileName, attachment.MediaType)
                : new Attachment(attachment.FileName, attachment.MediaType)))
            {
                mailMessage.Attachments.Add(mailAttach);
            }

            if (message.Resources == null || message.Resources.Count <= 0) return mailMessage;
            var htmlView = AlternateView.CreateAlternateViewFromString(message.Body, message.Encoding, "text/html");
            foreach (var id in message.Resources.Keys)
            {
                var r = message.Resources[id];
                r.ContentId = id;
                if (r.ContentStream != null)
                {
                    htmlView.LinkedResources.Add(r);
                }
            }
            mailMessage.AlternateViews.Add(htmlView);
            return mailMessage;
        }

        /// <summary>
        ///     Configures the message or the sender
        ///     with port information and eventual credential
        ///     informed
        /// </summary>
        /// <param name="message">Message instance</param>
        protected virtual void ConfigureSender(Message message)
        {
            if (!_configured)
            {
                if (HasCredentials)
                {
                    _smtpClient.Credentials = _credentials;
                }

                _smtpClient.Port = Port;
                _smtpClient.EnableSsl = EnableSsl;
                // REVIEW: might need to do more than this to enable ssl for all smtp servers

                _configured = true;
            }
        }
    }
}