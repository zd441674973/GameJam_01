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

    [Header("----------")]
    [SerializeField] Button _endTurnButton;
    [SerializeField] Button _skipButton;
    [SerializeField] Transform _enemyTurnStartUI;

    [Header("----------")]
    [SerializeField] Transform _darwCardFromEnemyHandUI;
    [SerializeField] TextMeshProUGUI _darwCardFromEnemyHandUIText;

    [Header("----------")]
    [SerializeField] Transform _destoryEnemyHandUI;
    [SerializeField] TextMeshProUGUI _destoryEnemyHandUIText;

    [Header("----------")]
    [SerializeField] Transform _discardPlayerHandUI;
    [SerializeField] TextMeshProUGUI _discardPlayerHandUIText;

    [Header("----------")]
    [SerializeField] Transform _siwtchHandCardTypeUI;
    [SerializeField] TextMeshProUGUI _siwtchHandCardTypeUIText;

    [Header("----------")]
    [SerializeField] Transform _playerPlayCardAreaUI;

    [Header("----------")]
    [SerializeField] Transform _cardInspectorUI;




    void Start()
    {
        _skipButton.onClick.AddListener
        (
            () =>
            {
                MouseActionManager.Instance.SkipCurrentState();
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

    public void SetSkipButtonTransform(bool condition) => _skipButton.GetComponent<Transform>().gameObject.SetActive(condition);

    public void SetEndTurnButtonFunction(bool condition) => _endTurnButton.enabled = condition;

    public void SetEnemyTurnStartUI(bool condition) => _enemyTurnStartUI.gameObject.SetActive(condition);




    public void SetDrawCardFromEnemyHandUI(bool condition) => _darwCardFromEnemyHandUI.gameObject.SetActive(condition);
    public void UpdateDrawCardFromEnemyHandUIText(int value) => _darwCardFromEnemyHandUIText.text = $"从敌人手中获得{value}张牌";




    public void SetDestoryEnemyHandUI(bool condition) => _destoryEnemyHandUI.gameObject.SetActive(condition);
    public void UpdateDestoryEnemyHandUIText(int value) => _destoryEnemyHandUIText.text = $"从敌人手牌中销毁{value}张牌";




    public void SetDiscardPlayerHandUI(bool condition) => _discardPlayerHandUI.gameObject.SetActive(condition);
    public void UpdateDiscardPlayerHandUIText(int value) => _discardPlayerHandUIText.text = $"选择{value}张牌并弃置";




    public void SetSwitchHandCardTypeUI(bool condition) => _siwtchHandCardTypeUI.gameObject.SetActive(condition);
    public void UpdateSwitchHandCardTypeUIText(int value) => _siwtchHandCardTypeUIText.text = $"SELECT TO SWITCH  \n {value} CARD-ATTRIBUTE \n FROM YOUR HAND";


    public void SetPlayerPlayCardAreaUI(bool condition) => _playerPlayCardAreaUI.gameObject.SetActive(condition);

    public void SetCardInspectorUI(bool condition) => _cardInspectorUI.gameObject.SetActive(condition);




}
