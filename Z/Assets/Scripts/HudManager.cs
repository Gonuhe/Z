using UnityEngine;
using System.Collections;

public class HudManager : MonoBehaviour {

    GameObject _dialogUI;
    DialogManager _dialogUIManager;
    public NotificationManager NotificationManager;

    private void Awake()
    {
        _dialogUI = transform.Find("DialogUI").gameObject;
        _dialogUIManager = _dialogUI.GetComponent<DialogManager>();
        _dialogUI.SetActive(false);

        NotificationManager = FindObjectOfType<NotificationManager>();
    }

    public DialogManager ShowDialogUI()
    {
        _dialogUI.SetActive(true);
        return _dialogUIManager;
    }

}
