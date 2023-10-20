using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideButton : BuffButton
{
    public override void OnClick()
    {
        base.OnClick();
    }

    public override void BuffExcute()
    {
        SlotManager.getInstance().addGuide();
    }
}
