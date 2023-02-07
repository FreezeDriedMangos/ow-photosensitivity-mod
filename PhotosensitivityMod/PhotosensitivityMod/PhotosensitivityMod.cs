using HarmonyLib;
using OWML.Common;
using OWML.ModHelper;
using System.Reflection;

namespace PhotosensitivityMod
{
	public class PhotosensitivityMod : ModBehaviour
	{
		public static PhotosensitivityMod Instance { get; private set; }

		private void Awake()
		{
			Instance = this;
		}

		private void Start()
		{
			Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly());

			Write($"{nameof(PhotosensitivityMod)} is loaded!");

			LoadManager.OnCompleteSceneLoad += (scene, loadScene) =>
			{
				switch (loadScene)
				{
					case OWScene.EyeOfTheUniverse:
						HandleEyeScene();
						break;
					case OWScene.SolarSystem:
						HandleSolarSystem();
						break;
				}
			};
		}

		public void HandleEyeScene()
		{

		}

		public void HandleSolarSystem()
		{

		}

		public static void Write(string msg)
		{
			Instance.ModHelper.Console.WriteLine(msg, MessageType.Info);
		}
	}
}