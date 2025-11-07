using System;
using UnityEngine;

public class GameEventsManager : MonoBehaviour
{
    public static GameEventsManager instance { get; private set; }

    public void Awake()
    {
        instance = this;
    }
    public event Action OnPlayerDeath;
    public void PlayerDeath()
    {
        if (OnPlayerDeath != null)
        {
            OnPlayerDeath();
        }
    }
    public event Action OnWin;
    public void Win()
    {
        if (OnWin != null)
        {
            OnWin();
        }
    }
}
