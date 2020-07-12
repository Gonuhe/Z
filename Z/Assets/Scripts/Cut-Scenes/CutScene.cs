using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class CutScene
{
    protected UnityAction callback;

    public CutScene(UnityAction callback)
    {
        this.callback = callback;
    }

    public virtual void StartCutScene()
    {
        FinishCutScene();
    }

    protected virtual void FinishCutScene()
    {
        callback.Invoke();
    }
}
