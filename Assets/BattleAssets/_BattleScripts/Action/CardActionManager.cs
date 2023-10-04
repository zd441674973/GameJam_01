using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CardActionManager : MonoBehaviour
{
    public static CardActionManager Instance;
    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Multiple instances occured");
            Destroy(Instance);
        }
        Instance = this;
    }

    [SerializeField] Transform _player;
    [SerializeField] Transform _enemy;

    void Start()
    {

    }

    void DealDamage(int value)
    {

    }

    void GainShield(int value)
    {

    }

    void GainHealth(int value)
    {

    }

    void DrawCard(int darwCount, List<Transform> cardList)
    {

    }

    void DiscardCard(int disCardCount, List<Transform> cardList)
    {

    }

    void DestroyCard()
    {

    }

    void GainEnergy(int value)
    {

    }

    void AttributeSwitch()
    {

    }









}
