using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unitScript : MonoBehaviour
{
    public string owner;
    public bool selected;


    private worldController worldController;

	
	void Start ()
    {
        selected = false;
        worldController = GameObject.FindGameObjectWithTag("World Controller").GetComponent<worldController>();
	}
	
	
	void Update ()
    {
        isThisUnitBeingSelected();
	}


    private void isThisUnitBeingSelected ()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetKeyDown(KeyCode.Mouse0) && Vector2.Distance(mousePos, transform.position) <= 0.17)
        {
            selected = true;
            worldController.whoIsSelected(this.gameObject);
            Debug.Log("This unit has been selected.");
        }
        else if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            selected = false;
        }
    }


    public void whereIsUnitGoing (GameObject square)
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && selected)
        {
            this.transform.position = square.transform.position;
            Debug.Log("I want to go there.");
        }
    }


    public string getOwner()
    {
        return owner;
    }
}
