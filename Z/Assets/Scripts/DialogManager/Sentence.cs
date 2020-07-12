using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Sentence
{
    public string Key;

    public string Text;

    public List<Choice> Choices;

    public Sentence(string key, string text, List<Choice> choices)
    {
        this.Key = key;
        this.Text = text;
        this.Choices = choices;
    }
}

public class Choice
{
    public string GoTo;
    public string Text;

    public Choice(string text, string goTo)
    {
        this.Text = text;
        this.GoTo = goTo;
    }
}
