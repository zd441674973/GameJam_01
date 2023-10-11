using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitDeckInitialization : MonoBehaviour
{

    [SerializeField] List<Transform> _totalCardList;
    [SerializeField] List<Transform> _playerInitialDeck;
    [SerializeField] List<Transform> _enemyInitialDeck;

    List<Transform> PlayerDeck() => CardDeckManager.Instance.GetPlayerDeck();

    List<Transform> EnemyDeck() => CardDeckManager.Instance.GetEnemyDeck();


    List<Transform> _baoZiDeck;
    List<Transform> _bianFuDeck;
    List<Transform> _niuniuList;
    List<Transform> _xiushiList;
    List<Transform> _zhizhuList;
    List<Transform> _zhujiaoList;



    void Start()
    {

        List<Card> enemyCardList = BattleSceneSetUp.Instance.GetEnemyCardList();
        FindCardFromTotalCardDeck(enemyCardList, _enemyInitialDeck);
        foreach (Transform card in _enemyInitialDeck)
        {
            Transform cloneCard = CardDeckManager.Instance.GenerateCard(card, transform);
            EnemyDeck().Add(cloneCard);
        }

        //Find player cards
        List<Card> playerCardList = BattleSceneSetUp.Instance.GetPlayerCardList();
        FindCardFromTotalCardDeck(playerCardList, _playerInitialDeck);
        foreach (Transform card in _playerInitialDeck)
        {
            Transform cloneCard = CardDeckManager.Instance.GenerateCard(card, transform);
            PlayerDeck().Add(cloneCard);
        }


        // debug only
       /* Transform B16 = CardDeckManager.Instance.GenerateCard(_totalCardList[16], transform);
        for (int i = 0; i < 20; i++)
        {
            PlayerDeck().Add(B16);
        }*/






        // List<Card> baoziList = BattleSceneSetUp.Instance.GetEnemyCardList();
        // FindCardFromTotalCardDeck(baoziList, _baoZiDeck);


        // List<Card> bianfuList = GameDataControl.GetInstance().EnemyInfo_BianFu.EnemyOwnedcards;
        // FindCardFromTotalCardDeck(bianfuList, _bianFuDeck);



        // List<Card> niuniuList = GameDataControl.GetInstance().EnemyInfo_NiuNiu.EnemyOwnedcards;
        // FindCardFromTotalCardDeck(niuniuList, _niuniuList);



        // List<Card> xiushiList = GameDataControl.GetInstance().EnemyInfo_XiuShi.EnemyOwnedcards;
        // FindCardFromTotalCardDeck(xiushiList, _xiushiList);



        // List<Card> zhizhuList = GameDataControl.GetInstance().EnemyInfo_ZhiZhu.EnemyOwnedcards;
        // FindCardFromTotalCardDeck(zhizhuList, _zhizhuList);



        // List<Card> zhujiaoList = GameDataControl.GetInstance().EnemyInfo_ZhuJiao.EnemyOwnedcards;
        // FindCardFromTotalCardDeck(zhujiaoList, _zhujiaoList);

    }

    // Look through _totalCardList to find CardTransform you need, add to Deck you want.
    void FindCardFromTotalCardDeck(List<Card> cardList, List<Transform> deckList)
    {
        foreach (var card in cardList)
        {
            for (int i = 0; i < card.PlayerOwnedNumber; i++)
            {
                deckList.Add(_totalCardList[card.CardID]);
            }
        }
    }





}
