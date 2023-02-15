using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager
{
    private static LevelManager instance;
    public static LevelManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new LevelManager();
            }
            return instance;
        }
    }
    public LevelPanel levelPanel;
    public LevelRequest levelRequest;
    public LevelManager()
    {
        Init();
    }

    public void Init()
    {

    }

    public void RefeshLevel(int ex)
    {
        levelPanel.RefeshLevel(ex);
    }

    public void SendRequest(int level)
    {
        levelRequest.SendRequest(level);
    }
}
