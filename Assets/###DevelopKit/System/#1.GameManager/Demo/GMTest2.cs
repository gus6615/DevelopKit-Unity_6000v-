using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GMTest2 : MonoBehaviour
{
    public static int currentPopupID;
    public static Vector2 currentPopupPos;

    private void Start()
    {
        currentPopupID = 1;
        currentPopupPos = new Vector2(0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
            GameManager.Instance.Scene.ChangeScene(SceneType.MainScene);
    }

    public void OnCreatePopup()
    {
        UIPopupTest popup = GameManager.Instance.UI.ShowPopup<UIPopupTest>("UIPopupTest");
        popup.Init(currentPopupID, currentPopupPos);

        currentPopupID++;
        currentPopupPos += popup.MoveVec;
    }
}
