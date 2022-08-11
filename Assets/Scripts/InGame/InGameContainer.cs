using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "inGameContainer", menuName = "ScriptableObjects/InGameContainerObject", order = 2)]
public class InGameContainer : ScriptableObject
{
    public Sprite bomb;
    public Sprite emptyCell;
    public Sprite wall;

}
