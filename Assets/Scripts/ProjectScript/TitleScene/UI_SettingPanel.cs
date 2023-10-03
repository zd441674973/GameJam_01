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
        // ��ȡ֧�ֵķֱ����б�
        resolutions = Screen.resolutions;

        // ���Dropdown�е�����ѡ��
        resolutionDropdown.ClearOptions();

        // �����ֱ���ѡ���б�
        List<string> resolutionOptions = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string resolutionOption = resolutions[i].width + " x " + resolutions[i].height;
            resolutionOptions.Add(resolutionOption);

            // ��鵱ǰ�ֱ����Ƿ�ƥ�䣬�Ա���Dropdown������Ĭ��ѡ��
            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        // ��ѡ���б���ӵ�Dropdown
        resolutionDropdown.AddOptions(resolutionOptions);

        // ����Ĭ��ѡ��
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }




    // ���ֱ���ѡ����仯ʱ����
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
        // �жϵ�ǰ�Ƿ���ȫ��ģʽ
        if (Screen.fullScreen)
        {
            // �����ȫ��ģʽ�����л������ڻ�ģʽ
            Screen.fullScreen = false;
        }
    }

    private void WindowScreen()
    {
        // �жϵ�ǰ�Ƿ���ȫ��ģʽ
        if (!Screen.fullScreen)
        {
            // �����ȫ��ģʽ�����л������ڻ�ģʽ
            Screen.fullScreen = true;
        }
    }
}
