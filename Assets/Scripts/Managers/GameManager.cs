using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour {

    private static GameManager mainGameManager;

	public PerlinMapGenerator mapGen;
	public Tile tilePrefab;

    public LineRenderer connectionCable;

    public List<GameObject> producersList;
	public List<GameObject> cablesList;
	public List<GameObject> consumersList;

	public int gridXSize;
	public int gridYSize;

    public List<List<Tile>> grid;

    private Tile selectedTile;

    private bool nuclearAlready;

	void Start () {
	    mainGameManager = this;
	    grid = mapGen.GenerateMap(tilePrefab, gridXSize, gridYSize);
	    /*grid = new List<List<Tile>>();

	    GameObject tileParent = new GameObject("Tiles");

		for(int i = 0; i < gridXSize; i++) {
            List<Tile> row = new List<Tile>();
			for(int j = 0; j < gridYSize; j++) {
				var tile = Instantiate(tilePrefab);

                tile.name = "Tile " + i + ", " + j;
			    tile.gridPosition = new Vector2Int(i, j);

				tile.transform.position = new Vector3(i, 0, j);
			    tile.transform.SetParent(tileParent.transform);
                if(i % 2 == 0 || j % 2 == 0) {
                    tile.SetType(Tile.Type.Grass);
                }
                else {
                    tile.SetType(Tile.Type.Grass);
                }
                row.Add(tile);
			}
            grid.Add(row);
		}*/

	    grid[2][2].CreateBuilding(producersList[0]);
        //grid[0][0].CreateBuilding(producersList[1]);
		grid[0][1].CreateBuilding(cablesList[0]);
		grid[1][0].CreateBuilding(cablesList[0]);
		grid[2][0].CreateBuilding(cablesList[0]);
		grid[3][0].CreateBuilding(consumersList[0]);
	    grid[2][3].CreateBuilding(consumersList[1]);

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

            if (!EventSystem.current.IsPointerOverGameObject()) {
                if (Physics.Raycast(ray, out hit, 100, LayerMask.GetMask("Tile", "UI"))) {
                    if (selectedTile != null) {
                        selectedTile.Select(false);
                    }

                    selectedTile = hit.transform.gameObject.GetComponent<Tile>();
                    selectedTile.Select(true);
                    UIManager.GetUIManager().ShowMenu(true, selectedTile);
                    AudioManager.GetAudioManager().PlaySelect();
                }
                else {
                    if (selectedTile != null) {
                        selectedTile.Select(false);
                        selectedTile = null;
                        UIManager.GetUIManager().ShowMenu(false, null);
                    }
                }
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

    public Tile[] GetBorderingTiles(Vector2Int pos) {
        Tile[] tiles = new Tile[4];

        if (pos.x > 0) {
            tiles[0] = grid[pos.x - 1][pos.y];
        }

        if (pos.y > 0) {
            tiles[1] = grid[pos.x][pos.y - 1];
        }
        if (pos.x < gridXSize - 1) {
            tiles[2] = grid[pos.x + 1][pos.y];
        }

        if (pos.y < gridYSize - 1) {
            tiles[3] = grid[pos.x][pos.y + 1];
        }

        return tiles;
    }

    public static GameManager GetGameManager() {
        if (mainGameManager == null) {
            mainGameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        }

        return mainGameManager;
    }

    public Tile GetSelectedTile() {
        return selectedTile;
    }

    public void BuySelected() {
        //Called from button, so need to get variables from main gameManager ↓
        if (GetGameManager().selectedTile != null && UIManager.GetUIManager().currentSelection != -1) {
            float buildingCost = UIManager.GetUIManager().lastBuilding.GetComponent<BuildingCost>().cost;

            if (!MoneyTracker.GetMoneyTracker().CanAfford(buildingCost)) {
                AudioManager.GetAudioManager().PlayDenied();
                return;
            }

            MoneyTracker.GetMoneyTracker().BuyFor(buildingCost);

            GameObject gameObject = UIManager.GetUIManager().lastBuilding;

            if (gameObject.name == "Nuclear Plant" && !GetGameManager().nuclearAlready) {
                SnapshotManager.GetSnapshotManager().FirstNuclear();
            }
            else {
                GetGameManager().nuclearAlready = true;
            }

            if (GetGameManager().selectedTile.CreateBuilding(UIManager.GetUIManager().lastBuilding)) {
                UIManager.GetUIManager().lastBuilding.GetComponent<BuildSound>().Play();
            }
            UIManager.GetUIManager().ShowMenu(true, GetGameManager().selectedTile);
        }
    }

    public void SellSelected() {
        float buildingCost = GetGameManager().selectedTile.building.GetComponent<BuildingCost>().cost;

        MoneyTracker.GetMoneyTracker().SellFor(buildingCost / 2);

        GetGameManager().selectedTile.DestoryBuilding();
        AudioManager.GetAudioManager().PlaySell();
        UIManager.GetUIManager().ShowMenu(true, GetGameManager().selectedTile);
    }

    public void UpgradeSelected() {
        AudioManager.GetAudioManager().PlayUpgrade();
    }
}
