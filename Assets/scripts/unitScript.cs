using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unitScript : MonoBehaviour
{
    public string owner;
    public bool selected;
    public GameObject unitPathEnd;
    public float unitSpeed;


    private worldController worldController;
    private combatController combatController;
    private ArrayList path;
    private bool hasArrived;
    private bool combatFlag;
    private bool engageCombat;

	
	void Start ()
    {
        selected = false;
        hasArrived = true;
        combatFlag = false;
        engageCombat = false;
        worldController = GameObject.FindGameObjectWithTag("World Controller").GetComponent<worldController>();
	}
	
	
	void Update ()
    {
        isThisUnitBeingSelected();
        moveAlongPath();
        attackEnemy();
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
        bool blocked = false;

        while(endTerrain.transform.position.y != pathPostion.y)
        {
            pathPostion = moveVertical(endTerrain.transform.position, pathPostion);
            RaycastHit2D pathFinder = Physics2D.Raycast(pathPostion, Vector2.up);

            if(pathFinder.collider.GetComponent<terrainInfo>().GetTerrainType().Equals("blocked"))
            {
                blocked = true;
            }

            if(pathFinder.collider != null)
            {
                newPath.Add(pathFinder.transform.position);
            }
        }

        while(endTerrain.transform.position.x != pathPostion.x)
        {
            pathPostion = moveHorizontal(endTerrain.transform.position, pathPostion);
            RaycastHit2D pathFinder = Physics2D.Raycast(pathPostion, Vector2.up);

            if(pathFinder.collider != null)
            {
                newPath.Add(pathFinder.transform.position);
            }
        }

        hasArrived = false;
        path = new ArrayList(newPath);
    }


    private void moveAlongPath()
    {
        //A-star path finding
        if(!hasArrived)
        {
            if(path.Count >= 0 && Vector3.Distance(this.transform.position, (Vector3)path[0]) >= 0.001f)
            {
                this.transform.position = Vector2.MoveTowards(this.transform.position, (Vector3)path[0] , unitSpeed * Time.deltaTime);
            }
            else
            {
                this.transform.position = (Vector3)path[0];
                path.RemoveAt(0);
                path.TrimToSize();
                Debug.Log("I have reached a node in my path.");

                /*if(!combatFlag)
                {
                    path.TrimToSize();
                }
                else
                {
                    path.RemoveRange(0, path.Count);
                    engageCombat = true;
                }*/

                if (path.Count <= 0)
                {
                    hasArrived = true;
                    path.Clear();
                }
            }
        }
    }


    private void attackEnemy()
    {

    }


    public void setCombatMode (bool isInCombat)
    {
        combatFlag = isInCombat;
    }


    public void setTargetForPath(GameObject newTerrainTarget)
    {
        unitPathEnd = newTerrainTarget;
    }
}
