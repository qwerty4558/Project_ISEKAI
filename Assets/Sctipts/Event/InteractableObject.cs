using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class InteractableObject : MonoBehaviour
{
    [SerializeField] private string interactorName;
    public string InteractorName { get { return interactorName; } }
    [SerializeField] private UnityEvent actionOnInteract;
    [SerializeField] private Transform namePosition;
    public Transform NamePosition { get { return namePosition; } }

    public void OnInteract()
    {
        if (actionOnInteract != null)
            actionOnInteract.Invoke();
    }
}
