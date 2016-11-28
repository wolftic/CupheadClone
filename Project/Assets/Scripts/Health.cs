using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Enemy))]
public class Health : MonoBehaviour {
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

    void Start()
    {
        _health = _startHealth;
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
