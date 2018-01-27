using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager mainGameManager;

	public Tile tilePrefab;

    public LineRenderer connectionCable;

    public List<GameObject> producersList;
	public List<GameObject> cablesList;
	public List<GameObject> consumersList;

	public int gridXSize;
	public int gridYSize;

    public List<List<Tile>> grid;

    private Tile selectedTile;

	void Start () {
	    mainGameManager = this;
        grid = new List<List<Tile>>();

		for(int i = 0; i < gridXSize; i++) {
            List<Tile> row = new List<Tile>();
			for(int j = 0; j < gridYSize; j++) {
				var tile = Instantiate(tilePrefab);

                tile.name = "Tile " + i + ", " + j;
			    tile.gridPosition = new Vector2Int(i, j);

				tile.transform.position = new Vector3(i, 0, j);
                if(i % 2 == 0 || j % 2 == 0) {
                    tile.setType(Tile.Type.Grass);
                }
                else {
                    tile.setType(Tile.Type.Water);  
                }
                row.Add(tile);
			}
            grid.Add(row);
		}

		grid[0][0].CreateBuilding(producersList[0]);
		grid[0][1].CreateBuilding(cablesList[0]);
		grid[1][0].CreateBuilding(cablesList[0]);
		grid[2][0].CreateBuilding(cablesList[0]);
		grid[3][0].CreateBuilding(consumersList[0]);

        Vector3 cameraPosition = Camera.main.transform.position;

        Camera.main.transform.position = new Vector3(gridXSize / 2, cameraPosition.y, gridYSize / 2);
	}

	void FixedUpdate() {
		EnergyTransmitter[,] oldState = new EnergyTransmitter[gridXSize, gridYSize];
		float[,] deltas = new float[gridXSize, gridYSize];

		for (int x = 0; x < gridXSize; x++) {
			for (int y = 0; y < gridYSize; y++) {
				if (grid[x][y].building != null) {
					oldState[x, y] = grid[x][y].building.GetComponent<EnergyTransmitter>();
				}
			}
		}

		for (int x = 0; x < gridXSize; x++) {
			for (int y = 0; y < gridYSize; y++) {
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
				if (x < gridXSize - 1 && oldState[x + 1, y] != null && oldState[x + 1, y].currentEnergy < oldState[x, y].currentEnergy) {
					cables.Add(oldState[x + 1, y]);
					cableIds.Add(new KeyValuePair<int, int>(x + 1, y));
				}
				if (y < gridYSize - 1 && oldState[x, y + 1] != null && oldState[x, y + 1].currentEnergy < oldState[x, y].currentEnergy) {
					cables.Add(oldState[x, y + 1]);
					cableIds.Add(new KeyValuePair<int, int>(x, y + 1));
				}

				for (int i = 0; i < cables.Count; i++) {
					KeyValuePair<int, int> cableId = cableIds[i];
					float delta = Mathf.Min((oldState[x, y].currentEnergy - cables[i].currentEnergy) / (cables.Count + 1), (cables[i].energyCapacity - cables[i].currentEnergy) / 2);
					deltas[cableId.Key, cableId.Value] += delta;
					deltas[x, y] -= delta;
				}
			}
		}

		for (int x = 0; x < gridXSize; x++) {
			for (int y = 0; y < gridYSize; y++) {
				if (oldState[x, y] != null) {
					oldState[x, y].currentEnergy += deltas[x, y];
				}
			}
		}
	}

	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, LayerMask.GetMask("Tile")))
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
		    if (selectedTile != null) {
		        selectedTile.CreateBuilding(consumersList[0]);
		    }
		}
	}

    public Tile GetTile(Vector2Int pos) {
        return grid[pos.x][pos.y];
    }

    public List<Tile> GetBorderingTiles(Vector2Int pos) {
        List<Tile> tiles = new List<Tile>(4);

        if (pos.x > 0) {
            tiles.Add(grid[pos.x - 1][pos.y]);
        }

        if (pos.y > 0) {
            tiles.Add(grid[pos.x][pos.y - 1]);
        }
        if (pos.x < gridXSize)
        {
            tiles.Add(grid[pos.x + 1][pos.y]);
        }

        if (pos.y < gridYSize)
        {
            tiles.Add(grid[pos.x][pos.y + 1]);
        }

        return tiles;
    }
}
