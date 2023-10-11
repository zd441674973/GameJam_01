using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTimer : MonoBehaviour
{
    private void Awake()
    {

        EventCenter.GetInstance().AddEventListener("AnimationTimerStart", StartTimer);
    }

    private void OnDestroy()
    {
        EventCenter.GetInstance().RemoveEventListener("AnimationTimerStart", StartTimer);
    }

    private void StartTimer()
    {
        // ����������YourFunction����
        Invoke("YourFunction", 2f);
    }

    private void YourFunction()
    {
        // ������ִ������Ҫ�Ĳ���
        EventCenter.GetInstance().EventTrigger("AnimationTimerTwoSeconds");
    }
}

