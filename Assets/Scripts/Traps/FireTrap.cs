using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrap : MonoBehaviour
{
    [SerializeField] private float damage;

    [Header ("Fire Trap Timers")]
    [SerializeField] private float activationDelay;
    [SerializeField] private float activeTime;

    [Header ("Audio Manager")]
    [SerializeField] private AudioClip fireSound;

    private Animator anim;
    private SpriteRenderer spriteRend;

    private bool triggered; //trap is triggered
    private bool active; //trap is active and can hurt player
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!triggered)
        {
            StartCoroutine(ActivateFireTrap());
        }
        if(active)
        {
            collision.GetComponent<Health>().TakeDamage(damage);
        }
    }

    private IEnumerator ActivateFireTrap()
    {
        triggered = true;
        spriteRend.color = new Color(255, 0, 0, 255); //when the trap is about to be active

        //activates the animation of the trap and turn on taking damage
        yield return new WaitForSeconds(activationDelay);
        SoundManager.instance.PlaySound(fireSound);
        anim.SetBool("activate", true);
        spriteRend.color = Color.white; //when the trap becomes active
        active = true;

        //wait for activeTime seconds and turn off the animation and damage
        yield return new WaitForSeconds(activeTime);
        anim.SetBool("activate", false);
        active = false;
        triggered = false;
    }
}
