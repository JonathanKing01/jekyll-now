using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Movement : MonoBehaviour {
	
	// Variables used in code
	[HideInInspector] public bool facingRight = true;
	[HideInInspector] public bool jump = false;
	[HideInInspector] public bool walljump = false;
	public float moveForce = 365f;
	public float maxSpeed = 5f;
	public bool wallgrab = false;
	private bool grounded = false;
	public float jumpForce = 1000f;
	float h = 0; // Direction variable
	Animator animator;
	
	// Find the player's rigidbody component at start up
	private Rigidbody2D rb2d;
	void Awake () 
	{
		rb2d = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator> ();
		
	}
	
	// Use this for initialization
	void Start () {
				
	}
	
	// Check if grounded or pressed into a wall. If space is pressed, either jump or wall jump.
	void Update () {
		
		Vector3 wallCheck = new Vector3 (transform.position.x+2f*h, transform.position.y, transform.position.z);
		wallgrab = Physics2D.Linecast(transform.position, wallCheck, 1 << LayerMask.NameToLayer("Ground"));
		
		Vector3 groundCheck = new Vector3 (transform.position.x, transform.position.y-1f, transform.position.z);
		grounded = Physics2D.Linecast(transform.position, groundCheck, 1 << LayerMask.NameToLayer("Ground"));
		
		if (Input.GetKey(KeyCode.W) && grounded){
			jump = true;
		} else if (Input.GetKey(KeyCode.W) && wallgrab){
			walljump = true;
		}
	}
	
	// Process directional commands and move character
	void FixedUpdate(){
		animator.SetBool ("Walking", false);

		if (Input.GetKey (KeyCode.D)) {
			h = 1;
			animator.SetBool ("Walking", true);
		}
		else if (Input.GetKey(KeyCode.A)){
			h = -1;
			animator.SetBool ("Walking", true);
		}
		else h=0;
		
		if (h * rb2d.velocity.x < maxSpeed)
			rb2d.AddForce(Vector2.right * h * moveForce);
		
		if (Mathf.Abs (rb2d.velocity.x) > maxSpeed)
			rb2d.velocity = new Vector2(Mathf.Sign (rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);
		
		if (h > 0 && !facingRight)
			Flip ();
		else if (h < 0 && facingRight)
			Flip ();

		
		if (jump)
		{
			rb2d.AddForce(new Vector2(0f, jumpForce));
			jump = false;
		}
		
	}
	
	// Flip player (used in changing direction in FixedUpdate)
	void Flip()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		Vector3 velocity = transform.GetComponent<Rigidbody2D>().velocity;
		theScale.x *= -1;
		velocity.x = 0;
		transform.localScale = theScale;
	}
	
}
