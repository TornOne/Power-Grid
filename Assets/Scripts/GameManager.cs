using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public Tile tilePrefab;
	public GameObject windmill;
	public GameObject cable;
	public GameObject house;
	public int gridX;
	public int gridY;

    public List<GameObject> producers;
    public List<GameObject> consumers;
    public List<GameObject> cables;

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
			
		grid[0][0].CreateBuilding(windmill);
		grid[0][1].CreateBuilding(cable);
		grid[1][0].CreateBuilding(cable);
		grid[2][0].CreateBuilding(cable);
		grid[3][0].CreateBuilding(house);

        Vector3 cameraPosition = Camera.main.transform.position;

        Camera.main.transform.position = new Vector3(gridX / 2, cameraPosition.y, gridY / 2);
	}

	void FixedUpdate() {
		EnergyTransmitter[,] oldState = new EnergyTransmitter[gridX, gridY];
		int[,] delta = new int[gridX, gridY];

		for (int x = 0; x < gridX; x++) {
			for (int y = 0; y < gridY; y++) {
				oldState[x, y] = grid[x][y].building.GetComponent<EnergyTransmitter>();
			}
		}

		for (int x = 0; x < gridX; x++) {
			for (int y = 0; y < gridY; y++) {
				if (oldState[x, y] == null) {
					continue;
				}

				List<EnergyTransmitter> cables = new List<EnergyTransmitter>();
				List<KeyValuePair<int, int>> cableIds = new List<KeyValuePair<int, int>>();
				if (x > 0 && oldState[x - 1, y] != null && oldState[x - 1, y].currentEnergy < oldState[x, y].currentEnergy) {
					cables.Add(oldState[x - 1, y]);
					cableIds.Add(new KeyValuePair<int, int>(x - 1, y));
				}
				if (y > 0 && oldState[x, y - 1] != null && oldState[x, y - 1].currentEnergy < oldState[x, y].currentEnergy) {
					cables.Add(oldState[x, y - 1]);
					cableIds.Add(new KeyValuePair<int, int>(x, y - 1));
				}
				if (x < gridX - 1 && oldState[x + 1, y] != null && oldState[x + 1, y].currentEnergy < oldState[x, y].currentEnergy) {
					cables.Add(oldState[x + 1, y]);
					cableIds.Add(new KeyValuePair<int, int>(x + 1, y));
				}
				if (y < gridY - 1 && oldState[x, y + 1] != null && oldState[x, y + 1].currentEnergy < oldState[x, y].currentEnergy) {
					cables.Add(oldState[x, y + 1]);
					cableIds.Add(new KeyValuePair<int, int>(x, y + 1));
				}
				cables.Add(oldState[x, y]);

				float sum = 0;
				foreach (EnergyTransmitter cable in cables) {
					sum += cable.currentEnergy;
				}

				for (int i = 0; i < cables.Count; i++) {
					KeyValuePair<int, int> cableId = cableIds[i];
					//loat amountAdded = Mathf.Min(sum / (cables.Count - i), cables[i].energyCapacity - )
					//delta[cableId.Key, cableId.Value] += sum / (cables.Count - i);
				}
			}
		}
	}

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

		if (Input.GetMouseButtonDown(1))
		{
			selectedTile.CreateBuilding(windmill);
		}
	}
}
