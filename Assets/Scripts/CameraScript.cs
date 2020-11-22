using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    Transform _playerPosition = null;

    void Awake()
    {
        _playerPosition = GameObject.Find("Player").GetComponent<Transform>();
    }

    void Update()
    {
        this.transform.position = new Vector3(_playerPosition.position.x, _playerPosition.position.y, this.transform.position.z);
    }
}
