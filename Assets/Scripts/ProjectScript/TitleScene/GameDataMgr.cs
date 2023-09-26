using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataMgr : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {

        //如果本次游戏没有执行过ParseData
        if (GameDataControl.GetInstance().isParseDataExecuted == false)
        {
            //执行一次
            GameDataControl.GetInstance().ParseData();
        }
        else
        {
            //如果本次游戏已经执行过PaeseData，就不再执行
            //确保一次游戏只执行一次ParseData
            return;
        }
    }
}
