using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	


	void Start(){
		Destroy (gameObject, 3f);
	}

	public void FixedUpdate(){
		transform.Translate (transform.right * 1.0f);
	}







}
