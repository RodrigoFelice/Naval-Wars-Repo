using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootSystem : MonoBehaviour
{
    [Header("Cannon Ball Refernce")]
    GameObject cannonBall;

    [Header("Shoot Settings")]
    [SerializeField] float primaryShootTime, secondaryShootTime;
    bool frontCannonShoot, lateralCannonShoot;

    [Header("Cannon Positions")]
    [SerializeField] Transform[] cannonPositions;


    void Update()
    {
        if(Input.GetMouseButtonDown(0)) StartCoroutine(FrontFire());
        
        if(Input.GetMouseButtonDown(1)) StartCoroutine(LateralFire());
        
    }

    IEnumerator FrontFire()
    {
        if(!frontCannonShoot)
        {
            frontCannonShoot = true;
            cannonBall = ObjectPoolingManager.instance.GetObject("cannonBall");
            cannonBall.transform.position = cannonPositions[0].transform.position;
            cannonBall.transform.rotation = cannonPositions[0].transform.rotation;
            yield return new WaitForSeconds(primaryShootTime);
            frontCannonShoot = false;
        }
    }

    IEnumerator LateralFire()
    {
        if(!lateralCannonShoot)
        {
            lateralCannonShoot = true;

            for(int i = 1; i < cannonPositions.Length; i++)
            {
                cannonBall = ObjectPoolingManager.instance.GetObject("cannonBall");
                cannonBall.transform.position = cannonPositions[i].transform.position;
                cannonBall.transform.rotation = cannonPositions[i].transform.rotation;
            }
            yield return new WaitForSeconds(secondaryShootTime);
            lateralCannonShoot = false;
        }
    }

}
