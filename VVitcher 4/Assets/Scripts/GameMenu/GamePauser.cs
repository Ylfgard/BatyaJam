using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GamePauser : MonoBehaviour
{
    public static bool isGamePaused { get; set; }

    public static void GamePause()
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        isGamePaused = true;
    }

    public static void GameContinue()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        isGamePaused = false;
    }
}
