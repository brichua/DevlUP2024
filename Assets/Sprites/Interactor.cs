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
        if(colliders.Length > 0)
        {
            
            int closest = 0;
            float closestDistance = Mathf.Infinity;
            for(int i = 0; i< colliders.Length; i++)
            {

                if (Vector2.Distance((colliders[i]).transform.position,transform.position)<closestDistance)
                {
                    
                    closest = i;
                    closestDistance = Vector2.Distance((colliders[i]).transform.position, transform.position);
                }
            }
            Interactable interactable = colliders[closest].GetComponent<Interactable>();
            if (colliders[closest].GetComponent<ItemPickup>() != null)
            {
                string closestName = colliders[closest].GetComponent<ItemPickup>().resource.name;
                //Debug.Log(closestName);
            }
            
            if (Input.GetKeyDown(KeyCode.E))
            {
                
                interactable.isFocus = true;
            }

        }
    }
}
