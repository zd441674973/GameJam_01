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

    public List<Card> AllCardList;

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

        AllCardList = ConvertDictionaryToList(cardInfoDic);

        //û���������ʱ����ʼ��һ��Ĭ������
        if (PlayerDataInfo == null)
        {
            PlayerDataInfo = new PlayerInfo();
            //�洢��
            JsonMgr.Instance.SaveData(PlayerDataInfo, "PlayerSaveData");
        }

/*        if (EnemyInfo_ZhiZhu == null)
        {
            EnemyInfo_ZhiZhu = new EnemyZhiZhu();
       
        }*/
/*
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

        }*/

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

    public List<Card> ConvertDictionaryToList(Dictionary<int, Card> dictionary)
    {
        List<Card> list = new List<Card>(dictionary.Values);
        return list;
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


public class EnemyZhiZhu
{
    public int ZhiZhuMaxHealth;

    public int ZhiZhuCardSum;

    public List<Card> ZhiZhuOwnedcards;


    public EnemyZhiZhu()
    {
        
        ZhiZhuMaxHealth = 120;
        ZhiZhuOwnedcards = new List<Card>();
        ZhiZhuCardSum = 0;

        /////��֩�����/////////////////////////

        ChangeZhiZhuCard(17, 8);
        ChangeZhiZhuCard(24, 4);
        ChangeZhiZhuCard(22, 2);
    }

    /// <summary>
    /// �����ӵ�п��������ı�ʱִ�еĺ���
    /// </summary>
    /// <param name="cardID"></param>
    /// <param name="number"></param>
    public void ChangeZhiZhuCard(int cardID, int numberToAdd)
    {
        bool cardFound = false; // ���ڱ���Ƿ��ҵ�ƥ��Ŀ�Ƭ

        // ���� List �е�ÿ�� Card ����
        foreach (var card in ZhiZhuOwnedcards)
        {
            // ����ҵ�ƥ��Ŀ�Ƭ ID
            if (card.CardID == cardID)
            {
                cardFound = true;
                // ��������
                card.PlayerOwnedNumber += numberToAdd;

                if (card.PlayerOwnedNumber <= 0)
                {
                    card.PlayerOwnedNumber = 0;
                }
            }
        }

        // ���û���ҵ�ƥ��Ŀ�Ƭ��������¿�Ƭ���б�
        if (!cardFound && numberToAdd > 0)
        {
            Card newCard = GameDataControl.GetInstance().GetCardInfo(cardID);
            newCard.PlayerOwnedNumber = numberToAdd;
            ZhiZhuOwnedcards.Add(newCard);
        }
    }

    /*
        void ChangeZhiZhuCard(int cardID, int numberToAdd)
        {
            ZhiZhuOwnedcards = new List<Card>();

            foreach (Card card in ZhiZhuOwnedcards)
            {
                if (card.CardID == cardID)
                {
                    for (int i = 0; i < numberToAdd; i++)
                    {
                        ZhiZhuOwnedcards.Add(card);
                    }
                }
            }
        }*/
    public int CalculateTotalNumber()
    {
        // ʹ��LINQ��Sum����������Card��Number�ܺ�
        ZhiZhuCardSum = ZhiZhuOwnedcards.Sum(card => card.PlayerOwnedNumber);

        return ZhiZhuCardSum;
    }
}

public class EnemyNiuNiu

{
    public int NiuNiuMaxHealth;

    public int NiuNiuCardSum;

    public List<Card> NiuNiuOwnedcards;


    public EnemyNiuNiu()
    {

        NiuNiuMaxHealth = 240;
        NiuNiuOwnedcards = new List<Card>();
        NiuNiuCardSum = 0;

        /////��ţţ����/////////////////////////
        ChangeNiuNiuCard(24, 3); //���5
        ChangeNiuNiuCard(22, 2); //����5
        ChangeNiuNiuCard(23, 1);  //������Ȧ2
        ChangeNiuNiuCard(17, 8);  //���ܹ���1

    }

    /// <summary>
    /// �����ӵ�п��������ı�ʱִ�еĺ���
    /// </summary>
    /// <param name="cardID"></param>
    /// <param name="number"></param>
    public void ChangeNiuNiuCard(int cardID, int number)
    {
        // �����б����Ƿ���ھ�����ͬID�Ŀ���
        Card existingCard = NiuNiuOwnedcards.Find(card => card.CardID == cardID);

        // ����ҵ�����ͬID�Ŀ���
        if (existingCard != null)
        {
            // �������п��Ƶ�����
            existingCard.PlayerOwnedNumber += number;

            // ��������������ڵ���0�����б����Ƴ�
            if (existingCard.PlayerOwnedNumber <= 0)
            {
                NiuNiuOwnedcards.Remove(existingCard);
            }
        }
        // ���û���ҵ���ͬID�Ŀ��ƣ�����number����0
        else if (number > 0)
        {
            // �����¿��Ʋ���ӵ��б�
            Card newCard = GameDataControl.GetInstance().GetCardInfo(cardID);
            newCard.PlayerOwnedNumber = number;
            NiuNiuOwnedcards.Add(newCard);
        }
    }

    public void SumPlayerCards()
    {
        List<Card> cards = GameDataControl.GetInstance().EnemyInfo_NiuNiu.NiuNiuOwnedcards;
        NiuNiuCardSum = cards.Sum(card => card.PlayerOwnedNumber);
    }
}

public class EnemyBaoZi

{
    public int BaoZiMaxHealth;

    public int BaoZiCardSum;

    public List<Card> BaoZiOwnedcards;


    public EnemyBaoZi()
    {

        BaoZiMaxHealth = 260;
        BaoZiOwnedcards = new List<Card>();
        BaoZiCardSum = 0;

        /////�����Ӽ���/////////////////////////
        ChangeBaoZiCard(18, 6);
        ChangeBaoZiCard(24, 3);
        ChangeBaoZiCard(19, 4);
        ChangeBaoZiCard(17, 4);
        ChangeBaoZiCard(22, 1);

    }

    /// <summary>
    /// �����ӵ�п��������ı�ʱִ�еĺ���
    /// </summary>
    /// <param name="cardID"></param>
    /// <param name="number"></param>
    public void ChangeBaoZiCard(int cardID, int number)
    {
        // �����б����Ƿ���ھ�����ͬID�Ŀ���
        Card existingCard = BaoZiOwnedcards.Find(card => card.CardID == cardID);

        // ����ҵ�����ͬID�Ŀ���
        if (existingCard != null)
        {
            // �������п��Ƶ�����
            existingCard.PlayerOwnedNumber += number;

            // ��������������ڵ���0�����б����Ƴ�
            if (existingCard.PlayerOwnedNumber <= 0)
            {
                BaoZiOwnedcards.Remove(existingCard);
            }
        }
        // ���û���ҵ���ͬID�Ŀ��ƣ�����number����0
        else if (number > 0)
        {
            // �����¿��Ʋ���ӵ��б�
            Card newCard = GameDataControl.GetInstance().GetCardInfo(cardID);
            newCard.PlayerOwnedNumber = number;
            BaoZiOwnedcards.Add(newCard);
        }
    }

    public void SumPlayerCards()
    {
        List<Card> cards = GameDataControl.GetInstance().EnemyInfo_BaoZi.BaoZiOwnedcards;
        BaoZiCardSum = cards.Sum(card => card.PlayerOwnedNumber);
    }
}


public class EnemyBianFu

{
    public int BianFuMaxHealth;

    public int BianFuCardSum;

    public List<Card> BianFuOwnedcards;


    public EnemyBianFu()
    {

        BianFuMaxHealth = 180;
        BianFuOwnedcards = new List<Card>();
        BianFuCardSum = 0;

        /////���������/////////////////////////
        ChangeBianFuCard(17, 2);
        ChangeBianFuCard(24, 2);
        ChangeBianFuCard(23, 2);
        ChangeBianFuCard(20, 4);
        ChangeBianFuCard(22, 4);
    }

    /// <summary>
    /// �����ӵ�п��������ı�ʱִ�еĺ���
    /// </summary>
    /// <param name="cardID"></param>
    /// <param name="number"></param>
    public void ChangeBianFuCard(int cardID, int number)
    {
        // �����б����Ƿ���ھ�����ͬID�Ŀ���
        Card existingCard = BianFuOwnedcards.Find(card => card.CardID == cardID);

        // ����ҵ�����ͬID�Ŀ���
        if (existingCard != null)
        {
            // �������п��Ƶ�����
            existingCard.PlayerOwnedNumber += number;

            // ��������������ڵ���0�����б����Ƴ�
            if (existingCard.PlayerOwnedNumber <= 0)
            {
                BianFuOwnedcards.Remove(existingCard);
            }
        }
        // ���û���ҵ���ͬID�Ŀ��ƣ�����number����0
        else if (number > 0)
        {
            // �����¿��Ʋ���ӵ��б�
            Card newCard = GameDataControl.GetInstance().GetCardInfo(cardID);
            newCard.PlayerOwnedNumber = number;
            BianFuOwnedcards.Add(newCard);
        }
    }

    public void SumPlayerCards()
    {
        List<Card> cards = GameDataControl.GetInstance().EnemyInfo_BianFu.BianFuOwnedcards;
        BianFuCardSum = cards.Sum(card => card.PlayerOwnedNumber);
    }
}

public class EnemyXiuShi

{
    public int XiuShiMaxHealth;

    public int XiuShiCardSum;

    public List<Card> XiuShiOwnedcards;


    public EnemyXiuShi()
    {

        XiuShiMaxHealth = 300;
        XiuShiOwnedcards = new List<Card>();
        XiuShiCardSum = 0;

        /////����ʿ����/////////////////////////
        ChangeXiuShiCard(20, 2);
        ChangeXiuShiCard(17, 2);
        ChangeXiuShiCard(19, 4);
        ChangeXiuShiCard(22, 2);
        ChangeXiuShiCard(24, 2);
        ChangeXiuShiCard(18, 4);
        ChangeXiuShiCard(23, 2);
    }

    /// <summary>
    /// �����ӵ�п��������ı�ʱִ�еĺ���
    /// </summary>
    /// <param name="cardID"></param>
    /// <param name="number"></param>
    public void ChangeXiuShiCard(int cardID, int number)
    {
        // �����б����Ƿ���ھ�����ͬID�Ŀ���
        Card existingCard = XiuShiOwnedcards.Find(card => card.CardID == cardID);

        // ����ҵ�����ͬID�Ŀ���
        if (existingCard != null)
        {
            // �������п��Ƶ�����
            existingCard.PlayerOwnedNumber += number;

            // ��������������ڵ���0�����б����Ƴ�
            if (existingCard.PlayerOwnedNumber <= 0)
            {
                XiuShiOwnedcards.Remove(existingCard);
            }
        }
        // ���û���ҵ���ͬID�Ŀ��ƣ�����number����0
        else if (number > 0)
        {
            // �����¿��Ʋ���ӵ��б�
            Card newCard = GameDataControl.GetInstance().GetCardInfo(cardID);
            newCard.PlayerOwnedNumber = number;
            XiuShiOwnedcards.Add(newCard);
        }
    }

    public void SumPlayerCards()
    {
        List<Card> cards = GameDataControl.GetInstance().EnemyInfo_XiuShi.XiuShiOwnedcards;
        XiuShiCardSum = cards.Sum(card => card.PlayerOwnedNumber);
    }
}

public class EnemyZhuJiao

{
    public int ZhuJiaoMaxHealth;

    public int ZhuJiaoCardSum;

    public List<Card> ZhuJiaoOwnedcards;


    public EnemyZhuJiao()
    {

        ZhuJiaoMaxHealth = 500;
        ZhuJiaoOwnedcards = new List<Card>();
        ZhuJiaoCardSum = 0;

        /////�����̼���/////////////////////////
        ChangeZhuJiaoCard(21, 6);
        ChangeZhuJiaoCard(18, 2);
        ChangeZhuJiaoCard(20, 4);
        ChangeZhuJiaoCard(23, 2);
        ChangeZhuJiaoCard(22, 5);
        ChangeZhuJiaoCard(19, 2);
        ChangeZhuJiaoCard(24, 5);
    }

    /// <summary>
    /// �����ӵ�п��������ı�ʱִ�еĺ���
    /// </summary>
    /// <param name="cardID"></param>
    /// <param name="number"></param>
    public void ChangeZhuJiaoCard(int cardID, int number)
    {
        // �����б����Ƿ���ھ�����ͬID�Ŀ���
        Card existingCard = ZhuJiaoOwnedcards.Find(card => card.CardID == cardID);

        // ����ҵ�����ͬID�Ŀ���
        if (existingCard != null)
        {
            // �������п��Ƶ�����
            existingCard.PlayerOwnedNumber += number;

            // ��������������ڵ���0�����б����Ƴ�
            if (existingCard.PlayerOwnedNumber <= 0)
            {
                ZhuJiaoOwnedcards.Remove(existingCard);
            }
        }
        // ���û���ҵ���ͬID�Ŀ��ƣ�����number����0
        else if (number > 0)
        {
            // �����¿��Ʋ���ӵ��б�
            Card newCard = GameDataControl.GetInstance().GetCardInfo(cardID);
            newCard.PlayerOwnedNumber = number;
            ZhuJiaoOwnedcards.Add(newCard);
        }
    }

    public void SumPlayerCards()
    {
        List<Card> cards = GameDataControl.GetInstance().EnemyInfo_ZhuJiao.ZhuJiaoOwnedcards;
        ZhuJiaoCardSum = cards.Sum(card => card.PlayerOwnedNumber);
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
