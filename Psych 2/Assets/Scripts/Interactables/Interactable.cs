using UnityEngine.Events;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public string interactableName;
    public bool isSelected;
    public UnityEvent OnInteract;

    public virtual void triggerInteract()
    {
        OnInteract.Invoke();
        Debug.Log("Interaction triggered");
    }
}
