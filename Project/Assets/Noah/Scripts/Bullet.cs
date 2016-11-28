using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
    public float damage = 1f;

    void Start(){
		Destroy (gameObject, 3f);
	}

	void Update(){
		transform.Translate (Vector3.up * 1.0f * Time.deltaTime);
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.transform.tag == "Player")
        {
            Health health = coll.transform.GetComponent<Health>();
            health.RemoveHealth(damage);
        }

        Destroy(gameObject);
    }
}
