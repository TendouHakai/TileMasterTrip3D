using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buffBackBtn : BuffButton
{
    public override void OnClick()
    {
        if (SlotManager.getInstance().isCanRemove())
        {
            base.OnClick();
        }
        else
        {
            SoundManager.getInstance().PlaySound("ButtonClick");
        }
    }

    public override void BuffExcute()
    {
        SlotManager.getInstance().Remove();
    }
}
