using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResistTheId : MonoBehaviour
{
    public Animator idAnimator;
    public RectTransform slider;
    public TMPro.TextMeshProUGUI searchbar;
    private string naughty = "Prograde - Old ; Retrograde - New";
    public int naughtyIndex;
    public float typeSpeed = 0.1f;
    public float typeSpeedDelta;
    public bool active;

    public void Update()
    {
        if (active)
        {
            MoveHandle();
        }
        else
        {
            StopHandle();
        }
    }

    private void MoveHandle()
    {
        idAnimator.SetBool("Move", true);

        if (typeSpeedDelta >= 0)
        {
            typeSpeedDelta -= Time.deltaTime;
        }
        else
        {
            if (naughtyIndex < naughty.Length)
            {
                searchbar.text += naughty.Substring(naughtyIndex, 1);
                naughtyIndex++;
                typeSpeedDelta = typeSpeed;
            }
            else
            {
                active = false;
            }
        }

        if(searchbar.text == naughty)
        {
            active = false;
        }
    }

    public void StopHandle()
    {
        idAnimator.SetBool("Move", false);
    }

    public int GetState()
    {
        float pos = Mathf.Abs(slider.anchoredPosition.x);

        if (pos > 187.5f)
        {
            return 0;
        }
        if (pos > 150f)
        {
            return 1;
        }
        if (pos > 100)
        {
            return 2;
        }
        else
        {
            return 3;
        }
    }
}
