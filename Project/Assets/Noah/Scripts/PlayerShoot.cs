﻿using UnityEngine;
using System.Collections;

public class PlayerShoot : MonoBehaviour {
    [SerializeField]
    private GameObject _bullet;
    [SerializeField]
    private float _bulletSpeed;

    void Update()
    {
        float xAxis = Input.GetAxisRaw("HorizontalShoot");
        float yAxis = Input.GetAxisRaw("VerticalShoot");

        if (Input.GetButtonDown("Fire1") && (xAxis != 0 || yAxis != 0))
        {
            Shoot(new Vector2(xAxis, Mathf.Abs(yAxis)));
        }
    }

    void Shoot(Vector3 dir)
    {
        GameObject bullInstance = Instantiate(_bullet, transform.position + dir, Quaternion.identity) as GameObject;
        bullInstance.transform.up = dir.normalized;
        bullInstance.GetComponent<Bullet>().speed = _bulletSpeed;
    }
}

