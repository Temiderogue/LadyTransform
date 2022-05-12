using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private Vector3 _offset;
    private Vector3 _newPosition;

    private void LateUpdate()
    {
        _newPosition = _player.position + _offset;
        transform.position = Vector3.Lerp(transform.position, _newPosition, 1f);
    }
}
