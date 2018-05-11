using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MouseHovering : MonoBehaviour,IEventSystemHandler {

    bool Active;
    Collider collider;

    public enum MenuButtons { Start, Quit };

    public MenuButtons thisButtonIs;

	// Use this for initialization
	void Start () {
        gameObject.GetComponent<TextMesh>().color = Color.red;
        collider =  GetComponent<Collider>();
    }

    public void OnPointerEnter(PointerEventData data)
    {
        print("Hello");
    }

    //Original Text
    void OnMouseEnter()
    {
        Active = true;
        gameObject.GetComponent<TextMesh>().color = Color.black;
    }

    //Changed Text when the mouse hovers over the text.
    void OnMouseExit()
    {
        Active = false;
        gameObject.GetComponent<TextMesh>().color = Color.red;
    }

    // Update is called once per frame
    void Update ()
    {  
        /*Code for changing the colour of the menu text when hovering the mouse over the text
         Changes colour again to original colour when the mouse leaves the menu text boundary*/
        Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit info;
        if (collider.Raycast(r, out info, 200) && !Active)
            OnMouseEnter();
         if (Active && (!collider.Raycast(r, out info, 200) ))
            OnMouseExit();

        if (Active && Input.GetMouseButtonDown(0))
            ApplyAction();
    }

    //The commands for pressing either the start or quit button
    private void ApplyAction()
    {
        switch (thisButtonIs) {

            case MenuButtons.Start:

                SceneManager.LoadScene("SetStart");//Switches the scene to the high score setup and general main level start setup
                break;

            case MenuButtons.Quit:

                Application.Quit();
                break;
        }
    }
}
