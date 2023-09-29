using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Multiple instances occured");
            Destroy(Instance);
        }
        Instance = this;
    }


    [SerializeField] Transform _playerCard;
    [SerializeField] Transform _EnemyCard;
    [SerializeField] List<Transform> _playerSlots;
    [SerializeField] List<Transform> _enemySlots;
    [SerializeField] Transform _battleSlot;


    void Start()
    {
        CardGeneration();




    }





    void CardGeneration()
    {
        foreach (var slot in _playerSlots) CardDeckManager.Instance.GenerateCard(_playerCard, slot);

        foreach (var slot in _enemySlots) CardDeckManager.Instance.GenerateCard(_EnemyCard, slot);
    }

    public Transform GetBattleSlot() => _battleSlot;

}
