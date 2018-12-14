using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class worldController : MonoBehaviour
{
    public int gameBoardWidth;
    public int gameBoardHeight;
    public GameObject plainTerrainPrefab;
    public GameObject blockedTerrainPrefab;


    private bool whoseTurn;
    private GameObject selectedUnit;
    private GameObject checkTerrain;

	
	void Start ()
    {
        int centerOffSetX = Mathf.RoundToInt((gameBoardWidth / 2) + 1);
        int centerOffSetY = Mathf.RoundToInt((gameBoardHeight / 2) + 1);

		for (int index = 0; index < gameBoardHeight; index++)
        {
            for (int count = 0; count < gameBoardWidth; count++)
            {   
                float rndNum = Random.Range(0, 10);

                if(rndNum <= 2)
                {
                    GameObject newTerrain = Instantiate(blockedTerrainPrefab, new Vector2(gameBoardWidth - centerOffSetX - index,
                        gameBoardHeight - centerOffSetY - count), Quaternion.identity, this.transform);
                        newTerrain.GetComponent<terrainInfo>().SetTerrainType("blocked");
                }
                else
                {
                    GameObject newTerrain = Instantiate(plainTerrainPrefab, new Vector2(gameBoardWidth - centerOffSetX - index,
                        gameBoardHeight - centerOffSetY - count), Quaternion.identity, this.transform);
                        newTerrain.GetComponent<terrainInfo>().SetTerrainType("plain");
                }
            }
        }
	}
	
	
	void Update ()
    {
		
	}


    public void whoIsSelected (GameObject unitWhoIsSelected)
    {
        selectedUnit = unitWhoIsSelected;
    }


    public void aUnitWantsToEnter(GameObject terrain, Vector2 mousePosition)
    {
        //Note: can get unitPathEnd from this terrain variable
        /*if (selectedUnit)
        {
            if (Vector2.Distance(terrain.transform.position, selectedUnit.transform.position) <= 1.0f)
            {
                selectedUnit.transform.position = terrain.transform.position;
            }
            selectedUnit.GetComponent<unitScript>().moveVertical(terrain.transform.position);
            selectedUnit.GetComponent<unitScript>().moveHorizontal(terrain.transform.position);
        }*/

        selectedUnit.GetComponent<unitScript>().createPath(terrain);
    }


    public GameObject getSelectedUnit ()
    {
        return selectedUnit;
    }
}
