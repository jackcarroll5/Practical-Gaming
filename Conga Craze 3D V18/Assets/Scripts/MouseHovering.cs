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
    void OnMouseEnter()
    {
        Active = true;
        gameObject.GetComponent<TextMesh>().color = Color.black;
    }

     void OnMouseExit()
    {
        Active = false;
        gameObject.GetComponent<TextMesh>().color = Color.red;
    }

    // Update is called once per frame
    void Update ()
    {  
        Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit info;
        if (collider.Raycast(r, out info, 200) && !Active)
            OnMouseEnter();
         if (Active && (!collider.Raycast(r, out info, 200) ))  OnMouseExit();

        if (Active && Input.GetMouseButtonDown(0))
            ApplyAction();
    }

    private void ApplyAction()
    {
        switch (thisButtonIs) {

            case MenuButtons.Start:

                SceneManager.LoadScene("SetStart");
                break;

            case MenuButtons.Quit:

                Application.Quit();
                break;
        }
    }
}
