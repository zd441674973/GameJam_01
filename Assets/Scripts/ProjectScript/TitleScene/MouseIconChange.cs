using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseIconChange : MonoBehaviour
{
    public Texture2D customCursor; // 拖拽你的鼠标图标纹理到这个字段上

    private void Start()
    {
        // 在游戏开始时设置鼠标图标为自定义图标
        Cursor.SetCursor(customCursor, Vector2.zero, CursorMode.Auto);
    }

    // 在游戏结束或特定条件下，恢复默认鼠标图标
    private void EndGameOrResetCursor()
    {
        // 恢复默认鼠标图标
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    /*// 在游戏中根据需要调用EndGameOrResetCursor方法，以重置鼠标图标
    // 例如，在游戏结束时调用这个方法
    private void GameOver()
    {
        // 游戏结束时，重置鼠标图标
        EndGameOrResetCursor();
    }*/
}
