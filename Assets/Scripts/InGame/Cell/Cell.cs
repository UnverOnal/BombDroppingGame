using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CellStatus
{
    free,
    locked
}

public class Cell : MonoBehaviour
{
    public CellType cellType;
    
    protected InGameContainer inGameContainer;

    public Vector2 locationIndexes;

    protected Cell[][] gridMatrix;

    public CellStatus status;

    public bool hasBeenUsedToCount;

    private Cell[] neighbourCells;
    public Cell[] NeighbourCells
    {
        get
        {
            if (neighbourCells == null)
                neighbourCells = GetNeighbours();

            return neighbourCells;
        }
    }

    private Pool cellPool;

    //Works like a constructor.
    public void SetCell(CellCreationData creationData)
    {
        this.cellType = creationData._cellType;
        this.inGameContainer = creationData._inGameContainer;
        this.locationIndexes = creationData._locationIndexes;
        this.gridMatrix = creationData._gridMatrix;

        SpriteRenderer spriteRenderer = creationData._cellGameObject.GetComponentInChildren<SpriteRenderer>();
        var sprite = GetSprite(cellType);
        spriteRenderer.sprite = sprite;

        //Sets collider
        this.gameObject.AddComponent<BoxCollider2D>().size = sprite.rect.size / sprite.pixelsPerUnit;
        
        //Sets layer
        this.gameObject.layer = LayerMask.NameToLayer(cellType.ToString());
    }
    
    protected Sprite GetSprite(CellType _cellType)
    {
        switch (_cellType)
        {
            case CellType.empty :
                return inGameContainer.emptyCell;
            
            case CellType.wall :
                return inGameContainer.wall;

            default:
                return null;
        }
    }

    public virtual Cell[] GetNeighbours()
    {
        List<Cell> neighbours = new List<Cell>();

        int x = (int)locationIndexes.x;
        int y = (int)locationIndexes.y;
        int row = gridMatrix.Length;
        int column = gridMatrix[0].Length;

        //Gets cells clockwise.
        if(row > x + 1)
            neighbours.Add(gridMatrix[x + 1][y]);
        if(y - 1 >= 0 )
            neighbours.Add(gridMatrix[x][y - 1]);
        if(x - 1 >= 0 )
            neighbours.Add(gridMatrix[x - 1][y]);
        if(column > y + 1)
            neighbours.Add(gridMatrix[x][y + 1]);

        return neighbours.ToArray();
    }
}
