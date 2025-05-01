using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Helpers
{
    using FirebaseAdmin.Messaging;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

  
        public interface IPushNotificationService
        {
            Task SendPushNotificationAsync(string deviceToken, string title, string body);
        }

        public class FirebasePushNotificationService : IPushNotificationService
        {
            public async Task SendPushNotificationAsync(string deviceToken, string title, string body)
            {
                var message = new Message()
                {
                    Token = deviceToken,
                    Notification = new Notification()
                    {
                        Title = title,
                        Body = body
                    }
                };

                string response = await FirebaseMessaging.DefaultInstance.SendAsync(message);
            }
        }   

}
