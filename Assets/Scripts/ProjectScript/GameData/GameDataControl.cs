using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using static UnityEditor.Progress;

public class GameDataControl : BaseManager<GameDataControl>
{
    //��ҵ�������Ϣ
    public PlayerInfo PlayerDataInfo;
    public bool isParseDataExecuted = false;


    //�ֵ�洢��Ҵ�Json��ȡ������Ʒ����
    public Dictionary<int, Card> cardInfoDic = new Dictionary<int, Card>();

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
            cardInfoDic.Add(CardInfos.cardInfo[i].ID, CardInfos.cardInfo[i]);
        }

        //û���������ʱ����ʼ��һ��Ĭ������
        if (PlayerDataInfo == null)
        {
            PlayerDataInfo = new PlayerInfo();
            //�洢��
            JsonMgr.Instance.SaveData(PlayerDataInfo, "PlayerSaveData");
        }
        //File.WriteAllBytes(PlayerInfoSaveAdress, Encoding.UTF8.GetBytes(xxx));

        ////////////////////////�¼������������ᴩ������Ϸ�����ݱ仯///////////////////////////////////////////////////////////
        ///��Щ��������ɾ��
/*        //ͨ���¼���������Ǯ�Ƿ��б仯
        EventCenter.GetInstance().AddEventListener<int>("MoneyChange", ChangeMoney);
        //ͨ���¼������Ƿ�浵
        EventCenter.GetInstance().AddEventListener("SavePlayerInfo", SavePlayerInfo);*/

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //ִ����󣬽���ǰParseDataִ��״̬����Ϊ�Ѿ�ִ�й�
        isParseDataExecuted = true;
    }
}

public class PlayerInfo
{
    public int currentTileID;
    public List<Card> PlayerOwnedcards;
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
    public int ID;
    public string Name;
    public string Icon;
    public string Type;
    public int Price;
    public string Tips;
    public int OpenChapter;
    public int Number;
    public string SkillName;
}
