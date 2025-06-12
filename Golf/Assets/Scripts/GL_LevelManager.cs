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

    // Start is called before the first frame update
    void Start()
    {
        _player1 = GameObject.Find("Player1").GetComponent<GL_PlayerInfo>();
        _player2 = GameObject.Find("Player2").GetComponent<GL_PlayerInfo>();

        GL_GameManager.Instance.CurrentLevelManager = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPlayer1(GL_PlayerInfo PlayerToSet)
    { _player1 = PlayerToSet; }

    public void SetPlayer2(GL_PlayerInfo PlayerToSet)
    { _player2 = PlayerToSet; }

    public void SetPlayer1Score(int ScoreToSet)
    { _p1Score = ScoreToSet; }

    public void SetPlayer2Score(int ScoreToSet)
    { _p2Score = ScoreToSet; }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "PlayerBall")
        {
            if (collision.transform.gameObject == _player1.gameObject)
            {
                _p1Score = _player1.GetShot();
                GL_GameManager.Instance.AddP1Score(_p1Score);
            }

            else if (collision.transform.gameObject == _player2.gameObject)
            {
                _p2Score = _player2.GetShot();
                GL_GameManager.Instance.AddP1Score(_p2Score);
            }
        }
    }
}
