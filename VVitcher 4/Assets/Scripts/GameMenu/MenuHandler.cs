using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _deathMenu;

    private bool _isGamePaused;

    public bool isGamePaused
    {
        get { return _isGamePaused; }
        private set { _isGamePaused = value; }
    }

    private void Start()
    {
        if(_pauseMenu != null)
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

    public void Restart() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    public void NewGame() => SceneManager.LoadScene(1);
    public void StartGame() => SceneManager.LoadScene(2);
    public void EndGame() => SceneManager.LoadScene(4);
    public void MainMenuButton() => SceneManager.LoadScene(0);
    public void ExitButton() => Application.Quit();

    public void OpenMenu(GameObject menu)
    {
        GamePauser.GamePause();
        menu.SetActive(true);
        isGamePaused = true;
    }

    public void CloseMenu(GameObject menu)
    {
        GamePauser.GameContinue();
        menu.SetActive(false);
        isGamePaused = false;
    }

    private void DeathMenu()
    {
        StartCoroutine(DeathMenuOpener());
    }

    IEnumerator DeathMenuOpener()
    {
        yield return new WaitForSeconds(2f);
        OpenMenu(_deathMenu);
    }
}
