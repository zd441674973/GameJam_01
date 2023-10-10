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
        // 在两秒后调用YourFunction函数
        Invoke("YourFunction", 2f);
    }

    private void YourFunction()
    {
        // 在这里执行你想要的操作
        EventCenter.GetInstance().EventTrigger("AnimationTimerTwoSeconds");
    }
}

