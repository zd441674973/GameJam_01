using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurn : MonoBehaviour
{

    void Update()
    {
        if (TurnSystem.Instance.IsPlayerTurn()) return;



    }


    bool TimeCounter(float time)
    {
        Debug.Log(time);
        time -= Time.deltaTime;
        if (time < 0) return true;
        else return false;
    }
}
