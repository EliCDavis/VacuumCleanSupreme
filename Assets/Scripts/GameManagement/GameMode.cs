using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using VGDC.Character;
using VGDC.GameManagement;

namespace VGDC.GameManagement {

	/// <summary>
	/// Abstract Game mode that you should extend from to create your own game
	/// mode.
	/// </summary>
	public abstract class GameMode : MonoBehaviour {

		protected MapDataBehavior mapData = null;

		/// <summary>
		/// This event is fired once a level has been loaded.
		/// Ideally a game mode is attatched to an object that has had
		/// DontDestroyOnLoad() on it.
		/// </summary>
		/// <param name="levelIndex">Level index.</param>
		void OnLevelWasLoaded(int levelIndex){

			mapData = (MapDataBehavior)Object.FindObjectOfType (typeof(MapDataBehavior));

			// If we where un able to find any map data
			if (mapData == null) {
				Debug.LogError ("Can't find any map data for the loaded map! Beware!");
			}

			// If this an appropriate scene for a game to play in.
			if (GameManager.canPlayGameInLevel (levelIndex)) {
				onGameStart ();
			} else {
				Debug.LogError ("This scene can not have a game mode loaded on it!\nGameMode not starting!");
			}

		}

//		public static int minNumberOfVacuumPlayers (){return 0;}
//		public static int maxNumberOfVacuumPlayers (){return 0;}
//		public static int minNumberOfBunnyPlayers (){return 0;}
//		public static int maxNumberOfBunnyPlayers (){return 0;}

		public abstract void onGameStart();

		public abstract void onCharacterDeath(CharacterBehavior character);

		public abstract void onCharacterAdd(CharacterBehavior character);


		/// <summary>
		/// By default when a vacuum eats a battery it will regain all it's charge
		/// </summary>
		/// <param name="vacuum">Vacuum.</param>
		public virtual void onVacuumBatteryConsumption(VacuumBehavior vacuum){
			vacuum.setCharge (1f);
		}


		public virtual void onBunnyJumpAction(BunnyBehavior bunny){

		}


		public virtual void onBunnyTrapAction(BunnyBehavior bunny){

		}


		/// <summary>
		/// Halts the game and goes back to a game menu.
		/// </summary>
		protected virtual void leaveGameAndGoToMenu(){
			SceneManager.LoadScene (0);
			Destroy (this);
		}

	}

}