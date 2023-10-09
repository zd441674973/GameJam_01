using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BKManager : MonoBehaviour
{

    public Image BK;

    private void Awake()
    {
        EventCenter.GetInstance().AddEventListener("turnOffBK", turnOFFBk);
    }

    private void OnDestroy()
    {
        EventCenter.GetInstance().RemoveEventListener("turnOffBK", turnOFFBk);
    }


    private void turnOFFBk()
    {
        StartCoroutine(Turnoff());

    }

    IEnumerator Turnoff()
    {
        yield return new WaitForSeconds(0.2f);
        BK.gameObject.SetActive(false);
       
    }
}

