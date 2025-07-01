using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

[System.Serializable]
public class SettingsData
{
    public int shadowQuality;
    public int antiAliasing;
    public float detailDensity;
    public int textureQuality;
    public int resolutionIndex;
    public int audioIndex;
    public float sensitivity;
    public bool completed;
}

public class PauseMenu : MonoBehaviour
{
    public bool TF = true;
    public MouseLook ml;
    private bool completed ;
    public AudioListener al;
    public GameObject pause, options, controls, panel, locked;
    public Shooting_Mechanics sm;
    public Slider detailSlider, sensitivity;
    public Camera cam;
    public TMP_Dropdown shadowDropdown, antiAliasingDropdown, textureQualityDropdown, resolutionDropdown, audio;
    public Terrain[] terrains;
    public Light light;

    private bool isPaused = false;
    private static string folderPath;
    private static string filePath;
    string cheatBuffer = "";

    private Resolution[] fixed16by9Resolutions = new Resolution[]
    {
        new Resolution { width = 1920, height = 1080 },
        new Resolution { width = 1600, height = 900 },
        new Resolution { width = 1366, height = 768 },
        new Resolution { width = 1280, height = 720 },
        new Resolution { width = 1024, height = 576 },
        new Resolution { width = 960,  height = 540 },
        new Resolution { width = 854,  height = 480 },
        new Resolution { width = 640,  height = 360 },
        new Resolution { width = 426,  height = 240 }
    };

    void Awake()
    {
        folderPath = Application.persistentDataPath + "/Wither Woods";
        filePath = folderPath + "/settings.json";
        terrains = FindObjectsOfType<Terrain>();
    }

    void Start()
    {
        if (!Directory.Exists(folderPath))
            Directory.CreateDirectory(folderPath);

        audio.onValueChanged.AddListener(Audio);
        detailSlider.onValueChanged.AddListener(UpdateDetailLevel);
        sensitivity.onValueChanged.AddListener(MouseSensitivity);
        shadowDropdown.onValueChanged.AddListener(OnShadowDropdownChanged);
        antiAliasingDropdown.onValueChanged.AddListener(OnAntiAliasingChanged);
        textureQualityDropdown.onValueChanged.AddListener(ChangeTextureQuality);
        resolutionDropdown.onValueChanged.AddListener(ChangeResolution);

        PopulateResolutionDropdown();
        LoadSettings();

        if (sensitivity.value <= 0f)
        {
            sensitivity.value = 0.5f;
            MouseSensitivity(0.5f);
        }
    }

    void PopulateResolutionDropdown()
    {
        resolutionDropdown.ClearOptions();
        var options = new System.Collections.Generic.List<string>();
        foreach (var res in fixed16by9Resolutions)
            options.Add(res.width + " x " + res.height);
        resolutionDropdown.AddOptions(options);
    }

    void Update()
    {
         foreach (char c in Input.inputString)
    {
        cheatBuffer += c;
        if (cheatBuffer.Length > 10) cheatBuffer = cheatBuffer.Substring(cheatBuffer.Length - 10);

        if (cheatBuffer.ToLower().Contains("withergod")) // your cheat code 🧙‍♂️
        {
            string json = File.ReadAllText(filePath);
                SettingsData obj = JsonUtility.FromJson<SettingsData>(json);
                obj.completed = true;
                completed = true; 
                File.WriteAllText(filePath, JsonUtility.ToJson(obj, true));
            cheatBuffer = "";
        }
    }
        if (Input.GetButtonDown("Cancel") && TF) TogglePause();

        if (panel != null && panel.activeSelf)
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                SettingsData obj = JsonUtility.FromJson<SettingsData>(json);
                obj.completed = true;
                completed = true; 
                File.WriteAllText(filePath, JsonUtility.ToJson(obj, true));
                
            }
        }

        if (locked != null)
        {
            LoadSettings();
            if(completed)
                locked.SetActive(false);
        }
    }

    public void Resume() => TogglePause();

    private void TogglePause()
    {
        isPaused = !isPaused;
        sm.enabled = !isPaused;
        pause.SetActive(isPaused);
        BackOption();
        BackControls();
        Time.timeScale = isPaused ? 0f : 1f;
        Cursor.lockState = isPaused ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = isPaused;
    }

    void Audio(int index)
    {
        al.enabled = index == 0;
        SaveSettings();
    }

    void OnShadowDropdownChanged(int index)
    {
        switch (index)
        {
            case 0: light.shadows = LightShadows.Soft; light.intensity = 0.7f; break;
            case 1: light.shadows = LightShadows.Hard; light.intensity = 0.7f; break;
            case 2: light.shadows = LightShadows.None; light.intensity = 0.2f; break;
        }
        SaveSettings();
    }

    void OnAntiAliasingChanged(int index)
    {
        var camData = cam.GetUniversalAdditionalCameraData();
        switch (index)
        {
            case 0: camData.antialiasing = AntialiasingMode.None; break;
            case 1: camData.antialiasing = AntialiasingMode.FastApproximateAntialiasing; break;
        }
        SaveSettings();
    }

    void ChangeTextureQuality(int index)
    {
        QualitySettings.globalTextureMipmapLimit = index;
        SaveSettings();
    }

    void ChangeResolution(int index)
{
    var res = fixed16by9Resolutions[index];
    if (Screen.currentResolution.width != res.width || Screen.currentResolution.height != res.height)
    {
        Screen.SetResolution(res.width, res.height, FullScreenMode.ExclusiveFullScreen);
        SaveSettings();
    }
}

    void UpdateDetailLevel(float value)
    {
        foreach (Terrain terrain in terrains)
            terrain.detailObjectDensity = value;
        SaveSettings();
    }

    void MouseSensitivity(float value)
    {
        value = Mathf.Clamp(value, 0.1f, 1f);
        ml.mouseSensitivity = value * 300f;
        SaveSettings();
    }

    public void Option() => options.SetActive(true);
    public void Controls() => controls.SetActive(true);
    public void BackOption() => options.SetActive(false);
    public void BackControls() => controls.SetActive(false);
    public void MainMenu() => SceneManager.LoadScene("MainMenu");
    public void Exit() => Application.Quit();

    void SaveSettings()
{
    SettingsData data;

    // Load existing settings if the file exists
    if (File.Exists(filePath))
    {
        string existingJson = File.ReadAllText(filePath);
        data = JsonUtility.FromJson<SettingsData>(existingJson);
    }
    else
    {
        data = new SettingsData();
    }

    // Update the rest of the settings but preserve `completed`
    data.shadowQuality = shadowDropdown.value;
    data.antiAliasing = antiAliasingDropdown.value;
    data.detailDensity = detailSlider.value;
    data.textureQuality = textureQualityDropdown.value;
    data.resolutionIndex = resolutionDropdown.value;
    data.audioIndex = audio.value;
    data.sensitivity = sensitivity.value;

    // Do NOT touch `data.completed` here — it retains its previous value

    string json = JsonUtility.ToJson(data, true);
    File.WriteAllText(filePath, json);
}

    void LoadSettings()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            SettingsData data = JsonUtility.FromJson<SettingsData>(json);

            shadowDropdown.value = data.shadowQuality;
            antiAliasingDropdown.value = data.antiAliasing;
            detailSlider.value = data.detailDensity;
            textureQualityDropdown.value = data.textureQuality;
            resolutionDropdown.value = data.resolutionIndex;
            audio.value = data.audioIndex;
            sensitivity.value = Mathf.Clamp(data.sensitivity, 0.1f, 1f);

            OnShadowDropdownChanged(data.shadowQuality);
            OnAntiAliasingChanged(data.antiAliasing);
            UpdateDetailLevel(data.detailDensity);
            ChangeTextureQuality(data.textureQuality);
            ChangeResolution(data.resolutionIndex);
            Audio(data.audioIndex);
            MouseSensitivity(sensitivity.value);
            completed = data.completed;
        }
    }

    public void ClearSettings()
    {
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            Debug.Log("Settings file deleted.");
            SaveSettings();
            LoadSettings();
        }
    }
}
