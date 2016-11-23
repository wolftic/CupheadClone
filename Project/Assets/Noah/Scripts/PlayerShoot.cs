using UnityEngine;
using System.Collections;

public class PlayerShoot : MonoBehaviour {

	public GameObject bullet;
	[SerializeField]
	Transform bulletSpwn;
	[SerializeField]
	private float _bulletSpeed;



	// Update is called once per frame
	void FixedUpdate(){
		if (Input.GetKeyDown (KeyCode.L)) {
			GameObject bullInstance = Instantiate(bullet, bulletSpwn.position, bulletSpwn.rotation) as GameObject;
			bullInstance.transform.right = Vector3.down;
		}

	}
}

