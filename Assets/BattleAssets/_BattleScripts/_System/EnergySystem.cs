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
    //[SerializeField] Image _brightBar;
    //[SerializeField] TextMeshProUGUI _brightBarText;

    [SerializeField][Range(0, 5)] int _brightBarEnergy;

    [SerializeField] int _brightBarMaxEnergy;
    [SerializeField] bool _isBrightBarFull;
    [SerializeField] List<Image> _brightBarImages;




    [Header("DarkBar")]
    //[SerializeField] Image _darkBar;
    //[SerializeField] TextMeshProUGUI _darkBarText;
    [SerializeField][Range(0, 5)] int _darkBarEnergy;
    [SerializeField] int _darkBarMaxEnergy;
    [SerializeField] bool _isDarkBarFull;
    [SerializeField] List<Image> _darkBarImages;




    public int GetCurrentBrightEnergy() => _brightBarEnergy;
    public int GetCurrentDarkEnergy() => _darkBarEnergy;

    public bool IsBrightEnergyFull() => _isBrightBarFull;
    public bool IsDarkEnergyFull() => _isDarkBarFull;






    void Start()
    {

        TurnSystem.Instance.OnEnemyTurnFinished += ResetEnergyBarValue;



    }

    void Update()
    {
        EnergyBarUpdation();
    }







    void EnergyBarUpdation()
    {
        // _brightBar.fillAmount = (float)_brightBarEnergy / (float)_brightBarMaxEnergy;
        // _brightBarText.text = _brightBarEnergy + "/" + _brightBarMaxEnergy;

        // _darkBar.fillAmount = (float)_darkBarEnergy / (float)_darkBarMaxEnergy;
        // _darkBarText.text = _darkBarEnergy + "/" + _darkBarMaxEnergy;

        EnergyBarImageUpdate(_brightBarImages, _brightBarEnergy);
        EnergyBarImageUpdate(_darkBarImages, _darkBarEnergy);


        if (_isBrightBarFull) _brightBarEnergy = _brightBarMaxEnergy;

        if (_isDarkBarFull) _darkBarEnergy = _darkBarMaxEnergy;
    }

    void EnergyBarValueCheck()
    {
        if (_brightBarEnergy >= _brightBarMaxEnergy)
        {
            _isBrightBarFull = true;
            _brightBarEnergy = _brightBarMaxEnergy;
        }

        if (_darkBarEnergy >= _darkBarMaxEnergy)
        {
            _isDarkBarFull = true;
            _darkBarEnergy = _darkBarMaxEnergy;
        }

        if (_brightBarEnergy < 0) _brightBarEnergy = 0;

        if (_darkBarEnergy < 0) _darkBarEnergy = 0;
    }


    void ResetEnergyBarValue()
    {
        if (_isBrightBarFull)
        {
            _brightBarEnergy = 0;
            _isBrightBarFull = false;
        }

        if (_isDarkBarFull)
        {
            _darkBarEnergy = 0;
            _isDarkBarFull = false;
        }
    }

    void EnergyBarImageUpdate(List<Image> images, int energyAmount)
    {
        foreach (var item in images)
        {
            item.enabled = false;
        }


        for (int i = 0; i < energyAmount; i++)
        {
            images[i].enabled = true;
        }
    }



    public void GainBrightEnergy(int value, bool isCardAction)
    {
        if (!_isBrightBarFull) _brightBarEnergy += value;

        if (!_isDarkBarFull && !isCardAction) _darkBarEnergy -= value;

        EnergyBarValueCheck();
    }
    public void GainDarkEnergy(int value, bool isCardAction)
    {
        if (!_isDarkBarFull) _darkBarEnergy += value;

        if (!_isBrightBarFull && !isCardAction) _brightBarEnergy -= value;

        EnergyBarValueCheck();
    }
















    // /// <summary>
    // /// string: "Bright"/"Dark" // int: calculate value, "1" for addition, "-1" for subtraction
    // /// </summary>
    // /// <param name="bar"></param>
    // /// <param name="value"></param>
    // public void EnergyBarCalculation(string bar, int value)
    // {
    //     switch (bar)
    //     {
    //         case "Bright":
    //             _brightBarEnergy += value;
    //             break;
    //         case "Dark":
    //             _darkBarEnergy += value;
    //             break;
    //     }
    //     EnergyBarValueCheck();
    // }



}
