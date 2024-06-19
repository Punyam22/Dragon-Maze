using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private AudioClip CheckpointSound;
    private Transform currentCheckpoint;
    private Health playerHealth;

    private UIManager uiManager;

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = GetComponent<Health>();
        uiManager = FindObjectOfType<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckRespawn()
    {
        //if checkpoint is not available
        if(currentCheckpoint == null)
        {
            //show game over scene
            uiManager.GameOver();
            
            return; //don't execute rest of the code.
        }

        //if check point is available
        transform.position = currentCheckpoint.position;
        playerHealth.Respawn();

        Camera.main.GetComponent<CameraController>().MoveToNewRoom(currentCheckpoint.parent);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Checkpoint")
        {
            currentCheckpoint = collision.transform;
            SoundManager.instance.PlaySound(CheckpointSound);
            collision.GetComponent<Collider2D>().enabled = false;
            collision.GetComponent<Animator>().SetTrigger("appear");
        }
    }
}
