using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    public float _health;
    [SerializeField] float _maxHealth;
    bool _isFullHealth;

    [SerializeField] Image _healthBar;
    [SerializeField] TextMeshProUGUI _textMesh;

    public float Health { get { return _health; } set { _health = value; } }
    public float MaxHealth { get { return _maxHealth; } set { _maxHealth = value; } }
    public bool IsHealthFull { get { return _isFullHealth; } }

    bool _isActive;
    void Start()
    {
        _maxHealth = _health;
        _isActive = true;
    }

    void Update()
    {
        if (!_isActive) return;
        HealthUpadte();

    }



    void HealthUpadte()
    {
        _healthBar.fillAmount = _health / _maxHealth;

        _textMesh.text = Mathf.RoundToInt(_health).ToString() + " / " + _maxHealth;

        if (_health == _maxHealth) _isFullHealth = true;
        else _isFullHealth = false;

       /* if (_health <= 0)
        {
            GameOver();
            _isActive = false;
        }*/

    }
/*
    void GameOver()
    {
        Debug.Log("GameOver");
    }*/
}
