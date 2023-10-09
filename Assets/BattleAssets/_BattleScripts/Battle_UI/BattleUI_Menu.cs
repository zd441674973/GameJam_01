using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUI_Menu : BasePanel
{
    public Button MenuButton;
    private bool MenuIsOpen; 

    protected override void Awake()
    {
        base.Awake();
        MenuIsOpen = false;

        MenuButton.onClick.AddListener(OpenMenu);
    }

    private void OpenMenu()
    {

        if (!MenuIsOpen)
        {
            MusicMgr.GetInstance().PlaySound("SystemSoundEffect/Ñ¡Ôñ2", false);

            UIManager.GetInstance().ShowPanel<UI_MenuPanel>("UI_MenuPanel", E_UI_Layer.Top);
            MenuIsOpen = true;
        }

        if (MenuIsOpen)
        {
            MusicMgr.GetInstance().PlaySound("SystemSoundEffect/Ñ¡Ôñ2", false);

            UIManager.GetInstance().HidePanel("UI_MenuPanel");
            MenuIsOpen = false;
        }
    }

}
