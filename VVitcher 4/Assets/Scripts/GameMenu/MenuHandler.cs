using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

[System.Serializable] public class VolumeSliders
{
    public Slider _masterVolumeSlider, _musicVolumeSlider, _SFXVolumeSlider, _UIVolumeSlider, _voiceVolumeSlider;
}

public class MenuHandler : MonoBehaviour
{
    [SerializeField] private GameObject _startMenu, _settingsMenu, _controlsMenu, _pauseMenu, _gameOverMenu;
    [SerializeField] private VolumeSliders vSliders;
    [SerializeField] private Image _loadingImage;
    [SerializeField] private Image _radialLoadingImage;
    [SerializeField] private TextMeshProUGUI versionText;
    [SerializeField] private ComicsPlayer comicsPlayer;

    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private Toggle fullscreenToggle;

    [SerializeField] private float deathMenuDelay = 4f;

    private FMOD.Studio.VCA masterVca, musicVca, SFXVca, UIVca, voiceVca;
    private float _masterVolume, _musicVolume, _SFXVolume, _UIVolume, _voiceVolume;

    private float t;
    private float currentAlpha;
    private float _fadeOutTime = 1f;
    private bool _isFullscreen;

    private Resolution currentResolution;

    private void Start()
    {
        if (versionText != null && versionText.isActiveAndEnabled) versionText.text = "v " + Application.version;

        #region VideoQualitySettings

        Debug.Log("Note: VSync was set by MenuHandler.cs");
        if (Screen.currentResolution.refreshRate <= 60)
            QualitySettings.vSyncCount = 1;
        else
            QualitySettings.vSyncCount = 2;

        LoadResolution();
        InitResolutionVideoSettings();

        #endregion

        #region SoundSettings

        masterVca = FMODUnity.RuntimeManager.GetVCA("vca:/Master");
        musicVca = FMODUnity.RuntimeManager.GetVCA("vca:/Music");
        SFXVca = FMODUnity.RuntimeManager.GetVCA("vca:/SFX");
        UIVca = FMODUnity.RuntimeManager.GetVCA("vca:/UI");
        voiceVca = FMODUnity.RuntimeManager.GetVCA("vca:/Voice");

        LoadVolumeLevels();

        #endregion

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null) player.GetComponent<PlayerMain>().onPlayerDeadCallback += GameOverMenu;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().buildIndex == 2 && !_pauseMenu.activeInHierarchy)
        {
            if(_pauseMenu.activeSelf)
                ClosePauseMenu(_pauseMenu);
            else
                OpenPauseMenu(_pauseMenu);
        }    
    }

    public void NewGame()
    {
        StartCoroutine(FadeOutDelayToLoadSceneCoroutine(1));
        //SceneManager.LoadScene(1);
    }
    public void StartGame()
    {
        if (Time.timeScale != 1) Time.timeScale = 1;
        StartCoroutine(FadeOutDelayToLoadSceneCoroutine(2));
        //SceneManager.LoadScene(2);
        //if (Time.timeScale != 1) GamePauser.GameContinue();
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
        ApplyResolutionSettings();
        SaveVolumeLevels();

        _settingsMenu.SetActive(false);
    }


    private void LoadResolution()
    {
        int index = PlayerPrefs.GetInt("CurrentResolutionIndex", -1);

        int width = PlayerPrefs.GetInt("Resolution_" + index + "_W", Screen.width);
        int height = PlayerPrefs.GetInt("Resolution_" + index + "_H", Screen.height);
        int refreshRate = PlayerPrefs.GetInt("Resolution_" + index + "_RR", Screen.currentResolution.refreshRate);
        _isFullscreen = (PlayerPrefs.GetInt("FullscreenToggle", 1) == 1) ? true : false;
        Screen.SetResolution(width, height, _isFullscreen, refreshRate);

        if (index >= 0) resolutionDropdown.value = index;
        currentResolution.width = width;
        currentResolution.height = height;
        currentResolution.refreshRate = refreshRate;
    }
    private void InitResolutionVideoSettings()
    {
        for (int i = 0; i < Screen.resolutions.Length; i++)
        {
            Resolution res = Screen.resolutions[i];
            //resolutionDropdown.options[i].text = res.width + " x " + res.height + ", " + res.refreshRate + " Hz";
            TMP_Dropdown.OptionData data = new TMP_Dropdown.OptionData();
            data.text = res.width + " x " + res.height + ", " + res.refreshRate + " Hz";

            PlayerPrefs.SetInt("Resolution_" + i + "_W", res.width);
            PlayerPrefs.SetInt("Resolution_" + i + "_H", res.height);
            PlayerPrefs.SetInt("Resolution_" + i + "_RR", res.refreshRate);

            //resolutionDropdown.options.Add(new TMP_Dropdown.OptionData(resolutionDropdown.options[i].text));
            resolutionDropdown.options.Add(new TMP_Dropdown.OptionData(data.text));

            if (res.width == currentResolution.width &&
                res.height == currentResolution.height &&
                Mathf.Abs(res.refreshRate - currentResolution.refreshRate) < 2)
            {
                //PlayerPrefs.SetInt("CurrentResolutionIndex", i);
                resolutionDropdown.value = i;
                currentResolution = res;
            }
        }
                
        fullscreenToggle.isOn = _isFullscreen;

        PlayerPrefs.SetInt("CurrentResolutionIndex", resolutionDropdown.value);
        SaveCurrentResolutionSettings();
    }
    private void ApplyResolutionSettings()
    {
        int width = PlayerPrefs.GetInt("Resolution_" + resolutionDropdown.value + "_W", Screen.width);
        int height = PlayerPrefs.GetInt("Resolution_" + resolutionDropdown.value + "_H", Screen.height);
        int refreshRate = PlayerPrefs.GetInt("Resolution_" + resolutionDropdown.value + "_RR", Screen.currentResolution.refreshRate);
        _isFullscreen = fullscreenToggle.isOn;

        if (currentResolution.width == width && 
            currentResolution.height == height &&
            currentResolution.refreshRate == refreshRate &&
            Screen.fullScreen == _isFullscreen)
            return;

        Screen.SetResolution(width, height, _isFullscreen, refreshRate);

        currentResolution.width = width;
        currentResolution.height = height;
        currentResolution.refreshRate = refreshRate;

        PlayerPrefs.SetInt("CurrentResolutionIndex", resolutionDropdown.value);
        SaveCurrentResolutionSettings();
    }
    private void SaveCurrentResolutionSettings()
    {
        PlayerPrefs.SetInt("CurrentResolution_W", currentResolution.width);
        PlayerPrefs.SetInt("CurrentResolution_H", currentResolution.height);
        PlayerPrefs.SetInt("CurrentResolution_RR", currentResolution.refreshRate);
        PlayerPrefs.SetInt("FullscreenToggle", _isFullscreen ? 1 : 0);
        PlayerPrefs.Save();
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
        if (!PlayerPrefs.HasKey("MasterVolume"))
            _masterVolume = vSliders._masterVolumeSlider.value;
        else
        {
            _masterVolume = PlayerPrefs.GetFloat("MasterVolume");
            vSliders._masterVolumeSlider.value = _masterVolume;
        }
        masterVca.setVolume(_masterVolume);

        if (!PlayerPrefs.HasKey("MusicVolume"))
            _musicVolume = vSliders._musicVolumeSlider.value;
        else
        {
            _musicVolume = PlayerPrefs.GetFloat("MusicVolume");
            vSliders._musicVolumeSlider.value = _musicVolume;
        }
            musicVca.setVolume(_musicVolume);

        if (!PlayerPrefs.HasKey("SFXVolume"))
            _SFXVolume = vSliders._SFXVolumeSlider.value;
        else
        {
            _SFXVolume = PlayerPrefs.GetFloat("SFXVolume");
            vSliders._SFXVolumeSlider.value = _SFXVolume;
        }
            SFXVca.setVolume(_SFXVolume);

        if (!PlayerPrefs.HasKey("UIVolume"))
            _UIVolume = vSliders._UIVolumeSlider.value;
        else
        {
            _UIVolume = PlayerPrefs.GetFloat("UIVolume");
            vSliders._UIVolumeSlider.value = _UIVolume;
        }
            UIVca.setVolume(_UIVolume);

        if (!PlayerPrefs.HasKey("VoiceVolume"))
            _voiceVolume = vSliders._voiceVolumeSlider.value;
        else
        {
            _voiceVolume = PlayerPrefs.GetFloat("VoiceVolume");
            vSliders._voiceVolumeSlider.value = _voiceVolume;
        }
            voiceVca.setVolume(_voiceVolume);
    }


    IEnumerator FadeOutDelayToLoadSceneCoroutine(int ndx)
    {
        yield return new WaitForSeconds(0.2f);

        currentAlpha = 0;
        t = 0;
        StartCoroutine(FadeOutToLoadCoroutine(ndx));
    }
    IEnumerator FadeOutToLoadCoroutine(int ndx)
    {
        t += Time.deltaTime / _fadeOutTime;
        currentAlpha = Mathf.Lerp(0, 1, t);
        _loadingImage.color = new Color(0, 0, 0, currentAlpha);

        if (currentAlpha >= 1)
        {
            StartCoroutine(LoadAsync(ndx));
            yield break;
        }

        yield return null;

        StartCoroutine(FadeOutToLoadCoroutine(ndx));
    }
    IEnumerator LoadAsync(int ndx)
    {
        AsyncOperation aLoad = SceneManager.LoadSceneAsync(ndx);
        aLoad.allowSceneActivation = false;

        while (!aLoad.isDone && !aLoad.allowSceneActivation)
        {
            _radialLoadingImage.fillAmount = Mathf.Clamp01(aLoad.progress / 0.9f);

            if (aLoad.progress >= 0.9f)
            {
                if (Time.timeScale != 1) GamePauser.GameContinue();
                yield return new WaitForSeconds(2);
                aLoad.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}
