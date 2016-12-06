using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {
    private Vector3 _spawnPosition;
   

    public float health {
        get
        {
            return _health;
        }
        set
        {
            _health = value;

            if (_health <= 0)
            {
                _isAlive = false;
            } else
            {
                _isAlive = true;
            }
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
        
        _spawnPosition = transform.position;
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

    void Update()
    {
        
        if (transform.position.y < -10)
        {
            health -= health;
        }

        if (!_isAlive)
        {
            transform.position = _spawnPosition;
            health = _startHealth;
        }
    }
}
