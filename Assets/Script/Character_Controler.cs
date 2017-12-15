using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Controler : MonoBehaviour {
    public float speed = 4.5f;
    public float jumpforce = 558;
    public Transform groundCheck;
    public LayerMask whatIsGround;

    public PC_Controler PC;
   

    Rigidbody2D rb;
    SpriteRenderer sprite;
   public  bool isGrounded = false;
    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Tab))
            PC.TurnPC();


    }
    private void FixedUpdate()
    {
        float move = Input.GetAxis("Horizontal") * speed;
        rb.velocity = new Vector2(move, rb.velocity.y);

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.15F, whatIsGround);
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
            rb.AddForce(Vector2.up * jumpforce);

        //Flip
        if (move < 0)
            sprite.flipX = true;
        else
            sprite.flipX = false;
    }

   


    
}
