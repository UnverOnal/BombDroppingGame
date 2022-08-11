using System;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

public class UnityEventInt : UnityEvent<int>{}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    [SerializeField] private InGameContainer inGameContainer;
    
    [HideInInspector] public UnityEventInt onLevelLoaded = new UnityEventInt();

    [Header("Level")]
    public int totalLevelCount;
    [SerializeField]private string levelFileName;
    [HideInInspector] public Level currentLevel;

    [SerializeField] private int horizontalMargin = 100;

    private void Awake() 
    {
        instance = Singleton.GetInstance<GameManager>();

        Application.targetFrameRate = 60;
    }

    private void OnEnable()
    {
        onLevelLoaded.AddListener(SpawnLevel);
    }

    private void OnDisable()
    {
        onLevelLoaded.RemoveListener(SpawnLevel);
    }

    private void Start()
    {
        //Set orthographic camera size, based on reference height and current screen height 
        //to fit in-game objects to screen.
        float referenceResolutionHeight = UiManager.instance.referenceResolution.y;
        Camera.main.orthographicSize *= Screen.height / referenceResolutionHeight;
    }

    private void SpawnLevel(int levelIndex)
    {
        LevelCreationData creationData = new LevelCreationData()
        {
            _levelName = levelFileName,
            _levelIndex = levelIndex,
            _inGameContainer = inGameContainer,
            _cellPool = GridManager.instance.cellPool,
            _horizontalMargin = horizontalMargin,
        };
        
        currentLevel = new Level(creationData);
        
        GridManager.instance.SetLevelGridData(currentLevel.Grid, levelIndex);
    }
}
