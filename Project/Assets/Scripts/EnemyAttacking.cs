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
    private AttackType _attackType;

    [Header("If body damage dealer")]
    [SerializeField]
    private int _attackWidth;
    [SerializeField]
    private float _attackRange, _attackSpeed, _attackResolution;

    [Header("If bullet shooter")]
    [SerializeField]
    [Range(0, 360)]
    private int _attackAngle, _maxAngleRange;
    [SerializeField]
    private float _shootSpeed;

    private float _cooldownTime;

    void Update()
    {
        if(_cooldownTime < Time.time)
        {
            if (_attackType == AttackType.BULLET_SHOOTER)
            {
                for (int i = 0; i < _maxAngleRange; i++)
                {
                    RaycastHit2D hit = Physics2D.Raycast(transform.position, Quaternion.Euler(0, 0, i - _attackAngle - _maxAngleRange / 2) * Vector3.up, _attackRange);
                    if (hit.collider != null && hit.collider.tag == "Player")
                    {
                        Debug.Log("Attack");
                        _cooldownTime = Time.time + _shootSpeed;
                        break;
                    }
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (_cooldownTime < Time.time)
        {
            if (coll.transform.tag == "Player")
            {
                Debug.Log("Attack");
                _cooldownTime = Time.time + _shootSpeed;
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (_attackType == AttackType.BULLET_SHOOTER)
        { 
            for (int i = 0; i < _maxAngleRange; i++)
            {
                Gizmos.DrawLine(transform.position, Quaternion.Euler(0, 0, i - _attackAngle - _maxAngleRange / 2) * Vector3.up + transform.position);
            }
        }
    }
}
