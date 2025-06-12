using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GL_PlayerInfo : MonoBehaviour
{
    private int _currentShot = 0;

    public void Shoot()
    {
        _currentShot++;
    }

    public int GetShot()
    {
        return _currentShot;
    }
}
