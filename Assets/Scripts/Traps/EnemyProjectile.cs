using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : EnemyDamage
{
    [SerializeField] private float speed;
    [SerializeField] private float resetTime;
    private float lifeTime;
    private Animator anim;
    
    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float movementSpeed = speed * Time.deltaTime;
        transform.Translate(movementSpeed, 0, 0);

        lifeTime += Time.deltaTime;
        if(lifeTime > resetTime)
        {
            gameObject.SetActive(false);
        } 
    }

    public void ActivateProjectile()
    {
        lifeTime = 0;
        gameObject.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision); // access the method of parent class

        if(anim != null)
        {
            anim.SetTrigger("explode"); //play explode animation with fireball
        }
        else
        {
            gameObject.SetActive(false); //deactivate gameobject with arrow
        }
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
