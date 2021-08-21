using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{
    [SerializeField] private GameObject menuFone;

    private void Start()
    {
        CloseMenu();    
    }

    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(menuFone.activeSelf)
                CloseMenu();
            else
                OpenMenu();
        }    
    }

    public void MainMenuButton() => SceneManager.LoadScene(0);
    public void ExitButton() => Application.Quit();

    void OpenMenu()
    {
        GamePauser.GamePause();
        menuFone.SetActive(true);
    }

    void CloseMenu()
    {
        GamePauser.GameContinue();
        menuFone.SetActive(false);
    }
}
