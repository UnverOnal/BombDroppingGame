using UnityEngine;
using UnityEngine.UI;

public class GamePanel : Panel, IEventDeallocation
{
    [SerializeField]private Text levelText;

    [SerializeField] private Text bombCountText;

    private UiManager _uiManager => UiManager.instance;
    
    private void OnEnable()
    {
        GameManager.instance.onLevelLoaded.AddListener(DisplayLevelCount);
        
        _uiManager.onGamePanelUnloaded.AddListener(DisableSmoothly);
        _uiManager.onGamePanelLoaded.AddListener(EnableSmoothly);
    }

    public void RemoveListeners()
    {
        GameManager.instance.onLevelLoaded.RemoveListener(DisplayLevelCount);
        
        _uiManager.onGamePanelUnloaded.RemoveListener(DisableSmoothly);
        _uiManager.onGamePanelLoaded.RemoveListener(EnableSmoothly);
    }

    private void DisplayLevelCount(int levelIndex)
    {
        levelText.text = "LEVEL " + levelIndex.ToString();
    }
    
    public void DisplayBombCount(int bombCount)
    {
        bombCountText.text = bombCount.ToString();
    }
}
