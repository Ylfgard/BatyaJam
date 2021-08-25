using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _deathMenu;

    [SerializeField] private float deathMenuDelay = 4f;

    private void Start()
    {
        if(Screen.currentResolution.refreshRate <= 60)
            QualitySettings.vSyncCount = 1;
        else
            QualitySettings.vSyncCount = 2;

        Debug.Log("Note: VSync was set by MenuHandler.cs");

        Cursor.lockState = CursorLockMode.None;

        if (_pauseMenu != null)
            CloseMenu(_pauseMenu);
        if(_deathMenu != null)
        {
            CloseMenu(_deathMenu);
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMain>().onPlayerDeadCallback += DeathMenu;
        }  
    }

    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.Escape) && _pauseMenu != null)
        {
            if(_pauseMenu != null)
            {
                if(_pauseMenu.activeSelf)
                    CloseMenu(_pauseMenu);
                else
                    OpenMenu(_pauseMenu);
            }
        }    
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        GamePauser.GameContinue();
    } 
    public void NewGame() => SceneManager.LoadScene(1);
    public void StartGame()
    {
        SceneManager.LoadScene(2);
        GamePauser.GameContinue();
    } 
    public void EndGame() => SceneManager.LoadScene(4);
    public void MainMenuButton() => SceneManager.LoadScene(0);
    public void ExitButton() => Application.Quit();

    public void OpenMenu(GameObject menu)
    {
        GamePauser.GamePause();
        menu.SetActive(true);
    }

    public void CloseMenu(GameObject menu)
    {
        GamePauser.GameContinue();
        menu.SetActive(false);
    }

    private void DeathMenu()
    {
        StartCoroutine(DeathMenuOpener());
    }

    IEnumerator DeathMenuOpener()
    {
        yield return new WaitForSeconds(deathMenuDelay);
        OpenMenu(_deathMenu);
    }
}
