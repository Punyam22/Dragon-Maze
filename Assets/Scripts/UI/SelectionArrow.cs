using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionArrow : MonoBehaviour
{
    [SerializeField] private RectTransform[] options;
    [SerializeField] private AudioClip arrowSound; //when arrow is moved up/down
    [SerializeField] private AudioClip interactSound; //when an option is selected
    private RectTransform rect;
    private int currentPosition;

    // Start is called before the first frame update
    void Start()
    {
        rect = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        //change position of arrow
        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            changePosition(-1);
        }
        if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            changePosition(1);
        }

        //interact with options
        if(Input.GetKeyDown(KeyCode.Return))
        {
            Interact();
        }

    }

    private void changePosition(int _change)
    {
        currentPosition += _change;

        if(_change != 0)
        {
            SoundManager.instance.PlaySound(arrowSound);
        }

        if(currentPosition < 0)
        {
            currentPosition = options.Length - 1;
        }
        else if(currentPosition > options.Length - 1)
        {
            currentPosition = 0;
        }

        rect.position = new Vector3(rect.position.x, options[currentPosition].position.y, 0);
    }

    private void Interact()
    {
        SoundManager.instance.PlaySound(interactSound);

        //access the button component of each option and call its function
        options[currentPosition].GetComponent<Button>().onClick.Invoke();
    }
}
