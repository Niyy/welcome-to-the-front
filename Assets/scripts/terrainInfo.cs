using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class terrainInfo : MonoBehaviour
{
    public string terrainType;

    public GameObject[] unitsOnTerrain;
    private int spotsLeftOnTerrain;
    private const int spotsOnTerrain = 4;
    private worldController worldController;

	
	void Start ()
    {
        unitsOnTerrain = new GameObject[spotsOnTerrain];
        spotsLeftOnTerrain = spotsOnTerrain;
        foreach(GameObject unit in GameObject.FindGameObjectsWithTag("Unit"))
        {
            if(unit.transform.position == this.transform.position)
            {
                unitsOnTerrain[spotsOnTerrain - spotsLeftOnTerrain] = unit;
                spotsLeftOnTerrain--;
            }
        }

        worldController = GameObject.FindGameObjectWithTag("World Controller").GetComponent<worldController>();
	}
	
	
	void Update ()
    {
        checkTerrainTile();
	}


    private void checkTerrainTile ()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(1) && Vector2.Distance(mousePos, transform.position) <= 0.5)
        {
            worldController.aUnitWantsToEnter(this.gameObject, mousePos);
            Debug.Log("I will allow you to pass.");
        }
    }


    private bool willCombatBegin(string unitOwner)
    {
        bool combatFlag = false;
        int checkPosition = 0;

        while (!combatFlag && spotsOnTerrain > checkPosition)
        {
            if(!unitsOnTerrain[checkPosition].GetComponent<unitScript>().getOwner().Equals(unitOwner))
            {
                combatFlag = true;
                break;
            }

            checkPosition++;
        }

        return combatFlag;
    }


    public bool isEnemyOnTile(string player)
    {
        if (unitsOnTerrain[0])
        {
            unitScript enemyScript = unitsOnTerrain[0].GetComponent<unitScript>();

            return (player != enemyScript.getOwner());
        }
        else
        {
            return (!unitsOnTerrain[0]);
        }
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject unitTryingToEnter = collision.gameObject;

        if (collision.gameObject.CompareTag("Unit"))
        {
            if (spotsLeftOnTerrain > 0)
            {
                if(willCombatBegin(unitTryingToEnter.GetComponent<unitScript>().getOwner())
                && spotsOnTerrain != spotsLeftOnTerrain)
                {
                    Debug.Log("A unit of opposite owner has caused your unit to engage in combat.");
                }
                unitsOnTerrain[spotsOnTerrain - spotsLeftOnTerrain] = unitTryingToEnter;
                spotsLeftOnTerrain--;
                Debug.Log("A unit has entered this terrain.");
            }
        }
    }


    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Unit"))
        {
            spotsLeftOnTerrain++;
            Debug.Log("A unit has left this terrain block. Spots: " + spotsLeftOnTerrain);
        }
    }
}
