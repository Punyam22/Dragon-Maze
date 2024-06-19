using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeHead : EnemyDamage
{
    [SerializeField] private float speed;
    [SerializeField] private float range;
    private Vector3 destination;
    [SerializeField] private float checkDelay;
    [SerializeField] private LayerMask playerLayer;

    [Header ("Audio Manager")]
    [SerializeField] private AudioClip impactSound;

    private float checkTimer;
    private bool attacking;
    private Vector3[] directions = new Vector3[4];

    private void OnEnable()
    {
        Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if(attacking)
            transform.Translate(speed * Time.deltaTime * destination);
        else
        {
            checkTimer += Time.deltaTime;
            if(checkTimer > checkDelay)
            {
                CheckForPlayer();
                for(int i = 0; i < directions.Length; i++)
                {
                    Debug.DrawRay(transform.position, directions[i], Color.red);
                    RaycastHit2D hit = Physics2D.Raycast(transform.position, directions[i], range, playerLayer); 

                    if(hit.collider != null && !attacking)
                    {
                        attacking = true;
                        destination = directions[i];
                        checkTimer = 0;
                    }
                }
            }
        }
    }

    private void CheckForPlayer()
    {
        CalculateDirections();
    }

    private void CalculateDirections()
    {
        directions[0] = transform.right * range; //right direction
        directions[1] = -transform.right * range; //left direction
        directions[2] = transform.up * range; //up direction
        directions[3] = -transform.up * range; //down direction
    }

    private void Stop()
    {
        destination = transform.position;
        attacking = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SoundManager.instance.PlaySound(impactSound);
        base.OnTriggerEnter2D(collision);
        Stop();
    }
}
