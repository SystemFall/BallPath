using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField]
    private float _explosionSpeed;
    private float _explosionMagnitude;

    private Transform _explosionTransform;

    private void Start()
    {
        _explosionSpeed = 6;
    }
    private void Awake()
    {
        _explosionTransform = this.transform;
    }
    private void FixedUpdate()
    {
        _explosionTransform.localScale += new Vector3(0.02f, 0, 0.02f) * _explosionSpeed;

        if (_explosionTransform.localScale.x >= _explosionMagnitude)
            Destroy(this.gameObject);
    }
    public void SetMagnitude(float value)
    {
        _explosionMagnitude = value;
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Obstacle")
        {
            collider.GetComponent<Cactus>().CreateDestroyEffect();
            Destroy(collider.gameObject);
        }
    }
}
