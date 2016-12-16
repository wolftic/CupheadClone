using UnityEngine;
using System.Collections;

public class PlayerShoot : MonoBehaviour {
    [SerializeField]
    private GameObject _bullet, _bulletShootVFX;
    [SerializeField]
    private float _bulletSpeed;
    [SerializeField]
    private Vector3 _offset;
    [SerializeField]
    private float _shootSpeed, _shootTimeDelay;
    private float _shootDelay;

    [SerializeField]
    private AudioClip _shootSound;

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
            StartCoroutine(Shoot(new Vector2(xAxis, Mathf.Abs(yAxis)), _shootTimeDelay));
        }

        anim.SetFloat("ShootDirX", Mathf.Abs(xAxis));
        anim.SetFloat("ShootDirY", Mathf.Abs(yAxis));
    }

    IEnumerator Shoot(Vector3 dir, float delayTime)
    {
        if (_shootDelay < Time.time)
        {
            anim.SetTrigger("Shoot");

            _shootDelay = _shootSpeed + Time.time;

            yield return new WaitForSeconds(delayTime);

            GameObject bullInstance = Instantiate(_bullet, transform.position + dir + _offset, Quaternion.identity) as GameObject;
            bullInstance.transform.up = dir.normalized;
            bullInstance.GetComponent<Bullet>().speed = _bulletSpeed;

            if (Input.GetAxisRaw("HorizontalShoot") != 0)
            {
                transform.localScale = new Vector3(Input.GetAxisRaw("HorizontalShoot"), 1, 1);
            }
            if(_bulletShootVFX)
            {
                Instantiate(_bulletShootVFX, transform.position + dir + _offset, Quaternion.identity);
                SoundManager.current.PlaySound(_shootSound);
            }
        }
    }
}

