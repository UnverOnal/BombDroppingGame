using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{   
    public static UiManager instance;

    [Header("Panels")]
    public MainPanel mainPanel;
    public GamePanel gamePanel;

    [HideInInspector] public UnityEvent onGamePanelLoaded = new UnityEvent();
    [HideInInspector] public UnityEvent onGamePanelUnloaded = new UnityEvent();
    [HideInInspector] public UnityEvent onMainPanelLoaded = new UnityEvent();

    public Vector2 referenceResolution => GetComponent<CanvasScaler>().referenceResolution;
    
    private void Awake() 
    {
        instance = Singleton.GetInstance<UiManager>();
    }

    private void OnDisable()
    {
        //UiManagers' lifetime larger than the panels
        gamePanel.RemoveListeners();
        mainPanel.RemoveListeners();
    }
}
