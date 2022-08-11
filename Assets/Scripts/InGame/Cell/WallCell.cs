using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WallCell : Cell
{
    //Gets neighbour empties while base method doesn't care types.
    public override Cell[] GetNeighbours()
    {
        Cell[] allNeighbours = base.GetNeighbours();

        Cell[] emptyCells = allNeighbours.Where(neighbour => neighbour.cellType == CellType.empty)
            .Select(neighbour => neighbour).ToArray();

        return emptyCells;
    }

    //Returns the most explosive empty cell around this wall i.e. has most wall neighbour.
    public EmptyCell GetMostExplosiveEmptyCell(WallCell[] wallCells)
    {
        EmptyCell[] emptyCells = NeighbourCells.Cast<EmptyCell>().ToArray();
        var emptyCellsSorted = emptyCells.OrderByDescending(i => i.NeighbourCells.Length).ToArray();

        return emptyCellsSorted[0];
    }
}
