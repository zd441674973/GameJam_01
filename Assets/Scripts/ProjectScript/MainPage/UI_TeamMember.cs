using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_TeamMember : BasePanel
{
    public Image Scroll;
     
    public float moveSpeed = 10f; // �ƶ��ٶ�
    public float targetY = 1182f; // ֹͣ��Y����
    public float delayBeforeReturnToMainMenu = 2.0f; // ����Ŀ��λ�ú�ȴ���ʱ�䣨�룩

    private bool isMoving = false;

    public override void ShowMe()
    {
        base.ShowMe();
    }

    private void Start()
    {
        // ����Э�̣��������ʼ�ƶ�ͼƬ
        StartCoroutine(StartMovingAfterDelay(2.0f));
    }

    private void Update()
    {
        // ��������ƶ����𽥸ı�ͼƬ��Y����
        if (isMoving)
        {
            float newY = Scroll.transform.position.y + moveSpeed * Time.deltaTime;
            Scroll.transform.position = new Vector3(Scroll.transform.position.x, newY, Scroll.transform.position.z);

            // ����ﵽĿ��Y���ֹ꣬ͣ�ƶ�
            if (Scroll.transform.position.y >= targetY)
            {
                isMoving = false;
                // ����Э�̣���ָ��ʱ��󷵻����˵�
                StartCoroutine(ReturnToMainMenuAfterDelay(delayBeforeReturnToMainMenu));
            }
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            ScenesMgr.GetInstance().LoadSceneAsyn("TitleScene", AfterReturnToTitle);
        }
    }

    // ��ָ���ӳٺ�ʼ�ƶ�
    private IEnumerator StartMovingAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        isMoving = true;
    }

    // ��ָ���ӳٺ󷵻����˵�
    private IEnumerator ReturnToMainMenuAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        // ������ִ�з������˵��Ĳ���
        ScenesMgr.GetInstance().LoadSceneAsyn("TitleScene", AfterReturnToTitle);
    }

    public void AfterReturnToTitle()
    {
        MusicMgr.GetInstance().PlaySound("SystemSoundEffect/ѡ��2", false);

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
