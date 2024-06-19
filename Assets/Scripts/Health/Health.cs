using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header ("Health")]
    [SerializeField] private float startingHealth;
    public float currentHealth {get; private set;}
    private Animator anim;
    private bool dead;

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfLashes;
    private SpriteRenderer spriteRend;

    [Header("Audio Manager")]
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip hurtSound;

    [Header("Components")]
    [SerializeField] private Behaviour[] components;
    // Start is called before the first frame update
    void Start()
    {
       currentHealth = startingHealth;
       anim = GetComponent<Animator>(); 
       spriteRend = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if(currentHealth > 0){
            anim.SetTrigger("hurt");
            StartCoroutine(Invunrability());
            SoundManager.instance.PlaySound(hurtSound);
        }
        else{
            if(!dead){
                SoundManager.instance.PlaySound(deathSound);

                foreach (Behaviour component in components)
                    component.enabled = false;

                anim.SetBool("grounded", true);    
                anim.SetTrigger("die");
                dead = true;
            }
        }
    }

    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }

    public void Respawn()
    {
        dead = false;
        AddHealth(startingHealth);
        anim.ResetTrigger("die");
        anim.Play("Idle");

        foreach (Behaviour component in components)
            component.enabled = true;
    }

    private IEnumerator Invunrability()
    {
        Physics2D.IgnoreLayerCollision(8, 9, true);
        for(int i = 0; i <= numberOfLashes; i++){
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfLashes * 2));
            spriteRend.color = new Color(1, 1, 1, 1);
            yield return new WaitForSeconds(iFramesDuration / (numberOfLashes * 2));
        } 
        Physics2D.IgnoreLayerCollision(8, 9, false);
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
