using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShieldSystem : MonoBehaviour
{
    [SerializeField] int _shieldValue;
    public int ShieldValue
    { get { return _shieldValue; } set { _shieldValue = value; } }

    [SerializeField] Transform _shieldBar;
    [SerializeField] Transform _shieldGirdGenerator;


    void Start()
    {
        ShieldValueUpadte();
    }

    void Update()
    {

    }


    public void ShieldValueUpadte()
    {
        DestoryShield();
        AddShield();
    }
    void AddShield()
    {
        for (int i = 0; i < _shieldValue; i++)
        {
            Instantiate(_shieldBar, _shieldGirdGenerator);
        }
    }
    void DestoryShield()
    {
        var shieldList = _shieldGirdGenerator.GetComponentsInChildren<Image>();
        foreach (var item in shieldList)
        {
            Destroy(item.gameObject);
        }
    }


}
