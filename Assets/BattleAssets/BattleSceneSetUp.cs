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

    // 获取Canvas的RectTransform
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
            //教程关大蜘蛛
            case 0:
                backGround.sprite = ResMgr.GetInstance().Load<Sprite>("Sprites/实验室");
                EnemyImage.sprite = ResMgr.GetInstance().Load<Sprite>("EnemySprites/蜘蛛boss");

                SetUpEnemyImage();

                //设置怪物卡牌及血量
                Enemycards = GameDataControl.GetInstance().EnemyInfo_ZhiZhu.EnemyOwnedcards;
                enemyHealthSystem.Health = GameDataControl.GetInstance().EnemyInfo_ZhiZhu.EnemyMaxHealth;

                //设置玩家卡牌及血量
                playerHealthSystem.Health = GameDataControl.GetInstance().PlayerDataInfo.playerMaxHealth;
                Playercards = GameDataControl.GetInstance().PlayerDataInfo.PlayerOwnedcards;
                break;
            //牛牛
            case 1:
                backGround.sprite = ResMgr.GetInstance().Load<Sprite>("Sprites/街角");
                EnemyImage.sprite = ResMgr.GetInstance().Load<Sprite>("EnemySprites/牛牛boss");

                //设置怪物卡牌及血量
                Enemycards = GameDataControl.GetInstance().EnemyInfo_ZhiZhu.EnemyOwnedcards;
                enemyHealthSystem.Health = GameDataControl.GetInstance().EnemyInfo_ZhiZhu.EnemyMaxHealth;

                //设置玩家卡牌及血量
                playerHealthSystem.Health = GameDataControl.GetInstance().PlayerDataInfo.playerMaxHealth;
                Playercards = GameDataControl.GetInstance().PlayerDataInfo.PlayerOwnedcards;

                break;
            //赛珀派
            case 2:
                backGround.sprite = ResMgr.GetInstance().Load<Sprite>("Sprites/工厂");
                EnemyImage.sprite = ResMgr.GetInstance().Load<Sprite>("EnemySprites/豹子boss");

                //设置怪物卡牌及血量
                Enemycards = GameDataControl.GetInstance().EnemyInfo_BaoZi.EnemyOwnedcards;
                enemyHealthSystem.Health = GameDataControl.GetInstance().EnemyInfo_BaoZi.EnemyMaxHealth;

                //设置玩家卡牌及血量
                playerHealthSystem.Health = GameDataControl.GetInstance().PlayerDataInfo.playerMaxHealth;
                Playercards = GameDataControl.GetInstance().PlayerDataInfo.PlayerOwnedcards;

                break;
            //卡玛佐兹
            case 3:
                backGround.sprite = ResMgr.GetInstance().Load<Sprite>("Sprites/市政厅");
                EnemyImage.sprite = ResMgr.GetInstance().Load<Sprite>("EnemySprites/蝙蝠boss"); 

                //设置怪物卡牌及血量
                Enemycards = GameDataControl.GetInstance().EnemyInfo_BianFu.EnemyOwnedcards;
                enemyHealthSystem.Health = GameDataControl.GetInstance().EnemyInfo_BianFu.EnemyMaxHealth;

                //设置玩家卡牌及血量
                playerHealthSystem.Health = GameDataControl.GetInstance().PlayerDataInfo.playerMaxHealth;
                Playercards = GameDataControl.GetInstance().PlayerDataInfo.PlayerOwnedcards;

                break;
            //兰福德
            case 4:
                backGround.sprite = ResMgr.GetInstance().Load<Sprite>("Sprites/教堂广场");
                EnemyImage.sprite = ResMgr.GetInstance().Load<Sprite>("EnemySprites/修士boss");

                //设置怪物卡牌及血量
                Enemycards = GameDataControl.GetInstance().EnemyInfo_XiuShi.EnemyOwnedcards;
                enemyHealthSystem.Health = GameDataControl.GetInstance().EnemyInfo_XiuShi.EnemyMaxHealth;

                //设置玩家卡牌及血量
                playerHealthSystem.Health = GameDataControl.GetInstance().PlayerDataInfo.playerMaxHealth;
                Playercards = GameDataControl.GetInstance().PlayerDataInfo.PlayerOwnedcards;

                break;
            //兰道尔
            case 5:
                backGround.sprite = ResMgr.GetInstance().Load<Sprite>("Sprites/教堂");
                EnemyImage.sprite = ResMgr.GetInstance().Load<Sprite>("EnemySprites/主教boss");

                //设置怪物卡牌及血量
                Enemycards = GameDataControl.GetInstance().EnemyInfo_ZhuJiao.EnemyOwnedcards;
                enemyHealthSystem.Health = GameDataControl.GetInstance().EnemyInfo_ZhuJiao.EnemyMaxHealth;

                //设置玩家卡牌及血量
                playerHealthSystem.Health = GameDataControl.GetInstance().PlayerDataInfo.playerMaxHealth;
                Playercards = GameDataControl.GetInstance().PlayerDataInfo.PlayerOwnedcards;

                break;
        }

    }

    private void CheckHealth()
    {
        //玩家失去所有生命值
        if (playerHealthSystem.Health <= 0 && !hasExecutedCheckHealth)
        {
            ScenesMgr.GetInstance().LoadSceneAsyn("TitleScene", AfterReturnToTitle);
        }
        //敌人失去所有生命值
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
   

        // 计算屏幕中央的位置
        Vector2 centerScreenPosition = new Vector2(Screen.width / 2f, Screen.height / 2f);

        // 将屏幕中央的位置转换为Canvas中的位置
        Vector2 centerCanvasPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, centerScreenPosition, null, out centerCanvasPosition);

        // 在Canvas中创建3D对象的Prefab
        GameObject spawnedObject = Instantiate(EnemyPrefab, centerCanvasPosition, Quaternion.identity);

        // 将生成的3D对象设置为Canvas的子对象
        spawnedObject.transform.SetParent(canvasRect, false);
    }
}
