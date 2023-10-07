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
    public void UpdateDrawCardFromEnemyHandUIText(int value) => _darwCardFromEnemyHandUIText.text = $"SELECT TO DRAW {value} CARD FROM ENEMY HAND";




    public void SetDestoryEnemyHandUI(bool condition) => _destoryEnemyHandUI.gameObject.SetActive(condition);
    public void UpdateDestoryEnemyHandUIText(int value) => _destoryEnemyHandUIText.text = $"SELECT TO DESTORY {value} CARD \n FROM ENEMY HAND";




    public void SetDiscardPlayerHandUI(bool condition) => _discardPlayerHandUI.gameObject.SetActive(condition);
    public void UpdateDiscardPlayerHandUIText(int value) => _discardPlayerHandUIText.text = $"SELECT TO DISCARD {value} CARD \n FROM YOUR HAND";




    public void SetSwitchHandCardTypeUI(bool condition) => _siwtchHandCardTypeUI.gameObject.SetActive(condition);
    public void UpdateSwitchHandCardTypeUIText(int value) => _siwtchHandCardTypeUIText.text = $"SELECT TO SWITCH  \n {value} CARD-ATTRIBUTE \n FROM YOUR HAND";






}
