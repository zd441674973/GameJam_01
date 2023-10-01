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

        ////////////////////////�¼������������ᴩ������Ϸ�����ݱ仯///////////////////////////////////////////////////////////
        ///��Щ��������ɾ��,ֻ��ִ��һ��
       //ͨ���¼��������������Ƿ��б仯
        EventCenter.GetInstance().AddEventListener<int,int>("CardChange", ChangeCard);
        //ͨ���¼������Ƿ�浵
        EventCenter.GetInstance().AddEventListener("SavePlayerInfo", SavePlayerInfo);
        //ͨ���¼������Ƿ�浵
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
    
    public PlayerInfo()
    {
        currentNodeID = 0;
        playerMaxHealth = 50;
        PlayerOwnedcards = new List<Card>();
        playerCardsSum = 0;

        drawNewCardTimes = 3;

        AlreadyFinishedAward_SelectNewCard = true;
        AlreadyFinishedAward_DelateCard = true;

        /////����Ҽ���/////////////////////////
        ChangeCard(0, 3);

        ChangeCard(1, 3);
        ChangeCard(2, 3);
        ChangeCard(3, 1);
        ChangeCard(4, 2);
        ChangeCard(5, 2);
        ChangeCard(8, 2);
        ChangeCard(9, 2);
        ChangeCard(11, 2);
    }

    /// <summary>
    /// �����ӵ�п��������ı�ʱִ�еĺ���
    /// </summary>
    /// <param name="cardID"></param>
    /// <param name="number"></param>
    public void ChangeCard(int cardID, int number)
    {
        List<Card> cardsToRemove = new List<Card>(); // ���ڴ洢��Ҫɾ���Ŀ���

        foreach (Card playerCard in PlayerOwnedcards)
        {
            // �������Ѿ�ӵ�иÿ���
            if (playerCard.CardID == cardID)
            {
                playerCard.PlayerOwnedNumber += number;
                if (playerCard.PlayerOwnedNumber <= 0)
                {
                    cardsToRemove.Add(playerCard); // ����Ҫɾ���Ŀ�����ӵ��б�
                }
            }
        }

        // �Ƴ���Ҫɾ���Ŀ���
        foreach (Card cardToRemove in cardsToRemove)
        {
            PlayerOwnedcards.Remove(cardToRemove);
        }

        // ������Ʋ����ڣ�����ӿ��Ʋ�����PlayerOwnedNumber
        if (cardsToRemove.Count == 0 && number > 0)
        {
            Card newCard = GameDataControl.GetInstance().GetCardInfo(cardID);
            newCard.PlayerOwnedNumber = number; // ����PlayerOwnedNumber
            PlayerOwnedcards.Add(newCard);
        }
    }

    public void SumPlayerCards()
    {
        List<Card> cards = GameDataControl.GetInstance().PlayerDataInfo.PlayerOwnedcards;
        playerCardsSum = cards.Sum(card => card.PlayerOwnedNumber);
    }
}


//DataInfos
/// //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



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
