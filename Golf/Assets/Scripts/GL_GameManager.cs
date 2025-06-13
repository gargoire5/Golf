using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GL_GameManager : MonoBehaviour
{
    public static GL_GameManager Instance;

    public int CurrentP1Score = 0;
    public int CurrentP2Score = 0;

    public int CurrentPlayerPlaying = 0;

    public GL_LevelManager CurrentLevelManager;

    private int _currentSceneIndex = 0;

    private List<string> _levelList = new List<string>();

    public bool IsMultiplayer = false;


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        _levelList.Add("Map1");
        _levelList.Add("Map2");
        _levelList.Add("Map3");
    }

    public void AddP1Score(int ScoreToAdd)
    {
        CurrentP1Score += ScoreToAdd;
    }

    public void AddP2Score(int ScoreToAdd)
    {
        CurrentP2Score += ScoreToAdd;
    }

    public void ChangeLevel()
    {
        _currentSceneIndex++;
        SceneManager.LoadScene(_levelList[_currentSceneIndex],LoadSceneMode.Single);
    }

    public void SwitchPlayer()
    {
        if (CurrentPlayerPlaying == 0)
        {
            CurrentPlayerPlaying = 1;
        }
        else
        {
            CurrentPlayerPlaying = 0;
        }

        CurrentLevelManager.SwitchPlayer(CurrentPlayerPlaying);
    }
}
