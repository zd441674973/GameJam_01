using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    private void Start()
    {
        // ���ҳ��������е�Canvas
        Canvas[] canvases = FindObjectsOfType<Canvas>();

        foreach (Canvas canvas in canvases)
        {
            // ����ÿ��Canvas�������Ӽ�
            foreach (Transform child in canvas.transform)
            {
                // ����Ӽ��������Ƿ����"x"
                if (child.name == "Bot")
                {
                    child.gameObject.SetActive(false);
                }
            }
        }
    }

    private void OnDestroy()
    {
        // ���ҳ��������е�Canvas
        Canvas[] canvases = FindObjectsOfType<Canvas>();

        foreach (Canvas canvas in canvases)
        {
            // ����ÿ��Canvas�������Ӽ�
            foreach (Transform child in canvas.transform)
            {
                // ����Ӽ��������Ƿ����"x"
                if (child.name == "Bot")
                {
                    child.gameObject.SetActive(true);
                }
            }
        }
    }

    private void OpenMenu()
    {

        if (!MenuIsOpen)
        {
            MusicMgr.GetInstance().PlaySound("SystemSoundEffect/ѡ��2", false);

            UIManager.GetInstance().ShowPanel<UI_MenuPanel>("UI_MenuPanel", E_UI_Layer.Top);
            MenuIsOpen = true;
        }

        if (MenuIsOpen)
        {
            MusicMgr.GetInstance().PlaySound("SystemSoundEffect/ѡ��2", false);

            UIManager.GetInstance().HidePanel("UI_MenuPanel");
            MenuIsOpen = false;
        }
    }

}
