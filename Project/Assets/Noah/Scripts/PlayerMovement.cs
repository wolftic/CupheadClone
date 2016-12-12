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
    BoxCollider2D col;
	private RaycastHit _hit;
	[SerializeField]
	private LayerMask groundLayer;
	[SerializeField]
	private float _dist;
    [SerializeField]
    private GameObject _runAnimationVFX, _fallAnimationVFX;
    private float _interval = 0.5f, _timer = 0f;
    Animator anim;
    bool _wasGrounded = false;

	// Use this for initialization
	void Start () {
		rb = gameObject.GetComponent<Rigidbody2D>();
        col = gameObject.GetComponent<BoxCollider2D>();
		anim = GetComponent<Animator> ();
	}
	void Update () {
		rb.velocity = Input.GetAxis ("Horizontal") * Vector3.right * _playerMovSpeed + rb.velocity.y * Vector3.up; // Player Movement over de horizontale as.
		//anim.SetFloat("movement", Mathf.Abs(Input.GetAxis("Horizontal")));
        if (_jumpCooldownTime < Time.time)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
        }

        if (Input.GetAxisRaw ("Horizontal") != 0)
        {
            transform.localScale = new Vector3(Input.GetAxisRaw("Horizontal"), 1, 1);
            if (_timer < Time.time && IsGrounded())
            {
                _timer = Time.time + _interval;
                Instantiate(_runAnimationVFX, transform.position, Quaternion.identity);
            }
        }

        anim.SetBool("inAir", !IsGrounded());
        anim.SetFloat("Speed", Mathf.Abs(Input.GetAxisRaw("Horizontal")));
	}

	bool IsGrounded(){
		Vector2 position = transform.position; // Vector2 positie van de speler
		Vector2 direction = Vector2.down; // De richting die de Raycast op gaat
        Vector2 step = new Vector2(col.size.x / _raysToShoot, 0f);
        bool collided = false;

        RaycastHit2D hit;

        for (int i = 0; i < Mathf.CeilToInt(_raysToShoot); i++)
        {
            hit = Physics2D.Linecast(position + (-_raysToShoot / 2 + i + .5f) * step, position + direction * _dist + (-_raysToShoot / 2 + i + .5f) * step, groundLayer);
            if (hit.collider != null)
            { //Als de collider de grond raakt = het true. Dus het tegenovergestelde van null
                collided = true;
                Debug.DrawLine(position + (-_raysToShoot / 2 + i + .5f) * step, position + direction * _dist + (-_raysToShoot / 2 + i + .5f) * step, Color.red);

                if (!_wasGrounded)
                {
                    Instantiate(_fallAnimationVFX, transform.position, Quaternion.identity);
                }

                _wasGrounded = true;
            }
        }

        _wasGrounded = collided;

        return collided;
	}
	public void Jump(){
		if (!IsGrounded()) {
			return;
		} else {
			rb.AddForce (Vector3.up * _playerJumpForce, ForceMode2D.Impulse); // Player springt in verticale richting
            _jumpCooldownTime = Time.time + _jumpCooldown; 
        }
	}

}
