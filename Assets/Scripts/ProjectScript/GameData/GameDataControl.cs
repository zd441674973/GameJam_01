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

        //没有玩家数据时，初始化一个默认数据
        if (PlayerDataInfo == null)
        {
            PlayerDataInfo = new PlayerInfo();
            //存储它
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
    /// 当敌人拥有卡牌数量改变时执行的函数
    /// </summary>
    /// <param name="cardID"></param>
    /// <param name="number"></param>
    public void ChangeEnemyCard(int cardID, int number)
    {
        // 查找列表中是否存在具有相同ID的卡牌
        Card existingCard = EnemyOwnedcards.Find(card => card.CardID == cardID);

        // 如果找到了相同ID的卡牌
        if (existingCard != null)
        {
            // 增加现有卡牌的数量
            existingCard.PlayerOwnedNumber += number;

            // 如果卡牌数量少于等于0，从列表中移除
            if (existingCard.PlayerOwnedNumber <= 0)
            {
                EnemyOwnedcards.Remove(existingCard);
            }
        }
        // 如果没有找到相同ID的卡牌，并且number大于0
        else if (number > 0)
        {
            // 创建新卡牌并添加到列表
            Card newCard = GameDataControl.GetInstance().GetCardInfo(cardID);
            newCard.PlayerOwnedNumber = number;
            EnemyOwnedcards.Add(newCard);
        }
    }
}


/////给敌人加牌/////////////////////////

public class EnemyZhiZhu : EnemyInfo
{
    public EnemyZhiZhu() : base("巨型蜘蛛")
    {
        EnemyMaxHealth = 120;
        ChangeEnemyCard(17, 7); //扉页7
        ChangeEnemyCard(24, 3); //幕鲁3
        ChangeEnemyCard(22, 2); //龙炎2
    }
}

public class EnemyNiuNiu : EnemyInfo
{
    public EnemyNiuNiu() : base("米诺陶洛斯")
    {
        EnemyMaxHealth = 240;
        ChangeEnemyCard(17, 6); //扉页7
        ChangeEnemyCard(24, 3); //幕鲁3
        ChangeEnemyCard(22, 2); //龙炎2
        ChangeEnemyCard(23, 1); //扉页7
    }
}

public class EnemyBaoZi : EnemyInfo
{
    public EnemyBaoZi() : base("赛珀派")
    {
        EnemyMaxHealth = 260;
        ChangeEnemyCard(17, 4); //扉页7
        ChangeEnemyCard(24, 3); //幕鲁3
        ChangeEnemyCard(22, 1); //龙炎2
        ChangeEnemyCard(18, 6); //黄衣6
        ChangeEnemyCard(19, 4); //阿瑞露4
    }
}
public class EnemyBianFu : EnemyInfo
{
    public EnemyBianFu() : base("卡玛佐兹")
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
    public EnemyXiuShi() : base("修士兰福德")
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
    public EnemyZhuJiao() : base("大主教兰道尔")
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
