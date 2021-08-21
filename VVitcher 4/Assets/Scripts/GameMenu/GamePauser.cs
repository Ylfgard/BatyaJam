using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GamePauser : MonoBehaviour
{
    public static void PauseGame()
    {
        Time.timeScale = 0;
    }

    public static void PauseContinue()
    {
        Time.timeScale = 1;
    }
}
