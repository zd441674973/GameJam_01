using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using UnityEngine;
using static UnityEditor.Progress;

public class GameDataControl : BaseManager<GameDataControl>
{
    //��ҵ�������Ϣ
    public PlayerInfo PlayerDataInfo;
    public EnemyZhiZhu EnemyInfo_ZhiZhu;
    public EnemyNiuNiu EnemyInfo_NiuNiu;
    public EnemyBaoZi EnemyInfo_BaoZi;
    public EnemyBianFu EnemyInfo_BianFu;
    public EnemyXiuShi EnemyInfo_XiuShi;
    public EnemyZhuJiao EnemyInfo_ZhuJiao;

    public bool isParseDataExecuted = false;


    //�ֵ�洢��Ҵ�Json��ȡ������Ʒ����
    public Dictionary<int, Card> cardInfoDic = new Dictionary<int, Card>();

    //��ҵĴ洢·��
    private static string PlayerInfoSaveAdress = Application.persistentDataPath + "/PlayerSaveData.json";

    /// <summary>
    /// �����洢���ݵ�Json��Ϣ
    /// </summary>
    public void ParseData()
    {

        //��ȡ������Json�ļ�//��Ʒ��Ϣ
        CardInfo CardInfos = JsonMgr.Instance.LoadData<CardInfo>("CardInfo");
        //�����ݼ��ϰ���ID�ŷֱ����
        for (int i = 0; i < CardInfos.cardInfo.Count; ++i)
        {
            cardInfoDic.Add(CardInfos.cardInfo[i].CardID, CardInfos.cardInfo[i]);
        }

        //û���������ʱ����ʼ��һ��Ĭ������
        if (PlayerDataInfo == null)
        {
            PlayerDataInfo = new PlayerInfo();
            //�洢��
            JsonMgr.Instance.SaveData(PlayerDataInfo, "PlayerSaveData");
        }

        if (EnemyInfo_ZhiZhu == null)
        {
            EnemyInfo_ZhiZhu = new EnemyZhiZhu();
       
        }

        if (EnemyInfo_NiuNiu == null)
        {
            EnemyInfo_NiuNiu = new EnemyNiuNiu();

        }

        if (EnemyInfo_BaoZi == null)
        {
            EnemyInfo_BaoZi = new EnemyBaoZi();

        }

        if (EnemyInfo_BianFu == null)
        {
            EnemyInfo_BianFu = new EnemyBianFu();

        }

        if (EnemyInfo_XiuShi == null)
        {
            EnemyInfo_XiuShi = new EnemyXiuShi();

        }

        if (EnemyInfo_ZhuJiao == null)
        {
            EnemyInfo_ZhuJiao = new EnemyZhuJiao();

        }

        ////////////////////////�¼������������ᴩ������Ϸ�����ݱ仯///////////////////////////////////////////////////////////
        ///��Щ��������ɾ��,ֻ��ִ��һ��
       //ͨ���¼��������������Ƿ��б仯
        EventCenter.GetInstance().AddEventListener<int,int>("CardChange", ChangeCard);
        //ͨ���¼������Ƿ�浵
        EventCenter.GetInstance().AddEventListener("SavePlayerInfo", SavePlayerInfo);
        //ͨ���¼��������㿨������
        EventCenter.GetInstance().AddEventListener("SumPlayerCard", SumPlayerCards);

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //ִ����󣬽���ǰParseDataִ��״̬����Ϊ�Ѿ�ִ�й�
        isParseDataExecuted = true;
    }

    /// <summary>
    /// �������ʹ��GameDataMgr���ÿ��������ı�
    /// </summary>
    /// <param name="money"></param>
    private void ChangeCard(int cardID, int number)
    {
        PlayerDataInfo.ChangeCard(cardID, number);
        //����Ǯ�ʹ洢����
        EventCenter.GetInstance().EventTrigger("SavePlayerInfo");
    }

    /// <summary>
    /// ������������ҿ�������
    /// </summary>
    /// <param name="money"></param>
    private void SumPlayerCards()
    {
        PlayerDataInfo.SumPlayerCards();
        EventCenter.GetInstance().EventTrigger("SavePlayerInfo");
    }

    /// <summary>
    /// �������ʹ��GameDataMgr���ô浵
    /// </summary>
    public void SavePlayerInfo()
    {
        JsonMgr.Instance.SaveData(PlayerDataInfo, "PlayerSaveData");
    }


    /// <summary>
    /// ��ȡ�����Ϣ----�Ժ�����Ϊ���ٶ�ȡ����浵
    /// </summary>
    public void LoadPlayerInfo()
    {
        if (File.Exists(PlayerInfoSaveAdress))
        {
            //��ȡ��Ϊ�浵��Json�ļ�
            PlayerDataInfo = JsonMgr.Instance.LoadData<PlayerInfo>("PlayerSaveData");
        }
    }


    /// <summary>
    /// ���ݿ���ID��ÿ�����Ϣ
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Card GetCardInfo(int cardId)
    {
        if (cardInfoDic.ContainsKey(cardId))
            return cardInfoDic[cardId];
        return null;
    }

}

public class PlayerInfo
{
    public int currentNodeID;
    public int playerMaxHealth;
    
    public int playerCardsSum;
    //ÿ�ν������Ի���¿��ƵĴ���
    public int drawNewCardTimes;
    public List<Card> PlayerOwnedcards;

    public bool AlreadyFinishedAward_SelectNewCard;
    public bool AlreadyFinishedAward_DelateCard;

    public string currentTargetScene;

    public float playerBKvalue;
    public float playerSEvalue;
    
    public PlayerInfo()
    {
        currentNodeID = 0;
        playerMaxHealth = 50;
        PlayerOwnedcards = new List<Card>();
        playerCardsSum = 0;

        drawNewCardTimes = 3;

        AlreadyFinishedAward_SelectNewCard = true;
        AlreadyFinishedAward_DelateCard = true;

        currentTargetScene = "MainPage";

        playerBKvalue = 0.1f;
        playerSEvalue = 0.5f;

        /////����Ҽ���/////////////////////////
        ChangeCard(0, 4); //���5
        ChangeCard(1, 4); //����5
        ChangeCard(2, 3);  //������Ȧ2
        ChangeCard(3, 1);  //���ܹ���1
        ChangeCard(15, 1); //�޸����
        ChangeCard(10, 6); //����10
        ChangeCard(13, 2); //��ű���
        ChangeCard(11, 1); //��ƿ2
    }

    /// <summary>
    /// �����ӵ�п��������ı�ʱִ�еĺ���
    /// </summary>
    /// <param name="cardID"></param>
    /// <param name="number"></param>
    public void ChangeCard(int cardID, int number)
    {
        // �����б����Ƿ���ھ�����ͬID�Ŀ���
        Card existingCard = PlayerOwnedcards.Find(card => card.CardID == cardID);

        // ����ҵ�����ͬID�Ŀ���
        if (existingCard != null)
        {
            // �������п��Ƶ�����
            existingCard.PlayerOwnedNumber += number;

            // ��������������ڵ���0�����б����Ƴ�
            if (existingCard.PlayerOwnedNumber <= 0)
            {
                PlayerOwnedcards.Remove(existingCard);
            }
        }
        // ���û���ҵ���ͬID�Ŀ��ƣ�����number����0
        else if (number > 0)
        {
            // �����¿��Ʋ���ӵ��б�
            Card newCard = GameDataControl.GetInstance().GetCardInfo(cardID);
            newCard.PlayerOwnedNumber = number;
            PlayerOwnedcards.Add(newCard);
        }
    }

    public void SumPlayerCards()
    {
        List<Card> cards = GameDataControl.GetInstance().PlayerDataInfo.PlayerOwnedcards;
        playerCardsSum = cards.Sum(card => card.PlayerOwnedNumber);
    }
}


public class EnemyInfo
{

    public string EnemyName;
    public int EnemyMaxHealth;
    public List<Card> EnemyOwnedcards;

    public EnemyInfo()
    {
        EnemyMaxHealth = 0;
        EnemyOwnedcards = new List<Card>();
        EnemyName = "name";

    }



    public EnemyInfo(string name)
    {
        EnemyMaxHealth = 100;                                                                
        EnemyOwnedcards = new List<Card>();
        EnemyName = name;

    }



    /// <summary>
    /// ������ӵ�п��������ı�ʱִ�еĺ���
    /// </summary>
    /// <param name="cardID"></param>
    /// <param name="number"></param>
    public void ChangeEnemyCard(int cardID, int number)
    {
        // �����б����Ƿ���ھ�����ͬID�Ŀ���
        Card existingCard = EnemyOwnedcards.Find(card => card.CardID == cardID);

        // ����ҵ�����ͬID�Ŀ���
        if (existingCard != null)
        {
            // �������п��Ƶ�����
            existingCard.PlayerOwnedNumber += number;

            // ��������������ڵ���0�����б����Ƴ�
            if (existingCard.PlayerOwnedNumber <= 0)
            {
                EnemyOwnedcards.Remove(existingCard);
            }
        }
        // ���û���ҵ���ͬID�Ŀ��ƣ�����number����0
        else if (number > 0)
        {
            // �����¿��Ʋ���ӵ��б�
            Card newCard = GameDataControl.GetInstance().GetCardInfo(cardID);
            newCard.PlayerOwnedNumber = number;
            EnemyOwnedcards.Add(newCard);
        }
    }
}


/////�����˼���/////////////////////////

public class EnemyZhiZhu : EnemyInfo
{
    public EnemyZhiZhu() : base("����֩��")
    {
        EnemyMaxHealth = 120;
        ChangeEnemyCard(17, 7); //��ҳ7
        ChangeEnemyCard(24, 3); //Ļ³3
        ChangeEnemyCard(22, 2); //����2
    }
}

public class EnemyNiuNiu : EnemyInfo
{
    public EnemyNiuNiu() : base("��ŵ����˹")
    {
        EnemyMaxHealth = 240;
        ChangeEnemyCard(17, 6); //��ҳ7
        ChangeEnemyCard(24, 3); //Ļ³3
        ChangeEnemyCard(22, 2); //����2
        ChangeEnemyCard(23, 1); //��ҳ7
    }
}

public class EnemyBaoZi : EnemyInfo
{
    public EnemyBaoZi() : base("������")
    {
        EnemyMaxHealth = 260;
        ChangeEnemyCard(17, 4); //��ҳ7
        ChangeEnemyCard(24, 3); //Ļ³3
        ChangeEnemyCard(22, 1); //����2
        ChangeEnemyCard(18, 6); //����6
        ChangeEnemyCard(19, 4); //����¶4
    }
}
public class EnemyBianFu : EnemyInfo
{
    public EnemyBianFu() : base("��������")
    {
        EnemyMaxHealth = 80;
        ChangeEnemyCard(23, 5);
        ChangeEnemyCard(20, 4);
        ChangeEnemyCard(23, 1);
        ChangeEnemyCard(24, 5);
        ChangeEnemyCard(22, 2);

    }
}
public class EnemyXiuShi : EnemyInfo
{
    public EnemyXiuShi() : base("��ʿ������")
    {
        EnemyMaxHealth = 90;
        ChangeEnemyCard(23, 5);
        ChangeEnemyCard(18, 4);
        ChangeEnemyCard(19, 3);
        ChangeEnemyCard(24, 2);
        ChangeEnemyCard(17, 3);

    }
}
public class EnemyZhuJiao : EnemyInfo
{
    public EnemyZhuJiao() : base("������������")
    {
        EnemyMaxHealth = 100;
        ChangeEnemyCard(23, 5);
        ChangeEnemyCard(21, 3);
        ChangeEnemyCard(20, 3);
        ChangeEnemyCard(24, 2);
        ChangeEnemyCard(22, 2);
        ChangeEnemyCard(23, 3);
        ChangeEnemyCard(18, 2);
    }
}


//���ڶ�ȡJson�����е���������Ϣ�����ݽṹ
public class CardInfo
{
    public List<Card> cardInfo;
}

//װ�ص���ÿ�����ݵ��࣬���ֱ�����Json�ļ���ֵ��Ӧ
[System.Serializable]
public class Card
{
    public int CardID;
    public string CardName;
    public string Type;
    public int PlayerOwnedNumber;
    public int ElectricEnergyEfficiencyChange;
    public int MagicEnergyEfficiencyChange;
    public int HealthToOpponentMin;
    public int HealthToOpponentMax;
    public int NumberOfAttack;
    public int HealthToSelfMin;
    public int HealthToSelfMax;
    public int SheildToEnemy;
    public int SheildToSelf;
    public int DrawCardFromLabrary;
    public int DrawCardFromEnemyLabrary;
    public int GetEnemyHandCard;
    public int DestroyEnemyHandCard;
    public string Description;
    public string Rarity;
}
