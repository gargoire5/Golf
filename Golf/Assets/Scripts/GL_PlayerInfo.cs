using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GL_PlayerInfo : MonoBehaviour
{
    private int _currentShot = 0;
    private int _playerID;

    private void Start()
    {
        if (gameObject.name == "Player1")
        {
            _playerID = 0;
        }
        else
        {
            _playerID = 1;
        }
    }

    public void Shoot()
    {
        _currentShot++;
        if (_playerID == 0)
        {
            GL_GameManager.Instance.CurrentLevelManager.SetPlayer1Score(_currentShot);
        }
        else
        {
            GL_GameManager.Instance.CurrentLevelManager.SetPlayer2Score(_currentShot);
        }
    }

    public int GetShot()
    {
        return _currentShot;
    }
}
