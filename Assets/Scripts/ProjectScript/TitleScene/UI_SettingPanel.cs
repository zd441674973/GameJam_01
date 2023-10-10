using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_SettingPanel : BasePanel
{
    public TMP_Dropdown resolutionDropdown;

    public Slider MusicSlider;
    public Image fillMusicImage;
    public Slider SoundEffectSlider;
    public Image fillSoundEffectImage;

    public Image fullScreenButtonImage;
    public Image windowScreenButtonImage;


    protected override void Awake()
    {
        base.Awake();

        GetControl<Button>("CloseSetting").onClick.AddListener(ClosePanel);
        MusicSlider.onValueChanged.AddListener(ChangeMusicVolume);
        SoundEffectSlider.onValueChanged.AddListener(ChangeSoundEffectVolume);
        ///////////////////////////////////////////////////////////////////////////////////////////////
       
    }

    private void Start()
    {
        MusicSlider.value = GameDataControl.GetInstance().PlayerDataInfo.playerBKvalue;
        SoundEffectSlider.value = GameDataControl.GetInstance().PlayerDataInfo.playerSEvalue;
    }



    private void ClosePanel()
    {
        MusicMgr.GetInstance().PlaySound("SystemSoundEffect/Ñ¡Ôñ2", false);

        UIManager.GetInstance().HidePanel("UI_SettingPanel");
    }

    private void ChangeMusicVolume(float volume)
    {

        MusicMgr.GetInstance().ChangeBKValue(volume);
        fillMusicImage.fillAmount = volume;

        GameDataControl.GetInstance().PlayerDataInfo.playerBKvalue = volume;

        GameDataControl.GetInstance().SavePlayerInfo();
    }

    private void ChangeSoundEffectVolume(float volume)
    {

        MusicMgr.GetInstance().ChangeSoundValue(volume);
        fillSoundEffectImage.fillAmount = volume;

        GameDataControl.GetInstance().PlayerDataInfo.playerSEvalue = volume;

        GameDataControl.GetInstance().SavePlayerInfo();
    }


}
