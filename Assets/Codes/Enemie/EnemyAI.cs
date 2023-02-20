using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public enum ShipBehaviours{ Follower,Shooter}
    ShipBehaviours shipBehaviours;

    [Header("Movement Settings")]
    GameObject player;
    public NavMeshAgent agent; 

    [Header("Life Settings")]
    [SerializeField] Transform spawnBullets;
    GameObject cannonBall;
    bool canShoot;

    [Header("Follow/Rotation Settings")]
    float rotationAngle; 
    Vector3 playerDistance;

    [Header("Ship Follower Collider")]
    public BoxCollider2D damagerCollider;

    void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        canShoot = false;
        ChooseShipType();
        Invoke(nameof(delayShoot), 3f);
    }

    void ChooseShipType()
    {
        shipBehaviours = (ShipBehaviours)Random.Range(0, System.Enum.GetValues(typeof(ShipBehaviours)).Length);

        if(shipBehaviours == ShipBehaviours.Follower)
        {
            this.gameObject.tag = "Boat";
            damagerCollider.enabled = true;
            agent.stoppingDistance = 0;
            return;
        }
        damagerCollider.enabled = false;
        agent.stoppingDistance = 4;
    }

    void delayShoot() => canShoot = true;

    void Update()
    {
        agent.SetDestination(player.transform.position);

        playerDistance = player.transform.position - transform.position;

        rotationAngle = Mathf.Atan2(playerDistance.x, playerDistance.y) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, -rotationAngle);

        if(shipBehaviours == ShipBehaviours.Shooter && agent.remainingDistance <= agent.stoppingDistance) StartCoroutine(Shoot());
    }


    IEnumerator Shoot()
    {
        if(canShoot)
        {
            canShoot = false;
            cannonBall = ObjectPoolingManager.instance.GetObject("cannonBall");
            cannonBall.transform.position = spawnBullets.transform.position;
            cannonBall.transform.rotation = spawnBullets.transform.rotation;
            yield return new WaitForSeconds(2f);
            canShoot = true;
        }
    }
}
