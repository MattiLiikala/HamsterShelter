using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreCounter : Counter {

    public static ScoreCounter Instance;

    public GameObject[] HamsterIcons;

    public Sprite HamsterAliveSprite, HamsterDeadSprite;

    protected override void Awake()
    {
        base.Awake();

        Instance = this;
    }

    public override int Count
    {
        get
        {
            return base.Count;
        }

        set
        {
            base.Count = value;
            SetHamsterIcons();
        }
    }

    private void SetHamsterIcons()
    {
        if (HamsterIcons == null) return;
        for (int i = 0; i < HamsterIcons.Length; i++)
        {
            HamsterIcons[i].GetComponent<Image>().color = i < Count ? Color.white : Color.gray;
            HamsterIcons[i].GetComponent<Image>().sprite = i < Count ? HamsterAliveSprite : HamsterDeadSprite;
        }
    }

}
