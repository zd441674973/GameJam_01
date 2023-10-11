using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using UnityEngine;
using static UnityEditor.Progress;

public class GameDataControl : BaseManager<GameDataControl>
{
    //玩家的数据信息
    public PlayerInfo PlayerDataInfo;
    public EnemyZhiZhu EnemyInfo_ZhiZhu;
    public EnemyNiuNiu EnemyInfo_NiuNiu;
    public EnemyBaoZi EnemyInfo_BaoZi;
    public EnemyBianFu EnemyInfo_BianFu;
    public EnemyXiuShi EnemyInfo_XiuShi;
    public EnemyZhuJiao EnemyInfo_ZhuJiao;

    public bool isParseDataExecuted = false;

    public List<Card> AllCardList;

    //字典存储玩家从Json读取到的物品数据
    public Dictionary<int, Card> cardInfoDic = new Dictionary<int, Card>();

    //玩家的存储路径
    private static string PlayerInfoSaveAdress = Application.persistentDataPath + "/PlayerSaveData.json";

    /// <summary>
    /// 解析存储数据的Json信息
    /// </summary>
    public void ParseData()
    {

        //读取并解析Json文件//物品信息
        CardInfo CardInfos = JsonMgr.Instance.LoadData<CardInfo>("CardInfo");
        //将数据集合按照ID号分别放入
        for (int i = 0; i < CardInfos.cardInfo.Count; ++i)
        {
            cardInfoDic.Add(CardInfos.cardInfo[i].CardID, CardInfos.cardInfo[i]);
        }

        AllCardList = ConvertDictionaryToList(cardInfoDic);

        //没有玩家数据时，初始化一个默认数据
        if (PlayerDataInfo == null)
        {
            PlayerDataInfo = new PlayerInfo();
            //存储它
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

        ////////////////////////事件监听，监听贯穿整个游戏的数据变化///////////////////////////////////////////////////////////
        ///这些监听不能删除,只能执行一次
       //通过事件监听监听卡牌是否有变化
        EventCenter.GetInstance().AddEventListener<int,int>("CardChange", ChangeCard);
        //通过事件监听是否存档
        EventCenter.GetInstance().AddEventListener("SavePlayerInfo", SavePlayerInfo);
        //通过事件监听计算卡牌数量
        EventCenter.GetInstance().AddEventListener("SumPlayerCard", SumPlayerCards);

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //执行完后，将当前ParseData执行状态调整为已经执行过
        isParseDataExecuted = true;
    }

    /// <summary>
    /// 方便外界使用GameDataMgr调用卡牌数量改变
    /// </summary>
    /// <param name="money"></param>
    private void ChangeCard(int cardID, int number)
    {
        PlayerDataInfo.ChangeCard(cardID, number);
        //减少钱就存储数据
        EventCenter.GetInstance().EventTrigger("SavePlayerInfo");
    }

    /// <summary>
    /// 方便外界计算玩家卡牌总数
    /// </summary>
    /// <param name="money"></param>
    private void SumPlayerCards()
    {
        PlayerDataInfo.SumPlayerCards();
        EventCenter.GetInstance().EventTrigger("SavePlayerInfo");
    }

    /// <summary>
    /// 方便外界使用GameDataMgr调用存档
    /// </summary>
    public void SavePlayerInfo()
    {
        JsonMgr.Instance.SaveData(PlayerDataInfo, "PlayerSaveData");
    }


    /// <summary>
    /// 读取玩家信息----以后将其作为快速读取最近存档
    /// </summary>
    public void LoadPlayerInfo()
    {
        if (File.Exists(PlayerInfoSaveAdress))
        {
            //读取作为存档的Json文件
            PlayerDataInfo = JsonMgr.Instance.LoadData<PlayerInfo>("PlayerSaveData");
        }
    }


    /// <summary>
    /// 根据卡牌ID获得卡牌信息
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
    //每次奖励可以获得新卡牌的次数
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

        /////给玩家加牌/////////////////////////
        ChangeCard(0, 4); //射击5
        ChangeCard(1, 4); //护盾5
        ChangeCard(2, 3);  //增幅线圈2
        ChangeCard(3, 1);  //护盾过载1
        ChangeCard(15, 1); //修复组件
        ChangeCard(10, 6); //虹吸10
        ChangeCard(13, 2); //电磁爆破
        ChangeCard(11, 1); //电瓶2
    }

    /// <summary>
    /// 当玩家拥有卡牌数量改变时执行的函数
    /// </summary>
    /// <param name="cardID"></param>
    /// <param name="number"></param>
    public void ChangeCard(int cardID, int number)
    {
        // 查找列表中是否存在具有相同ID的卡牌
        Card existingCard = PlayerOwnedcards.Find(card => card.CardID == cardID);

        // 如果找到了相同ID的卡牌
        if (existingCard != null)
        {
            // 增加现有卡牌的数量
            existingCard.PlayerOwnedNumber += number;

            // 如果卡牌数量少于等于0，从列表中移除
            if (existingCard.PlayerOwnedNumber <= 0)
            {
                PlayerOwnedcards.Remove(existingCard);
            }
        }
        // 如果没有找到相同ID的卡牌，并且number大于0
        else if (number > 0)
        {
            // 创建新卡牌并添加到列表
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

        /////给蜘蛛加牌/////////////////////////

        ChangeZhiZhuCard(17, 8);
        ChangeZhiZhuCard(24, 4);
        ChangeZhiZhuCard(22, 2);
    }

    /// <summary>
    /// 当玩家拥有卡牌数量改变时执行的函数
    /// </summary>
    /// <param name="cardID"></param>
    /// <param name="number"></param>
    public void ChangeZhiZhuCard(int cardID, int numberToAdd)
    {
        bool cardFound = false; // 用于标记是否找到匹配的卡片

        // 遍历 List 中的每个 Card 对象
        foreach (var card in ZhiZhuOwnedcards)
        {
            // 如果找到匹配的卡片 ID
            if (card.CardID == cardID)
            {
                cardFound = true;
                // 增加数量
                card.PlayerOwnedNumber += numberToAdd;

                if (card.PlayerOwnedNumber <= 0)
                {
                    card.PlayerOwnedNumber = 0;
                }
            }
        }

        // 如果没有找到匹配的卡片，就添加新卡片到列表
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
        // 使用LINQ的Sum函数来计算Card的Number总和
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

        /////给牛牛加牌/////////////////////////
        ChangeNiuNiuCard(24, 3); //射击5
        ChangeNiuNiuCard(22, 2); //护盾5
        ChangeNiuNiuCard(23, 1);  //增幅线圈2
        ChangeNiuNiuCard(17, 8);  //护盾过载1

    }

    /// <summary>
    /// 当玩家拥有卡牌数量改变时执行的函数
    /// </summary>
    /// <param name="cardID"></param>
    /// <param name="number"></param>
    public void ChangeNiuNiuCard(int cardID, int number)
    {
        // 查找列表中是否存在具有相同ID的卡牌
        Card existingCard = NiuNiuOwnedcards.Find(card => card.CardID == cardID);

        // 如果找到了相同ID的卡牌
        if (existingCard != null)
        {
            // 增加现有卡牌的数量
            existingCard.PlayerOwnedNumber += number;

            // 如果卡牌数量少于等于0，从列表中移除
            if (existingCard.PlayerOwnedNumber <= 0)
            {
                NiuNiuOwnedcards.Remove(existingCard);
            }
        }
        // 如果没有找到相同ID的卡牌，并且number大于0
        else if (number > 0)
        {
            // 创建新卡牌并添加到列表
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

        /////给豹子加牌/////////////////////////
        ChangeBaoZiCard(18, 6);
        ChangeBaoZiCard(24, 3);
        ChangeBaoZiCard(19, 4);
        ChangeBaoZiCard(17, 4);
        ChangeBaoZiCard(22, 1);

    }

    /// <summary>
    /// 当玩家拥有卡牌数量改变时执行的函数
    /// </summary>
    /// <param name="cardID"></param>
    /// <param name="number"></param>
    public void ChangeBaoZiCard(int cardID, int number)
    {
        // 查找列表中是否存在具有相同ID的卡牌
        Card existingCard = BaoZiOwnedcards.Find(card => card.CardID == cardID);

        // 如果找到了相同ID的卡牌
        if (existingCard != null)
        {
            // 增加现有卡牌的数量
            existingCard.PlayerOwnedNumber += number;

            // 如果卡牌数量少于等于0，从列表中移除
            if (existingCard.PlayerOwnedNumber <= 0)
            {
                BaoZiOwnedcards.Remove(existingCard);
            }
        }
        // 如果没有找到相同ID的卡牌，并且number大于0
        else if (number > 0)
        {
            // 创建新卡牌并添加到列表
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

        /////给蝙蝠加牌/////////////////////////
        ChangeBianFuCard(17, 2);
        ChangeBianFuCard(24, 2);
        ChangeBianFuCard(23, 2);
        ChangeBianFuCard(20, 4);
        ChangeBianFuCard(22, 4);
    }

    /// <summary>
    /// 当玩家拥有卡牌数量改变时执行的函数
    /// </summary>
    /// <param name="cardID"></param>
    /// <param name="number"></param>
    public void ChangeBianFuCard(int cardID, int number)
    {
        // 查找列表中是否存在具有相同ID的卡牌
        Card existingCard = BianFuOwnedcards.Find(card => card.CardID == cardID);

        // 如果找到了相同ID的卡牌
        if (existingCard != null)
        {
            // 增加现有卡牌的数量
            existingCard.PlayerOwnedNumber += number;

            // 如果卡牌数量少于等于0，从列表中移除
            if (existingCard.PlayerOwnedNumber <= 0)
            {
                BianFuOwnedcards.Remove(existingCard);
            }
        }
        // 如果没有找到相同ID的卡牌，并且number大于0
        else if (number > 0)
        {
            // 创建新卡牌并添加到列表
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

        /////给修士加牌/////////////////////////
        ChangeXiuShiCard(20, 2);
        ChangeXiuShiCard(17, 2);
        ChangeXiuShiCard(19, 4);
        ChangeXiuShiCard(22, 2);
        ChangeXiuShiCard(24, 2);
        ChangeXiuShiCard(18, 4);
        ChangeXiuShiCard(23, 2);
    }

    /// <summary>
    /// 当玩家拥有卡牌数量改变时执行的函数
    /// </summary>
    /// <param name="cardID"></param>
    /// <param name="number"></param>
    public void ChangeXiuShiCard(int cardID, int number)
    {
        // 查找列表中是否存在具有相同ID的卡牌
        Card existingCard = XiuShiOwnedcards.Find(card => card.CardID == cardID);

        // 如果找到了相同ID的卡牌
        if (existingCard != null)
        {
            // 增加现有卡牌的数量
            existingCard.PlayerOwnedNumber += number;

            // 如果卡牌数量少于等于0，从列表中移除
            if (existingCard.PlayerOwnedNumber <= 0)
            {
                XiuShiOwnedcards.Remove(existingCard);
            }
        }
        // 如果没有找到相同ID的卡牌，并且number大于0
        else if (number > 0)
        {
            // 创建新卡牌并添加到列表
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

        /////给主教加牌/////////////////////////
        ChangeZhuJiaoCard(21, 6);
        ChangeZhuJiaoCard(18, 2);
        ChangeZhuJiaoCard(20, 4);
        ChangeZhuJiaoCard(23, 2);
        ChangeZhuJiaoCard(22, 5);
        ChangeZhuJiaoCard(19, 2);
        ChangeZhuJiaoCard(24, 5);
    }

    /// <summary>
    /// 当玩家拥有卡牌数量改变时执行的函数
    /// </summary>
    /// <param name="cardID"></param>
    /// <param name="number"></param>
    public void ChangeZhuJiaoCard(int cardID, int number)
    {
        // 查找列表中是否存在具有相同ID的卡牌
        Card existingCard = ZhuJiaoOwnedcards.Find(card => card.CardID == cardID);

        // 如果找到了相同ID的卡牌
        if (existingCard != null)
        {
            // 增加现有卡牌的数量
            existingCard.PlayerOwnedNumber += number;

            // 如果卡牌数量少于等于0，从列表中移除
            if (existingCard.PlayerOwnedNumber <= 0)
            {
                ZhuJiaoOwnedcards.Remove(existingCard);
            }
        }
        // 如果没有找到相同ID的卡牌，并且number大于0
        else if (number > 0)
        {
            // 创建新卡牌并添加到列表
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





//用于读取Json中所有道具配置信息的数据结构
public class CardInfo
{
    public List<Card> cardInfo;
}

//装载道具每个数据的类，名字必须与Json文件键值对应
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
