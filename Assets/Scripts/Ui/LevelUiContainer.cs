using UnityEngine.UI;
using UnityEngine;

[CreateAssetMenu(fileName = "levelUiContainer", menuName = "ScriptableObjects/LevelUiContainerObject", order = 1)]
public class LevelUiContainer : ScriptableObject
{
    [Header("Sprites")]
    public Sprite playButtonSprite;
    public Sprite lockedButtonSprite;

    [Header("Texts")]
    public string playButtonText;
    public string lockedButtonText;
}
