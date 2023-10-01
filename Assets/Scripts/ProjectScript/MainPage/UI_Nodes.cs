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
    Node5,
    Node6,
    Node7,
    Node8,
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
        button.enabled = false;
        buttonImage.enabled = false;
        buttonText.enabled = false;
        currentPlayerNodeID = GameDataControl.GetInstance().PlayerDataInfo.currentNodeID;

        int nodeID = ((int)nodes);
        if(currentPlayerNodeID == nodeID)
        {
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
        switch (nodeName)
        {
            case "Node0":
                //����ս������0��������ҵ�currentPlayerNodeID + 1;
                Debug.Log("1");
                break;
            case "Node1":
                Debug.Log("2");
                break;
            case "Node2":
                Debug.Log("3");
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
        }

    }
}
