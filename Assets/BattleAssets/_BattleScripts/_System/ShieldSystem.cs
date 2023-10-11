using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShieldSystem : MonoBehaviour
{
    [SerializeField] int _shieldValue;
    public GameObject sheildIcon;

    public int ShieldValue
    { get { return _shieldValue; } set { _shieldValue = value; } }

    // [SerializeField] Transform _shieldBar;
    // [SerializeField] Transform _shieldGirdGenerator;

    [SerializeField] TextMeshProUGUI _shieldText;


    void Start()
    {

    }

    void Update()
    {
        ShieldValueUpadte();

    }



    public void ShieldValueUpadte()
    {
        _shieldText.text = _shieldValue.ToString();
        if (_shieldValue < 0) _shieldValue = 0;

/*
        if(_shieldValue <= 0)
        {
            sheildIcon.gameObject.SetActive(false);
        }
        else
        {
            sheildIcon.gameObject.SetActive(true);
        }*/
    }






    // void ShieldUpadte()
    // {

    //     _shieldText.text = _shieldValue.ToString();

    // }
    // void AddShield()
    // {
    //     for (int i = 0; i < _shieldValue; i++)
    //     {
    //         Instantiate(_shieldBar, _shieldGirdGenerator);
    //     }
    // }
    // void DestoryShield()
    // {
    //     var shieldList = _shieldGirdGenerator.GetComponentsInChildren<Image>();
    //     foreach (var item in shieldList)
    //     {
    //         Destroy(item.gameObject);
    //     }
    // }


}
