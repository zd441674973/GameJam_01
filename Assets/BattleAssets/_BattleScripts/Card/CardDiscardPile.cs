using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDiscardPile : MonoBehaviour
{
    public static CardDiscardPile Instance;
    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Multiple instances occured");
            Destroy(Instance);
        }
        Instance = this;
    }

    [SerializeField] List<Transform> _playerDiscardPile;

    [SerializeField] List<Transform> _enemyDiscardPile;

    public List<Transform> GetPlayerDiscardDeck() => _playerDiscardPile;
    public List<Transform> GetEnemyDiscardDeck() => _enemyDiscardPile;
}
