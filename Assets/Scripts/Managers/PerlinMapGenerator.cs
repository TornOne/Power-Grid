using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinMapGenerator : MonoBehaviour {
	public List<List<Tile>> GenerateMap(Tile tilePrefab, int width, int height, float waterAmount = 0.4f) {
		float offsetX = Random.Range(0f, 1024f);
		float offsetY = Random.Range(0f, 1024f);
		GameObject tileParent = new GameObject("Tiles");

		List<List<Tile>> grid = new List<List<Tile>>(height);

		for (int x = 0; x < width; x++) {
			List<Tile> row = new List<Tile>(width);

			for (int y = 0; y < height; y++) {
				Tile tile = Instantiate(tilePrefab, new Vector3(x, 0, y), Quaternion.Euler(90, 0, 0), tileParent.transform);
				tile.name = "Tile " + x + ", " + y;
				tile.gridPosition = new Vector2Int(x, y);

				float value = Mathf.PerlinNoise(offsetX + 8f * x / width, offsetY + 8f * y / height) * 0.4f;
				value += Mathf.PerlinNoise((offsetX + 8f * x / width) * 2.5f, (offsetY + 8f * y / height) * 2.5f) * 0.2f;
				value += Mathf.PerlinNoise((offsetX + 8f * x / width) * 0.3f, (offsetY + 8f * y / height) * 0.3f) * 0.4f;
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
