using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityStandardAssets.ImageEffects;

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
    private bool _isAlive = true, useAnimationCamera = false, restartScene = false;

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
                if(useAnimationCamera)
                {
                    Camera.main.GetComponent<Animator>().SetBool("Open", false);
                }
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
        
        if (transform.position.y < -10 && isAlive)
        {
            RemoveHealth(health);
        }

        if (!_isAlive && _canRespawn)
        {
            if(restartScene)
            {
                SceneManager.LoadScene(0);
            }
            health = _startHealth;
            Invoke("Respawn", 1.5f);
        }
    }

    void Respawn()
    {
        GetComponent<SpriteRenderer>().enabled = true;
        transform.position = _spawnPosition;
        if (useAnimationCamera)
        {
            Camera.main.GetComponent<Animator>().SetBool("Open", true);
        }
    }
}
