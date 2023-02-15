using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelPanel : BaseUIPanel
{

    public Text lv_text;

    public Slider lv_count_slider;

    //等级经验
    public int experience = 1;
    //人物等级
    public int level = 1;

    //获得经验刷新等级
    public void RefeshLevel(int ex)
    {
        experience += ex;

        lv_count_slider.value = experience % 10;
        lv_text.text = string.Format("LV.{0}", level += experience / 10);
        LevelManager.Instance.SendRequest(level);
    }
    public void SetLevelOnStart()
    {
        level = GameLifeCycle.Instance.level;
        lv_text.text = string.Format("LV.{0}", level);
    }
    protected override void InitPanel()
    {
        LevelManager.Instance.levelPanel = this;
        SetLevelOnStart();
        base.InitPanel();
    }

}
