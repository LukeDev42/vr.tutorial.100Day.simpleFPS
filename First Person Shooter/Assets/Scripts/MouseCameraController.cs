using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCameraController : MonoBehaviour {

    public float Sensitivity = 5.0f;
    public float Smoothing = 2.0f;

    private Vector2 _mouseLook;
    private Vector2 _smoothV;

    private GameObject _player;

    private void Awake()
    {
        _player = transform.parent.gameObject;
    }

    private void Update()
    {
        Vector2 mouseDirection = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        mouseDirection.x *= Sensitivity * Smoothing;
        mouseDirection.y *= Sensitivity * Smoothing;

        _smoothV.x = Mathf.Lerp(_smoothV.x, mouseDirection.x, 1f / Smoothing);
        _smoothV.y = Mathf.Lerp(_smoothV.y, mouseDirection.y, 1f / Smoothing);

        _mouseLook += _smoothV;
        _mouseLook.y = Mathf.Clamp(_mouseLook.y, -90, 90);

        transform.localRotation = Quaternion.AngleAxis(-_mouseLook.y, Vector3.right);
        _player.transform.rotation = Quaternion.AngleAxis(_mouseLook.x, _player.transform.up);
    }
}
