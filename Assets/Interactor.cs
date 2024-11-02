using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] float interactionRadius = 2f;
    [SerializeField] LayerMask interactableLayer;

    private Collider2D[] colliders = new Collider2D[3];
    [SerializeField] private int numFound;
    // Update is called once per frame
    void Update()
    {
        colliders =  Physics2D.OverlapCircleAll(transform.position, interactionRadius, interactableLayer);
        numFound = colliders.Length;
        if(colliders.Length > 0 && Input.GetKeyDown(KeyCode.E))
        {
            Interactable interacable = colliders[0].GetComponent<Interactable>();
            interacable.Interact();

        }
    }
}
