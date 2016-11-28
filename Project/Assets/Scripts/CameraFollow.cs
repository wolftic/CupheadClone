using UnityEngine; 
using System.Collections;

public class CameraFollow : MonoBehaviour {
    [SerializeField]
    private Transform _target;
    [SerializeField]
    private Vector3 _offset;
    [SerializeField]
    private float _movementSpeed;
    [SerializeField]
    private Transform _leftTop, _rightBot;
    private Rect _cameraBounds;

    private Camera cam;

	void Start ()
    {
        _cameraBounds.xMin = _leftTop.position.x;
        _cameraBounds.xMax = _rightBot.position.x;
        _cameraBounds.yMin = _rightBot.position.y;
        _cameraBounds.yMax = _leftTop.position.y;

        cam = Camera.main;
	}

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, CameraInBounds() + _offset, _movementSpeed * Time.deltaTime);
    }
    
    Vector3 CameraInBounds()
    {
        float camVertExtent = cam.orthographicSize;
        float camHorzExtent = cam.aspect * camVertExtent;

        float leftBound = _cameraBounds.min.x + camHorzExtent;
        float rightBound = _cameraBounds.max.x - camHorzExtent;
        float bottomBound = _cameraBounds.min.y + camVertExtent;
        float topBound = _cameraBounds.max.y - camVertExtent;

        float camX = Mathf.Clamp(_target.transform.position.x, leftBound, rightBound);
        float camY = Mathf.Clamp(_target.transform.position.y, bottomBound, topBound);

        return new Vector3(camX, camY, 0);
    }

    void OnDrawGizmos()
    {
        if (_cameraBounds != null)
        {
            Gizmos.DrawWireCube(_cameraBounds.center, _cameraBounds.size);
        }
    }
}
