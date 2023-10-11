using System.Collections;
using System.Collections.Generic;
using System.Resources;
using TMPro;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//װ��Json�����list�����ֱ�����Json�ļ���ֵ��Ӧ
public class Dialogues
{
    public List<Dialogue> Dialogue;
}
//װ��ÿ�����ݵ��࣬���ֱ�����Json�ļ���ֵ��Ӧ
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

    //���ڼ������Ƿ�λ�ڰ�ť��������
    private bool hitButton;
    //��ť����
    public Rect ButtonArea;


    //���MenuPanel�Ƿ���
    private bool MenuPanelIsOpen;
    private bool IsOkToPlayEnding;

    public Image BackGround;
    //����ɫ����
    public Image SpriteLeft;
    //�Ҳ��ɫ����
    public Image SpriteRight;
    
    //��ɫ�����ı�
    public TMP_Text nameText;
    //��ɫ�Ի��ı�
    public TMP_Text contentText;

    //���浱ǰ�ĶԻ�ID����ֵ
    public int dialogIndex = 0;
    private int currentdialogIndex = 0;

    //Ϊ�˷�����ã������ֵ�װ�����ݼ����е�����
    private Dictionary<int, Dialogue> dialogdic = new Dictionary<int, Dialogue>();

    public override void ShowMe()
    {
        base.ShowMe();
        //�����˵��Ƿ�򿪣�һ���򿪾ͽ�������Ϊtrue
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
            //�������һ�����飬��ǰ��ҽ�����5������boss�Ժ����õ�ǰ����+1�Ϳ��Բ��վ־���
            //������
            //��ʾ��ť
            //�Ķ��ż�
            openMailButton.gameObject.SetActive(true);
            openMailButton.interactable = true;
            MenuPanelIsOpen = true;

            openMailButton.onClick.AddListener(ReadMail);

            IsOkToPlayEnding = true;

        }


        //��ʼʱ�Զ�����Json�ļ�
        ParseData(GameDataControl.GetInstance().PlayerDataInfo.currentNodeID);

        //��ʼ���趨
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
        MusicMgr.GetInstance().PlaySound("SystemSoundEffect/ѡ��2", false);

        openMailButton.gameObject.SetActive(false);
        openMailButton.interactable = false;
        mail.gameObject.SetActive(true);
        closeMailButton.gameObject.SetActive(true);
        closeMailButton.interactable = true;

        closeMailButton.onClick.AddListener(CloseMail);
    }

    private void CloseMail()
    {
        MusicMgr.GetInstance().PlaySound("SystemSoundEffect/ѡ��2", false);

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
            MusicMgr.GetInstance().PlayBkMusic("maou_bgm_piano17-�����µĽ���");
        }
        else if(backgroundMusicName <= 5 && backgroundMusicName > 0)
        {
            MusicMgr.GetInstance().PlayBkMusic("maou_bgm_piano29-����");
        }
        else if(backgroundMusicName == 6)
        {
            MusicMgr.GetInstance().PlayBkMusic("maou_bgm_piano39-���");
        }
        else
        {
            MusicMgr.GetInstance().PlayBkMusic("maou_bgm_piano09-��ʼ");
        }

    }

    /// <summary>
    /// ������Ӧint��Json�ļ�
    /// </summary>
    public void ParseData(int DialogueDocumentationName)
    {
        //��ȡ������Json�ļ�
        Dialogues dialogues = new Dialogues();
        dialogues = JsonMgr.Instance.LoadData<Dialogues>("StroyDIalogue/Dialogue" + DialogueDocumentationName);
        //�����ݼ��ϰ���ID�ŷֱ����
        for (int i = 0; i < dialogues.Dialogue.Count; i++)
        {
            dialogdic.Add(dialogues.Dialogue[i].DialogueID, dialogues.Dialogue[i]);
        }

    }



    /// <summary>
    /// ���ݶԻ�ID���õ���ϸ��Ϣ
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
    /// �����ı�
    /// </summary>
    /// <param name="ID">Json�ļ��е�ID����</param>
    /// <param name="name">Character��Name</param>
    /// <param name="content">�Ի�����</param>
    public void UpdateText(int ID, string name, string content)
    {
        nameText.text = name;

        if(name == "�԰�")
        {
            nameText.enabled = false;
        }
        else if(name == "����Ц" || name == "����Τ" || name == "������" || name == "���޿�")
        {
            nameText.enabled = true;
            nameText.text = "����";
        }
        else
        {
            nameText.enabled = true;
        }

        contentText.text = content;
    }

    /// <summary>
    /// ��������
    /// </summary>
    /// <param name="ID"></param>
    /// <param name="name"></param>
    /// <param name="position"></param>
    public void UpdateImage(int ID, string name, string position)
    {
        if (position == "left")
        {

            if (name == "�԰�")
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
            if (name == "�԰�")
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
    /// ��ʾ�ı�������
    /// </summary>
    public void ShowDialogue()
    {
   



        //�������������ʱ�������ı�������
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
                BackGround.sprite = ResMgr.GetInstance().Load<Sprite>("Sprites/ʵ����");
                break;
            case 1:
                BackGround.sprite = ResMgr.GetInstance().Load<Sprite>("Sprites/�ֽ�");
                break;
            case 2:
                BackGround.sprite = ResMgr.GetInstance().Load<Sprite>("Sprites/����");
                break;
            case 3:
                BackGround.sprite = ResMgr.GetInstance().Load<Sprite>("Sprites/������");
                break;
            case 4:
                BackGround.sprite = ResMgr.GetInstance().Load<Sprite>("Sprites/���ù㳡");
                break;
            case 5:
                BackGround.sprite = ResMgr.GetInstance().Load<Sprite>("Sprites/����");
                break;
            case 6:
                BackGround.sprite = ResMgr.GetInstance().Load<Sprite>("Sprites/����");
                break;

        }
    }


    /// <summary>
    /// �������Ƿ�λ�ڰ�ť��������
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

        MusicMgr.GetInstance().PlaySound("SystemSoundEffect/ѡ��2", false);


        if (GameDataControl.GetInstance().PlayerDataInfo.currentNodeID == 6 && IsOkToPlayEnding)
        {
            UIManager.GetInstance().ShowPanel<UI_TeamMember>("UI_TeamMember", E_UI_Layer.Top);
        }
        else
        {
            ScenesMgr.GetInstance().LoadSceneAsyn("BattleScene", AfterLoadToBattle);
        }

        //������ǰ�׶�
        //UIManager.GetInstance().HidePanel("UI_DialoguePanel");
        //EventCenter.GetInstance().EventTrigger("currentPlayerNodeIDchange");
    }

    /// <summary>
    /// �����������ִ�е��߼�
    /// </summary>
    private void PressMouse()
    {

        if (!hitButton && !MenuPanelIsOpen)
        {
            //�������Ч
            //��Ҫһ���µ�
            MusicMgr.GetInstance().PlaySound("SystemSoundEffect/��ҳ2", false);

            //�������ʹ�Ի�������ȥ
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
                BackGround.sprite = ResMgr.GetInstance().Load<Sprite>("Sprites/�ֽ�");
            }


            if (dialogIndex == dialogdic.Count)
            {
                //������ǰ�׶�
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

