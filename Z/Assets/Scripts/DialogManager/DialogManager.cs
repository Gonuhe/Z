using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Events;

//TODO séparer le modèle du DialogManager et la vue
//TODO supporté l'injection de paramètres, comme le nom du héros.
public class DialogManager : MonoBehaviour
{

    public const int CHOICES_LIMIT = 4;
    public Color SELECTED_CHOICE_COLOR;
    public Color UNSELECTED_CHOICE_COLOR = new Color(1,1,1,1);

    private Text _mainDialogUI;
    private List<GameObject> _choicesUI = new List<GameObject>(CHOICES_LIMIT);

    private Dialog _currentDialog;
    private UnityAction _callback;

    private Sentence _currentSentence;
    private int _selectedChoice = -1;

    private void Awake()
    {
        _mainDialogUI = transform.Find("MainText").GetComponentInChildren<Text>();
        GameObject choices = transform.Find("Choices").gameObject;
        _choicesUI.Add(choices.transform.Find("Choice1").gameObject);
        _choicesUI.Add(choices.transform.Find("Choice2").gameObject);
        _choicesUI.Add(choices.transform.Find("Choice3").gameObject);
        _choicesUI.Add(choices.transform.Find("Choice4").gameObject);
    }

    private void Update()
    {
        if(_currentDialog != null)
        {
            if(Input.GetKeyDown(KeyCode.DownArrow))
                SelectChoice((_selectedChoice+1) % _currentSentence.Choices.Count);
            if (Input.GetKeyDown(KeyCode.E))
                ValidateChoice();
            if (Input.GetKeyUp(KeyCode.UpArrow))
            {
                int toSelect = _selectedChoice - 1;
                if (toSelect < 0)
                    toSelect += _currentSentence.Choices.Count;
                SelectChoice(toSelect);
            }
        }
    }

    public void ShowDialog(Dialog dialog, UnityAction callback)
    {
        _currentDialog = dialog;
        _callback = callback;
        ShowSentence(dialog.GetSentence(Dialog.DIALOG_BEGIN_KEY));
    }

    private void ShowSentence(Sentence sentence)
    {
        _currentSentence = sentence;
        _mainDialogUI.text = sentence.Text;

        if (sentence.Choices.Count > _choicesUI.Count)
            throw new System.InvalidOperationException("Not enough choices UI to show the number of choices (only "+CHOICES_LIMIT+" are permitted)."); //or _choices have not been correctly initialised.

        for (int i = 0; i < _choicesUI.Count; i++)
        {
            if (i >= sentence.Choices.Count)
                _choicesUI[i].SetActive(false);
            else
            {
                _choicesUI[i].SetActive(true);
                _choicesUI[i].GetComponentInChildren<Text>().text = sentence.Choices[i].Text;
            }
        }

        if (sentence.Choices.Count == 0)
            _selectedChoice = -1; //TODO this is not really supported : validate does not work
        else
            SelectChoice(0);
    }

    private void SelectChoice(int i)
    {
        if(_selectedChoice != -1)
        {
            _choicesUI[_selectedChoice].GetComponentInChildren<Image>().color = UNSELECTED_CHOICE_COLOR;
        }
        _choicesUI[i].GetComponentInChildren<Image>().color = SELECTED_CHOICE_COLOR;
        _selectedChoice = i;
    }

    private void ValidateChoice()
    {
        _choicesUI[_selectedChoice].GetComponentInChildren<Image>().color = UNSELECTED_CHOICE_COLOR;
        string nextSentenceKey = _currentSentence.Choices[_selectedChoice].GoTo;
        if (nextSentenceKey == Dialog.DIALOG_END_KEY)
            EndDialog();
        else
            ShowSentence(_currentDialog.GetSentence(nextSentenceKey));

    }

    private void EndDialog()
    {
        _currentDialog = null;
        _currentSentence = null;
        _selectedChoice = -1;
        _callback.Invoke();
    }

}
