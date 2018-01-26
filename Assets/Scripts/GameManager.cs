using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public Tile tilePrefab;
	public int gridX;
	public int gridY;

    public List<List<Tile>> grid;

    private Tile selectedTile;

	// Use this for initialization
	void Start () {
        grid = new List<List<Tile>>();

		for(int i = 0; i < gridX; i++) {
            List<Tile> row = new List<Tile>();
			for(int j = 0; j < gridY; j++) {
				var tile = Instantiate(tilePrefab);

                tile.name = "Tile " + i + ", " + j;

				tile.transform.position = new Vector3(i, 0, j);
                if(i % 2 == 0) {
                    tile.setType(Tile.Type.Grass);
                }
                else {
                    tile.setType(Tile.Type.Water);  
                }
                row.Add(tile);
			}
            grid.Add(row);
		}

        Vector3 cameraPosition = Camera.main.transform.position;

        Camera.main.transform.position = new Vector3(gridX / 2, cameraPosition.y, gridY / 2);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                if(selectedTile != null) {
                    selectedTile.Select(false);
                }

                selectedTile = hit.transform.gameObject.GetComponent<Tile>();
                selectedTile.Select(true);
            }
        }
	}
}
