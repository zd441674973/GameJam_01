using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using static UnityEditor.Progress;

public class GameDataControl : BaseManager<GameDataControl>
{
    //玩家的数据信息
    public PlayerInfo PlayerDataInfo;
    public bool isParseDataExecuted = false;


    //字典存储玩家从Json读取到的物品数据
    public Dictionary<int, Card> cardInfoDic = new Dictionary<int, Card>();

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
        //File.WriteAllBytes(PlayerInfoSaveAdress, Encoding.UTF8.GetBytes(xxx));

        ////////////////////////事件监听，监听贯穿整个游戏的数据变化///////////////////////////////////////////////////////////
        ///这些监听不能删除
/*        //通过事件监听监听钱是否有变化
        EventCenter.GetInstance().AddEventListener<int>("MoneyChange", ChangeMoney);
        //通过事件监听是否存档
        EventCenter.GetInstance().AddEventListener("SavePlayerInfo", SavePlayerInfo);*/

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //执行完后，将当前ParseData执行状态调整为已经执行过
        isParseDataExecuted = true;
    }
}

public class PlayerInfo
{
    public int currentTileID;
    public List<Card> PlayerOwnedcards;
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
