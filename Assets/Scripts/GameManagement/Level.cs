using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class Level
{
    private readonly LevelCreationData creationData;
    
    public int levelIndex => creationData._levelIndex;
    public Grid Grid { get; private set; }

    public Level(LevelCreationData _creationData)
    {
        this.creationData = _creationData;

        SpawnLevel(out var grid);
        this.Grid = grid;
    }
    
    private void SpawnLevel(out Grid _grid)
    {
        int[][] levelMatrix = GetLevelMatrix(creationData._levelName, creationData._levelIndex);
        
        var grid = new Grid(levelMatrix, creationData._cellPool, creationData._inGameContainer, creationData._horizontalMargin);
        grid.CreateGrid();

        _grid = grid;
    }
    
    private int[][] GetLevelMatrix(string _levelName, int _levelIndex)
    {
        string fileName = _levelName + _levelIndex.ToString();
        TextAsset level = Resources.Load<TextAsset>("Levels/" + fileName);

        List<List<int>> matrix = new List<List<int>>();
        if (level != null)
        {
            using (StreamReader stream = new StreamReader(new MemoryStream(level.bytes)))
            {
                while(!stream.EndOfStream)
                {
                    string input = stream.ReadLine( );
            
                    var line = input.Split(',').Select(element => Convert.ToInt32(element)).ToList();
                    
                    matrix.Add(line);
                }
                stream.Close( );
            }
        }
        return matrix.Select(line => line.ToArray()).ToArray();
    }
}