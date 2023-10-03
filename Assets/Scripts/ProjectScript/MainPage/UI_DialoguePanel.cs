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
    //���ڼ������Ƿ�λ�ڰ�ť��������
    private bool hitButton;
    //��ť����
    public Rect ButtonArea;


    //���MenuPanel�Ƿ���
    private bool MenuPanelIsOpen;

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


        //��ʼʱ�Զ�����Json�ļ�
        ParseData(GameDataControl.GetInstance().PlayerDataInfo.currentNodeID);

        //��ʼ���趨
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


    /// <summary>
    /// �����������ִ�е��߼�
    /// </summary>
    private void PressMouse()
    {
        if (!hitButton && !MenuPanelIsOpen)
        {
            //�������Ч
            MusicMgr.GetInstance().PlaySound("maou_se_sound20_Maou-Select", false);

            //�������ʹ�Ի�������ȥ
            dialogIndex = GetInFo(dialogIndex).JumpToID;
            currentdialogIndex = dialogIndex;

            if (dialogIndex == dialogdic.Count)
            {
                //������ǰ�׶�
                UIManager.GetInstance().HidePanel("UI_DialoguePanel");
                //���ս������ִ��
                //EventCenter.GetInstance().EventTrigger("currentPlayerNodeIDchange");
                
                if(GameDataControl.GetInstance().PlayerDataInfo.currentNodeID > 5)
                {
                    //������
                }
                else if(GameDataControl.GetInstance().PlayerDataInfo.currentNodeID == 0)
                {
                    //����������
                }
                else
                {
                    //�����Ӧս������
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
