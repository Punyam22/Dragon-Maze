using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTrap : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] arrow;

    [Header ("Sound Manager")]
    [SerializeField] private AudioClip arrowSound; 
    private float cooldownTimer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cooldownTimer += Time.deltaTime;
        if(cooldownTimer >= attackCooldown)
        {
            Attack();
        }
    }

    private void Attack()
    {
        cooldownTimer = 0;

        arrow[Findarrow()].transform.position = firePoint.position;
        arrow[Findarrow()].GetComponent<EnemyProjectile>().ActivateProjectile();
        SoundManager.instance.PlaySound(arrowSound);
    }

    private int Findarrow()
    {
        for(int i = 0; i < arrow.Length; i++)
        {
            if(!arrow[i].activeInHierarchy)
            {
                return i;
            }
        }
        return 0;
    }
}
