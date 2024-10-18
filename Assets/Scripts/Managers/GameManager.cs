using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private LevelManager[] _lvls;
    [SerializeField] private GameObject _player;

    public static GameManager _instance;

    private GameState _currentState;
    private LevelManager _currentLvl;
    private int _currentLvlIndex = 0;

    public enum GameState
    {
        Intro,
        LvlStart,
        LvlIn,
        LvlEnd,
        GameOver,
        GameEnd
    }

    private void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(_instance);
            return;
        }

        _instance = this;
    }

    private void Start()
    {
        if (_lvls.Length > 0)
        {
            ChangeState(GameState.Intro, _lvls[_currentLvlIndex]);
        }
    }

    public void ChangeState(GameState state, LevelManager lvl)
    {
        _currentState = state;
        _currentLvl = lvl;

        switch (_currentState) //type switch press tab twice, add the state then tab twice
        {
            case GameState.Intro:
                StartIntro();
                break;
            case GameState.LvlStart:
                InitiateLvl();
                break;
            case GameState.LvlIn:
                RunLvl();
                break;
            case GameState.LvlEnd:
                CompleteLvl();
                break;
            case GameState.GameOver:
                GameOver();
                break;
            case GameState.GameEnd:
                GameEnd();
                break;
            default:
                break;
        }

    }

    private void StartIntro()
    {
        Debug.Log("Intro Started...");

        ChangeState(GameState.LvlStart, _currentLvl);
    }

    private void InitiateLvl()
    {
        Debug.Log("Level Start");

        _currentLvl.StartLvl();
        ChangeState(GameState.LvlIn, _currentLvl);
    }

    private void RunLvl()
    {
        Debug.Log("Level in " + _currentLvl.gameObject.name);
    }

    private void CompleteLvl()
    {
        Debug.Log("Level End");

        //go to the next lvl
        ChangeState(GameState.LvlStart, _lvls[++_currentLvlIndex]); //Will be 1 instead of 0 with [_currentlvlindex++]
    }

    private void GameOver()
    {
        _player.SetActive(false);    
        Debug.Log("Game Over, you Lose");
    }

    private void GameEnd()
    {
        _player.SetActive(false);
        Debug.Log("Game Over, you Win");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

}
