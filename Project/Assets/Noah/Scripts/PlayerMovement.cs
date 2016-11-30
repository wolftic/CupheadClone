using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	[SerializeField]
	private float _playerMovSpeed = 10.0f;
	[SerializeField]
	private float _playerJumpForce = 5.0f;
    [SerializeField]
    private float _jumpCooldown = 0.2f, _jumpCooldownTime = 0f;
    [SerializeField]
    private float _raysToShoot = 10f;

    Rigidbody2D rb;
	private RaycastHit _hit;
	[SerializeField]
	private LayerMask groundLayer;
	[SerializeField]
	private float _dist;
	Animator anim;


	// Use this for initialization
	void Start () {
		rb = gameObject.GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator> ();
	}
	void FixedUpdate () {
		  
		rb.velocity = Input.GetAxis ("Horizontal") * Vector3.right * _playerMovSpeed + rb.velocity.y * Vector3.up; // Player Movement over de horizontale as.
		//anim.SetFloat("movement", Mathf.Abs(Input.GetAxis("Horizontal")));
        if (_jumpCooldownTime < Time.time)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
        }
	}
	bool IsGrounded(){
		Vector2 position = transform.position; // Vector2 positie van de speler
		Vector2 direction = Vector2.down; // De richting die de Raycast op gaat
        Vector2 step = new Vector2(1f / _raysToShoot, 0f);
        bool collided = false;
        
        for (int i = 0; i < _raysToShoot; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(position + (-_raysToShoot / 2 - i) * step, direction, _dist, groundLayer); //Nieuw Raycast hit die de positie,richting,afstand en de layer pakt 
            //RaycastHit2D hit = Physics2D.Linecast(position + (-5 - i) * step, position + direction * _dist + (-5 + i) * step, groundLayer);
            Debug.Log(position + (-5 + 10) * step);
            if (hit.collider != null)
            { //Als de collider de grond raakt = het true. Dus het tegenovergestelde van null
                collided = true;
            }
            Debug.DrawLine(position + (-_raysToShoot / 2 + i) * step, position + direction * _dist + (-_raysToShoot / 2 + i) * step, Color.red, .1f);
        }

        return collided;
	}
	public void Jump(){
		if (!IsGrounded()) {
			return;
		} else {
			rb.AddForce (Vector3.up * _playerJumpForce); // Player springt in verticale richting
            _jumpCooldownTime = Time.time + _jumpCooldown; 

        }
	}

}
