using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
#if UNITY_IOS
using Unity.Notifications.iOS;
#endif

#if UNITY_IOS

public class NotificationManagerIOS : MonoBehaviour
{
    private string notificationID = "Record_notification";
    private int ID;
    private string RecordTitle, RecordBody;
    [Header("Время до уведомления")] public int recordDelay;

    private void Start()
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

        iOSNotificationTimeIntervalTrigger timeTrigger = new iOSNotificationTimeIntervalTrigger()
        {
            TimeInterval = new TimeSpan(recordDelay, 0, 0),
            Repeats = false
        };

        iOSNotificationCalendarTrigger calendarTrigger = new iOSNotificationCalendarTrigger()
        {
            // Year = xxxx,
            // Month = xx,
            // Day = xx,
               Hour = 12,
               Minute = 0,
            // Second = xx,
               Repeats = true
        };

        iOSNotificationLocationTrigger locationTrigger = new iOSNotificationLocationTrigger()
        {
            Center = new Vector2(2.3f , 49f),
            Radius = 250f,
            NotifyOnEntry = true,
            NotifyOnExit = false
        };

        iOSNotification notification = new iOSNotification()
        {
            Identifier = notificationID,
            Title = RecordTitle,
            Body = RecordBody,
            ShowInForeground = true,
            ForegroundPresentationOption = (PresentationOption.Alert | PresentationOption.Sound),
            CategoryIdentifier = "category_a",
            ThreadIdentifier = "thread1",
            Trigger = calendarTrigger
        };

        iOSNotificationCenter.ScheduleNotification(notification);

        iOSNotificationCenter.OnRemoteNotificationReceived += recievedNotification =>
        {
            Debug.Log("Recieved notification " + notification.Identifier + "!");
        };

        iOSNotification notificationIntentData = iOSNotificationCenter.GetLastRespondedNotification();
    }

    private void OnApplicationPause(bool pause)
    {
        iOSNotificationCenter.RemoveScheduledNotification(notificationID);

        iOSNotificationCenter.RemoveDeliveredNotification(notificationID);
    }
}

#endif