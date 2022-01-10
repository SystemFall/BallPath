using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsController : MonoBehaviour
{
    public Bullet _bulletPrefab;

    public GameController _gameController;
    public Path _pathInstance;

    private Transform _ballTransform;

    private Ball _ballInstance;
    private Bullet _bulletInstance;

    private void Start()
    {
        _ballInstance = Ball.Instance;
        _ballTransform = Ball.Instance.transform;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CreateBullet();
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (!_ballInstance._isBallMoving)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                _bulletInstance.ShootBullet(ray);
                _pathInstance.UpdateCastThickness();
            }
        }
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if(!_ballInstance._isBallMoving)
            {
                Vector3 growth = new Vector3(Time.deltaTime, Time.deltaTime, Time.deltaTime) * _gameController._growthRate;

                _bulletInstance.IncreaseOverTimeBullet(growth);
                _ballInstance.DecreaseOverTimeBall(growth);
                _gameController.SynchronizePathWidth(_ballInstance.transform);

                if (_ballTransform.localScale.x < Ball.Instance._ballReserve)
                    _gameController.GameOver(0);
            }
        }
    }
    public void CreateBullet()
    {
        if (!_ballInstance._isBallMoving)
        {
            _bulletInstance = Instantiate(_bulletPrefab).GetComponent<Bullet>();
            Ball.Instance.InstantDecreaseBall();
        }
    }
}
