using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

public class EmptyCell : Cell
{
    public void PlaceBomb(GameObject cellGameObject)
    {
        PlaceBombSprite(cellGameObject);
        
        status = CellStatus.locked;

        ExplodeNeighbourWalls();

        this.gameObject.layer = LayerMask.NameToLayer("Default");
    }
    
    private void PlaceBombSprite(GameObject cellGameObject)
    {
        //Creates and place on center of empty cell
        cellGameObject.transform.SetParent(this.transform);
        cellGameObject.transform.localScale = Vector3.one;
        cellGameObject.transform.localPosition = Vector3.zero;

        //Sets sprite & sorting order
        var spriteRenderer = cellGameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = inGameContainer.bomb;
        spriteRenderer.sortingOrder = 10;
    }

    //Gets neighbour walls while base method doesn't care types.
    public override Cell[] GetNeighbours()
    {
        Cell[] allNeighbours = base.GetNeighbours();

        Cell[] walls = allNeighbours.Where(neighbour => neighbour.cellType == CellType.wall)
            .Select(neighbour => neighbour).ToArray();

        return walls;
    }

    //Used for counting neighbour walls as 'exploded' when a bomb is placed by user.
    private void ExplodeNeighbourWalls()
    {
        foreach (var cell in NeighbourCells)
        {
            WallCell wall = (WallCell)cell;
            wall.status = CellStatus.locked;
        }
    }

    //Used for marking neighbour walls as 'used', to calculate minimum bomb amount.
    public void MarkNeighbourWalls()
    {
        foreach (var cell in NeighbourCells)
        {
            WallCell wall = (WallCell)cell;
            wall.hasBeenUsedToCount = true;
        }
    }
}