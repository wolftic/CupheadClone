using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
    public float damage = 1f;
    public float speed = 1f;
    [SerializeField]
    private string targetTag;

    void Start(){
		Destroy (gameObject, 3f);
	}

	void Update(){
		transform.Translate (Vector3.up * speed * Time.deltaTime);
	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.transform.tag == targetTag)
        {
            Health health = coll.transform.GetComponent<Health>();
            health.RemoveHealth(damage);
            Debug.Log(damage);
        }

        Destroy(gameObject);
    }
}
