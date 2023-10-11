using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class NumSlot : MonoBehaviour
{
    public static event Action StartTutorialEvent;

    public TextMeshProUGUI slotNum;

    [Header("NewData")]
    [SerializeField] float _timer;
    [SerializeField] float _waitForTutorialTimer;
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

            if (BattleSceneSetUp.Instance.CurrentLevel == 0)
            {
                _randomNum = 3;
                slotNum.text = _randomNum.ToString();

                StartCoroutine(waitToStarTutorial(_waitForTutorialTimer));
            }

            LevelManager.Instance.SetDarkBeginBuff(_randomNum);

           



        }
    }

    void SlotMachineUpdating()
    {
        _randomNum = UnityEngine.Random.Range(1, 6);

        slotNum.text = _randomNum.ToString();
    }

    IEnumerator waitToStarTutorial(float timer)
    {
        yield return new WaitForSeconds(timer);
        StartTutorialEvent?.Invoke();
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
