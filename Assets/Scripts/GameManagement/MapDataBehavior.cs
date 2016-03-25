using UnityEngine;
using System.Collections;

namespace VGDC.GameManagement {

	/// <summary>
	/// Container class for map builders to define important data inside of the map
	/// for game modes to function properly.
	/// </summary>
	public class MapDataBehavior : MonoBehaviour {


		/// <summary>
		/// A list of spawn areas, generally defined with cubes for visualization.
		/// Empty Gameobjects only please
		/// </summary>
		[SerializeField]
		private Transform[] spawnAreas;


		[SerializeField]
		private Transform[] consumableSpawnAreas;


		public Vector3 getRandomSpawnPoint(){

			if (spawnAreas == null) {
				Debug.LogError ("There are no spawn areas to get a spawn point from!");
				return Vector3.zero;
			}

			return getRandomSpawnPoint(Random.Range (0, spawnAreas.Length-1));

		}


		public Vector3 getRandomConsumableSpawnPoint(){

			if (consumableSpawnAreas == null) {
				Debug.LogError ("There are no consumable spawn areas to get a spawn point from!");
				return Vector3.zero;
			}

			return getRandomSpawnPoint(Random.Range (0, consumableSpawnAreas.Length-1));

		}


		public Vector3 getRandomSpawnPoint(int spawnAreaIndex){

			if (spawnAreas == null) {
				Debug.LogError ("There are no spawn areas to get a spawn point from!");
				return Vector3.zero;
			}

			// Make sure that the spawn area index is valid!
			if (spawnAreaIndex < 0 || spawnAreaIndex > spawnAreas.Length - 1) {
				Debug.LogError ("The spawn area index you are trying to use it out of array bounds! Returning Vector.zero!");
				return Vector3.zero;
			}

			return positionWithinObject(spawnAreas[spawnAreaIndex]);

		}


		public Vector3 getRandomConsumableSpawnPoint(int spawnAreaIndex){

			if (consumableSpawnAreas == null) {
				Debug.LogError ("There are no spawn areas to get a spawn point from!");
				return Vector3.zero;
			}

			// Make sure that the spawn area index is valid!
			if (spawnAreaIndex < 0 || spawnAreaIndex > consumableSpawnAreas.Length - 1) {
				Debug.LogError ("The spawn area index you are trying to use it out of array bounds! Returning Vector.zero!");
				return Vector3.zero;
			}

			return positionWithinObject(consumableSpawnAreas[spawnAreaIndex]);

		}


		public Transform[] getSpawnAreas(){
			return spawnAreas;
		}


		public Transform[] getConsumableSpawnAreas(){
			return consumableSpawnAreas;
		}


		/// <summary>
		/// Grabs a random position within the objects transform.
		/// Area is interpreted as a rectangular prism
		/// </summary>
		/// <returns>A position contained within the area.</returns>
		/// <param name="spawnArea">Spawn area.</param>
		private Vector3 positionWithinObject(Transform spawnArea){
			
			Vector3 pos = spawnArea.transform.position;

			pos.x += Random.Range (-spawnArea.lossyScale.x/2, spawnArea.lossyScale.x/2);
			pos.y += Random.Range (-spawnArea.lossyScale.y/2, spawnArea.lossyScale.y/2);
			pos.z += Random.Range (-spawnArea.lossyScale.z/2, spawnArea.lossyScale.z/2);

			return pos;

		}

	}

}