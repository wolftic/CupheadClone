using UnityEngine;
using System.Collections;

public class DestroyAfter : MonoBehaviour
{
    [SerializeField]
    private float _destroyAfter = 0.1f;

    void Start()
    {
        Destroy(gameObject, _destroyAfter);
    }
}
