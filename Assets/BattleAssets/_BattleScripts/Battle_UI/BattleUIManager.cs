using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleUIManager : MonoBehaviour
{
    public static BattleUIManager Instance;
    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Multiple instances occured");
            Destroy(Instance);
        }
        Instance = this;
    }

    [SerializeField] Button _endTurnButton;
    [SerializeField] Transform _enemyTurnStartUI;
    [SerializeField] Transform _darwCardFromEnemyHandUI;
    [SerializeField] TextMeshProUGUI _darwCardFromEnemyHandUIText;
    [SerializeField] Button _drawEnmyCardskipButton;
    [SerializeField] Transform _destoryEnemyHandUI;
    [SerializeField] TextMeshProUGUI _destoryEnemyHandUIText;

    void Start()
    {
        _drawEnmyCardskipButton.onClick.AddListener
        (
            () =>
            {
                MouseActionManager.Instance.SkipDrawFromOpponentHandState();
            }
        );

        _endTurnButton.onClick.AddListener
        (
            () =>
            {
                if (!TurnSystem.Instance.IsPlayerTurn()) return;
                TurnSystem.Instance.NextTurn();
            }
        );


    }

    public void SetEndTurnButtonFunction(bool condition) => _endTurnButton.enabled = condition;

    public void SetEnemyTurnStartUI(bool condition) => _enemyTurnStartUI.gameObject.SetActive(condition);

    public void SetDrawCardFromEnemyHandUI(bool condition) => _darwCardFromEnemyHandUI.gameObject.SetActive(condition);
    public void SetDrawCardFromEnemyHandUIText(int value) => _darwCardFromEnemyHandUIText.text = $"SELECT {value} CARD FROM ENEMY HAND";

    public void DestoryEnemyHandUI(bool condition) => _destoryEnemyHandUI.gameObject.SetActive(condition);
    public void DestoryEnemyHandUIText(int value) => _destoryEnemyHandUIText.text = $"DESTORY {value} CARD FROM ENEMY HAND";


}
