using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GamePauser : MonoBehaviour
{
    public static void GamePause()
    {
        Time.timeScale = 0;
    }

    public static void GameContinue()
    {
        Time.timeScale = 1;
    }
}
