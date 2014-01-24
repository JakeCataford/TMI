using UnityEngine;
using System.Collections.Generic;

namespace TMI {
	public class TilemapManager : Singleton<TilemapManager> {

		protected TilemapManager() {}

		public List<Tilemap> layers = new List<Tilemap>();
		public Tileset defaultTileset;

		/// <summary>
		/// Creates a new tilemap and assigns it to the topmost layer.
		/// </summary>
		/// <returns>The new Tilemap.</returns>
		/// <param name="width">Width in tiles.</param>
		/// <param name="height">Height in tiles.</param>
		public Tilemap CreateTilemap(int width, int height) {
			GameObject go = new GameObject ();
			Tilemap tilemap = go.AddComponent<Tilemap> ();
			tilemap.width = width;
			tilemap.height = height;
			tilemap.tileset = defaultTileset;
			tilemap.Resize ();
			layers.Add (tilemap);
			OrganizeLayers ();
			return tilemap;
		}

		public void DestroyTilemap(int index) {
			DestroyImmediate (layers [index].gameObject);
			layers.RemoveAt (index);
			OrganizeLayers ();
		}

		private void OrganizeLayers() {
			for (int i = 0; i < layers.Count; i++) {
				layers[i].transform.position = new Vector3(0,0,i);
			}
			RenderAllLayers ();
		}

		public void RenderAllLayers() {
			foreach (Tilemap t in layers) {
				t.Render();
			}
		}
	}
}
