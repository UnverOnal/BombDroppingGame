using UnityEngine;
using UnityEngine.UI;

public class LevelUi : MonoBehaviour
{
    [HideInInspector]public int index;
    
    [SerializeField] private LevelUiContainer levelUiContainer;
    
    [Header("Button")]
    [SerializeField] private Text buttonText;
    [SerializeField] private Image button;
    
    [Header("Progress Bar")]
    [SerializeField] private Image progressFillRect;
    [SerializeField] private Transform progressParent;
    [SerializeField] private Text progressText;
    
    [Header("Misc")]
    [SerializeField] private Text levelCountText;
    [SerializeField] private Transform starsParent;
    
    [HideInInspector]public int starCount;
    private int PreviousStarCount { get; set; }
    public int previousAverage => GetPreviousAverage();

    private LevelUi previousLevelUi;
    
    private bool isFirstLevelUi => index == 1;
    public bool isPlayable => previousLevelUi?.starCount > 0;
    
    public void Initialize(int _index, LevelUi _previousLevelUi)
    {
        index = _index;
        previousLevelUi = _previousLevelUi;
        PreviousStarCount = GetPreviousStarCount(previousLevelUi);
        
        starCount = Data.instance.GetStars(index);
        SetStars();
        
        //Set level count
        levelCountText.text = "LEVEL-" + index.ToString();
        
        //Set button based on star count    
        SetButton();
    }

    public void UpdateLevelUi()
    {
        starCount = Data.instance.GetStars(index);
        SetStars();
        
        PreviousStarCount = GetPreviousStarCount(previousLevelUi);
        
        SetButton();
    }

    private void SetButton()
    {
        string _buttonText;

        if (isPlayable || isFirstLevelUi)
        {
            _buttonText = levelUiContainer.playButtonText;
            button.sprite = levelUiContainer.playButtonSprite;
            
            if(!button.GetComponent<PlayButton>())
                button.gameObject.AddComponent<PlayButton>();
        }
        else
        {
            _buttonText = levelUiContainer.lockedButtonText;
            button.sprite = levelUiContainer.lockedButtonSprite;
        }
        buttonText.text = _buttonText;
    }

    //Returns average of previous levelUi objects stars
    private int GetPreviousAverage()
    {
        if (index < 2)
            return 1;
        
        return PreviousStarCount / (index - 1);
    }

    private int GetPreviousStarCount(LevelUi _previousLevelUi)
    {
        if (!_previousLevelUi)
            return 0;
        
        return _previousLevelUi.starCount + _previousLevelUi.PreviousStarCount;
    }

    public void SetStars()
    {
        progressParent.gameObject.SetActive(false);
        starsParent.gameObject.SetActive(true);
        
        Image[] stars = starsParent.GetComponentsInChildren<Image>();

        for (int i = 0; i < starCount; i++)
            stars[i].color = Color.white;
    }

    public void SetUnlockProgress()
    {
        int averageStars = GetPreviousAverage();
        int starsToUnlock = (index - 1) * 2;

        string progress = PreviousStarCount.ToString() + "/" + starsToUnlock.ToString();
        
        if(averageStars < 2)
            SetUnlockProgressUis((float)PreviousStarCount / starsToUnlock, progress);
    }
    
    private void SetUnlockProgressUis(float fillAmount, string progress)
    {
        starsParent.gameObject.SetActive(false);
        progressParent.gameObject.SetActive(true);

        progressFillRect.fillAmount = fillAmount;

        progressText.text = progress;
    }
}
