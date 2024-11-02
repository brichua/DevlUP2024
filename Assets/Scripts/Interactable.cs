using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 2f;
    public bool isFocus = false;
    private GameObject player;

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    public virtual void Interact()
    {
       
            Debug.Log("Interacting with " + transform.name);
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius); 
    }

    private void Update()
    {
        
        if (isFocus)
        {
            isFocus = false;
            if (player == null)
            {
                player = GameObject.Find("Player");
            }
            float distance = Vector2.Distance(player.transform.position, transform.position);
            if (distance < radius)
            {
                Interact();
            }
        }    
    }
}
