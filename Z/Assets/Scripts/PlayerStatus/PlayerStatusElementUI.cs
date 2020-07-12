using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerStatusElementUI : MonoBehaviour
{

    public PlayerStatusElement source;
    Slider _slider;

    private void Awake()
    {
        _slider = GetComponentInChildren<Slider>();
    }


    void Update ()
    {
        if(source != null)
        {
            _slider.value = source.Value / source.MaxValue;
        }
    }
}
