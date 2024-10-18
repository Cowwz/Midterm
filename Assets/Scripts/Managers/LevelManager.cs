using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private bool _isFinalLvl;

    public UnityEvent onLvlStart;
    public UnityEvent onLvlEnd;
    
    public void StartLvl()
    {
        onLvlStart?.Invoke();
    }

    public void EndLvl()
    {
        onLvlEnd?.Invoke();

        if (_isFinalLvl)
        {
            GameManager._instance.ChangeState(GameManager.GameState.GameEnd, this);
        }
        else
        {
            GameManager._instance.ChangeState(GameManager.GameState.LvlEnd, this);
        }
    }
}
