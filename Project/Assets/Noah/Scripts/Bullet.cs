using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	void Start(){
		Destroy (gameObject, 3f);
	}

	public void BulletRight(){
		transform.Translate (transform.right * 0.2f);
	}


}
