using UnityEngine;
using System.Collections;

public class PlayerShoot : MonoBehaviour {
    [SerializeField]
    private GameObject _bullet;
    [SerializeField]
    private float _bulletSpeed;
    [SerializeField]
    private Vector3 _offset;

    Animator anim;

    void Start ()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        float xAxis = Input.GetAxisRaw("HorizontalShoot");
        float yAxis = Input.GetAxisRaw("VerticalShoot");

        if ((xAxis != 0 || yAxis != 0))
        {
            Shoot(new Vector2(xAxis, Mathf.Abs(yAxis)));
        }

        anim.SetFloat("ShootDirX", Mathf.Abs(xAxis));
        anim.SetFloat("ShootDirY", Mathf.Abs(yAxis));
    }

    void Shoot(Vector3 dir)
    {
        GameObject bullInstance = Instantiate(_bullet, transform.position + dir + _offset, Quaternion.identity) as GameObject;
        bullInstance.transform.up = dir.normalized;
        bullInstance.GetComponent<Bullet>().speed = _bulletSpeed;
        anim.SetTrigger("Shoot");
        if (Input.GetAxisRaw("HorizontalShoot") != 0)
        {
            transform.localScale = new Vector3(Input.GetAxisRaw("HorizontalShoot"), 1, 1);
        }
    }
}

