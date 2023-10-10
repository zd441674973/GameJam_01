using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;

public class BattleSceneSetUp : MonoBehaviour
{

    public static BattleSceneSetUp Instance;
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


    //Ѫ�����
    private float playerCurrentHealth;
    private float enemyCurrentHealth;

    private bool IsTriggerPlayerHealthChange = false; // ���ڱ���������ֵ�Ƿ����仯
    private bool IsTriggerEnemyHealthChange = false;

    private bool IsFistEnter;

    private Animator animator;










    void Start()
    {

        //currentLevel = GameDataControl.GetInstance().PlayerDataInfo.currentNodeID;

        // Debug.Log("currentLevel: " + currentLevel);
        currentLevel = 0;


        hasExecutedCheckHealth = false;
        IsFistEnter = false;

        playerHealthSystem = PlayerManager.Instance.GetHealthSystem();
        enemyHealthSystem = EnemyManager.Instance.GetHealthSystem();




        ChangeSet();

        // Debug.Log("PlayerMaxHealth: " + GameDataControl.GetInstance().PlayerDataInfo.playerMaxHealth);
        // Debug.Log("EnemyMaxHealth: " + GameDataControl.GetInstance().EnemyInfo_ZhiZhu.EnemyMaxHealth);

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

                animator = EnemyImage.gameObject.AddComponent<Animator>();
                animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animator/ZhiZhu");

                MusicMgr.GetInstance().PlayBkMusic("ħ���� ��`��  �ե��󥿥��`15-�̳�");

                //���ù��￨�Ƽ�Ѫ��
                Enemycards = GameDataControl.GetInstance().EnemyInfo_ZhiZhu.EnemyOwnedcards;
                enemyHealthSystem.Health = GameDataControl.GetInstance().EnemyInfo_ZhiZhu.EnemyMaxHealth;
                enemyCurrentHealth = enemyHealthSystem.Health;

                //������ҿ��Ƽ�Ѫ��
                playerHealthSystem.Health = GameDataControl.GetInstance().PlayerDataInfo.playerMaxHealth;
                playerCurrentHealth = playerHealthSystem.Health;
                Playercards = GameDataControl.GetInstance().PlayerDataInfo.PlayerOwnedcards;
                break;
            //ţţ

            case 1:
                backGround.sprite = ResMgr.GetInstance().Load<Sprite>("Sprites/�ֽ�");
                EnemyImage.sprite = ResMgr.GetInstance().Load<Sprite>("EnemySprites/ţţboss");

                animator = EnemyImage.gameObject.AddComponent<Animator>();
                animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animator/NiuNiu");

                MusicMgr.GetInstance().PlayBkMusic("maou_game_battle09-ţͷ��");


                //���ù��￨�Ƽ�Ѫ��
                Enemycards = GameDataControl.GetInstance().EnemyInfo_ZhiZhu.EnemyOwnedcards;
                enemyHealthSystem.Health = GameDataControl.GetInstance().EnemyInfo_ZhiZhu.EnemyMaxHealth;
                enemyCurrentHealth = enemyHealthSystem.Health;


                //������ҿ��Ƽ�Ѫ��
                playerHealthSystem.Health = GameDataControl.GetInstance().PlayerDataInfo.playerMaxHealth;
                Playercards = GameDataControl.GetInstance().PlayerDataInfo.PlayerOwnedcards;
                playerCurrentHealth = playerHealthSystem.Health;

                break;
            //������
            case 2:
                backGround.sprite = ResMgr.GetInstance().Load<Sprite>("Sprites/����");
                EnemyImage.sprite = ResMgr.GetInstance().Load<Sprite>("EnemySprites/����boss");

                animator = EnemyImage.gameObject.AddComponent<Animator>();
                animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animator/BaoZi");

                MusicMgr.GetInstance().PlayBkMusic("ħ���� ��`��  �ե��󥿥��`03-�ڱ�");

                //���ù��￨�Ƽ�Ѫ��
                Enemycards = GameDataControl.GetInstance().EnemyInfo_BaoZi.EnemyOwnedcards;
                enemyHealthSystem.Health = GameDataControl.GetInstance().EnemyInfo_BaoZi.EnemyMaxHealth;
                enemyCurrentHealth = enemyHealthSystem.Health;


                //������ҿ��Ƽ�Ѫ��
                playerHealthSystem.Health = GameDataControl.GetInstance().PlayerDataInfo.playerMaxHealth;
                Playercards = GameDataControl.GetInstance().PlayerDataInfo.PlayerOwnedcards;
                playerCurrentHealth = playerHealthSystem.Health;

                break;
            //��������
            case 3:
                backGround.sprite = ResMgr.GetInstance().Load<Sprite>("Sprites/������");
                EnemyImage.sprite = ResMgr.GetInstance().Load<Sprite>("EnemySprites/����boss");

                animator = EnemyImage.gameObject.AddComponent<Animator>();
                animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animator/BianFu");

                MusicMgr.GetInstance().PlayBkMusic("ħ���� ��`��  �ե��󥿥��`11-����");

                //���ù��￨�Ƽ�Ѫ��
                Enemycards = GameDataControl.GetInstance().EnemyInfo_BianFu.EnemyOwnedcards;
                enemyHealthSystem.Health = GameDataControl.GetInstance().EnemyInfo_BianFu.EnemyMaxHealth;
                enemyCurrentHealth = enemyHealthSystem.Health;


                //������ҿ��Ƽ�Ѫ��
                playerHealthSystem.Health = GameDataControl.GetInstance().PlayerDataInfo.playerMaxHealth;
                Playercards = GameDataControl.GetInstance().PlayerDataInfo.PlayerOwnedcards;
                playerCurrentHealth = playerHealthSystem.Health;

                break;
            //������
            case 4:
                backGround.sprite = ResMgr.GetInstance().Load<Sprite>("Sprites/���ù㳡");
                EnemyImage.sprite = ResMgr.GetInstance().Load<Sprite>("EnemySprites/��ʿboss");

                animator = EnemyImage.gameObject.AddComponent<Animator>();
                animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animator/XiuShi");

                MusicMgr.GetInstance().PlayBkMusic("ħ���� ��`��  �ե��󥿥��`12-��ʿ");

                //���ù��￨�Ƽ�Ѫ��
                Enemycards = GameDataControl.GetInstance().EnemyInfo_XiuShi.EnemyOwnedcards;
                enemyHealthSystem.Health = GameDataControl.GetInstance().EnemyInfo_XiuShi.EnemyMaxHealth;
                enemyCurrentHealth = enemyHealthSystem.Health;

                //������ҿ��Ƽ�Ѫ��
                playerHealthSystem.Health = GameDataControl.GetInstance().PlayerDataInfo.playerMaxHealth;
                Playercards = GameDataControl.GetInstance().PlayerDataInfo.PlayerOwnedcards;
                playerCurrentHealth = playerHealthSystem.Health;

                break;
            //������
            case 5:
                backGround.sprite = ResMgr.GetInstance().Load<Sprite>("Sprites/����");
                EnemyImage.sprite = ResMgr.GetInstance().Load<Sprite>("EnemySprites/����boss");

                animator = EnemyImage.gameObject.AddComponent<Animator>();
                animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animator/ZhuJiao");

                MusicMgr.GetInstance().PlayBkMusic("maou_bgm_piano36-����");

                //���ù��￨�Ƽ�Ѫ��
                Enemycards = GameDataControl.GetInstance().EnemyInfo_ZhuJiao.EnemyOwnedcards;
                enemyHealthSystem.Health = GameDataControl.GetInstance().EnemyInfo_ZhuJiao.EnemyMaxHealth;
                enemyCurrentHealth = enemyHealthSystem.Health;

                //������ҿ��Ƽ�Ѫ��
                playerHealthSystem.Health = GameDataControl.GetInstance().PlayerDataInfo.playerMaxHealth;
                Playercards = GameDataControl.GetInstance().PlayerDataInfo.PlayerOwnedcards;
                playerCurrentHealth = playerHealthSystem.Health;

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

        // ����������ֵ�Ƿ�ı䲢�����¼�
        if (playerHealthSystem.Health < playerCurrentHealth && !IsTriggerPlayerHealthChange)
        {
            playerCurrentHealth = playerHealthSystem.Health; // �����������ֵ

            //Debug.Log(1);
            //animator.SetBool("", true);                               // �����������ֵ�ı��¼�
            IsTriggerPlayerHealthChange = true;
        }
        else
        {
            IsTriggerPlayerHealthChange = false; // �������ֵδ�����仯�����ñ�־
        }

        // ����������ֵ�Ƿ�ı䲢�����¼�
        if (enemyHealthSystem.Health < enemyCurrentHealth && !IsTriggerEnemyHealthChange)
        {
            enemyCurrentHealth = enemyHealthSystem.Health; // ���µ�������ֵ
            animator.SetBool("IsGetHit", true);
            //Debug.Log("���˱�������");
            // ������������ֵ�ı��¼�
            IsTriggerEnemyHealthChange = true;
        }
        else
        {
            //Debug.Log("����û������");
            animator.SetBool("IsGetHit", false);
            IsTriggerEnemyHealthChange = false; // ��������ֵδ�����仯�����ñ�־
        }
    }

    private void AfterReturnToTitle()
    {

        //UIManager.GetInstance().HidePanel("UI_TitleScene");
    }
    private void loadScene()
    {
        //UIManager.GetInstance().HidePanel("UI_GameMap");
        //EventCenter.GetInstance().EventTrigger("turnOffBK");
    }

}
