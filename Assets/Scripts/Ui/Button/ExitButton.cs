using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButton : ButtonCustom
{
    protected override void Start()
    {
        base.Start();
        
        AddToButtonEvent(Exit);
    }

    private void Exit()
    {
        UiManager.instance.onGamePanelUnloaded.Invoke();
    }
}
