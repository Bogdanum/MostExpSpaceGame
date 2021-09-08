using UnityEngine;
#if UNITY_ANDROID
using Unity.Notifications.Android;

public class NotificationManager : MonoBehaviour
{
	
	AndroidNotificationChannel defaultChannel;
	private string RecordTitle, RecordBody;
	[Header("Время до уведомления(ч)")] public int recordDelay;

    void Start()
    {
		switch (PlayerPrefs.GetInt("Language", 1))
        {
			case 0:
				RecordTitle = "Your ship is ready for battle!";
				RecordBody = "It's time to become the best!";
				break;
			case 1:
				RecordTitle = "Ваш корабль готов к бою!";
				RecordBody = "Самое время стать лучшим!";
				break;
			case 2:
				RecordTitle = "你的船准备战斗！";
				RecordBody = "是时候成为最好的了！";
				break;
			case 3:
				RecordTitle = "Ihr Schiff ist bereit für den Kampf!";
				RecordBody = "Es ist Zeit, der Beste zu werden!";
				break;
			case 4:
				RecordTitle = "あなたの船は戦いの準備ができています！";
				RecordBody = "最高になる時が来ました！";
				break;
			case 5:
				RecordTitle = "Votre navire est prêt pour la bataille !";
				RecordBody = "Il est temps de devenir le meilleur !";
				break;
			case 6:
				RecordTitle = "Seu navio está pronto para a batalha!";
				RecordBody = "É hora de se tornar o melhor!";
				break;
			case 7:
				RecordTitle = "Tu nave esta lista para la batalla!";
				RecordBody = "¡Es hora de convertirse en el mejor!";
				break;
			case 8:
				RecordTitle = "Geminiz savaşa hazır!";
				RecordBody = "En iyi olma zamanı!";
				break;
			case 9:
				RecordTitle = "Ваш корабель готовий до бою!";
				RecordBody = "Саме час бути кращим!";
				break;
		}

		AndroidNotificationCenter.CancelAllNotifications();

		defaultChannel = new AndroidNotificationChannel()
		{
			Id = "MESG",
			Name = "MESG",
			Description = "Generic Notification",
			EnableLights = true,
			Importance = Importance.Default
		};

		AndroidNotificationCenter.RegisterNotificationChannel(defaultChannel);

			AndroidNotification notification = new AndroidNotification()
			{
				Title = RecordTitle,
				Text = RecordBody,
				LargeIcon = "app_icon_large",
				FireTime = System.DateTime.Now.AddHours(recordDelay)
			};
			AndroidNotificationCenter.SendNotification(notification, defaultChannel.Id);

		
		
		AndroidNotificationCenter.NotificationReceivedCallback receivedNotificationHandler = delegate (AndroidNotificationIntentData data)
		{
			var msg = "Notification received: " + data.Id + "\n";
			msg += "\n Notification received: ";
			msg += "\n .Title: " + data.Notification.Title;
			msg += "\n .Body: " + data.Notification.Text;
			msg += "\n .Channel: " + data.Channel;
			Debug.Log(msg);
		};

		AndroidNotificationCenter.OnNotificationReceived += receivedNotificationHandler;

		var notificationIntentData = AndroidNotificationCenter.GetLastNotificationIntent();

		if (notificationIntentData != null) Debug.Log("App was opened with notifcation!");
	}
    
}
#endif