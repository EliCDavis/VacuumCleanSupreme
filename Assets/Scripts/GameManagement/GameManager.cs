using UnityEngine;
using VGDC.Character;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace VGDC.GameManagement {


	/// <summary>
	/// The Game manager talks to things associated with the current game mode in order
	/// to manage the state of the game with ease.  
	/// 
	/// Calls events of the current GameMode to
	/// execute, as well as keeps up with information like who's alive in the scene.
	/// </summary>
	public class GameManager {


		/// <summary>
		/// The only instance of the game manager that will ever exist.
		/// </summary>
		private static GameManager instance = null;


		/// <summary>
		/// The current mode being played that the manager alerts of events going on in the scene
		/// </summary>
		private GameMode currentModeBeingPlayed;


		/// <summary>
		/// A list of every character alive in the scene
		/// </summary>
		private List<CharacterBehavior> charactersInScene = null;


		/// <summary>
		/// Singleton method guarantees only one instance of game manager 
		/// ever exists in the application.  Lazy loaded so it's only taking
		/// up memory once it's needed in the game, speeding up loading time.
		/// </summary>
		/// <returns>The only instance in the application</returns>
		public static GameManager getInstance(){

			if (instance == null) {
				instance = new GameManager();
			}

			return instance;

		}


		/// <summary>
		/// Depending on the level index passed in, this will determine if the level is a map or not
		/// that a game mode can be played in.
		/// </summary>
		/// <returns><c>true</c>, if play game in level was caned, <c>false</c> otherwise.</returns>
		/// <param name="levelIndex">Level index.</param>
		public static bool canPlayGameInLevel(int levelIndex){

			//TODO Implement this actually

			return levelIndex > 0;
		}


		/// <summary>
		/// Sets up the game to be played in the map specified
		/// </summary>
		/// <param name="mode">Mode.</param>
		/// <param name="map">Map.</param>
		public void playGame(GameModeType mode, MapType map){

			Debug.Log ("about to load scene");

			switch(map){

			case MapType.Prototype:
				SceneManager.LoadScene ("PrototypeMap", LoadSceneMode.Single);		
				break;

			}

			Debug.Log ("Scene loaded");

			GameObject container = new GameObject ("_SCRIPTS_");
			Object.DontDestroyOnLoad (container);

			switch(mode){

			case GameModeType.ProtectTheQueen:
				
				currentModeBeingPlayed = container.AddComponent<ProtectTheQueen.ProtectTheQueenModeBehavior> ();
				break;

			}


		}


		/// <summary>
		/// Adds a character to the game.
		/// </summary>
		/// <param name="character">Character.</param>
		public void addCharacterToGame(CharacterBehavior character){

			// If the character their trying to add is null
			if (character == null) {
				Debug.LogError("The character your trying to add to the Game Manager is null!");
				return;
			}

			// If the character their trying to add has already been added
			if (charactersInScene.Contains (character)) {
				Debug.LogError("The character your trying is add is already listed in the Game Manager!");
				return;
			}

			charactersInScene.Add (character);

			if (currentModeBeingPlayed != null) {

				// Alert the game mode.
				currentModeBeingPlayed.onCharacterAdd (character);

			} else {
				Debug.LogWarning("You just added a character to the scene while theres no game mode running!");
			}

		}


		/// <summary>
		/// Removes the character from the game and alerta the game mode that
		/// it has died
		/// </summary>
		/// <param name="character">Character.</param>
		public void removeCharacterFromGame(CharacterBehavior character){
			
			// If the character their trying to remove is null
			if (character == null) {
				Debug.LogError("The character your trying to remove from the Game Manager is null!");
				return;
			}
			
			// If the character their trying to remove isn't here
			if (!charactersInScene.Contains (character)) {
				Debug.LogError("The character your trying to remove is not listed in the Game Manager!");
				return;
			}
			
			charactersInScene.Remove (character);
			
			if (currentModeBeingPlayed != null) {
				
				// Alert the game mode.
				currentModeBeingPlayed.onCharacterDeath (character);
				
			} else {
				Debug.LogWarning("You just removed a character from the scene while theres no game mode running!");
			}
			
		}


		/// <summary>
		/// Deletes all characters from the scene
		/// </summary>
		public void clearCharactersInScene(){


			// Destroy the gameobjects that the players control in the scene.
			for (int i = 0; i < charactersInScene.Count; i ++) {

				if(charactersInScene[i].gameObject != null){

					// TODO convert this into a network call
					Object.Destroy(charactersInScene[i].gameObject);

				}

			}

			charactersInScene.Clear ();

		}



		public void notifyVacuumBatteryConsumption(VacuumBehavior vacuum){

			if (currentModeBeingPlayed != null) {
				currentModeBeingPlayed.onVacuumBatteryConsumption (vacuum);
			}

		}


		public void notifyBunnyJumpAction(BunnyBehavior bunny){

			if (currentModeBeingPlayed != null) {
				currentModeBeingPlayed.onBunnyJumpAction (bunny);
			}

		}


		public void notifyBunnyTrapAction(BunnyBehavior bunny){

			if (currentModeBeingPlayed != null) {
				currentModeBeingPlayed.onBunnyTrapAction (bunny);
			}

		}
			

		/// <summary>
		/// Private constructor garunteeing only the class can initialize an instance of itself
		/// </summary>
		private GameManager(){

			// Initialize all variables
			charactersInScene = new List<CharacterBehavior> ();

		}

	}

}