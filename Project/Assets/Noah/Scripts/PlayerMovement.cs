using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	[SerializeField]
	private float _playerMovSpeed = 10.0f;
	[SerializeField]
	private float _playerJumpForce = 5.0f;
	[SerializeField]
	//private bool _isCrouching = false;
	Rigidbody2D rb;
	private RaycastHit _hit;
	[SerializeField]
	private LayerMask groundLayer;
	[SerializeField]
	private float _dist;


	// Use this for initialization
	void Start () {
		rb = gameObject.GetComponent<Rigidbody2D>();
	}
	void FixedUpdate () {
		  
		rb.velocity = Input.GetAxis ("Horizontal") * Vector3.right * _playerMovSpeed + rb.velocity.y * Vector3.up; // Player Movement over de horizontale as.
		if(Input.GetKeyDown(KeyCode.Space)){ 
			Jump();
		}
	}
	bool IsGrounded(){
		Vector2 position = transform.position; // Vector2 positie van de speler
		Vector2 direction = Vector2.down; // De richting die de Raycast op gaat
		RaycastHit2D hit = Physics2D.Raycast (position, direction, _dist, groundLayer); //Nieuw Raycast hit die de positie,richting,afstand en de layer pakt 

		if (hit.collider != null) { //Als de collider de grond raakt = het true. Dus het tegenovergestelde van null
			return true; 
		} else { //Anders return die false en is de player in de lucht
			return false; 
		}
	}
	void Jump(){
		if (!IsGrounded()) {
			return;
		} else {
			rb.AddForce (Vector3.up * _playerJumpForce); // Player springt in verticale richting
		}
	}

}
