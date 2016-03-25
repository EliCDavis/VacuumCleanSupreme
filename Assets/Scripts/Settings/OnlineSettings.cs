using System.Xml.Serialization;
using System.IO;

namespace VGDC.Settings {

	public class OnlineSettings {

		static OnlineSettings instance = null;

		/// <summary>
		/// Singleton pattern insuring theirs only one instance of the settings
		/// at any given time.
		/// </summary>
		/// <returns>The instance.</returns>
		public static OnlineSettings getInstance(){

			if (instance == null) {

				OnlineSettings loadAttempt = load ();

				if (loadAttempt == null) {
					instance = new OnlineSettings ();
				} else {
					instance = loadAttempt;
				}

			}

			return instance;

		}

		string usename;

		string picURL;

		private static bool currentlyLoading = false;

		private OnlineSettings(){
			usename = "Guest";
			picURL = "http://med-fom-endocrinology.sites.olt.ubc.ca/files/2012/08/cropped-profile-place-holder-img18.jpg";
		}


		public string PicURL {
			get {
				return picURL;
			}
			set {
				picURL = value;
				save ();
			}
		}


		public string Usename {
			get {
				return usename;
			}
			set {
				usename = value;
				save ();
			}
		}


		/// <summary>
		/// Writes the settings to an xml file
		/// </summary>
		private void save(){

			// Can't have multiple filestreams going at once, let's chill
			if (currentlyLoading) {
				return;
			}

			string path = "Storage/OnlineSettings.xml";

			using(FileStream outputFile = File.Create(path)){

				XmlSerializer formatter = new XmlSerializer (typeof(OnlineSettings));
				formatter.Serialize (outputFile, this);
				outputFile.Close ();

			}

		}


		/// <summary>
		/// Loads an xml file into a settings object if one exists
		/// </summary>
		private static OnlineSettings load(){

			currentlyLoading = true;

			// Load in the new recording
			XmlSerializer serializer = new XmlSerializer (typeof(OnlineSettings));

			try{
				using (FileStream fileStream = new FileStream("Storage/OnlineSettings.xml", FileMode.Open)) 
				{
					OnlineSettings result = (OnlineSettings) serializer.Deserialize(fileStream);
					fileStream.Close ();
					currentlyLoading = false;
					return result;
				}
			} catch{
				currentlyLoading = false;
				return null;
			}


		}

	}

}