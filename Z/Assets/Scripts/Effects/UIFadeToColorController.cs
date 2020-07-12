using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;

[RequireComponent(typeof(Image))]
public class UIFadeToColorController : MonoBehaviour
{
    public float duration = 1.0f;

    private bool _isFading = false;
    public bool isFading { get { return _isFading; } }

    private Color _originColor;

    private Color _targetColor;
    public Color targetColor;

    private float _startTime;

    public UnityEvent OnFadeDone = new UnityEvent();

    private Image _image;

    void Awake ()
    {
        _image = GetComponent<Image>();
    }

    public void Fade()
    {
        _originColor = _image.color;
        _targetColor = targetColor;

        _startTime = Time.time;

        _isFading = true;
    }

    void FixedUpdate()
    {
        if (_isFading)
        {
            float timeSinceStarted = Time.time - _startTime;
            float percentageComplete = timeSinceStarted / duration;

            _image.color = Color.Lerp(_originColor, _targetColor, percentageComplete);

            if (percentageComplete >= 1.0f)
            {
                _isFading = false;
                OnFadeDone.Invoke();
                OnFadeDone.RemoveAllListeners();
            }
        }
    }
}
