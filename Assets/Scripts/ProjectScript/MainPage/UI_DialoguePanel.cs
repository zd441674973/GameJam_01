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
    private int backgroundMusicName;
    public Button openMailButton;
    public Button closeMailButton;
    public Image mail;

    private bool shouldPlayerChurchMusic;

    //用于检测鼠标是否位于按钮所在区域
    private bool hitButton;
    //按钮区域
    public Rect ButtonArea;


    //检测MenuPanel是否开启
    private bool MenuPanelIsOpen;
    private bool IsOkToPlayEnding;

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

        IsOkToPlayEnding = false;

        if (GameDataControl.GetInstance().PlayerDataInfo.currentNodeID == 6)
        {
            //播完最后一个剧情，当前玩家进度是5，打完boss以后，设置当前进度+1就可以播终局剧情
            //进入结局
            //显示按钮
            //阅读信件
            openMailButton.gameObject.SetActive(true);
            openMailButton.interactable = true;
            MenuPanelIsOpen = true;

            openMailButton.onClick.AddListener(ReadMail);

            IsOkToPlayEnding = true;

        }


        //开始时自动解析Json文件
        ParseData(GameDataControl.GetInstance().PlayerDataInfo.currentNodeID);

        //初始化设定
        currentdialogIndex = dialogIndex;

    }


    protected override void Awake()
    {
        base.Awake();

        GetControl<Button>("ButtonSkip").onClick.AddListener(SkipDiaglogue);
        shouldPlayerChurchMusic = false;

        backgroundMusicName = GameDataControl.GetInstance().PlayerDataInfo.currentNodeID;

        ShowBackGround();
        PlayMusic();

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

    private void ReadMail()
    {
        MusicMgr.GetInstance().PlaySound("SystemSoundEffect/选择2", false);

        openMailButton.gameObject.SetActive(false);
        openMailButton.interactable = false;
        mail.gameObject.SetActive(true);
        closeMailButton.gameObject.SetActive(true);
        closeMailButton.interactable = true;

        closeMailButton.onClick.AddListener(CloseMail);
    }

    private void CloseMail()
    {
        MusicMgr.GetInstance().PlaySound("SystemSoundEffect/选择2", false);

        mail.gameObject.SetActive(false);
        closeMailButton.gameObject.SetActive(false);
        closeMailButton.interactable = false;

        MenuPanelIsOpen = false;
        PressMouse();
    }


    private void PlayMusic()
    {

        if(backgroundMusicName == 0 && shouldPlayerChurchMusic)
        {
            MusicMgr.GetInstance().PlayBkMusic("maou_bgm_piano17-笼罩下的教堂");
        }
        else if(backgroundMusicName <= 5 && backgroundMusicName > 0)
        {
            MusicMgr.GetInstance().PlayBkMusic("maou_bgm_piano29-剧情");
        }
        else if(backgroundMusicName == 6)
        {
            MusicMgr.GetInstance().PlayBkMusic("maou_bgm_piano39-后记");
        }
        else
        {
            MusicMgr.GetInstance().PlayBkMusic("maou_bgm_piano09-开始");
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
        else if(name == "吉罗笑" || name == "吉罗韦" || name == "吉罗牙" || name == "吉罗口")
        {
            nameText.enabled = true;
            nameText.text = "吉罗";
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

            if (name == "旁白")
            {
                SpriteLeft.gameObject.SetActive(false);
                SpriteRight.gameObject.SetActive(false);
            }
            else
            {
                SpriteRight.gameObject.SetActive(false);
                SpriteLeft.gameObject.SetActive(true);
            }

          

            SpriteLeft.sprite = ResMgr.GetInstance().Load<Sprite>("Sprites/" + name);
        }
        else if (position == "right")
        {
            if (name == "旁白")
            {
                SpriteLeft.gameObject.SetActive(false);
                SpriteRight.gameObject.SetActive(false);
            }
            else
            {

                SpriteRight.gameObject.SetActive(true);
                SpriteLeft.gameObject.SetActive(false);
            }


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

    private void ShowBackGround()
    {
        switch (GameDataControl.GetInstance().PlayerDataInfo.currentNodeID)
        {
            case 0:
                BackGround.sprite = ResMgr.GetInstance().Load<Sprite>("Sprites/实验室");
                break;
            case 1:
                BackGround.sprite = ResMgr.GetInstance().Load<Sprite>("Sprites/街角");
                break;
            case 2:
                BackGround.sprite = ResMgr.GetInstance().Load<Sprite>("Sprites/工厂");
                break;
            case 3:
                BackGround.sprite = ResMgr.GetInstance().Load<Sprite>("Sprites/市政厅");
                break;
            case 4:
                BackGround.sprite = ResMgr.GetInstance().Load<Sprite>("Sprites/教堂广场");
                break;
            case 5:
                BackGround.sprite = ResMgr.GetInstance().Load<Sprite>("Sprites/教堂");
                break;
            case 6:
                BackGround.sprite = ResMgr.GetInstance().Load<Sprite>("Sprites/教堂");
                break;

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



    private void SkipDiaglogue()
    {

        MusicMgr.GetInstance().PlaySound("SystemSoundEffect/选择2", false);


        if (GameDataControl.GetInstance().PlayerDataInfo.currentNodeID == 6 && IsOkToPlayEnding)
        {
            UIManager.GetInstance().ShowPanel<UI_TeamMember>("UI_TeamMember", E_UI_Layer.Top);
        }
        else
        {
            ScenesMgr.GetInstance().LoadSceneAsyn("BattleScene", AfterLoadToBattle);
        }

        //结束当前阶段
        //UIManager.GetInstance().HidePanel("UI_DialoguePanel");
        //EventCenter.GetInstance().EventTrigger("currentPlayerNodeIDchange");
    }

    /// <summary>
    /// 点击鼠标左键后执行的逻辑
    /// </summary>
    private void PressMouse()
    {

        if (!hitButton && !MenuPanelIsOpen)
        {
            //鼠标点击音效
            //需要一个新的
            MusicMgr.GetInstance().PlaySound("SystemSoundEffect/翻页2", false);

            //点击鼠标会使对话进行下去
            dialogIndex = GetInFo(dialogIndex).JumpToID;
            currentdialogIndex = dialogIndex;




            if(currentdialogIndex == 26 && GameDataControl.GetInstance().PlayerDataInfo.currentNodeID == 0)
            {
                shouldPlayerChurchMusic = true;
                PlayMusic();
                //shouldPlayerChurchMusic = false;
            }

            if (currentdialogIndex == 24 && GameDataControl.GetInstance().PlayerDataInfo.currentNodeID == 0)
            {
                BackGround.sprite = ResMgr.GetInstance().Load<Sprite>("Sprites/街角");
            }


            if (dialogIndex == dialogdic.Count)
            {
                //结束当前阶段
                UIManager.GetInstance().HidePanel("UI_DialoguePanel");
     

                if (GameDataControl.GetInstance().PlayerDataInfo.currentNodeID < 6)
                {
                    ScenesMgr.GetInstance().LoadSceneAsyn("BattleScene", AfterLoadToBattle);
                }
                
                
                if(GameDataControl.GetInstance().PlayerDataInfo.currentNodeID == 6 && IsOkToPlayEnding)
                {
                    UIManager.GetInstance().ShowPanel<UI_TeamMember>("UI_TeamMember", E_UI_Layer.Top);
                }
            }
        }
    }

    private void AfterLoadToBattle()
    {
        UIManager.GetInstance().HidePanel("UI_MenuPanel");
        UIManager.GetInstance().HidePanel("UI_MainPage");
        UIManager.GetInstance().HidePanel("UI_CardLibrary");
        UIManager.GetInstance().HidePanel("UI_GameMap");
        UIManager.GetInstance().HidePanel("AwardPanel");
        UIManager.GetInstance().HidePanel("DelateCardPanel");
        UIManager.GetInstance().HidePanel("UI_DialoguePanel");
        UIManager.GetInstance().HidePanel("UI_TitleScene");
    }
}

