using BaseTemplate.Behaviours;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoSingleton<PlayerController>
{
    [Header("References")]
    [SerializeField] Ship _playerShip;
    [SerializeField] Transform _beforeStartTranform, _afterStartTranform, _cannonTransform;


    //Data
    [SerializeField] float _nextFire = 0f, _fireRate = 2f, _smoothSpeed = 0.125f;

    //Cache
    Vector3 mousePos, worldPos;
    bool _isEnter = false;

    public void Init()
    {
        EnterLevelAnimation();
    }

    // Update is called once per frame
    void Update()
    {
        if (_isEnter == false) return;

        #region PlayerFollowMouse
        mousePos = Mouse.current.position.ReadValue();
        worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        worldPos.z = transform.position.z;
        #endregion

        if (Time.time > _nextFire)
        {
            _nextFire = Time.time + _fireRate;
            Shoot();
        }
    }

    void FixedUpdate()
    {
        Vector3 desiredPosition = new Vector3(worldPos.x, worldPos.y, transform.position.z);
        Vector3 smoothPosition = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed);
        transform.position = smoothPosition;
    }


    void Shoot()
    {
        if (_playerShip._numberOfMissile == 1)
        {
            InitBullet();

            return;
        }

        float startRotation = _playerShip._spreadOfMissile / 2;
        float angleIncrease = _playerShip._spreadOfMissile  / (_playerShip._numberOfMissile - 1);

        for (int i = 0; i < _playerShip._numberOfMissile; i++)
        {
            float tempRotation = startRotation - angleIncrease * i;

            GameObject bullet = InitBullet();

            bullet.transform.rotation = Quaternion.Euler(0, 0, tempRotation);
        }
    }

    private GameObject InitBullet()
    {
        GameObject bullet = PoolManager.Instance.gameobjectPoolDictionary["Missile"].Get();

        bullet.transform.localPosition = _cannonTransform.position;

        bullet.GetComponent<Bullet>().Init();

        return bullet;
    }

    void EnterLevelAnimation()
    {
        transform.DOMoveY(_beforeStartTranform.position.y, 0f);

        transform.DOMoveY(_afterStartTranform.position.y, 1f).SetEase(Ease.OutSine).OnComplete(() => _isEnter = true);
    }
}
