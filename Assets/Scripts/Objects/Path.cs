using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    private float _castThickness;
    private Vector3 _rayStart;
    private Vector3 _rayDirection;
    private Transform _ballTransform;

    private void Start()
    {
        _castThickness = 0.5f;
        _rayStart = new Vector3(0, 0.3f, 0);
        _rayDirection = new Vector3(0, 0.3f, 15);
        _ballTransform = Ball.Instance.transform;
    }
    private void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.SphereCast(_rayStart, _castThickness, _rayDirection, out hit))
        {
            if (hit.collider.tag == "Destination")
                Ball.Instance.Move();
        }
    }
    public void UpdateCastThickness()
    {
        _castThickness = _ballTransform.localScale.x / 2;
    }
}
