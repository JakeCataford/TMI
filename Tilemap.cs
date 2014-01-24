using UnityEngine;
using System.Collections.Generic;

namespace TMI {
	public class Tilemap : TMIBehaviour {
		public Tileset tileset;
		public int width;
		public int height;

		public int[][] map;

		public void Awake() {
			Resize ();
		}

		public void Resize() {
			int[][] old = map;

			map = new int[width][];
			for(int i = 0; i < width; i++) {
				map[i] = new int[height];
			}

			if (old != null) {
				for (int i = 0; i < width; i ++) {
					for (int j = 0; j < height; j++) {
							map [i] [j] = old [i] [j];
					}
				}
			}
		}

		public void SetTile(int index, int x, int y) {
			Debug.Log (index + " : " + x + " : " + y);
			if (map == null) {
				Resize();
			}
			map [x] [y] = index;
			Render ();
		}

		public void Render() {
			if (map == null) {
				Resize();
			}
			name = "Layer " + (Manager.layers.IndexOf (this) + 1);
			foreach (Transform t in transform) {
				DestroyImmediate(t.gameObject);
			}

			if(tileset != null) {
				for(int i = 0; i < width; i++) {
					for(int j = 0; j < height; j++) {
						//if(map[i][j] != 0) {
							Debug.Log(map[i][j]);
							GameObject go = new GameObject();
							SpriteRenderer sr = go.AddComponent<SpriteRenderer>();
							sr.sprite = tileset.Tiles[map[i][j]];
							go.transform.position = new Vector3(i,j,transform.position.z);
							go.transform.parent = transform;
							if(tileset.IsSolid(map[i][j])) {
								go.AddComponent<BoxCollider2D>();
							}
						//}
					}
				}
			}
		}
	}
}
