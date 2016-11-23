using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour {
    [SerializeField]
    private float _speed;

    [SerializeField]
    private GameObject[] _waypoints;
    private int _currentWaypoint = 0;

    private float _percentage = 0;
    private float _distanceToWaypoint;
    private float _distanceMoved;

    private GameObject _currentWaypointGameobject;
    private GameObject _nextWaypointGameobject;

    void Start()
    {
        NextWaypoint();
    }

    void Update()
    {
        if (_percentage >= 1)
        {
            NextWaypoint();
        }

        _distanceMoved += Time.deltaTime * _speed;
        _percentage = (_distanceMoved / _distanceToWaypoint);

        transform.position = Vector3.Lerp(_currentWaypointGameobject.transform.position, _nextWaypointGameobject.transform.position, Mathf.SmoothStep(0, 1, _percentage));
    }

    void NextWaypoint()
    {
        _percentage = 0;
        _distanceMoved = 0;
        _currentWaypoint++;
        if (_currentWaypoint > _waypoints.Length-1)
        {
            _currentWaypoint = 0;
        }

        _nextWaypointGameobject = _waypoints[_currentWaypoint];
        _currentWaypointGameobject = _waypoints[(_currentWaypoint == 0) ? _waypoints.Length -1 : _currentWaypoint - 1];

        _distanceToWaypoint = Vector3.Distance(_nextWaypointGameobject.transform.position, _currentWaypointGameobject .transform.position);
    }
}
