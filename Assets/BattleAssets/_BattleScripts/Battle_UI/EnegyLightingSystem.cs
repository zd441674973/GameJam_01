using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnegyLightingSystem : MonoBehaviour
{
    public GameObject BrightEffect;
    public GameObject DarkEffect;


    // Update is called once per frame
    void Update()
    {
        if (EnergySystem.Instance.IsBrightEnergyFull())
        {
            BrightEffect.SetActive(true);
        }
        else
        {
            BrightEffect.SetActive(false);
        }

        if (EnergySystem.Instance.IsDarkEnergyFull())
        {
            DarkEffect.SetActive(true);
        }
        else
        {
            DarkEffect.SetActive(false);
        }

    }
}
