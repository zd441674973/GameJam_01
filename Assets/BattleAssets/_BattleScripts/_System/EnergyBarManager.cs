using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using JetBrains.Annotations;
using Unity.Mathematics;


public class EnergyBarManager : MonoBehaviour
{
    public static EnergyBarManager Instance;
    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Multiple instances occured");
            Destroy(Instance);
        }
        Instance = this;
    }


    [Header("BrightBar")]
    [SerializeField] Image _brightBar;
    [SerializeField] TextMeshProUGUI _brightBarText;
    [SerializeField] int _brightBarEnergy;
    [SerializeField] int _brightBarMaxEnergy;




    [Header("DarkBar")]
    [SerializeField] Image _darkBar;
    [SerializeField] TextMeshProUGUI _darkBarText;
    [SerializeField] int _darkBarEnergy;
    [SerializeField] int _darkBarMaxEnergy;

    // [SerializeField] bool _isCalculating;
    // public bool IsCalculatingEnerygBar
    // { get { return _isCalculating; } set { _isCalculating = value; } }


    void Update()
    {
        EnergyBarUpdation();
    }

    void EnergyBarUpdation()
    {
        _brightBar.fillAmount = (float)_brightBarEnergy / (float)_brightBarMaxEnergy;
        _brightBarText.text = _brightBarEnergy + "/" + _brightBarMaxEnergy;

        _darkBar.fillAmount = (float)_darkBarEnergy / (float)_darkBarMaxEnergy;
        _darkBarText.text = _darkBarEnergy + "/" + _darkBarMaxEnergy;
    }

    void EnergyBarValueCheck()
    {
        if (_brightBarEnergy > _brightBarMaxEnergy || _brightBarEnergy < 0)
        {
            // Do something
            _brightBarEnergy = 0;
        }
        if (_darkBarEnergy > _darkBarMaxEnergy || _darkBarEnergy < 0)
        {
            // Do something
            _darkBarEnergy = 0;
        }
    }



    /// <summary>
    /// string: "Bright"/"Dark" // int: calculate value // string: "Addition" for add, "Subtraction" for minus
    /// </summary>
    /// <param name="bar"></param>
    /// <param name="value"></param>
    /// <param name="calculation"></param>
    public void EnergyBarCalculation(string bar, int value, string calculation)
    {
        switch (bar)
        {
            case "Bright":
                switch (calculation)
                {
                    case "Addition":
                        _brightBarEnergy += value;
                        break;
                    case "Subtraction":
                        _brightBarEnergy -= value;
                        break;
                }
                break;
            case "Dark":
                switch (calculation)
                {
                    case "Addition":
                        _darkBarEnergy += value;
                        break;
                    case "Subtraction":
                        _darkBarEnergy -= value;
                        break;
                }
                break;
        }
        EnergyBarValueCheck();
    }




}
