using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class Grid
{
    private readonly int[][] matrix;

    private readonly InGameContainer inGameContainer;
    
    public Cell[][] gridMatrix;
    public List<WallCell> wallCells = new List<WallCell>();

    private Pool cellPool;

    private readonly float cellScale;
    private readonly float cellDistance;
    
    public Grid(int[][] _matrix, Pool _cellPool, InGameContainer _inGameContainer, int horizontalMargin)
    {
        this.matrix = _matrix;
        this.inGameContainer = _inGameContainer;
        this.cellPool = _cellPool;
        
        gridMatrix = new Cell[matrix.Length][];
        
        //Gets scale to fit cells to screen - for any level.
        //Reference sprite is empty cell.
        cellScale = GetScale(inGameContainer.emptyCell, matrix.Length, horizontalMargin);
        
        //Distance between cells.
        //cellScale : moves based on own size.
        //1.5f : cell unit
        cellDistance = cellScale * 1.5f;
    }

    public void CreateGrid()
    {
        int lineCount = matrix.Length;
        int lineSize = matrix[0].Length;

        Vector2 origin = new Vector2(-(lineCount - 1) / 2f, -(lineSize - 1) / 2f);
        
        for (int i = 0; i < lineCount; i++)
        {
            Cell[] cellsLine = new Cell[lineSize];
            gridMatrix[i] = cellsLine;
            
            for (int j = 0; j < lineSize; j++)
            {
                CellType cellType = (CellType)matrix[i][j];
                var cell = CreateCell(cellType, GetPosition(i, j, origin) * cellDistance, new Vector2(i, j));
                cellsLine[j] = cell;
                
                if(cellType == CellType.wall)
                    wallCells.Add((WallCell)cell);
            }
        }
    }

    private Cell CreateCell(CellType cellType, Vector2 screenPosition, Vector2 gridLocation)
    {
        //Creates cell gameObject
        GameObject cellGameObjectClone = cellPool.GetPooledObject();
        cellGameObjectClone.transform.position = screenPosition;
        cellGameObjectClone.transform.localScale *= cellScale;
        
        //Sets cell
        Type type = GetCellType(cellType);
        var cell = cellGameObjectClone.AddComponent(type) as Cell;

        CellCreationData creationData = new CellCreationData()
        {
            _cellGameObject = cellGameObjectClone,
            _cellType = cellType,
            _gridMatrix = gridMatrix,
            _locationIndexes = gridLocation,
            _inGameContainer = inGameContainer
        };
        
        cell?.SetCell(creationData);

        return cell;
    }

    //Gets position on which cells place.
    private Vector2 GetPosition(int x, int y, Vector2 originPoint)
    {
        return new Vector2(x, y) + originPoint;
    }

    //Gets scale, based on horizontal cell count and screen width.
    private float GetScale(Sprite cell ,int horizontalCellCount, int horizontalMargin)
    {
        int width = Screen.width - horizontalMargin;
        float originalCellSize = cell.rect.width;
        float targetCellSize = (float)width / horizontalCellCount;

        float scale = targetCellSize / originalCellSize;
        return scale;
    }

    private Type GetCellType(CellType cellType)
    {
        switch (cellType)
        {
            case CellType.empty :
                return typeof(EmptyCell);
            
            case CellType.wall :
                return typeof(WallCell);
            
            default :
                return typeof(Cell);
        }
    }
}
