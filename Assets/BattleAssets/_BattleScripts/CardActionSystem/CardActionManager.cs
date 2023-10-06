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

    public event Action DrawFromOpponentHand;









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
        // int currentCardCount = LevelManager.Instance.GetCurrentHandCardCount();
        // LevelManager.Instance.PlayerDrawCard(currentCardCount + 1);

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

    // public void DrawOpponentHand(Transform cardDrawn)
    // {
    //     BattleUIManager.Instance.SetDrawCardFromEnemyHandUI(true);

    //     if (!IsPlayerTurn()) LevelManager.Instance.EnemyDrawFromPlayerHand(cardDrawn);
    //     if (IsPlayerTurn()) LevelManager.Instance.PlayerDrawFromEnemyHand(cardDrawn);


    // }
    public void DrawOpponentHand()
    {
        DrawFromOpponentHand?.Invoke();
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
