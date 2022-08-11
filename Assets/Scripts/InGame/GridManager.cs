using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager instance;

    private Grid grid;

    private int currentLevelIndex;
    
    [HideInInspector] public int bombCount;

    [HideInInspector] public Cell[][] gridMatrix => grid.gridMatrix;

    private Pool bombPool;
    [HideInInspector]public Pool cellPool;

    private void Awake()
    {
        instance = Singleton.GetInstance<GridManager>();
    }
    
    private void OnEnable()
    {
        UiManager.instance.onGamePanelUnloaded.AddListener(ResetGrid);
    }

    private void OnDisable()
    {
        UiManager.instance.onGamePanelUnloaded.RemoveListener(ResetGrid);
    }

    private void Start()
    {
        cellPool = new Pool(20, Resources.Load<GameObject>("inGame/cell"), this.gameObject);
        bombPool = new Pool(5, Resources.Load<GameObject>("inGame/emptySpriteRenderer"), this.gameObject);
    }

    private void Update()
    {
        var cellHit = InputManager.instance.hit.transform?.GetComponent<Cell>();
        if(!cellHit || bombCount < 1) return;

        var cell = cellHit as EmptyCell;
        cell.PlaceBomb(bombPool.GetPooledObject());
        UpdateBombCount();

        if (AreAllWallsExploded())
        {
            SetStarCount(bombCount);
            UiManager.instance.onGamePanelUnloaded.Invoke();
        }
    }

    private void ResetGrid()
    {
        bombPool.ResetPool();
        cellPool.ResetPool(cell => 
        {
            Destroy(cell.GetComponent<BoxCollider2D>());
            Destroy(cell.GetComponent<Cell>());
        });
    }

    public int CalculateBombCount()
    {
        List<WallCell> wallCells = grid.wallCells;

        int bombCount = 0;

        //Checks all wallCells respectively.
        //Gets most explosive empty cells and counts them.
        //Marks those emptyCells - and walls around them as well - to avoid repeated checks. 
        foreach (var wallCell in wallCells)
        {
            if (wallCell.hasBeenUsedToCount) continue;
            
            EmptyCell suitableEmptyCell = wallCell.GetMostExplosiveEmptyCell(grid.wallCells.ToArray());
            suitableEmptyCell.hasBeenUsedToCount = true;
            suitableEmptyCell.MarkNeighbourWalls();

            bombCount++;
        }
        return bombCount;
    }

    public void SetLevelGridData(Grid _grid, int levelIndex)
    {
        this.grid = _grid;
        this.currentLevelIndex = levelIndex;

        bombCount = CalculateBombCount() + 2;
        
        UiManager.instance.gamePanel.DisplayBombCount(bombCount);
    }

    private bool AreAllWallsExploded()
    {
        int wallCount = grid.wallCells.Count;

        var explodedCells = from wall in grid.wallCells
            where wall.status == CellStatus.locked
            select wall;

        int explodedWallCount = explodedCells.ToArray().Length;

        return wallCount == explodedWallCount;
    }

    private void UpdateBombCount()
    {
        bombCount--;
        UiManager.instance.gamePanel.DisplayBombCount(bombCount);
    }

    private void SetStarCount(int _bombCount)
    {
        int starCount;
        
        switch (_bombCount)
        {
            case 2 :
                starCount = 3;
                break;
            case 1 :
                starCount = 2;
                break;
            case 0 :
                starCount = 1;
                break;
            default:
                starCount = 1;
                break;
        }

        var starCountToSave = Data.instance.GetStars(currentLevelIndex);
        starCountToSave = starCountToSave > starCount ? starCountToSave : starCount;
        Data.instance.SetStars(starCountToSave, currentLevelIndex);
    }
}
