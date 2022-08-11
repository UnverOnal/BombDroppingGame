using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data
{
    private static Data _instance;

    public static Data instance
    {
        get
        {
            if (_instance == null)
                _instance = new Data();

            return _instance;
        }
    }

    public void SetStars(int starCount, int levelIndex)
    {
        PlayerPrefs.SetInt("levelIndex" + levelIndex.ToString(), starCount);
        PlayerPrefs.Save();
    }

    public int GetStars(int levelIndex)
    {
        return PlayerPrefs.GetInt("levelIndex" + levelIndex.ToString(), 0);
    }
}
