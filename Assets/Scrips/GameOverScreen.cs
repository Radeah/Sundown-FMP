using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.V)) 
        {
            SceneManager.LoadScene("Lvl 2");
        }
        
        if (Input.GetKeyDown(KeyCode.H)) 
        {
            SceneManager.LoadScene("Menu"); 
        }
    }
    public void Retry()
    {
        SceneManager.LoadScene("Lvl 1");
    }
    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }



}
