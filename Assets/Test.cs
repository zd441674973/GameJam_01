using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("1");
            Function();
        }

    }

    void Function()
    {
        GameDataControl.GetInstance().PlayerDataInfo.currentNodeID += 1;

        EventCenter.GetInstance().EventTrigger("currentPlayerNodeIDchange");
    }
}
