using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Hover : UI_Base
{
    public override void Init()
    {
        base.Init();

        // Hover�� Pivot�� ���ϴ����� ����
        target.pivot = Vector2.zero;
    }
}