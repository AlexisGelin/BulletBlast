using BaseTemplate.Behaviours;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoSingleton<PlayerController>
{
    //Data
    [SerializeField] float _nextFire = 0f, _fireRate = 2f;

    //Cache
    Vector3 mousePos, worldPos;

    public void Init()
    {

    }

    // Update is called once per frame
    void Update()
    {
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
        transform.position = worldPos;
    }


    void Shoot()
    {
        Debug.Log("Shoot");
    }
}
