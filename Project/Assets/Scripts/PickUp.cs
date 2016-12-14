using UnityEngine;
using System.Collections;

public class PickUp : MonoBehaviour {
    [SerializeField]
    private GameObject _pickUpFX;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            Instantiate(_pickUpFX, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
        }
    }
}
