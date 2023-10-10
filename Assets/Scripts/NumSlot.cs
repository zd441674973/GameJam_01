using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NumSlot : MonoBehaviour
{
    // char[] numbersRand = { '1', '2', '3', '4', '5', '6' };
    // public float time;



    public TextMeshProUGUI slotNum;

    [Header("NewData")]
    [SerializeField] float _timer;
    bool _isActive;
    int _randomNum;

    void Start()
    {
        _isActive = true;
        CustomTimer.Instance.WaitforTime(_timer);

    }


    void Update()
    {
        if (!_isActive) return;

        SlotMachineUpdating();

        if (CustomTimer.Instance.TimesUp())
        {
            _isActive = false;
            LevelManager.Instance.SetDarkBeginBuff(_randomNum);

        }
    }

    void SlotMachineUpdating()
    {
        _randomNum = Random.Range(0, 6);
        slotNum.text = _randomNum.ToString();
    }







    // public void OffEnable()
    // {
    //     Invoke("DelayNum", time);

    // }

    // public void OnEnable()
    // {
    //     enabled = true;
    // }

    // void DelayNum()
    // {
    //     enabled = false;
    // }
}
