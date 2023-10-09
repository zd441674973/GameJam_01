using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;

public class BattleSceneSetUp : MonoBehaviour
{
    private int currentLevel;
    public Image backGround;
    public Image EnemyImage;
    public Image PlayerImage;

    public HealthSystem playerHealthSystem;
    public HealthSystem enemyHealthSystem;

    private List<Card> Playercards = new List<Card>();
    private List<Card> Enemycards = new List<Card>();

    private bool hasExecutedCheckHealth;

    // ��ȡCanvas��RectTransform
    public RectTransform canvasRect;
    public GameObject EnemyPrefab;

    private void Awake()
    {
        currentLevel = GameDataControl.GetInstance().PlayerDataInfo.currentNodeID;


    }


    void Start()
    {
        hasExecutedCheckHealth = false;

        ChangeSet();

    }

    private void Update()
    {

        //Debug.Log(enemyHealthSystem.Health);

        CheckHealth();
    }

    private void ChangeSet()
    {
        switch (currentLevel)
        {
            //�̳̹ش�֩��
            case 0:
                backGround.sprite = ResMgr.GetInstance().Load<Sprite>("Sprites/ʵ����");
                EnemyImage.sprite = ResMgr.GetInstance().Load<Sprite>("EnemySprites/֩��boss");

                SetUpEnemyImage();

                //���ù��￨�Ƽ�Ѫ��
                Enemycards = GameDataControl.GetInstance().EnemyInfo_ZhiZhu.EnemyOwnedcards;
                enemyHealthSystem.Health = GameDataControl.GetInstance().EnemyInfo_ZhiZhu.EnemyMaxHealth;

                //������ҿ��Ƽ�Ѫ��
                playerHealthSystem.Health = GameDataControl.GetInstance().PlayerDataInfo.playerMaxHealth;
                Playercards = GameDataControl.GetInstance().PlayerDataInfo.PlayerOwnedcards;
                break;
            //ţţ
            case 1:
                backGround.sprite = ResMgr.GetInstance().Load<Sprite>("Sprites/�ֽ�");
                EnemyImage.sprite = ResMgr.GetInstance().Load<Sprite>("EnemySprites/ţţboss");

                //���ù��￨�Ƽ�Ѫ��
                Enemycards = GameDataControl.GetInstance().EnemyInfo_ZhiZhu.EnemyOwnedcards;
                enemyHealthSystem.Health = GameDataControl.GetInstance().EnemyInfo_ZhiZhu.EnemyMaxHealth;

                //������ҿ��Ƽ�Ѫ��
                playerHealthSystem.Health = GameDataControl.GetInstance().PlayerDataInfo.playerMaxHealth;
                Playercards = GameDataControl.GetInstance().PlayerDataInfo.PlayerOwnedcards;

                break;
            //������
            case 2:
                backGround.sprite = ResMgr.GetInstance().Load<Sprite>("Sprites/����");
                EnemyImage.sprite = ResMgr.GetInstance().Load<Sprite>("EnemySprites/����boss");

                //���ù��￨�Ƽ�Ѫ��
                Enemycards = GameDataControl.GetInstance().EnemyInfo_BaoZi.EnemyOwnedcards;
                enemyHealthSystem.Health = GameDataControl.GetInstance().EnemyInfo_BaoZi.EnemyMaxHealth;

                //������ҿ��Ƽ�Ѫ��
                playerHealthSystem.Health = GameDataControl.GetInstance().PlayerDataInfo.playerMaxHealth;
                Playercards = GameDataControl.GetInstance().PlayerDataInfo.PlayerOwnedcards;

                break;
            //��������
            case 3:
                backGround.sprite = ResMgr.GetInstance().Load<Sprite>("Sprites/������");
                EnemyImage.sprite = ResMgr.GetInstance().Load<Sprite>("EnemySprites/����boss"); 

                //���ù��￨�Ƽ�Ѫ��
                Enemycards = GameDataControl.GetInstance().EnemyInfo_BianFu.EnemyOwnedcards;
                enemyHealthSystem.Health = GameDataControl.GetInstance().EnemyInfo_BianFu.EnemyMaxHealth;

                //������ҿ��Ƽ�Ѫ��
                playerHealthSystem.Health = GameDataControl.GetInstance().PlayerDataInfo.playerMaxHealth;
                Playercards = GameDataControl.GetInstance().PlayerDataInfo.PlayerOwnedcards;

                break;
            //������
            case 4:
                backGround.sprite = ResMgr.GetInstance().Load<Sprite>("Sprites/���ù㳡");
                EnemyImage.sprite = ResMgr.GetInstance().Load<Sprite>("EnemySprites/��ʿboss");

                //���ù��￨�Ƽ�Ѫ��
                Enemycards = GameDataControl.GetInstance().EnemyInfo_XiuShi.EnemyOwnedcards;
                enemyHealthSystem.Health = GameDataControl.GetInstance().EnemyInfo_XiuShi.EnemyMaxHealth;

                //������ҿ��Ƽ�Ѫ��
                playerHealthSystem.Health = GameDataControl.GetInstance().PlayerDataInfo.playerMaxHealth;
                Playercards = GameDataControl.GetInstance().PlayerDataInfo.PlayerOwnedcards;

                break;
            //������
            case 5:
                backGround.sprite = ResMgr.GetInstance().Load<Sprite>("Sprites/����");
                EnemyImage.sprite = ResMgr.GetInstance().Load<Sprite>("EnemySprites/����boss");

                //���ù��￨�Ƽ�Ѫ��
                Enemycards = GameDataControl.GetInstance().EnemyInfo_ZhuJiao.EnemyOwnedcards;
                enemyHealthSystem.Health = GameDataControl.GetInstance().EnemyInfo_ZhuJiao.EnemyMaxHealth;

                //������ҿ��Ƽ�Ѫ��
                playerHealthSystem.Health = GameDataControl.GetInstance().PlayerDataInfo.playerMaxHealth;
                Playercards = GameDataControl.GetInstance().PlayerDataInfo.PlayerOwnedcards;

                break;
        }

    }

    private void CheckHealth()
    {
        //���ʧȥ��������ֵ
        if (playerHealthSystem.Health <= 0 && !hasExecutedCheckHealth)
        {
            ScenesMgr.GetInstance().LoadSceneAsyn("TitleScene", AfterReturnToTitle);
        }
        //����ʧȥ��������ֵ
        if (enemyHealthSystem.Health <= 0 && !hasExecutedCheckHealth)
        {
            GameDataControl.GetInstance().PlayerDataInfo.drawNewCardTimes = 3;

            if (currentLevel == 0)
            {
                GameDataControl.GetInstance().PlayerDataInfo.currentNodeID = 1;
            }

            if (currentLevel == 1)
            {
                GameDataControl.GetInstance().PlayerDataInfo.currentNodeID = 2;
            }

            if (currentLevel == 2)
            {
                GameDataControl.GetInstance().PlayerDataInfo.currentNodeID = 3;
            }

            if (currentLevel == 3)
            {
                GameDataControl.GetInstance().PlayerDataInfo.currentNodeID = 4;
            }

            if (currentLevel == 4)
            {
                GameDataControl.GetInstance().PlayerDataInfo.currentNodeID = 5;
            }

            if (currentLevel == 5)
            {
                GameDataControl.GetInstance().PlayerDataInfo.currentNodeID = 6;
            }


            GameDataControl.GetInstance().PlayerDataInfo.AlreadyFinishedAward_SelectNewCard = false;

            EventCenter.GetInstance().EventTrigger("currentPlayerNodeIDchange");

            ScenesMgr.GetInstance().LoadSceneAsyn("LoadingScene", loadScene);
            //ScenesMgr.GetInstance().LoadSceneAsyn("MainPage", AfterReturnToMain);

            hasExecutedCheckHealth = true;
        }
    }

    private void AfterReturnToTitle()
    {

    }
    private void loadScene()
    {/*
        UIManager.GetInstance().HidePanel("UI_MainPage");
        UIManager.GetInstance().HidePanel("UI_GameMap");*/
        //EventCenter.GetInstance().EventTrigger("turnOffBK");
    }


    private void SetUpEnemyImage()
    {
   

        // ������Ļ�����λ��
        Vector2 centerScreenPosition = new Vector2(Screen.width / 2f, Screen.height / 2f);

        // ����Ļ�����λ��ת��ΪCanvas�е�λ��
        Vector2 centerCanvasPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, centerScreenPosition, null, out centerCanvasPosition);

        // ��Canvas�д���3D�����Prefab
        GameObject spawnedObject = Instantiate(EnemyPrefab, centerCanvasPosition, Quaternion.identity);

        // �����ɵ�3D��������ΪCanvas���Ӷ���
        spawnedObject.transform.SetParent(canvasRect, false);
    }
}
