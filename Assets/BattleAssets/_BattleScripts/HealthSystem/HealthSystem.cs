using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] float _health;
    [SerializeField] float _maxHealth;

    [SerializeField] Image _healthBar;
    [SerializeField] TextMeshProUGUI _textMesh;


    void Start()
    {
        _maxHealth = _health;
    }

    void Update()
    {
        HealthUpadte();
    }



    void HealthUpadte()
    {
        _healthBar.fillAmount = _health / _maxHealth;

        _textMesh.text = Mathf.RoundToInt(_health).ToString() + " / " + _maxHealth;
    }
}
