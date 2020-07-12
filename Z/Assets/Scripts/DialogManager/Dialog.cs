using UnityEngine;
using System.Collections;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Linq;
using System;

public class Dialog
{
    private const string DIALOG_FOLDER = "Dialog/";
    private const string DEFAULT_DIALOG_PATH = "Dialog/Dialog";
    public const string DIALOG_BEGIN_KEY = "Begin";
    public const string DIALOG_END_KEY = "End";

    public string Key;
    private string _path;
    private Dictionary<string, Sentence> _sentences;

    public Dialog(string key) : this(DEFAULT_DIALOG_PATH, key) { }

    /// <summary>
    /// Load a dialog from XML.
    /// </summary>
    /// <param name="path">the path from the dialog foler</param>
    /// <param name="key"></param>
    public Dialog(string path, string key)
    {
        Key = key;
        _path = DIALOG_FOLDER+path;
        _sentences = new Dictionary<string, Sentence>();
        LoadDialog();
    }

    #region LoadDialog
    private void LoadDialog()
    {
        TextAsset xml = (TextAsset)Resources.Load(_path);
        if (xml == null)
            throw new InvalidOperationException("This dialog file does not exist");
        XDocument xmlAllDialogs = XDocument.Parse(xml.text);

        XElement xmlDialog = xmlAllDialogs.Root.Elements("Dialog").Where(p => (string)p.Attribute("Key") == Key).FirstOrDefault();
        if (xmlDialog == null)
            Debug.LogError("Dialog not found : " + Key);
        else
        {
            foreach (XElement sentenceElement in xmlDialog.Elements("Sentence"))
            {
                try
                {
                    Sentence s = ParseSentence(sentenceElement);
                    _sentences.Add(s.Key, ParseSentence(sentenceElement));
                }
                catch(System.ArgumentException e)
                {
                    Debug.LogError("Error parsing sentence (it will be ignored) : "+e);
                    continue;
                }
            }

        }
    }

    private Sentence ParseSentence(XElement sentence)
    {
        XAttribute sentenceKey = sentence.Attribute("Key");
        if (sentenceKey == null || sentenceKey.Value == null || sentenceKey.Value == "" || _sentences.ContainsKey(sentenceKey.Value))
            throw new System.ArgumentException("A sentence has no key in dialog "+Key);

        List<Choice> choices = new List<Choice>();
        foreach(XElement choice in sentence.Elements("Choice"))
        {
            XAttribute goTo = choice.Attribute("Goto");
            if (goTo == null)
            {
                Debug.LogWarning("No GoTo attribute have been specified for a choice in sentence (" + Key + "," + sentenceKey + ")");
                continue;
            }

            choices.Add(new Choice(choice.Value, goTo.Value));
        }
        return new Sentence(sentenceKey.Value, sentence.Element("Text").Value, choices);
    }

    #endregion

    public Sentence GetSentence(string sentenceKey)
    {
        if (_sentences.ContainsKey(sentenceKey))
            return _sentences[sentenceKey];
        else
            throw new System.ArgumentException("No sentence with this key (" + Key + "," + sentenceKey + ")");
    }
}
