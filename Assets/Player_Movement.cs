using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float speed;
    [SerializeField] Rigidbody2D rb;
    

    private Vector2 movement;
    private Vector2 smoothedMovInput;
    private Vector2 smoothVelocity;

    
    public bool talking = false;

    private Animator animator;
    private const string horizontal = "Horizontal";
    private const string vertical = "Vertical";



    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
       
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        animator.SetFloat(horizontal, movement.x);
        animator.SetFloat(vertical, movement.y);
 

    }

    private void FixedUpdate()
    {
        if (!talking)
        {
            smoothedMovInput = Vector2.SmoothDamp(smoothedMovInput, movement, ref smoothVelocity, 0.05f);
            rb.velocity = smoothedMovInput * speed;
        }
        else
        {
            rb.velocity = new Vector3(0, 0, 0);
        }

    }

}
