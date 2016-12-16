using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
    public float damage = 1f;
    public float speed = 1f;
    [SerializeField]
    private string targetTag;
    [SerializeField]
    private bool explodeWhenLand = false;
    [SerializeField]
    private GameObject explosionFX, _hitvfx;
    [SerializeField]
    private AudioClip _hitSound;

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
            if(coll.transform.GetComponent<Health>() != null)
            {
                Health health = coll.transform.GetComponent<Health>();
                health.RemoveHealth(damage);
                if(_hitSound)
                {
                    SoundManager.current.PlaySound(_hitSound);
                }
                if(_hitvfx)
                {
                    Instantiate(_hitvfx, transform.position, Quaternion.identity);
                }
            }
        }

        if (explodeWhenLand)
        {
            Instantiate(explosionFX, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}
