using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Enemy))]
public class EnemyAttacking : MonoBehaviour
{
    enum AttackType
    {
        BODY_DAMAGE_DEALER,
        BULLET_SHOOTER
    }

    [Header("Settings")]
    [SerializeField]
    private float _damage;
    [SerializeField]
    private float _attackRange;
    [SerializeField]
    private AttackType _attackType;

    [Header("If body damage dealer")]
    [SerializeField]
    private int _attackWidth;
    [SerializeField]
    private float _attackSpeed;

    [Header("If bullet shooter")]
    [SerializeField]
    [Range(0, 360)]
    private int _attackAngle;
    [SerializeField]
    private int _maxAngleRange;
    [SerializeField]
    private float _shootSpeed, _bullets, _bulletSpeed, _shootDelay;
    [SerializeField]
    private Transform _bulletPrefab;
    [SerializeField]
    private Transform _muzzle;
    [SerializeField]
    private bool _affectedByDistance = false;
    [SerializeField]
    private float _affectionDistance = 5f;

    [SerializeField]
    private AudioClip _shootSound;

    private float _cooldownTime;

    Animator anim;

    void Start ()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if(_cooldownTime < Time.time)
        {
            if (_attackType == AttackType.BULLET_SHOOTER) // Als schieter
            {
                for (int i = 0; i < _maxAngleRange; i++)
                {
                    RaycastHit2D hit = Physics2D.Raycast(_muzzle.position, Quaternion.Euler(0, 0, i - _attackAngle - _maxAngleRange / 2) * Vector3.up, _attackRange); // Raycast met bepaalde angle
                    if (hit.collider != null && hit.collider.tag == "Player")
                    {
                        Debug.DrawRay(_muzzle.position, Quaternion.Euler(0, 0, i - _attackAngle - _maxAngleRange / 2) * Vector3.up * _attackRange, Color.red, 1.0f);

                        direction = Quaternion.Euler(0, 0, i - _attackAngle - _maxAngleRange / 2) * Vector3.up;

                        Invoke("Shoot", _shootDelay);
                        anim.SetTrigger("Attack");

                        _cooldownTime = Time.time + _shootSpeed;
                        break;
                    }
                }

                if (_affectedByDistance)
                {
                    if (Vector3.Distance (GameObject.FindGameObjectWithTag("Player").transform.position, transform.position) <= _affectionDistance)
                    {
                        Invoke("Shoot", _shootDelay);
                        anim.SetTrigger("Attack");

                        direction = Quaternion.Euler(0, 0, Random.Range(0, _maxAngleRange) - _attackAngle - _maxAngleRange / 2) * Vector3.up;

                        _cooldownTime = Time.time + _shootSpeed;
                    }
                }
            }
        }
    }

    Vector3 direction;

    void Shoot()
    {
        if (_bullets > 0)
        {
            Transform bullet = Instantiate(_bulletPrefab) as Transform;
            bullet.position = _muzzle.position;
            bullet.up = direction.normalized; //Verander richting van kogel
            bullet.GetComponent<Bullet>().damage = _damage;
            bullet.GetComponent<Bullet>().speed = _bulletSpeed;
            _bullets--;
            SoundManager.current.PlaySound(_shootSound);
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (_attackType != AttackType.BODY_DAMAGE_DEALER) return; //Als geen body dealer negeer de rest van deze functie

        if (_cooldownTime < Time.time)
        {
            if (coll.transform.tag == "Player")
            {
                Health health = coll.transform.GetComponent<Health>();
                health.RemoveHealth(_damage);
                _cooldownTime = Time.time + _attackSpeed;
                Debug.Log("Attacked");
            }
        }
    }

    void OnDrawGizmosSelected() //Tekent in de editor
    {
        Gizmos.color = new Color(1, 1, 0, 0.75F);
        for (int i = 0; i < _maxAngleRange; i++)
        {
            Gizmos.DrawRay(_muzzle.position, Quaternion.Euler(0, 0, i - _attackAngle - _maxAngleRange / 2) * Vector3.up * _attackRange);
        }
    }
}
