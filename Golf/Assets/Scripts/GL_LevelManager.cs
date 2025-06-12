using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GL_LevelManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _hole;

    [SerializeField]
    private string _name;

    [SerializeField]
    private int _par;

    private int _p1Score = 0;
    private int _p2Score = 0;

    private GL_PlayerInfo _player1 = null;
    private GL_PlayerInfo _player2 = null;

    private bool _p1HasEnded = false;
    private bool _p2HasEnded = false;

    // Start is called before the first frame update
    void Start()
    {
        _player1 = GameObject.Find("Player1").GetComponent<GL_PlayerInfo>();

        if (GL_GameManager.Instance.IsMultiplayer)
            _player2 = GameObject.Find("Player2").GetComponent<GL_PlayerInfo>();
        else
            Destroy(GameObject.Find("Player2"));

        GL_GameManager.Instance.CurrentLevelManager = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (GL_GameManager.Instance.IsMultiplayer)
        {
            if (_p1HasEnded)
            {
                GL_GameManager.Instance.ChangeLevel();
            }
        }
        else
        {
            if (_p1HasEnded && _p2HasEnded)
            {
                GL_GameManager.Instance.ChangeLevel();
            }
        }
    }

    public void SetPlayer1(GL_PlayerInfo PlayerToSet)
    { _player1 = PlayerToSet; }

    public void SetPlayer2(GL_PlayerInfo PlayerToSet)
    { _player2 = PlayerToSet; }

    public void SetPlayer1Score(int ScoreToSet)
    {
        _p1Score = ScoreToSet;
        if (_p1Score == 20)
        {
            _p1HasEnded = true;
        }
    }

    public void SetPlayer2Score(int ScoreToSet)
    {
        _p2Score = ScoreToSet;
        if (_p2Score == 20)
        {
            _p2HasEnded = true;
        }
    }

    public void SwitchPlayer(int ID)
    {
        switch (ID)
        {
            case 0:
                _player2.gameObject.SetActive(false);
                _player1.gameObject.SetActive(true);
                break;
            case 1:
                _player1.gameObject.SetActive(false);
                _player2.gameObject.SetActive(true);
                break;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "PlayerBall")
        {
            if (collision.transform.gameObject == _player1.gameObject)
            {
                _p1Score = _player1.GetShot();
                GL_GameManager.Instance.AddP1Score(_p1Score);
                _p1HasEnded = true;
                Debug.Log("Victoire");
            }

            else if (collision.transform.gameObject == _player2.gameObject)
            {
                _p2Score = _player2.GetShot();
                GL_GameManager.Instance.AddP1Score(_p2Score);
                _p2HasEnded = true;
            }
        }
    }
}
