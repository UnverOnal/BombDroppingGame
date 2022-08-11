using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButton : ButtonCustom
{
    private LevelUi levelUi;
    
    protected override void Start()
    {
        base.Start();
        
        AddToButtonEvent(Play);

        levelUi = GetComponentInParent<LevelUi>();
    }

    private new void Play()
    {
        UiManager.instance.mainPanel.clickedLevelUiIndex = levelUi.index;
        
        UiManager.instance.onGamePanelLoaded.Invoke();
        GameManager.instance.onLevelLoaded.Invoke(levelUi.index);
    }
}
