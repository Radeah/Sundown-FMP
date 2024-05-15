using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Notes : MonoBehaviour
{
    public GameObject player;
    public GameObject noteUI;
    public GameObject hud;
    public GameObject inv;

    public CharacterController FPScontroller;

    public GameObject pickUpText;

    public AudioSource pickUpSound;

    public bool inReach;

    public AudioClip notePickupSound; // new variable for the note pickup sound effect

    // Start is called before the first frame update
    void Start()
    {
        noteUI.SetActive(false);
        hud.SetActive(true);
        inv.SetActive(true);
        pickUpText.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Reach")
        {
            inReach = true;
            pickUpText.SetActive(true);
        }

    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Reach")
        {
            inReach = false;
            pickUpText.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && inReach)
        {
            noteUI.SetActive(true);
            Debug.Log("Note UI activated.");
            AudioSource.PlayClipAtPoint(notePickupSound, transform.position); // play the pickup sound effect
            pickUpSound.Play();
            hud.SetActive(true);
            inv.SetActive(true);
            FPScontroller.enabled = false;
            Cursor.visible = true; //need change line
            Cursor.lockState = CursorLockMode.None; //need to change line 
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            Debug.Log("Functions does run");
            noteUI.SetActive(false);
            hud.SetActive(true);
            inv.SetActive(true);
            FPScontroller.enabled = true; //change line of code
        }
    }

    /* public void ExitButton()
    {
        Debug.Log("Functions does run");
        noteUI.SetActive(false);
        hud.SetActive(true);
        inv.SetActive(true);
        FPScontroller.enabled = true; //change line of code

    } */ 
    

}