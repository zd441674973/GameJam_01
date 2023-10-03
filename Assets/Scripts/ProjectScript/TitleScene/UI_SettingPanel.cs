using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_SettingPanel : BasePanel
{
    public TMP_Dropdown resolutionDropdown;

    public Slider MusicSlider;
    public Slider SoundEffectSlider;

    private Resolution[] resolutions;

    protected override void Awake()
    {
        base.Awake();

        GetControl<Button>("CloseSetting").onClick.AddListener(ClosePanel);
        GetControl<Button>("ButtonFullScreen").onClick.AddListener(FullScreen);
        GetControl<Button>("ButtonWindowScreen").onClick.AddListener(WindowScreen);

        MusicSlider.onValueChanged.AddListener(ChangeMusicVolume);
        SoundEffectSlider.onValueChanged.AddListener(ChangeSoundEffectVolume);
        ///////////////////////////////////////////////////////////////////////////////////////////////
        // 获取支持的分辨率列表
        resolutions = Screen.resolutions;

        // 清除Dropdown中的所有选项
        resolutionDropdown.ClearOptions();

        // 创建分辨率选项列表
        List<string> resolutionOptions = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string resolutionOption = resolutions[i].width + " x " + resolutions[i].height;
            resolutionOptions.Add(resolutionOption);

            // 检查当前分辨率是否匹配，以便在Dropdown中设置默认选项
            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        // 将选项列表添加到Dropdown
        resolutionDropdown.AddOptions(resolutionOptions);

        // 设置默认选项
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }




    // 当分辨率选项发生变化时调用
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    private void ClosePanel()
    {
        UIManager.GetInstance().HidePanel("UI_SettingPanel");
    }

    private void ChangeMusicVolume(float volume)
    {
        MusicMgr.GetInstance().ChangeBKValue(volume);
    }

    private void ChangeSoundEffectVolume(float volume)
    {
        MusicMgr.GetInstance().ChangeSoundValue(volume);
    }

    private void FullScreen()
    {
        // 判断当前是否处于全屏模式
        if (Screen.fullScreen)
        {
            // 如果是全屏模式，则切换到窗口化模式
            Screen.fullScreen = false;
        }
    }

    private void WindowScreen()
    {
        // 判断当前是否处于全屏模式
        if (!Screen.fullScreen)
        {
            // 如果是全屏模式，则切换到窗口化模式
            Screen.fullScreen = true;
        }
    }
}
