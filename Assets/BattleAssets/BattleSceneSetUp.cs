using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleSceneSetUp : MonoBehaviour
{

    public static BattleSceneSetUp Instance;

    public TMP_Text EnemyName;

    public GameObject PlayerGetHitAnimation;

    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Multiple instances occured");
            Destroy(Instance);
        }
        Instance = this;

    }

    private int currentLevel;
    public int CurrentLevel { get { return currentLevel; } }
    public Image backGround;
    public Image EnemyImage;
    public Image PlayerImage;

    public HealthSystem playerHealthSystem;
    public HealthSystem enemyHealthSystem;

    private List<Card> Playercards = new List<Card>();
    public List<Card> GetPlayerCardList() => Playercards;
    private List<Card> Enemycards = new List<Card>();
    public List<Card> GetEnemyCardList() => Enemycards;

    private bool hasExecutedCheckHealth;


    //血量检测
    private float playerCurrentHealth;
    private float enemyCurrentHealth;

    private bool IsTriggerPlayerHealthChange = false; // 用于标记玩家生命值是否发生变化
    private bool IsTriggerEnemyHealthChange = false;


    private Animator animator;



    void Start()
    {


        EventCenter.GetInstance().EventTrigger("InBattleScene");

        EventCenter.GetInstance().AddEventListener("AnimationTimerTwoSeconds", FinishBattleFunction);

        currentLevel = GameDataControl.GetInstance().PlayerDataInfo.currentNodeID;

        // Debug.Log("currentLevel: " + currentLevel);
        //currentLevel = 0;


        hasExecutedCheckHealth = false;

        playerHealthSystem = PlayerManager.Instance.GetHealthSystem();
        enemyHealthSystem = EnemyManager.Instance.GetHealthSystem();


        MusicMgr.GetInstance().ChangeBKValue(GameDataControl.GetInstance().PlayerDataInfo.playerBKvalue);
        MusicMgr.GetInstance().ChangeSoundValue(GameDataControl.GetInstance().PlayerDataInfo.playerSEvalue);

        ChangeSet();

        // Debug.Log("PlayerMaxHealth: " + GameDataControl.GetInstance().PlayerDataInfo.playerMaxHealth);
        // Debug.Log("EnemyMaxHealth: " + GameDataControl.GetInstance().EnemyInfo_ZhiZhu.EnemyMaxHealth);



    }

    private void OnDestroy()
    {
        EventCenter.GetInstance().RemoveEventListener("AnimationTimerTwoSeconds", FinishBattleFunction);

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

                EnemyName.text = "巨蜘蛛";

                animator = EnemyImage.gameObject.AddComponent<Animator>();
                animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animator/ZhiZhu");

                MusicMgr.GetInstance().PlayBkMusic("魔王魂 ループ  ファンタジー15-教程");

                //设置怪物卡牌及血量
                Enemycards = GameDataControl.GetInstance().EnemyInfo_ZhiZhu.EnemyOwnedcards;
                enemyHealthSystem.Health = GameDataControl.GetInstance().EnemyInfo_ZhiZhu.EnemyMaxHealth;
                enemyCurrentHealth = enemyHealthSystem.Health;

                //设置玩家卡牌及血量
                playerHealthSystem.Health = GameDataControl.GetInstance().PlayerDataInfo.playerMaxHealth;
                playerCurrentHealth = playerHealthSystem.Health;
                Playercards = GameDataControl.GetInstance().PlayerDataInfo.PlayerOwnedcards;
                break;
            //牛牛

            case 1:
                backGround.sprite = ResMgr.GetInstance().Load<Sprite>("Sprites/街角");
                EnemyImage.sprite = ResMgr.GetInstance().Load<Sprite>("EnemySprites/牛牛boss");

                EnemyName.text = "米诺陶洛斯";

                animator = EnemyImage.gameObject.AddComponent<Animator>();
                animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animator/NiuNiu");

                MusicMgr.GetInstance().PlayBkMusic("maou_game_battle09-牛头怪");


                //设置怪物卡牌及血量
                Enemycards = GameDataControl.GetInstance().EnemyInfo_ZhiZhu.EnemyOwnedcards;
                enemyHealthSystem.Health = GameDataControl.GetInstance().EnemyInfo_ZhiZhu.EnemyMaxHealth;
                enemyCurrentHealth = enemyHealthSystem.Health;


                //设置玩家卡牌及血量
                playerHealthSystem.Health = GameDataControl.GetInstance().PlayerDataInfo.playerMaxHealth;
                Playercards = GameDataControl.GetInstance().PlayerDataInfo.PlayerOwnedcards;
                playerCurrentHealth = playerHealthSystem.Health;

                break;
            //赛珀派
            case 2:
                backGround.sprite = ResMgr.GetInstance().Load<Sprite>("Sprites/工厂");
                EnemyImage.sprite = ResMgr.GetInstance().Load<Sprite>("EnemySprites/豹子boss");

                EnemyName.text = "赛珀派";

                animator = EnemyImage.gameObject.AddComponent<Animator>();
                animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animator/BaoZi");

                MusicMgr.GetInstance().PlayBkMusic("魔王魂 ループ  ファンタジー03-黑豹");

                //设置怪物卡牌及血量
                Enemycards = GameDataControl.GetInstance().EnemyInfo_BaoZi.EnemyOwnedcards;
                enemyHealthSystem.Health = GameDataControl.GetInstance().EnemyInfo_BaoZi.EnemyMaxHealth;
                enemyCurrentHealth = enemyHealthSystem.Health;


                //设置玩家卡牌及血量
                playerHealthSystem.Health = GameDataControl.GetInstance().PlayerDataInfo.playerMaxHealth;
                Playercards = GameDataControl.GetInstance().PlayerDataInfo.PlayerOwnedcards;
                playerCurrentHealth = playerHealthSystem.Health;

                break;
            //卡玛佐兹
            case 3:
                backGround.sprite = ResMgr.GetInstance().Load<Sprite>("Sprites/市政厅");
                EnemyImage.sprite = ResMgr.GetInstance().Load<Sprite>("EnemySprites/蝙蝠boss");

                EnemyName.text = "卡玛佐兹";

                animator = EnemyImage.gameObject.AddComponent<Animator>();
                animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animator/BianFu");

                MusicMgr.GetInstance().PlayBkMusic("魔王魂 ループ  ファンタジー11-蝙蝠");

                //设置怪物卡牌及血量
                Enemycards = GameDataControl.GetInstance().EnemyInfo_BianFu.EnemyOwnedcards;
                enemyHealthSystem.Health = GameDataControl.GetInstance().EnemyInfo_BianFu.EnemyMaxHealth;
                enemyCurrentHealth = enemyHealthSystem.Health;


                //设置玩家卡牌及血量
                playerHealthSystem.Health = GameDataControl.GetInstance().PlayerDataInfo.playerMaxHealth;
                Playercards = GameDataControl.GetInstance().PlayerDataInfo.PlayerOwnedcards;
                playerCurrentHealth = playerHealthSystem.Health;

                break;
            //兰福德
            case 4:
                backGround.sprite = ResMgr.GetInstance().Load<Sprite>("Sprites/教堂广场");
                EnemyImage.sprite = ResMgr.GetInstance().Load<Sprite>("EnemySprites/修士boss");

                EnemyName.text = "兰福德";

                animator = EnemyImage.gameObject.AddComponent<Animator>();
                animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animator/XiuShi");

                MusicMgr.GetInstance().PlayBkMusic("魔王魂 ループ  ファンタジー12-修士");

                //设置怪物卡牌及血量
                Enemycards = GameDataControl.GetInstance().EnemyInfo_XiuShi.EnemyOwnedcards;
                enemyHealthSystem.Health = GameDataControl.GetInstance().EnemyInfo_XiuShi.EnemyMaxHealth;
                enemyCurrentHealth = enemyHealthSystem.Health;

                //设置玩家卡牌及血量
                playerHealthSystem.Health = GameDataControl.GetInstance().PlayerDataInfo.playerMaxHealth;
                Playercards = GameDataControl.GetInstance().PlayerDataInfo.PlayerOwnedcards;
                playerCurrentHealth = playerHealthSystem.Health;

                break;
            //兰道尔
            case 5:
                backGround.sprite = ResMgr.GetInstance().Load<Sprite>("Sprites/教堂");
                EnemyImage.sprite = ResMgr.GetInstance().Load<Sprite>("EnemySprites/主教boss");

                EnemyName.text = "兰道尔";

                animator = EnemyImage.gameObject.AddComponent<Animator>();
                animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animator/ZhuJiao");

                MusicMgr.GetInstance().PlayBkMusic("maou_bgm_piano36-主教");

                //设置怪物卡牌及血量
                Enemycards = GameDataControl.GetInstance().EnemyInfo_ZhuJiao.EnemyOwnedcards;
                enemyHealthSystem.Health = GameDataControl.GetInstance().EnemyInfo_ZhuJiao.EnemyMaxHealth;
                enemyCurrentHealth = enemyHealthSystem.Health;

                //设置玩家卡牌及血量
                playerHealthSystem.Health = GameDataControl.GetInstance().PlayerDataInfo.playerMaxHealth;
                Playercards = GameDataControl.GetInstance().PlayerDataInfo.PlayerOwnedcards;
                playerCurrentHealth = playerHealthSystem.Health;

                break;
        }

    }

    private void FinishBattleFunction()
    {
        //生命值上限奖励
        GameDataControl.GetInstance().PlayerDataInfo.playerMaxHealth += 10;
        //手牌奖励
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

        EventCenter.GetInstance().EventTrigger("NotInBattleScene");

        GameDataControl.GetInstance().PlayerDataInfo.AlreadyFinishedAward_SelectNewCard = false;

        EventCenter.GetInstance().EventTrigger("currentPlayerNodeIDchange");

        ScenesMgr.GetInstance().LoadSceneAsyn("LoadingScene", loadScene);
        //ScenesMgr.GetInstance().LoadSceneAsyn("MainPage", AfterReturnToMain);




    }


    private void CheckHealth()
    {

        //玩家失去所有生命值
        if (playerHealthSystem.Health <= 0 && !hasExecutedCheckHealth)
        {
            EventCenter.GetInstance().EventTrigger("NotInBattleScene");

            ScenesMgr.GetInstance().LoadSceneAsyn("TitleScene", AfterReturnToTitle);
        }
        //敌人失去所有生命值
        if (enemyHealthSystem.Health <= 0 && !hasExecutedCheckHealth)
        {

            animator.SetBool("IsDead", true);

            hasExecutedCheckHealth = true;
            EventCenter.GetInstance().EventTrigger("AnimationTimerStart");



        }




        // 检查玩家生命值是否改变并触发事件
        if (playerHealthSystem.Health < playerCurrentHealth && !IsTriggerPlayerHealthChange)
        {
            playerCurrentHealth = playerHealthSystem.Health; // 更新玩家生命值

            PlayerGetHitAnimation.gameObject.SetActive(true);

            PlayerGetHitAnimation.gameObject.GetComponent<Animator>().SetBool("IsGetHit", true);

            Invoke("SetPlayerGetHitAnimationToFalse", 1f);

            IsTriggerPlayerHealthChange = true;
        }
        else
        {
            IsTriggerPlayerHealthChange = false; // 玩家生命值未发生变化，重置标志
        }

        // 检查敌人生命值是否改变并触发事件
        if (enemyHealthSystem.Health < enemyCurrentHealth && !IsTriggerEnemyHealthChange)
        {
            enemyCurrentHealth = enemyHealthSystem.Health; // 更新敌人生命值
            animator.SetBool("IsGetHit", true);
            //Debug.Log("敌人被攻击了");
            // 触发敌人生命值改变事件
            IsTriggerEnemyHealthChange = true;
        }
        else
        {
            //Debug.Log("敌人没被攻击");
            animator.SetBool("IsGetHit", false);
            IsTriggerEnemyHealthChange = false; // 敌人生命值未发生变化，重置标志
        }
    }



    private void SetPlayerGetHitAnimationToFalse()
    {
        PlayerGetHitAnimation.gameObject.SetActive(false);
    }



    private void AfterReturnToTitle()
    {

        UIManager.GetInstance().HidePanel("UI_TitleScene");
    }
    private void loadScene()
    {
        //UIManager.GetInstance().HidePanel("UI_GameMap");
        //EventCenter.GetInstance().EventTrigger("turnOffBK");
    }

}
