using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NotificationManager : MonoBehaviour
{

    private Text _notificationsText;

    public float NotificationTime;

    private float _currentTime;

    void Awake ()
    {
        _notificationsText = GetComponent<Text>();
        _notificationsText.text = "";
    }
    
    void Update ()
    {
        if(_currentTime > 0)
        {
            _currentTime -= Time.deltaTime;
            if (_currentTime <= 0)
                _notificationsText.text = "";
        }
    }

    public void ShowNotification(string notif)
    {
        _notificationsText.text = notif;
        _currentTime = NotificationTime;
    }
}
