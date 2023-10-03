using System.Collections;
using System.Collections.Generic;
using System.Resources;
using TMPro;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//装载Json的类的list，名字必须与Json文件键值对应
public class Dialogues
{
    public List<Dialogue> Dialogue;
}
//装载每个数据的类，名字必须与Json文件键值对应
[System.Serializable]
public class Dialogue
{
    public string IsOption;
    public int DialogueID;
    public string Character;
    public string PicturePosition;
    public string Content;
    public int JumpToID;
}

public class UI_DialoguePanel : BasePanel
{
    //用于检测鼠标是否位于按钮所在区域
    private bool hitButton;
    //按钮区域
    public Rect ButtonArea;


    //检测MenuPanel是否开启
    private bool MenuPanelIsOpen;

    public Image BackGround;
    //左侧角色立绘
    public Image SpriteLeft;
    //右侧角色立绘
    public Image SpriteRight;
    
    //角色姓名文本
    public TMP_Text nameText;
    //角色对话文本
    public TMP_Text contentText;

    //保存当前的对话ID索引值
    public int dialogIndex = 0;
    private int currentdialogIndex = 0;

    //为了方便调用，利用字典装载数据集合中的数据
    private Dictionary<int, Dialogue> dialogdic = new Dictionary<int, Dialogue>();

    public override void ShowMe()
    {
        base.ShowMe();
        //监听菜单是否打开，一旦打开就将其设置为true
        MenuPanelIsOpen = false;
        EventCenter.GetInstance().AddEventListener("MenuIsOpen", () =>
        {
            MenuPanelIsOpen = true;
        });
        EventCenter.GetInstance().AddEventListener("MenuIsClose", () =>
        {
            MenuPanelIsOpen = false;
        });


        //开始时自动解析Json文件
        ParseData(GameDataControl.GetInstance().PlayerDataInfo.currentNodeID);

        //初始化设定
        currentdialogIndex = dialogIndex;

    }


    protected override void Awake()
    {
        base.Awake();
    }

    public override void HideMe()
    {
        EventCenter.GetInstance().RemoveEventListener("MenuIsOpen", () => { });
        EventCenter.GetInstance().RemoveEventListener("MenuIsClose", () => { });
    }


    // Update is called once per frame
    void Update()
    {
        ShowDialogue();
        checkButtonHit();

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            PressMouse();
        }

    }

    /// <summary>
    /// 解析对应int的Json文件
    /// </summary>
    public void ParseData(int DialogueDocumentationName)
    {
        //读取并解析Json文件
        Dialogues dialogues = new Dialogues();
        dialogues = JsonMgr.Instance.LoadData<Dialogues>("StroyDIalogue/Dialogue" + DialogueDocumentationName);
        //将数据集合按照ID号分别放入
        for (int i = 0; i < dialogues.Dialogue.Count; i++)
        {
            dialogdic.Add(dialogues.Dialogue[i].DialogueID, dialogues.Dialogue[i]);
        }

    }



    /// <summary>
    /// 根据对话ID，得到详细信息
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Dialogue GetInFo(int id)
    {
        if (dialogdic.ContainsKey(id))
            return dialogdic[id];
        return null;
    }

    /// <summary>
    /// 更新文本
    /// </summary>
    /// <param name="ID">Json文件中的ID索引</param>
    /// <param name="name">Character的Name</param>
    /// <param name="content">对话内容</param>
    public void UpdateText(int ID, string name, string content)
    {
        nameText.text = name;

        if(name == "旁白")
        {
            nameText.enabled = false;
        }
        else
        {
            nameText.enabled = true;
        }

        contentText.text = content;
    }

    /// <summary>
    /// 更新立绘
    /// </summary>
    /// <param name="ID"></param>
    /// <param name="name"></param>
    /// <param name="position"></param>
    public void UpdateImage(int ID, string name, string position)
    {
        if (position == "left")
        {
            SpriteLeft.sprite = ResMgr.GetInstance().Load<Sprite>("Sprites/" + name);
        }
        else if (position == "right")
        {
            SpriteRight.sprite = ResMgr.GetInstance().Load<Sprite>("Sprites/" + name);
        }
        else if (position == "no")
        {
            SpriteLeft.sprite = null;
            SpriteRight.sprite = null;
        }
    }

    /// <summary>
    /// 显示文本和立绘
    /// </summary>
    public void ShowDialogue()
    {
        //当存在索引编号时，更新文本和立绘
        if (dialogdic.ContainsKey(dialogIndex))
        {
            UpdateText(GetInFo(dialogIndex).DialogueID, GetInFo(dialogIndex).Character, GetInFo(dialogIndex).Content);
            UpdateImage(GetInFo(dialogIndex).DialogueID, GetInFo(dialogIndex).Character, GetInFo(dialogIndex).PicturePosition);
        }

    }

    /// <summary>
    /// 检测鼠标是否位于按钮所在区域
    /// </summary>
    private void checkButtonHit()
    {
        Vector3 MousePosition = Input.mousePosition;
        if (ButtonArea.Contains(MousePosition))
        {
            hitButton = true;
        }
        else
        {
            hitButton = false;
        }
    }


    /// <summary>
    /// 点击鼠标左键后执行的逻辑
    /// </summary>
    private void PressMouse()
    {
        if (!hitButton && !MenuPanelIsOpen)
        {
            //鼠标点击音效
            MusicMgr.GetInstance().PlaySound("maou_se_sound20_Maou-Select", false);

            //点击鼠标会使对话进行下去
            dialogIndex = GetInFo(dialogIndex).JumpToID;
            currentdialogIndex = dialogIndex;

            if (dialogIndex == dialogdic.Count)
            {
                //结束当前阶段
                UIManager.GetInstance().HidePanel("UI_DialoguePanel");
                //完成战斗后再执行
                //EventCenter.GetInstance().EventTrigger("currentPlayerNodeIDchange");
                
                if(GameDataControl.GetInstance().PlayerDataInfo.currentNodeID > 5)
                {
                    //进入结局
                }
                else if(GameDataControl.GetInstance().PlayerDataInfo.currentNodeID == 0)
                {
                    //进入主界面
                }
                else
                {
                    //进入对应战斗场景
                    //1

                    //2

                    //3

                    //4

                    //5
                }
            }
        }
    }
}
