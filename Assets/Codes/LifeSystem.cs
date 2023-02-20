using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//

public class LifeSystem : MonoBehaviour
{
    [Header("Life Settings")]
    public float maxHealth;
    float currentHealth;
    bool hasDied;

    [Header("LifeBar Settings")]
    [SerializeField] GameObject lifeBar;

    [Header("Explosion Settings")]
    [SerializeField] GameObject shipExplosion;

    [Header("Is Enemie or Player")]
    public bool isTheEnemie;


    void OnEnable() 
    {
        hasDied = false;
        currentHealth = maxHealth; 
        lifeBar.SetActive(true);
        lifeBar.GetComponent<LifeBar>().UpdateLifeBar(currentHealth, maxHealth);
        if(isTheEnemie) transform.parent.GetComponent<EnemyAI>().enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Cannon") && !hasDied)
        {
            LoseHealth(2);
            other.GetComponent<CannonBall>().StartCoroutine(other.GetComponent<CannonBall>().ExplosionAppears());
        } 

        if(other.gameObject.CompareTag("Boat") && !hasDied && !isTheEnemie) 
        {
            LoseHealth(3);
            other.GetComponentInChildren<LifeSystem>().LoseHealth((int)other.GetComponentInChildren<LifeSystem>().maxHealth);
        }
    }

    public void LoseHealth(int damage)
    {
        currentHealth -= damage;
        lifeBar.GetComponent<LifeBar>().UpdateLifeBar(currentHealth, maxHealth);

        if(currentHealth <= 0) 
        {
            currentHealth = 0;
            StartCoroutine(Die());

            if(isTheEnemie) 
            {
                transform.parent.GetComponent<EnemyAI>().agent.stoppingDistance = 4;
                transform.parent.GetComponent<EnemyAI>().enabled = false;
                transform.parent.gameObject.tag = "Untagged";
            }
        }
        else GetComponent<ShipApparence>().CalculateDamage(currentHealth);
    }

    
    IEnumerator Die()
    {
        hasDied = true;
        yield return new WaitForSeconds(0.2f);
        shipExplosion.SetActive(true);
        lifeBar.SetActive(false);
        yield return new WaitForSeconds(0.7f);
        shipExplosion.SetActive(false);
        GetComponent<ShipApparence>().ChangeShipSkin(3);
        yield return new WaitForSeconds(1f);

        if(isTheEnemie)
        {
            GameManager.instance.changeScore(10);
            ObjectPoolingManager.instance.ReturnObject("Enemie",this.transform.parent.gameObject);
        }
       
        else GameManager.instance.ChangeGameState("Ending");
    }
}
