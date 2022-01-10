using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    #region Singleton

    private static Ball _instance;

    public static Ball Instance => _instance;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    #endregion

    const float _fenceLocationZ = 15.3f;
    const float _fenceTriggerLocationZ = 10;

    [SerializeField]
    private float _ballAcceleration;
    [SerializeField]
    private float _ballDecreaseModifier;

    public GameController _gameController;
    public Destination _destinationObject;

    public float _ballReserve;
    public bool _isBallMoving;

    private Transform _ballTransform;
    private Rigidbody _rigidBody;

    private Vector3 _destination;

    private void Start()
    {
        _ballAcceleration = 15;
        _ballReserve = 0.2f;
        _isBallMoving = false;
        _ballDecreaseModifier = 0.06f;
        _rigidBody = this.GetComponent<Rigidbody>();
        _ballTransform = this.transform;
        _destination = GameObject.FindGameObjectWithTag("Destination").transform.position;
    }
    public void Move()
    {
        if (!_isBallMoving && !_gameController._isGameOver)
        {
            this.GetComponent<Rigidbody>().isKinematic = false;
            _rigidBody.AddForce(_destination.normalized * _ballAcceleration);
        }
        _isBallMoving = true;
    }
    private void Update()
    {
        if (_ballTransform.position.z >= _fenceLocationZ)
            _gameController.GamePassed();
        if(_ballTransform.position.z >= _fenceTriggerLocationZ)
            _destinationObject.OpenFence();
    }
    public void DecreaseOverTimeBall(Vector3 value)
    {
        _ballTransform.localScale -= value;
    }
    public void InstantDecreaseBall()
    {
        Vector3 newBallScale = _ballTransform.localScale;
        newBallScale = new Vector3(newBallScale.x - _ballDecreaseModifier, newBallScale.y - _ballDecreaseModifier, newBallScale.z - _ballDecreaseModifier);
        _ballTransform.localScale = newBallScale;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacle")
            _gameController.GameOver(1);
    }
}
