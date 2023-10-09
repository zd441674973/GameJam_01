using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum Nodes
{
    Node0,
    Node1,
    Node2,
    Node3,
    Node4,
    Node5
}

public class UI_Nodes : BasePanel
{
    private string nodeName;
    public Button button;
    public Image buttonImage;
    public TMP_Text buttonText;
    public Image NodeImage;
    private int currentPlayerNodeID;


    protected override void Awake()
    {
        base.Awake();

        nodeName = gameObject.name;

        SetNodeID();
        GetControl<Button>("ButtonStartBattle").onClick.AddListener(StartBattle);
        EventCenter.GetInstance().AddEventListener("currentPlayerNodeIDchange", SetNodeID);
    }


    private void OnDestroy()
    {
        EventCenter.GetInstance().RemoveEventListener("currentPlayerNodeIDchange", SetNodeID);
    }

    private void SetButtons(Nodes nodes)
    {
        button.gameObject.SetActive(false);
        button.enabled = false;

        buttonImage.enabled = false;
        buttonText.enabled = false;
        currentPlayerNodeID = GameDataControl.GetInstance().PlayerDataInfo.currentNodeID;

        int nodeID = ((int)nodes);
        if(currentPlayerNodeID == nodeID)
        {

            button.gameObject.SetActive(true);
            button.enabled = true;
            buttonImage.enabled = true;
            buttonText.enabled = true;
            //NodeImage.sprite = ResMgr.GetInstance().Load<Sprite>("Sprites/NodeIcon/" + nodeID);
        }
        else if (currentPlayerNodeID > nodeID)
        {
            //NodeImage.sprite = ResMgr.GetInstance().Load<Sprite>("Sprites/NodeIcon/" + nodeID);
        }
        else
        {
            //NodeImage.sprite = ResMgr.GetInstance().Load<Sprite>("Sprites/NodeIcon/Unknow");
        }
    }


    private void StartBattle()
    {
        MusicMgr.GetInstance().PlaySound("SystemSoundEffect/选择2", false);

        switch (nodeName)
        {
            case "Node0":
                //进入战斗场景0，打完玩家的currentPlayerNodeID + 1;
                UIManager.GetInstance().ShowPanel<UI_DialoguePanel>("UI_DialoguePanel", E_UI_Layer.Mid);
                break;
            case "Node1":
                UIManager.GetInstance().ShowPanel<UI_DialoguePanel>("UI_DialoguePanel", E_UI_Layer.Mid);
                break;
            case "Node2":
                UIManager.GetInstance().ShowPanel<UI_DialoguePanel>("UI_DialoguePanel", E_UI_Layer.Mid);
                break;
            case "Node3":
                UIManager.GetInstance().ShowPanel<UI_DialoguePanel>("UI_DialoguePanel", E_UI_Layer.Mid);
                break;
            case "Node4":
                UIManager.GetInstance().ShowPanel<UI_DialoguePanel>("UI_DialoguePanel", E_UI_Layer.Mid);
                break;
            case "Node5":
                UIManager.GetInstance().ShowPanel<UI_DialoguePanel>("UI_DialoguePanel", E_UI_Layer.Mid);
                break;
        }
    }

    private void SetNodeID()
    {
        switch (nodeName)
        {
            case "Node0":
                SetButtons(Nodes.Node0);
                break;
            case "Node1":
                SetButtons(Nodes.Node1);
                break;
            case "Node2":
                SetButtons(Nodes.Node2);
                break;
            case "Node3":
                SetButtons(Nodes.Node3);
                break;
            case "Node4":
                SetButtons(Nodes.Node4);
                break;
            case "Node5":
                SetButtons(Nodes.Node5);
                break;
        }

    }
}
