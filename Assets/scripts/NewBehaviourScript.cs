using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{

    Rigidbody2D rb;
    float maxSpeed = .3f;
    float move = 1;
    Animator animator;
    int state;
    bool ishurt;
   
    int NoOfHits=0;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        state = animator.GetInteger("CurrentState");
        ishurt = animator.GetBool("IsHurt");
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(move * maxSpeed, 0);
        
    }
    
  


    // Update is called once per frame
    void Update()

    {
        if (Input.GetMouseButtonDown(0))

        {
            //power up
            var purple = GameObject.FindWithTag("powerup");
            //jump anywhere
            if (state == 0 )
            {
                state = 1;
                animator.SetInteger("CurrentState", state);
                rb.AddForce(Vector2.up * 75, ForceMode2D.Impulse);
                
            }  

            //powerup
            if (purple)
            {
                ishurt = false;
                animator.SetBool("IsHurt", ishurt);  
              
            }
        }

        
        
        //lose scene
        if (NoOfHits == 2 && ishurt == true)
        {
            SceneManager.LoadScene("Lose");

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //platform, balck bars
        if (collision.gameObject.tag == "platform")
        {
            state = 0;
            animator.SetInteger("CurrentState", state);
        }


        //boxes
        if (collision.gameObject.tag == "hazard")
        {
            ishurt = true;
            animator.SetBool("IsHurt", ishurt);

            rb.AddForce(new Vector2(-1, 0), ForceMode2D.Impulse);
            NoOfHits = NoOfHits + 1;

            state = 0;
            animator.SetInteger("CurrentState", state);

        }


        //goal game object
        if (collision.gameObject.tag == "goal")
        {
            SceneManager.LoadScene("Win");

        }
        
        //end border to back to game
        if (collision.gameObject.tag == "end")
        {
            rb.AddForce(new Vector2(1, 0), ForceMode2D.Impulse);
            

        }
    }

  




}
