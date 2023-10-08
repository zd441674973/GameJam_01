using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_TeamMember : BasePanel
{
    public Image Scroll;
     
    public float moveSpeed = 10f; // 移动速度
    public float targetY = 1182f; // 停止的Y坐标
    public float delayBeforeReturnToMainMenu = 2.0f; // 到达目标位置后等待的时间（秒）

    private bool isMoving = false;

    public override void ShowMe()
    {
        base.ShowMe();
    }

    private void Start()
    {
        // 启动协程，在两秒后开始移动图片
        StartCoroutine(StartMovingAfterDelay(2.0f));
    }

    private void Update()
    {
        // 如果正在移动，逐渐改变图片的Y坐标
        if (isMoving)
        {
            float newY = Scroll.transform.position.y + moveSpeed * Time.deltaTime;
            Scroll.transform.position = new Vector3(Scroll.transform.position.x, newY, Scroll.transform.position.z);

            // 如果达到目标Y坐标，停止移动
            if (Scroll.transform.position.y >= targetY)
            {
                isMoving = false;
                // 启动协程，在指定时间后返回主菜单
                StartCoroutine(ReturnToMainMenuAfterDelay(delayBeforeReturnToMainMenu));
            }
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            ScenesMgr.GetInstance().LoadSceneAsyn("TitleScene", AfterReturnToTitle);
        }
    }

    // 在指定延迟后开始移动
    private IEnumerator StartMovingAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        isMoving = true;
    }

    // 在指定延迟后返回主菜单
    private IEnumerator ReturnToMainMenuAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        // 在这里执行返回主菜单的操作
        ScenesMgr.GetInstance().LoadSceneAsyn("TitleScene", AfterReturnToTitle);
    }

    public void AfterReturnToTitle()
    {
        MusicMgr.GetInstance().PlaySound("SystemSoundEffect/选择2", false);

        UIManager.GetInstance().HidePanel("UI_MenuPanel");
        UIManager.GetInstance().HidePanel("UI_MainPage");
        UIManager.GetInstance().HidePanel("UI_CardLibrary");
        UIManager.GetInstance().HidePanel("UI_GameMap");
        UIManager.GetInstance().HidePanel("AwardPanel");
        UIManager.GetInstance().HidePanel("DelateCardPanel");
        UIManager.GetInstance().HidePanel("UI_DialoguePanel");
        UIManager.GetInstance().HidePanel("UI_TeamMember");


    }
}
