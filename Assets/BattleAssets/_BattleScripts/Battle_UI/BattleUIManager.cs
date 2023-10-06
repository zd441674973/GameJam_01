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

    [SerializeField] Transform _enemyTurnStartText;


    void Start()
    {

    }

    public void SetEnemyTurnStartText(bool condition) => _enemyTurnStartText.gameObject.SetActive(condition);


}
