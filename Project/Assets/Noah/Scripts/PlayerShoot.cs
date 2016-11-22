using UnityEngine;
using System.Collections;

public class PlayerShoot : MonoBehaviour {

	public GameObject bullet;
	[SerializeField]
	Transform bulletSpwn;
	[SerializeField]
	private float _bulletSpeed;



	void Start () {
		//bullet = gameObject.GetComponent<Rigidbody2D> ();

	}
	
	// Update is called once per frame
	void FixedUpdate(){
		if (Input.GetKeyDown (KeyCode.L)) {
			GameObject bullInstance = Instantiate (bullet, bulletSpwn.position, bulletSpwn.rotation) as GameObject;
		}




	}
}

