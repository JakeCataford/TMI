using UnityEngine;
using System.Collections;

namespace TMI {
	public class TMIBehaviour : MonoBehaviour {
		public static TilemapManager Manager {
			get {
					return TilemapManager.Instance;
			}
			private set {}
		}

	}
}
