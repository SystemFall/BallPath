using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    const float _minExplosionMagnitude = 0.04f;
    const float _magnitudeNormalizeCoefficient = 7.5f;
    const float _minBulletScale = 0.2f;
    const float _explosionReductionFactor = 0.6f;
    const float _magnitudeReductionFactor = 10;

    [SerializeField]
    private float _speed;
    
    public GameObject _explosionPrefab;

    private Explosion _explosionInstance;

    private float _explosionMagnitude;
    
    private bool _isBulletMoving;
    private Vector3 _destination;
    private Ray _ray;

    private Transform _bulletTransform;
   
    private void Start()
    {
        _isBulletMoving = false;
        _speed = 0.065f;
    }
    private void Awake()
    {
        _bulletTransform = this.transform;
    }
    private void FixedUpdate()
    {
        if (_isBulletMoving)
            _bulletTransform.Translate(new Vector3(_destination.x, 0, _destination.z).normalized * _speed);
    }
    public void ShootBullet(Ray ray)
    {
        _isBulletMoving = true;
        _ray = ray;

        RaycastHit hit;
        if (Physics.Raycast(_ray, out hit, Mathf.Infinity) && _isBulletMoving)
            _destination = new Vector3(hit.point.x, 0, hit.point.z - this.transform.position.z);

        _explosionMagnitude = _bulletTransform.localScale.x - _minBulletScale;
    }
    public void IncreaseOverTimeBullet(Vector3 value)
    {
        _bulletTransform.localScale += value;
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Obstacle" || collider.tag == "Destination" && _isBulletMoving)
        {
            _isBulletMoving = false;
            Explode();
        }
    }
    private void Explode()
    {
        if (_explosionMagnitude > _minExplosionMagnitude)
            _explosionMagnitude = (_explosionMagnitude * _magnitudeNormalizeCoefficient / (_minBulletScale + _explosionMagnitude / _magnitudeReductionFactor)) * _explosionReductionFactor;
        else
            _explosionMagnitude = 0;
        CreateExplosion();
        Destroy(this.gameObject);
    }
    private void CreateExplosion()
    {
        _explosionInstance = Instantiate(_explosionPrefab, _bulletTransform.position, Quaternion.identity).GetComponent<Explosion>();
        _explosionInstance.SetMagnitude(_explosionMagnitude);
    }
}