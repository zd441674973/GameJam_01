using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseIconChange : MonoBehaviour
{
    public Texture2D customCursor; // ��ק������ͼ����������ֶ���

    private void Start()
    {
        // ����Ϸ��ʼʱ�������ͼ��Ϊ�Զ���ͼ��
        Cursor.SetCursor(customCursor, Vector2.zero, CursorMode.Auto);
    }

    // ����Ϸ�������ض������£��ָ�Ĭ�����ͼ��
    private void EndGameOrResetCursor()
    {
        // �ָ�Ĭ�����ͼ��
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    /*// ����Ϸ�и�����Ҫ����EndGameOrResetCursor���������������ͼ��
    // ���磬����Ϸ����ʱ�����������
    private void GameOver()
    {
        // ��Ϸ����ʱ���������ͼ��
        EndGameOrResetCursor();
    }*/
}
