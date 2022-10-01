# if UNAVINAR_NOTIFICATIONS
using System;
using System.Collections.Generic;
using Unity.Notifications.Android;
using UnityEngine;
using Random = UnityEngine.Random;


namespace Unavinar.Notifications
{
    public class NotificationManager : MonoBehaviour
    {
        /// <summary>
        /// Disable all notifications before first game scene loads
        /// </summary>
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Execute()
        {
            AndroidNotificationCenter.Initialize();
            AndroidNotificationCenter.CancelAllNotifications();
        }

        [SerializeField] private string notificationTitle = "Unavinar";
        [SerializeField] private List<NotificationChannel> channels;

        /// <summary>
        /// Your body text for notifications
        /// \U00000000 - is 32 bit emoji
        /// </summary>
        private string[] _notificationText =
        {
            "\U0001F62C Enemy troops are approaching!",
            "\U0001F93A It's time for an advance!",
            "\U0001F916 Assemble your troops! The boss is approaching!",
            "\U0001F3AF Snipers are reporting for duty!",
            "\U0001F418 Tanks are filled up and ready to go!",
            "\U0001F985 Let's call for air support!"
        };
        
        private void Start()
        {
            Debug.Assert(channels.Count > 0, "Channels Count is <= 0");

            // Register all Notifications Channels
            foreach (var channel in channels)
            {
                RegisterNotificationChannel(channel.Id, channel.ChannelName, channel.Importance, channel.Description);
            }

            // Schedule all notifications here
            ScheduleNotification(channels[0].Id, notificationTitle,
                _notificationText[Random.Range(0, _notificationText.Length)], DateTime.Now.AddHours(8));
            ScheduleNotification(channels[0].Id, notificationTitle,
                _notificationText[Random.Range(0, _notificationText.Length)], DateTime.Now.AddHours(16));
        }

        private void RegisterNotificationChannel(string id, string channelName, Importance importance, string description)
        {
            AndroidNotificationChannel notificationChannel = new AndroidNotificationChannel()
            {
                Id = id,
                Name = channelName,
                Importance = importance,
                Description = description
            };
            
            AndroidNotificationCenter.RegisterNotificationChannel(notificationChannel);
        }

        private void ScheduleNotification(string channelId, string title, string text, DateTime time)
        {
            AndroidNotification notification = new AndroidNotification();
            notification.Title = title;
            notification.Text = text;
            notification.SmallIcon = "small";
            notification.LargeIcon = "large";
            notification.ShowTimestamp = true;
            notification.FireTime = time;

            AndroidNotificationCenter.SendNotification(notification, channelId);
        }
    }

    [Serializable]
    public struct NotificationChannel
    {
        public string Id;
        public string ChannelName;
        public Importance Importance;
        [Multiline]
        public string Description;
    }
}
#endif
