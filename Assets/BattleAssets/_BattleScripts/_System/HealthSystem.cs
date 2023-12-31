using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] float _health;
    [SerializeField] float _maxHealth;
    [SerializeField] bool _isFullHealth;

    [SerializeField] Image _healthBar1;
    [SerializeField] Image _healthBar2;
    [SerializeField] TextMeshProUGUI _textMesh;

    public float Health { get { return _health; } set { _health = value; } }
    public float MaxHealth { get { return _maxHealth; } set { _maxHealth = value; } }
    public bool IsHealthFull { get { return _isFullHealth; } }

    bool _isActive;

    [Header("DEBUG_ONLY")]
    [SerializeField] bool _isInifiteHealth;


    void Start()
    {
        _maxHealth = _health;
        _isActive = true;
    }

    void Update()
    {
        //if (!_isActive) return;
        HealthUpadte();

    }



    void HealthUpadte()
    {
        _healthBar1.fillAmount = _health / _maxHealth;
        _healthBar2.fillAmount = _health / _maxHealth;

        _textMesh.text = Mathf.RoundToInt(_health).ToString() + " / " + _maxHealth;

        if (_health >= _maxHealth)
        {
            _health = _maxHealth;
            _isFullHealth = true;
        }
        else _isFullHealth = false;

        if (_isInifiteHealth)
        {
            _health = float.MaxValue;
        }


        /*if (_health <= 0)
         {
             GameOver();
             _isActive = false;
         }*/

        // MaxHealthCheck(_isFullHealth);


        if (_health <= 0)
        {
            _health = 0;
            Debug.Log("GameOver");
            return;
        }


    }

    // void MaxHealthCheck(bool condition)
    // {
    //     if (condition) _health = _maxHealth;
    // }

    // void GameOver()
    // {
    //     Debug.Log("GameOver");
    // }
}
