using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipApparence : MonoBehaviour
{
    [Header("Ship Skins Settings")]
    [SerializeField] SpriteRenderer thisShipSkin, thisShipIcon;
    [SerializeField] ShipSkins[] allPossibleShipSkins;
    int choosenShipSkin;

    [Header("Ship Damage Settings")]
    public float mediumDamaged, bigDamaged;

     void OnEnable() 
    {
        float maxHealth = GetComponent<LifeSystem>().maxHealth;
        mediumDamaged = maxHealth / 1.5f;
        bigDamaged = maxHealth / 2.5f;
        thisShipSkin.GetComponentInParent<SpriteRenderer>();
        choosenShipSkin = Random.Range(0,allPossibleShipSkins.Length);
        ChangeShipSkin(0);
    }

    public void CalculateDamage(float currentHealth)
    {
        if(currentHealth <= mediumDamaged)
        {
            ChangeShipSkin(1);
        }
        
        if(currentHealth <= bigDamaged)
        {
            ChangeShipSkin(2);
        }
    }

    public void ChangeShipSkin(int shipState)
    {
        thisShipSkin.sprite = allPossibleShipSkins[choosenShipSkin].allShipSkins[shipState];
        thisShipIcon.sprite = allPossibleShipSkins[choosenShipSkin].allShipSkins[shipState];
    }
}
