using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SimpleTranslationCutScene : CutScene
{

    private GameObject _mover;
    MoveToController _moverMoveToController;

    private Vector3 _targetPosition;

    private int _duration;

    public SimpleTranslationCutScene(GameObject mover, Vector3 targetPosition, int duration, UnityAction callback) : base(callback)
    {
        this._mover = mover;
        this._targetPosition = targetPosition;
        this._duration = duration;
    }

    public override void StartCutScene()
    {
        _moverMoveToController = _mover.GetComponent<MoveToController>();
        if (_moverMoveToController == null)
            _moverMoveToController = _mover.AddComponent<MoveToController>();

        _moverMoveToController.duration = _duration;
        _moverMoveToController.MovementDone.AddListener(FinishCutScene);
        _moverMoveToController.MoveTo(_targetPosition);
    }

    protected override void FinishCutScene()
    {
        _moverMoveToController.MovementDone.RemoveListener(FinishCutScene);
        base.FinishCutScene();
    }
}
