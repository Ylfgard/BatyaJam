using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable] public class VolumeSliders
{
    public Slider _masterVolumeSlider, _musicVolumeSlider, _SFXVolumeSlider, _UIVolumeSlider, _voiceVolumeSlider;
}

public class MenuHandler : MonoBehaviour
{
    [SerializeField] private GameObject _startMenu, _settingsMenu, _controlsMenu, _pauseMenu, _gameOverMenu;
    [SerializeField] private VolumeSliders vSliders;

    [SerializeField] private float deathMenuDelay = 4f;

    private FMOD.Studio.VCA masterVca, musicVca, SFXVca, UIVca, voiceVca;
    private float _masterVolume, _musicVolume, _SFXVolume, _UIVolume, _voiceVolume;

    private void Start()
    {
        #region QualitySettings
        Debug.Log("Note: VSync was set by MenuHandler.cs");
        if(Screen.currentResolution.refreshRate <= 60)
            QualitySettings.vSyncCount = 1;
        else
            QualitySettings.vSyncCount = 2;
        #endregion

        #region SoundSettings
        masterVca = FMODUnity.RuntimeManager.GetVCA("vca:/Master");
        musicVca = FMODUnity.RuntimeManager.GetVCA("vca:/Music");
        SFXVca = FMODUnity.RuntimeManager.GetVCA("vca:/SFX");
        UIVca = FMODUnity.RuntimeManager.GetVCA("vca:/UI");
        voiceVca = FMODUnity.RuntimeManager.GetVCA("vca:/Voice");
        LoadVolumeLevels();
        #endregion

        //if (_pauseMenu != null)
        //    ClosePauseMenu(_pauseMenu);
        //if (_gameOverMenu != null)
        //{
        //    //ClosePauseMenu(_gameOverMenu);
        //    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMain>().onPlayerDeadCallback += GameOverMenu;
        //}

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null) player.GetComponent<PlayerMain>().onPlayerDeadCallback += GameOverMenu;

        Cursor.lockState = CursorLockMode.None;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && _pauseMenu != null)
        {
            if(_pauseMenu.activeSelf)
                ClosePauseMenu(_pauseMenu);
            else
                OpenPauseMenu(_pauseMenu);
        }    
    }

    public void NewGame() => SceneManager.LoadScene(1);
    public void StartGame()
    {
        SceneManager.LoadScene(2);
        if (Time.timeScale != 1) GamePauser.GameContinue();
    } 
    public void EndGame() => SceneManager.LoadScene(4);
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        if(Time.timeScale != 1) GamePauser.GameContinue();
        Cursor.lockState = CursorLockMode.Locked;
    } 

    public void MainMenuButton() => SceneManager.LoadScene(0);
    public void ExitButton() => Application.Quit();

    public void OpenPauseMenu(GameObject menu)
    {
        GamePauser.GamePause();
        menu.SetActive(true);
    }
    public void ClosePauseMenu(GameObject menu)
    {
        GamePauser.GameContinue();
        menu.SetActive(false);
    }

    private void GameOverMenu() => StartCoroutine(GameOverMenuCoroutine());
    IEnumerator GameOverMenuCoroutine()
    {
        yield return new WaitForSeconds(deathMenuDelay);
        OpenPauseMenu(_gameOverMenu);
    }

    public void OpenSettingsMenu() => _settingsMenu.SetActive(true);
    public void CloseSettingsMenu()
    {
        SaveVolumeLevels();
        _settingsMenu.SetActive(false);
    }


    public void SetMasterVolume(float volume)
    {
        masterVca.setVolume(volume);
        _masterVolume = volume;
    }
    public void SetMusicVolume(float volume)
    {
        musicVca.setVolume(volume);
        _musicVolume = volume;
    }
    public void SetSFXVolume(float volume)
    {
        SFXVca.setVolume(volume);
        _SFXVolume = volume;
    }
    public void SetUIVolume(float volume)
    {
        UIVca.setVolume(volume);
        _UIVolume = volume;
    }
    public void SetVoiceVolume(float volume)
    {
        voiceVca.setVolume(volume);
        _voiceVolume = volume;
    }
    private void SaveVolumeLevels()
    {
        PlayerPrefs.SetFloat("MasterVolume", _masterVolume);
        PlayerPrefs.SetFloat("MusicVolume", _musicVolume);
        PlayerPrefs.SetFloat("SFXVolume", _SFXVolume);
        PlayerPrefs.SetFloat("UIVolume", _UIVolume);
        PlayerPrefs.SetFloat("VoiceVolume", _voiceVolume);
        PlayerPrefs.Save();
    }
    private void LoadVolumeLevels()
    {
        if (PlayerPrefs.HasKey("MasterVolume"))
        {
            _masterVolume = PlayerPrefs.GetFloat("MasterVolume");
            vSliders._masterVolumeSlider.value = _masterVolume;
            masterVca.setVolume(_masterVolume);
        }
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            _musicVolume = PlayerPrefs.GetFloat("MusicVolume");
            vSliders._musicVolumeSlider.value = _musicVolume;
            musicVca.setVolume(_musicVolume);
        }
        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            _SFXVolume = PlayerPrefs.GetFloat("SFXVolume");
            vSliders._SFXVolumeSlider.value = _SFXVolume;
            SFXVca.setVolume(_SFXVolume);
        }
        if (PlayerPrefs.HasKey("UIVolume"))
        {
            _UIVolume = PlayerPrefs.GetFloat("UIVolume");
            vSliders._UIVolumeSlider.value = _UIVolume;
            UIVca.setVolume(_UIVolume);
        }
        if (PlayerPrefs.HasKey("VoiceVolume"))
        {
            _voiceVolume = PlayerPrefs.GetFloat("VoiceVolume");
            vSliders._voiceVolumeSlider.value = _voiceVolume;
            voiceVca.setVolume(_voiceVolume);
        }
    }
}
