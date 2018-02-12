using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class worldController : MonoBehaviour
{
    public int gameBoardWidth;
    public int gameBoardHeight;
    public GameObject plainTerrainPrefab;


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
                Instantiate(plainTerrainPrefab, new Vector2(gameBoardWidth - centerOffSetX - index,
                    gameBoardHeight - centerOffSetY - count), Quaternion.identity, this.transform);
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
        if (Vector2.Distance(terrain.transform.position, selectedUnit.transform.position) <= 1.0f)
        {
            selectedUnit.transform.position = terrain.transform.position;
        }
    }


    public GameObject getSelectedUnit ()
    {
        return selectedUnit;
    }
}
