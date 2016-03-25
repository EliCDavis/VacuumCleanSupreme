using UnityEngine;

namespace VGDC.Consumables {

	/// <summary>
	/// Consumable factory that encapsualtes all creation logic for consumable objects
	/// such as battery localy and across a network.
	/// </summary>
	public static class ConsumableFactory  {


		/// <summary>
		/// Creates the consumable in the scene for local players to interact with
		/// </summary>
		/// <returns>The consumable.</returns>
		/// <param name="type">Type.</param>
		/// <param name="position">Position.</param>
		public static GameObject createConsumable(ConsumableType type, Vector3 position){

			// Get a reference to what we want to create
			GameObject consumableRef = getReferenceToType (type);

			// Instantiate the reference into the scene.
			GameObject consumable = (GameObject)GameObject.Instantiate (consumableRef, position, Quaternion.identity);

			return consumable;

		}


		/// <summary>
		/// Creates a photon gameobject instance over the network so everyone
		/// can interact with the consumable
		/// </summary>
		/// <returns>The photon consumable.</returns>
		/// <param name="type">Type of consumable</param>
		/// <param name="position">Position in the scene</param>
		public static GameObject createPhotonConsumable(ConsumableType type, Vector3 position){

			return null;

		}


		/// <summary>
		/// Keeps accessing the resources folder all in one place so that
		/// if anything changes in there, you only have to update this one method 
		/// and no where else in the code when it comes to consumables
		/// </summary>
		/// <returns>The reference to type.</returns>
		/// <param name="type">Type.</param>
		private static GameObject getReferenceToType(ConsumableType type){

			switch(type){

			case ConsumableType.Battery:
				return Resources.Load<GameObject> ("Consumables/Battery");

			case ConsumableType.Trap:
				return Resources.Load<GameObject> ("Consumables/Trap");

			default:
				return null;

			}

		}

	}

}