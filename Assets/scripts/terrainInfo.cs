using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class terrainInfo : MonoBehaviour
{
    public string terrainType;

    private GameObject[] unitsOnTerrain;
    private int spotsLeftOnTerrain;
    private const int spotsOnTerrain = 1;
    private worldController worldController;

	
	void Start ()
    {
        unitsOnTerrain = new GameObject[4];
        spotsLeftOnTerrain = 1;

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
        if (collision.gameObject.CompareTag("Unit"))
        {
            if (spotsLeftOnTerrain > 0)
            {
                unitsOnTerrain[spotsOnTerrain - spotsLeftOnTerrain] = collision.gameObject;
                Debug.Log("A unit has entered this terrain.");
            }
        }
    }


    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Unit"))
        {
            spotsLeftOnTerrain--;
            Debug.Log("A unit has left this terrain block.");
        }
    }
}
