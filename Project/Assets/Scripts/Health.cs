using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {
    private Vector3 _spawnPosition;
    [SerializeField]
    private GameObject _hitvfx, _deathvfx;
    [SerializeField]
    private bool _canRespawn;

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
        Instantiate(_hitvfx, transform.position, Quaternion.identity);
        if (health <= 0)
        {
            if(_canRespawn)
            {
                GetComponent<SpriteRenderer>().enabled = false;
            } else {
                gameObject.SetActive(false);
            }
            Instantiate(_deathvfx, transform.position, Quaternion.identity);
        }
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

        if (!_isAlive && _canRespawn)
        {
            transform.position = _spawnPosition;
            GetComponent<SpriteRenderer>().enabled = true;
            health = _startHealth;
        }
    }
}
