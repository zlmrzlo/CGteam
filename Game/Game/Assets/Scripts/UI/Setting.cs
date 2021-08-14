using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.IO;

public class SettingData
{
    public float brightness = 0;
    public float bgmVolume = 0.5f;
    public float effectVolume = 0.5f;
    public float mouseSensitivity = 1;
    public int quialityIndex = 3;
    public bool isFullscreen = true;
    public int resolutionIndex = -1;
    public string resolution = "640 x 480";
    public string[] keyBindKey = { 
        "UP", "DOWN", "LEFT", "RIGHT", "RUN", "CROUCH", "JUMP", "ACT", "USE" 
    };
    public KeyCode[] keyBindVal = { 
        KeyCode.W, KeyCode.S, KeyCode.A, KeyCode.D, KeyCode.LeftShift, KeyCode.LeftControl, KeyCode.Space, KeyCode.E, KeyCode.Mouse0 
    };
}

public class Setting : MonoBehaviour
{
    public static SettingData settingData = new SettingData();
    public static bool isLoaded = false;
    private static string SETTING_DATA_DIRECTORY;
    private static string SETTING_FILENAME = "/Setting.txt";

    [SerializeField] private GameObject baseUI;
    [SerializeField] private GameObject TitleUI;
    [SerializeField] private GameObject KeyBindUI;
    [SerializeField] private Slider brightness;
    [SerializeField] private Slider volumeBGM;
    [SerializeField] private Slider volumeEffect;
    [SerializeField] private Slider mouse;
    private PlayerController mouseLook;
    private KeyBindManager kbManager;
    public AudioMixer bgmMixer;
    public AudioMixer effectMixer;

    [SerializeField] private Dropdown qualityDropdown;
    [SerializeField] private Toggle fullscreenToggle;
    [SerializeField] private Dropdown resolutionDropdown;
    Resolution[] resolutions;


    public void Start()
    {
        // Resolution Setting
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        int currentResolutionIndex = 0;
        List<string> options = new List<string>();
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);
            if (resolutions[i].width == Screen.currentResolution.width &&
                    resolutions[i].height == Screen.currentResolution.height)
                currentResolutionIndex = i;
        }
        resolutionDropdown.AddOptions(options);
        //resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
        // Load Setting Values
        SETTING_DATA_DIRECTORY = Application.dataPath + "/Setting/";
        if (!Directory.Exists(SETTING_DATA_DIRECTORY))
            Directory.CreateDirectory(SETTING_DATA_DIRECTORY);
        if (File.Exists(SETTING_DATA_DIRECTORY + SETTING_FILENAME))
        {
            //Debug.Log("Loading Settings.txt");
            isLoaded = true;
            string loadJson = File.ReadAllText(SETTING_DATA_DIRECTORY + SETTING_FILENAME);
            settingData = JsonUtility.FromJson<SettingData>(loadJson);
            mouseLook = FindObjectOfType<PlayerController>();
            kbManager = FindObjectOfType<KeyBindManager>();

            // Load Brightness
            RenderSettings.ambientIntensity = settingData.brightness;
            brightness.value = settingData.brightness;
            // Load BGM Volume
            bgmMixer.SetFloat("BGMVolume", Mathf.Log10(settingData.bgmVolume * 2) * 20);
            volumeBGM.value = settingData.bgmVolume;
            // Load Effect Volume
            effectMixer.SetFloat("EffectVolume", Mathf.Log10(settingData.effectVolume * 2) * 20);
            volumeEffect.value = settingData.effectVolume;
            // Load Mouse Sensitivity
            if (mouseLook)
                mouseLook.lookSensitivity = settingData.mouseSensitivity;
            mouse.value = settingData.mouseSensitivity;
            // Load quality
            QualitySettings.SetQualityLevel(settingData.quialityIndex);
            qualityDropdown.value = settingData.quialityIndex;
            // Load Fullscreen
            Screen.fullScreen = settingData.isFullscreen;
            fullscreenToggle.isOn = settingData.isFullscreen;
            // Load KeyBinds
            for (int i = 0; i < settingData.keyBindKey.Length; i++)
                kbManager.LoadKey(settingData.keyBindKey[i], settingData.keyBindVal[i]);
            //Debug.Log(KeyBindManager.KeyBinds);
            // Load Resolution if possible
            if (settingData.resolutionIndex != -1)
            {
                Resolution resolution;
                if (settingData.resolutionIndex <= resolutions.Length &&
                settingData.resolution == 
                resolutions[settingData.resolutionIndex].width + " x " + resolutions[settingData.resolutionIndex].height)
                {
                    resolution = resolutions[settingData.resolutionIndex];
                    resolutionDropdown.value = settingData.resolutionIndex;
                }
                else
                {
                    resolution = resolutions[0];
                    resolutionDropdown.value = 0;
                    settingData.resolutionIndex = 0;
                    settingData.resolution = resolution.width + " x " + resolution.height;
                }
                Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
            }
            //resolutionDropdown.value = currentResolutionIndex;
            //settingData.resolution = resolutions[currentResolutionIndex].width + " x " +
            //    resolutions[currentResolutionIndex].height;
        }
        else
        {
            Debug.Log("세팅 파일이 없습니다.");
            string json = JsonUtility.ToJson(settingData);
            File.WriteAllText(SETTING_DATA_DIRECTORY + SETTING_FILENAME, json);
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            if (GameManager.isPause)
            {
                CloseMenu();
                SaveSetting();
            }
    }
    public void SetBrightness(float sliderValue)
    {
        RenderSettings.ambientIntensity = sliderValue;
        settingData.brightness = sliderValue;
        SaveSetting();
    }

    public void SetBGMVolume(float sliderValue)
    {
        bgmMixer.SetFloat("BGMVolume", Mathf.Log10(sliderValue * 2) * 20);
        settingData.bgmVolume = sliderValue;
        SaveSetting();
    }
    public void SetEffectVolume(float sliderValue)
    {
        effectMixer.SetFloat("EffectVolume", Mathf.Log10(sliderValue * 2) * 20);
        settingData.effectVolume = sliderValue;
        SaveSetting();
    }

    public void SetMouseSensitivity(float sliderValue)
    {
        mouseLook = FindObjectOfType<PlayerController>();
        if (mouseLook)
            mouseLook.lookSensitivity = sliderValue;
        settingData.mouseSensitivity = sliderValue;
        SaveSetting();
    }

    public void SetResolution(int index)
    {
        Resolution resolution = resolutions[index];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        settingData.resolutionIndex = index;
        settingData.resolution = resolution.width + " x " + resolution.height;
        SaveSetting();
    }

    public void SetQuality (int index)
    {
        QualitySettings.SetQualityLevel(index);
        settingData.quialityIndex = index;
        SaveSetting();
    }

    public void SetFullScreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        settingData.isFullscreen = isFullscreen;
        SaveSetting();
    }

    public void ResetBrightness()
    {
        brightness.value = 1f;
        settingData.brightness = 1f;
        SaveSetting();
    }
    public void ResetBGMVolume()
    {
        volumeBGM.value = 0.5f;
        settingData.bgmVolume = 0.5f;
        SaveSetting();
    }
    public void ResetEffectVolume()
    {
        volumeEffect.value = 0.5f;
        settingData.effectVolume = 0.5f;
        SaveSetting();
    }
    public void ResetMouse()
    {
        mouse.value = 1.0f;
        settingData.mouseSensitivity = 1.0f;
        SaveSetting();
    }

    public void OpenKeybind()
    {
        KeyBindUI.SetActive(true);
        SaveSetting();
    }

    public void CloseMenu()
    {
        baseUI.SetActive(false);
        if(TitleUI) TitleUI.SetActive(true);
        SaveSetting();
    }

    public static void SaveSetting()
    {
        SETTING_DATA_DIRECTORY = Application.dataPath + "/Setting/";
        string json = JsonUtility.ToJson(settingData);
        File.WriteAllText(SETTING_DATA_DIRECTORY + SETTING_FILENAME, json);
    }
}