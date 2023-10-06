using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using JetBrains.Annotations;
using Unity.Mathematics;


public class EnergySystem : MonoBehaviour
{
    public static EnergySystem Instance;
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




    public int GetCurrentLightEnergy() => _brightBarEnergy;
    public int GetCurrentDarkEnergy() => _darkBarEnergy;





    void Start()
    {
    }

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
    /// string: "Bright"/"Dark" // int: calculate value, "1" for addition, "-1" for subtraction
    /// </summary>
    /// <param name="bar"></param>
    /// <param name="value"></param>
    public void EnergyBarCalculation(string bar, int value)
    {
        switch (bar)
        {
            case "Bright":
                _brightBarEnergy += value;
                break;
            case "Dark":
                _darkBarEnergy += value;
                break;
        }
        EnergyBarValueCheck();
    }




}
