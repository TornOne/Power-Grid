using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinMapGenerator : MonoBehaviour {
	public List<List<Tile>> GenerateMap(Tile tilePrefab, int width, int height, float waterAmount = 0.3f) {
		float offsetX = Random.Range(0f, float.MaxValue / 2);
		float offsetY = Random.Range(0f, float.MaxValue / 2);
		GameObject tileParent = new GameObject("Tiles");

		List<List<Tile>> grid = new List<List<Tile>>(height);

		for (int x = 0; x < width; x++) {
			List<Tile> row = new List<Tile>(width);

			for (int y = 0; y < width; y++) {
				Tile tile = Instantiate(tilePrefab, new Vector3(x, 0, y), Quaternion.Euler(90, 0, 0), tileParent.transform);
				tile.name = "Tile " + x + ", " + y;
				tile.gridPosition = new Vector2Int(x, y);

				float value = Mathf.PerlinNoise(offsetX + x * 0.03f, offsetY + y * 0.03f);
				Debug.Log(x + " " + y + " " + value);
				if (value > waterAmount) {
					tile.SetType(Tile.Type.Grass);
				} else {
					tile.SetType(Tile.Type.Water);
				}
				row.Add(tile);
			}

			grid.Add(row);
		}

		return grid;
	}
}
