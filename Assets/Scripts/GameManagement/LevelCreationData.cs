using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct LevelCreationData
{
    public int _levelIndex;
    public int _horizontalMargin;

    public string _levelName;
    
    public InGameContainer _inGameContainer;

    public Pool _cellPool;
}
