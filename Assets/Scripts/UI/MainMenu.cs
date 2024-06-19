using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuScreen;
    // Start is called before the first frame update
    void Start()
    {
        mainMenuScreen.SetActive(true);
    }

    //Main menu function
    public void StartFunc()
    {
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Application.Quit(); //quits the game in build mode 

        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; //quits run mode in unity editor
        #endif
    }
}
