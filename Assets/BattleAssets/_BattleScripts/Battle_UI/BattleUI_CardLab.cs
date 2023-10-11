using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUI_CardLab : BasePanel
{
    public Button cardLabraryButton;
    private bool CardLabraryIsOpen;

    protected override void Awake()
    {
        base.Awake();
        CardLabraryIsOpen = false;

        cardLabraryButton.onClick.AddListener(OpenCardLabrary);
    }

    public void OpenCardLabrary()
    {
        if (!CardLabraryIsOpen)
        {

            MusicMgr.GetInstance().PlaySound("SystemSoundEffect/Ñ¡Ôñ2", false);

            UIManager.GetInstance().ShowPanel<UI_CardLibrary>("UI_CardLibrary", E_UI_Layer.Mid);
            CardLabraryIsOpen = true;
        }

        if (CardLabraryIsOpen)
        {
            MusicMgr.GetInstance().PlaySound("SystemSoundEffect/Ñ¡Ôñ2", false);

            UIManager.GetInstance().HidePanel("UI_CardLibrary");
            CardLabraryIsOpen = false;
        }
    }


}
