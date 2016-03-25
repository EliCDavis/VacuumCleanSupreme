using UnityEngine;
using System.Collections;

namespace VGDC.Character {

	/// <summary>
	/// Bunny behavior that encapsulates all actions the bunny can perform in game
	/// </summary>
	public class BunnyBehavior : CharacterBehavior {

		/// <summary>
		/// Lays a trap that will slow vacuums.
		/// </summary>
		public void layTrap(){
			GameManagement.GameManager.getInstance ().notifyBunnyTrapAction (this);
		}

		/// <summary>
		/// Has the bunny make a small hop
		/// </summary>
		public void jump(){
			GameManagement.GameManager.getInstance ().notifyBunnyJumpAction (this);
		}

	}

}