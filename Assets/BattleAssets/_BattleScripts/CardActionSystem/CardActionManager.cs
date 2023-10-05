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

    HealthSystem _playerHealth;
    HealthSystem _enemyHealth;
    ShieldSystem _playerShield;
    ShieldSystem _enemyShield;

    void Start()
    {
        _playerHealth = PlayerManager.Instance.GetHealthSystem();
        _enemyHealth = EnemyManager.Instance.GetHealthSystem();

        _playerShield = PlayerManager.Instance.GetShieldSystem();
        _enemyShield = EnemyManager.Instance.GetShieldSystem();
    }

    bool IsPlayerTurn() => TurnSystem.Instance.IsPlayerTurn();

    public void DealDamage(int value)
    {
        //if (!IsPlayerTurn()) _playerHealth.Health += (float)value;
        //if (IsPlayerTurn()) _enemyHealth.Health += (float)value;

        if (!IsPlayerTurn())
        {
            _playerShield.ShieldValue += value;
            int damageToHealth = _playerShield.ShieldValue;
            if (damageToHealth < 0) _playerHealth.Health += (float)damageToHealth;
            _playerShield.ShieldValueUpadte();
        }

        if (IsPlayerTurn())
        {
            _enemyShield.ShieldValue += value;
            int damageToHealth = _enemyShield.ShieldValue;
            if (damageToHealth < 0) _enemyHealth.Health += (float)damageToHealth;
            _enemyShield.ShieldValueUpadte();
        }
    }

    public void GainShield(int value)
    {
        if (!IsPlayerTurn()) _enemyShield.ShieldValue += value;
        if (IsPlayerTurn()) _playerShield.ShieldValue += value;


        _enemyShield.ShieldValueUpadte();
        _playerShield.ShieldValueUpadte();
    }

    public void GainHealth(int value)
    {
        if (_enemyHealth.IsHealthFull) return;
        if (_playerHealth.IsHealthFull) return;

        if (!IsPlayerTurn()) _enemyHealth.Health += (float)value;
        if (IsPlayerTurn()) _playerHealth.Health += (float)value;
    }

    public void DrawCard(int darwCount)
    {
        // int currentCardCount = LevelManager.Instance.GetCurrentHandCardCount();
        // LevelManager.Instance.PlayerDrawCard(currentCardCount + 1);
    }

    public void DiscardCard(int disCardCount, List<Transform> cardList)
    {

    }

    public void DestroyCard()
    {

    }

    public void GainEnergy(int value)
    {

    }

    public void AttributeSwitch()
    {

    }









}
