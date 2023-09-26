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

        //���������Ϸû��ִ�й�ParseData
        if (GameDataControl.GetInstance().isParseDataExecuted == false)
        {
            //ִ��һ��
            GameDataControl.GetInstance().ParseData();
        }
        else
        {
            //���������Ϸ�Ѿ�ִ�й�PaeseData���Ͳ���ִ��
            //ȷ��һ����Ϸִֻ��һ��ParseData
            return;
        }
    }
}
