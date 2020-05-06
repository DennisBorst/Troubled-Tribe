using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    [SerializeField] private float shootSpeed;
    [SerializeField] private float arrowSpeed;
    [SerializeField] private float arrowDuration;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Arrow arrowPrefab;

    private float currenShootTime;
    private bool canShoot;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (canShoot)
            {
                canShoot = false;
                Arrow newArrow = Instantiate(arrowPrefab, firePoint.position, firePoint.rotation) as Arrow;
                newArrow.speed = arrowSpeed;
                newArrow.duration = arrowDuration;
            }
        }

        Reload();
    }

    private void Reload()
    {
        if (!canShoot)
        {
            currenShootTime = Timer(currenShootTime);

            if (currenShootTime <= 0)
            {
                canShoot = true;
                currenShootTime = shootSpeed;
            }
        }
    }

    private float Timer(float timer)
    {
        timer -= Time.deltaTime;
        return timer;
    }
}
