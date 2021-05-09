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
    public string resolution = "320 x 240";
}

public class Setting : MonoBehaviour
{
    private SettingData settingData = new SettingData();
    private string SETTING_DATA_DIRECTORY;
    private string SETTING_FILENAME = "/Setting.txt";

    [SerializeField] private GameObject baseUI;
    [SerializeField] private GameObject TitleUI;
    [SerializeField] private Slider brightness;
    [SerializeField] private Slider volumeBGM;
    [SerializeField] private Slider volumeEffect;
    [SerializeField] private Slider mouse;
    private PlayerController mouseLook;
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
        // Load Setting Values
        SETTING_DATA_DIRECTORY = Application.dataPath + "/Setting/";
        if (!Directory.Exists(SETTING_DATA_DIRECTORY))
            Directory.CreateDirectory(SETTING_DATA_DIRECTORY);
        if (File.Exists(SETTING_DATA_DIRECTORY + SETTING_FILENAME))
        {
            string loadJson = File.ReadAllText(SETTING_DATA_DIRECTORY + SETTING_FILENAME);
            settingData = JsonUtility.FromJson<SettingData>(loadJson);
            mouseLook = FindObjectOfType<PlayerController>();

            // Load Brightness
            RenderSettings.ambientLight = new Color(settingData.brightness, settingData.brightness, settingData.brightness, 1);
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
            // Load Resolution if possible
            if (settingData.resolutionIndex != -1)
                currentResolutionIndex = settingData.resolutionIndex;
            settingData.resolutionIndex = currentResolutionIndex;
            settingData.resolution = resolutions[currentResolutionIndex].width + " x " +
                resolutions[currentResolutionIndex].height;
        }
        else
        {
            Debug.Log("세팅 파일이 없습니다.");
            string json = JsonUtility.ToJson(settingData);
            File.WriteAllText(SETTING_DATA_DIRECTORY + SETTING_FILENAME, json);
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }
    public void SetBrightness(float sliderValue)
    {
        RenderSettings.ambientLight = new Color(sliderValue, sliderValue, sliderValue, 1);
        settingData.brightness = sliderValue;
    }

    public void SetBGMVolume(float sliderValue)
    {
        bgmMixer.SetFloat("BGMVolume", Mathf.Log10(sliderValue * 2) * 20);
        settingData.bgmVolume = sliderValue;
    }
    public void SetEffectVolume(float sliderValue)
    {
        effectMixer.SetFloat("EffectVolume", Mathf.Log10(sliderValue * 2) * 20);
        settingData.effectVolume = sliderValue;
    }

    public void SetMouseSensitivity(float sliderValue)
    {
        mouseLook = FindObjectOfType<PlayerController>();
        if (mouseLook)
            mouseLook.lookSensitivity = sliderValue;
        settingData.mouseSensitivity = sliderValue;
    }

    public void SetResolution(int index)
    {
        Resolution resolution = resolutions[index];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        settingData.resolutionIndex = index;
        settingData.resolution = resolution.width + " x " + resolution.height;
    }

    public void SetQuality (int index)
    {
        QualitySettings.SetQualityLevel(index);
        settingData.quialityIndex = index;
    }

    public void SetFullScreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        settingData.isFullscreen = isFullscreen;
    }

    public void ResetBrightness()
    {
        brightness.value = 0f;
        settingData.brightness = 0f;
    }
    public void ResetBGMVolume()
    {
        volumeBGM.value = 0.5f;
        settingData.bgmVolume = 0.5f;
    }
    public void ResetEffectVolume()
    {
        volumeEffect.value = 0.5f;
        settingData.effectVolume = 0.5f;
    }
    public void ResetMouse()
    {
        mouse.value = 1.0f;
        settingData.mouseSensitivity = 1.0f;
    }

    public void CloseMenu()
    {
        baseUI.SetActive(false);
        TitleUI.SetActive(true);
        SaveSetting();
    }

    private void SaveSetting()
    {
        SETTING_DATA_DIRECTORY = Application.dataPath + "/Setting/";
        string json = JsonUtility.ToJson(settingData);
        File.WriteAllText(SETTING_DATA_DIRECTORY + SETTING_FILENAME, json);
    }
}