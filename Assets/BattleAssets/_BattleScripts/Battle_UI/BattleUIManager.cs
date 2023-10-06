using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [SerializeField] Transform _enemyTurnStartUI;
    [SerializeField] Transform _darwCardFromEnemyHandUI;


    void Start()
    {

    }

    public void SetEnemyTurnStartUI(bool condition) => _enemyTurnStartUI.gameObject.SetActive(condition);
    public void SetDrawCardFromEnemyHandUI(bool condition) => _darwCardFromEnemyHandUI.gameObject.SetActive(condition);


}
