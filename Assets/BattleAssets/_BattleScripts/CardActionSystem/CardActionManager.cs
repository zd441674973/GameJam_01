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
    int _currentBrightEnergy;
    int _currentDarkEnergy;

    public event Action DrawOpponentHandEvent;
    public event Action DestoryOpponentCardEvent;
    public event Action DiscardPlayerHandEvent;
    public event Action SwitchPlayerHandCardTypeEvent;










    void Start()
    {
        _playerHealth = PlayerManager.Instance.GetHealthSystem();
        _enemyHealth = EnemyManager.Instance.GetHealthSystem();

        _playerShield = PlayerManager.Instance.GetShieldSystem();
        _enemyShield = EnemyManager.Instance.GetShieldSystem();

    }

    void Update()
    {
        _currentBrightEnergy = EnergySystem.Instance.GetCurrentBrightEnergy();
        _currentDarkEnergy = EnergySystem.Instance.GetCurrentDarkEnergy();

    }


    public int GetCurrentBrightEnergy() => _currentBrightEnergy;
    public int GetCurrentDarkEnergy() => _currentDarkEnergy;




    public void BrightCardPlayedExtraDamage()
    {
        int currentBrightEnergy = GetCurrentBrightEnergy();
        DamageOpponent(currentBrightEnergy);
    }

    public void DarkCardPlayedExtraDamage()
    {
        int totalDarkDamageBuff = LevelManager.Instance.GetTotalDarkDamageBuff();
        DamageOpponent(totalDarkDamageBuff);
    }






    bool IsPlayerTurn() => TurnSystem.Instance.IsPlayerTurn();

    public void DamageOpponent(int value)
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






    public int GetShieldValue()
    {
        // player turn
        if (IsPlayerTurn()) return _playerShield.ShieldValue;
        // enemy turn
        else return _enemyShield.ShieldValue;
    }
    public int GetOpponentShieldValue()
    {
        // enemy turn
        if (!IsPlayerTurn()) return _playerShield.ShieldValue;
        // player turn
        else return _enemyShield.ShieldValue;
    }
    public void GainShield(int value)
    {
        if (!IsPlayerTurn()) _enemyShield.ShieldValue += value;
        if (IsPlayerTurn()) _playerShield.ShieldValue += value;

        _enemyShield.ShieldValueUpadte();
        _playerShield.ShieldValueUpadte();
    }
    public void OpponentGainShield(int value)
    {
        if (IsPlayerTurn()) _enemyShield.ShieldValue += value;
        if (!IsPlayerTurn()) _playerShield.ShieldValue += value;

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
    public void OpponentGainHealth(int value)
    {
        if (_enemyHealth.IsHealthFull) return;
        if (_playerHealth.IsHealthFull) return;

        if (IsPlayerTurn()) _enemyHealth.Health += (float)value;
        if (!IsPlayerTurn()) _playerHealth.Health += (float)value;
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
    public void OpponentDrawCard(int value)
    {
        if (!IsPlayerTurn())
        {
            int currentCardCount = LevelManager.Instance.GetPlayerCurrentHandCardCount();
            LevelManager.Instance.PlayerDrawCard(currentCardCount + value);
        }

        if (IsPlayerTurn())
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





    public void PlayerDrawOpponentHand(int value)
    {
        if (IsPlayerTurn())
        {
            DrawOpponentHandEvent?.Invoke();
            MouseActionManager.Instance.DrawFromOpponentHandCount = value;
        }

        if (!IsPlayerTurn()) LevelManager.Instance.EnemyDrawFromPlayerHandFunctionSet(value);
    }
    public void OpponentDrawPlayerHand(int value)
    {
        if (!IsPlayerTurn())
        {
            DrawOpponentHandEvent?.Invoke();
            MouseActionManager.Instance.DrawFromOpponentHandCount = value;
        }

        if (IsPlayerTurn()) LevelManager.Instance.EnemyDrawFromPlayerHandFunctionSet(value);
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
    public void OpponentDestoryCard(int value)
    {
        if (!IsPlayerTurn())
        {
            DestoryOpponentCardEvent?.Invoke();
            MouseActionManager.Instance.DestoryFromOpponentHandCount = value;
        }

        if (IsPlayerTurn()) LevelManager.Instance.EnemyDestoryPlayerHandCard(value);
    }




    public void DiscardCard(int value)
    {
        if (IsPlayerTurn())
        {
            DiscardPlayerHandEvent?.Invoke();
            MouseActionManager.Instance.DiscardPlayerHandCount = value;
        }

        if (!IsPlayerTurn())
        {
            if (LevelManager.Instance.GetEnemyCurrentHandCardCount() < value) TurnSystem.Instance.NextTurn();

            else LevelManager.Instance.EnemyDiscardMultipleRandomHandCard(value);
        }
    }
    public void OpponentDiscardCard(int value)
    {
        if (!IsPlayerTurn())
        {
            DiscardPlayerHandEvent?.Invoke();
            MouseActionManager.Instance.DiscardPlayerHandCount = value;
        }

        if (IsPlayerTurn())
        {
            // if (LevelManager.Instance.GetEnemyCurrentHandCardCount() < value) TurnSystem.Instance.NextTurn();

            // else
            LevelManager.Instance.EnemyDiscardMultipleRandomHandCard(value);
        }
    }




    public void DiscardBrightHandCard(out int discardCount)
    {
        if (IsPlayerTurn())
        {
            LevelManager.Instance.DiscardPlayerCurrentBrightHandCard(out discardCount);
        }
        if (!IsPlayerTurn())
        {
            LevelManager.Instance.DiscardEnemyCurrentBrightHandCard(out discardCount);
        }
        else discardCount = 0;
    }














    public void GainBrightEnergy(int value)
    {
        EnergySystem.Instance.GainBrightEnergy(value, true);
    }

    public void GainDarkEnergy(int value)
    {
        EnergySystem.Instance.GainDarkEnergy(value, true);
    }




    public void AttributeSwitch(int value)
    {
        if (IsPlayerTurn())
        {
            SwitchPlayerHandCardTypeEvent?.Invoke();
            MouseActionManager.Instance.SwitchPlayerHandCardTypeCount = value;
        }

        if (!IsPlayerTurn())
        {
            if (LevelManager.Instance.GetEnemyCurrentHandCardCount() < value) TurnSystem.Instance.NextTurn();

            else LevelManager.Instance.EnemySwitchCardAttribute(value);
        }
    }

    public void OpponentAttributeSwitch(int value)
    {
        if (!IsPlayerTurn())
        {
            SwitchPlayerHandCardTypeEvent?.Invoke();
            MouseActionManager.Instance.SwitchPlayerHandCardTypeCount = value;
        }

        if (IsPlayerTurn())
        {
            // if (LevelManager.Instance.GetEnemyCurrentHandCardCount() < value) TurnSystem.Instance.NextTurn();

            // else 

            LevelManager.Instance.EnemySwitchCardAttribute(value);
        }
    }









}
