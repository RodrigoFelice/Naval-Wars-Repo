using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeBar : MonoBehaviour
{
    [Header("Life Bar Settings")]
    [SerializeField] Transform followPlayer,pivot; 

    [Header("Camera Settings")]
    Camera mainCamera; 

    void OnEnable() => this.gameObject.SetActive(true);

    void Start() => mainCamera = Camera.main; 

    void LateUpdate()
    {
       transform.position = followPlayer.position + new Vector3(0, 1, 0);
       transform.rotation = Quaternion.LookRotation(transform.position - mainCamera.transform.position, Vector3.up);
    }

    public void UpdateLifeBar(float currentHealth, float maxHealth) 
    {
        pivot.transform.localScale = new Vector3(currentHealth/maxHealth,transform.localScale.y,transform.localScale.z);

        if(pivot.transform.localScale.x <= 0)  pivot.transform.localScale = new Vector3(0f ,transform.localScale.y,transform.localScale.z);
    } 
}
