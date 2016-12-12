using UnityEngine;
using System.Collections;

public class Explode : MonoBehaviour {
    [SerializeField]
    private float radius;
    [SerializeField]
    private Vector3 blastOffset;

    void Start()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position + blastOffset, radius);
        int i = 0;
        while (i < hitColliders.Length)
        {
            if (hitColliders[i].tag == "Player")
            {
                Health hp = hitColliders[i].GetComponent<Health>();
                hp.RemoveHealth(1f);
            }
            i++;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position + blastOffset, radius);
    }
}
