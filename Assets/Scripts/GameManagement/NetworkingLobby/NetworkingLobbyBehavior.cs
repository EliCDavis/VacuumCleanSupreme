using UnityEngine;
using System.Collections;
using VGDC.Settings;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon;

namespace VGDC.GameManagement.NetworkingLobby {

	public class NetworkingLobbyBehavior : Photon.MonoBehaviour {

		[SerializeField]
		private Image profilePic;


		[SerializeField]
		private Text onlineName;


		[SerializeField]
		private GameObject editPlayerPanel;

		[SerializeField]
		private GameObject roomListingsPanel;

		[SerializeField]
		private GameObject roomPrefab;

		/// <summary>
		/// The time in seconds of when the last room refresh occured
		/// </summary>
		private float lastRefreshTime = 0f;


		/// <summary>
		/// Asynchronus so that we can load in the users profile image
		/// </summary>
		IEnumerator Start() {

			onlineName.text = OnlineSettings.getInstance ().Usename;

			// Start a download of the given URL
			WWW www = new WWW(OnlineSettings.getInstance().PicURL);

			// Wait for download to complete
			yield return www;

			// assign texture
			profilePic.sprite = Sprite.Create(www.texture, new Rect(0,0, www.texture.width, www.texture.height), Vector2.one/2f);

		}


		/// <summary>
		/// Opens the edit profile menu and set's the input fields 
		/// to what the online settings are currentely
		/// </summary>
		public void openEditMenu(){
			editPlayerPanel.SetActive (true);
			editPlayerPanel.transform.FindChild ("InputUsernameField").GetComponentInChildren<InputField>().text = OnlineSettings.getInstance ().Usename;
			editPlayerPanel.transform.FindChild ("InputPicURLField").GetComponentInChildren<InputField>().text = OnlineSettings.getInstance().PicURL;
		}


		/// <summary>
		/// Saves the edit options, and refreshes the scene so changes
		/// can be seen.
		/// </summary>
		public void saveEditOptions(){
			OnlineSettings.getInstance ().Usename = editPlayerPanel.transform.FindChild ("InputUsernameField").GetComponentInChildren<InputField> ().text;
			OnlineSettings.getInstance ().PicURL = editPlayerPanel.transform.FindChild ("InputPicURLField").GetComponentInChildren<InputField> ().text;
			SceneManager.LoadScene (SceneManager.GetActiveScene().name);
		}

		public void OnPhotonPlayerConnected(PhotonPlayer other)
		{
			Debug.Log( "OnPhotonPlayerConnected() " + other.name ); // not seen if you're the player connecting
		}


		public void OnJoinedLobby(){

			refreshRoomsAvailable ();

		}


		/// <summary>
		/// Refreshs the rooms available.
		/// </summary>
		public void refreshRoomsAvailable(){
			
			lastRefreshTime = Time.time;

			// Destroy Current Room listings
			for (int i = 0; i < roomListingsPanel.transform.childCount; i++) {
				Destroy (roomListingsPanel.transform.GetChild(i));
			}

			RoomInfo[] rooms = PhotonNetwork.GetRoomList();

			for (int i = 0; i < rooms.Length; i++) {
			
			}

		}


		public void Update(){

			// how often the page refreshes
			float refreshRate = 2f;

			// If it's time to do a refresh
			if (lastRefreshTime + refreshRate < Time.time) {
				refreshRoomsAvailable ();
			}

		}

	}

}