using UnityEngine;
using UnityEngine.UI;

public class ButtonHitbox : MonoBehaviour
{
    public float alphaThreshhold = 0.1f;
    void Start()
    {
        GetComponent<Image>().alphaHitTestMinimumThreshold = alphaThreshhold;
    }
}
