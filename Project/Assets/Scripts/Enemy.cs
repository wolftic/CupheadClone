using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
    public string name {
        get
        {
            return _name;
        }
    }

    [SerializeField]
    private string _name;

    void Start () {
	
	}
}
