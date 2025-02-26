using UnityEngine;

public class BlinkerObject : RandomableObject
{
    private const float UNIT = 1.0f / 3.0f;

    private Transform TopRenderer;
    private Transform BottomRenderer;

    private void Start()
    {
        if (!randomableObject)
        {
            return;
        }

        TopRenderer = transform.parent.Find(nameof(TopRenderer));
        BottomRenderer = transform.parent.Find(nameof(BottomRenderer));

        switch (normalizedTime)
        {
            case > UNIT * 2.0f:
                SetTopLight(2);
                SetBottomLight(0);
                break;
            case > UNIT * 1.0f:
                SetTopLight(1);
                SetBottomLight(1);
                break;
            default:
                SetTopLight(0);
                SetBottomLight(1);
                break;
        }
    }

    private void SetTopLight(int index)
    {
        for (int i = 0; i < TopRenderer.childCount; i++)
        {
            if (i == index)
            {
                TopRenderer.GetChild(i).gameObject.SetActive(true);
                continue;
            }

            TopRenderer.GetChild(i).gameObject.SetActive(false);
        }
    }

    private void SetBottomLight(int index)
    {
        for (int i = 0; i < BottomRenderer.childCount; i++)
        {
            if (i == index)
            {
                BottomRenderer.GetChild(i).gameObject.SetActive(true);
                continue;
            }

            BottomRenderer.GetChild(i).gameObject.SetActive(false);
        }
    }
}