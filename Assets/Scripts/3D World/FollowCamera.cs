using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] GameObject _player;
    [SerializeField] private Vector3 _cameraOffset = new Vector3 (0, 0, 0);

    void LateUpdate()
    {
        transform.position = _player.transform.position + _cameraOffset;
    }
}



