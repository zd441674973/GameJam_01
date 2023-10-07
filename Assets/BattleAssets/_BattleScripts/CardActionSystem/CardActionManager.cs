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
    int _currentLightEnergy;
    int _currentDarkEnergy;

    public event Action DrawOpponentHandEvent;
    public event Action DestoryOpponentCardEvent;
    public event Action DiscardPlayerHandEvent;










    void Start()
    {
        _playerHealth = PlayerManager.Instance.GetHealthSystem();
        _enemyHealth = EnemyManager.Instance.GetHealthSystem();

        _playerShield = PlayerManager.Instance.GetShieldSystem();
        _enemyShield = EnemyManager.Instance.GetShieldSystem();

        _currentLightEnergy = EnergySystem.Instance.GetCurrentLightEnergy();
        _currentDarkEnergy = EnergySystem.Instance.GetCurrentDarkEnergy();
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

    public void DamageSelf(int value)
    {
        if (!IsPlayerTurn())
        {
            _enemyShield.ShieldValue += value;
            int damageToHealth = _enemyShield.ShieldValue;
            if (damageToHealth < 0) _enemyHealth.Health += (float)damageToHealth;
            _enemyShield.ShieldValueUpadte();
        }

        if (IsPlayerTurn())
        {
            _playerShield.ShieldValue += value;
            int damageToHealth = _playerShield.ShieldValue;
            if (damageToHealth < 0) _playerHealth.Health += (float)damageToHealth;
            _playerShield.ShieldValueUpadte();
        }
    }

    public void GainShield(int value)
    {
        if (!IsPlayerTurn()) _enemyShield.ShieldValue += value;
        if (IsPlayerTurn()) _playerShield.ShieldValue += value;


        _enemyShield.ShieldValueUpadte();
        _playerShield.ShieldValueUpadte();
    }

    public int GetShieldValue()
    {
        // player turn
        if (IsPlayerTurn()) return _playerShield.ShieldValue;
        // enemy turn
        else return _enemyShield.ShieldValue;
    }

    public void GainHealth(int value)
    {
        if (_enemyHealth.IsHealthFull) return;
        if (_playerHealth.IsHealthFull) return;

        if (!IsPlayerTurn()) _enemyHealth.Health += (float)value;
        if (IsPlayerTurn()) _playerHealth.Health += (float)value;
    }

    public void DrawCard(int value)
    {
        if (IsPlayerTurn())
        {
            int currentCardCount = LevelManager.Instance.GetPlayerCurrentHandCardCount();
            LevelManager.Instance.PlayerDrawCard(currentCardCount + value);
        }

        if (!IsPlayerTurn())
        {
            int currentCardCount = LevelManager.Instance.GetEnemyCurrentHandCardCount();
            LevelManager.Instance.EnemyDrawCard(currentCardCount + value);
        }
    }

    public void DrawOpponentDeck(int value)
    {
        if (IsPlayerTurn())
        {
            int currentCardCount = LevelManager.Instance.GetPlayerCurrentHandCardCount();
            LevelManager.Instance.PlayerDrawFromEnemyDeck(currentCardCount + value);
        }

        if (!IsPlayerTurn())
        {
            int currentCardCount = LevelManager.Instance.GetEnemyCurrentHandCardCount();
            LevelManager.Instance.EnemyDrawFromPlayerDeck(currentCardCount + value);
        }
    }
    public void DrawOpponentHand(int value)
    {
        if (IsPlayerTurn())
        {
            DrawOpponentHandEvent?.Invoke();
            MouseActionManager.Instance.DrawFromOpponentHandCount = value;
        }

        if (!IsPlayerTurn()) LevelManager.Instance.EnemyDrawFromPlayerHandFunctionSet(value);
    }


    public void DestoryOpponentCard(int value)
    {
        if (IsPlayerTurn())
        {
            DestoryOpponentCardEvent?.Invoke();
            MouseActionManager.Instance.DestoryFromOpponentHandCount = value;
        }

        if (!IsPlayerTurn()) LevelManager.Instance.EnemyDestoryPlayerHandCard(value);
    }

    public void DiscardCard(int value)
    {
        if (IsPlayerTurn())
        {
            DiscardPlayerHandEvent?.Invoke();
            MouseActionManager.Instance.DiscardPlayerHandCount = value;
        }

        if (!IsPlayerTurn()) LevelManager.Instance.EnemyDiscardHandCard(value);

    }

    public void GainBrightEnergy(int value)
    {
        //EnergySystem.Instance.EnergyBarCalculation("Bright", value);
        EnergySystem.Instance.GainBrightEnergy(value, true);
    }

    public void GainDarkEnergy(int value)
    {
        //EnergySystem.Instance.EnergyBarCalculation("Dark", value);
    }

    public void AttributeSwitch(CardData card)
    {
        card.IsBrightCard = !card.IsBrightCard;
    }









}
