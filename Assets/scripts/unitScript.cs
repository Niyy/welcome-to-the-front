using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unitScript : MonoBehaviour
{
    public string owner;
    public bool selected;
    public GameObject unitPathEnd;


    private worldController worldController;
    private ArrayList terrainInPath;

	
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
            //this.transform.position = square.transform.position;
            Debug.Log("I want to go there.");
        }
    }


    public string getOwner()
    {
        return owner;
    }


    public Vector2 moveVertical(Vector2 target, Vector2 pathPostion)
    {
        Vector2 nextTerrainInPath = Vector2.zero;

        if(target.y != pathPostion.y)
        {
            if(target.y < pathPostion.y)
            {
                //transform.position = new Vector2(transform.position.x, transform.position.y - 1);
                nextTerrainInPath = new Vector2(pathPostion.x, pathPostion.y - 1);
            }
            else if (target.y > pathPostion.y)
            {
                //transform.position = new Vector2(transform.position.x, transform.position.y + 1);
                nextTerrainInPath = new Vector2(pathPostion.x, pathPostion.y + 1);
            }
        }

        //Debug.Log("next terrainin path: nextTerrainInPath: " + nextTerrainInPath);
        return nextTerrainInPath;
    }

    
    public Vector2 moveHorizontal(Vector2 target, Vector2 pathPostion)
    {
        Vector2 nextTerrainInPath = Vector2.zero;

        if(target.x != pathPostion.x)
        {
            if(target.x < pathPostion.x)
            {
                //transform.position = new Vector2(transform.position.x - 1, transform.position.y);
                nextTerrainInPath = new Vector2(pathPostion.x - 1, pathPostion.y);
            }
            else if (target.x > pathPostion.x)
            {
                //transform.position = new Vector2(transform.position.x + 1, transform.position.y);
                nextTerrainInPath = new Vector2(pathPostion.x + 1, pathPostion.y);
            }
        }

        return nextTerrainInPath;
    }


    public void createPath (GameObject endTerrain)
    {
        ArrayList newPath = new ArrayList();
        Vector2 pathPostion = this.transform.position;

        while(endTerrain.transform.position.y != pathPostion.y)
        {
            pathPostion = moveVertical(endTerrain.transform.position, pathPostion);
            RaycastHit2D pathFinder = Physics2D.Raycast(pathPostion, Vector2.up);

            if(pathFinder.collider != null)
            {
                newPath.Add(pathFinder.transform.position);
                Debug.Log("Part of path: " + newPath[newPath.Count - 1]);
            }
        }

        while(endTerrain.transform.position.x != pathPostion.x)
        {
            pathPostion = moveHorizontal(endTerrain.transform.position, pathPostion);
            RaycastHit2D pathFinder = Physics2D.Raycast(pathPostion, Vector2.up);

            if(pathFinder.collider != null)
            {
                newPath.Add(pathFinder.transform.position);
                Debug.Log("Part of path: " + newPath[newPath.Count - 1]);
            }
        }
    }


    private void moveAlongPath()
    {

    }


    public void setTargetForPath(GameObject newTerrainTarget)
    {
        unitPathEnd = newTerrainTarget;
    }
}
