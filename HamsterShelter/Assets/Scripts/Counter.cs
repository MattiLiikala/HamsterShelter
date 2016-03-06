using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{
    public string Text;

    private Text textComponent;

    [SerializeField]
    private int total;
    private int count;

    public int Total
    {
        get { return total; }
        set
        {
            total = value;
            if (textComponent != null) textComponent.text = Text + count + "/" + total;
        }
    }
    public virtual int Count
    {
        get { return count; }
        set
        {
            count = value;
            total = System.Math.Max(count, total);
            if (textComponent != null) textComponent.text = Text + count + "/" + total;
        }
    }


    protected virtual void Awake()
    {
        textComponent = GetComponent<Text>();
    }
}
