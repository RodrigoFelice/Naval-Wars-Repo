using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//

public class CannonBall : MonoBehaviour
{
    [Header("Animator Settings")]
    [SerializeField] float previousSpeed, timeToDissapear, explosionTime; 
    float speed;

    [Header("Animator Settings")]
    [SerializeField] Animator anim;

    [Header("Sprite Cannon")]
    [SerializeField] Sprite cannonBallImage;


    void OnEnable() 
    {
        anim.enabled = false;
        GetComponent<SpriteRenderer>().sprite = cannonBallImage;
        StartCoroutine(DissapearCannonBall());
    } 
        
    
    void Update() => transform.position += transform.up * speed * Time.deltaTime;
    

    IEnumerator DissapearCannonBall()
    {
        speed = previousSpeed;
        yield return new WaitForSeconds(timeToDissapear);
        ReturnToPool(this.gameObject);   
    }


    public IEnumerator ExplosionAppears()
    {
        StopCoroutine(DissapearCannonBall());
        anim.enabled = true;
        speed = 0;
        yield return new WaitForSeconds(explosionTime);
        ReturnToPool(this.gameObject); 
    }

    void ReturnToPool(GameObject objectToReturn)
    { 
    
        ObjectPoolingManager.instance.ReturnObject("cannonBall", objectToReturn);
    }
}
