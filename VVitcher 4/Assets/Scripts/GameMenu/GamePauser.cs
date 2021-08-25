using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GamePauser : MonoBehaviour
{
    public static bool isGamePaused { get; set; }
    private static int pauseCounter;

    private static void UnpauseGame(Scene currentScene, Scene nextScene)
    {
        if(isGamePaused)
        {
            pauseCounter = 0;    
            GameContinue();
        }
    }

    public static void GamePause()
    {
        SceneManager.activeSceneChanged += UnpauseGame;
        pauseCounter++;
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        isGamePaused = true;
    }

    public static void GameContinue()
    {
        if(pauseCounter > 0)
            pauseCounter--;
        if(pauseCounter <= 0)
        {
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            isGamePaused = false;
        }
    }
}
