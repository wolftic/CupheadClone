using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour {
    public float health {
        get
        {
            return _health;
        }
        set
        {
            if (_health <= 0)
            {
                _isAlive = false;
            }
            _health = value;
        }
    }
    public bool isAlive
    {
        get
        {
            return _isAlive;
        }
    }

    [SerializeField]
    private float _health;
    [SerializeField]
    private bool _isAlive = true;

    [SerializeField]
    private float _startHealth = 10f;
    [SerializeField]
    private bool _deathOnJump = false;

    void Start()
    {
        _health = _startHealth;
    }

    void Update()
    {
        if (_deathOnJump)
        {
            for (int i = 0; i < 10; i++)
            {
                Vector3 xOffset = new Vector3((i - 5 + 0.5f) / 10f, 0.01f);
                RaycastHit2D hit = Physics2D.Raycast(transform.position + xOffset + Vector3.up / 2, Vector3.up, .1f);
                Debug.DrawRay(transform.position + xOffset + Vector3.up / 2, Vector3.up * .1f);
                if (hit.collider != null)
                {
                    if (hit.transform.tag == "Player")
                    {
                        Debug.Log(hit.transform.name);

                        RemoveHealth(health);
                        break;
                    }
                }
            }
        }
    }

    public void RemoveHealth(float dmg)
    {
        health -= dmg;
    }

    public void AddHealth(float heal)
    {
        health += heal;
    }
}
