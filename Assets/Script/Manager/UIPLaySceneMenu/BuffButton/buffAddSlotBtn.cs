using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buffAddSlotBtn : BuffButton
{
    public override void OnClick()
    {
        base.OnClick();
    }

    public override void BuffExcute()
    {
        SlotManager.getInstance().addASlot();
        this.gameObject.SetActive(false);
    }

    public override void BuffUpdate()
    {
        
    }
}
