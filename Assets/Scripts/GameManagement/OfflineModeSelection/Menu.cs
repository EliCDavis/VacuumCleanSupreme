using UnityEngine;
using System.Collections;
using VGDC.GameManagement;

namespace VGDC.GameManagement.OfflineModeSelection{

	public class Menu : MonoBehaviour {

		/// <summary>
		/// Loads the game mode protect the queen for offline play
		/// </summary>
		public void playProtectTheQueen(){

			GameManager.getInstance ().playGame (GameModeType.ProtectTheQueen, MapType.Prototype);

		}

	}

}