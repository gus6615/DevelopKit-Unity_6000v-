using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Popup : UI_Base
{
    public override void Init()
    {
        base.Init();

        // Popup�� Pivot�� �߾����� ����
        target.pivot = new Vector2(0.5f, 0.5f);
    }
}
