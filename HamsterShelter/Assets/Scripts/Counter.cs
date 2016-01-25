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
            textComponent.text = Text + count + "/" + total;
        }
    }
    public int Count
    {
        get { return count; }
        set
        {
            count = value;
            total = System.Math.Max(count, total);
            textComponent.text = Text + count + "/" + total;
        }
    }


    void Awake()
    {
        textComponent = GetComponent<Text>();
    }
}
