using HarmonyLib;
using OWML.Common;
using OWML.ModHelper;
using System.Reflection;
using System.Linq;
using UnityEngine;

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

				// reduce intensity of lightning bolts

				Material newLightningMaterial = null;
				var lightnings = UnityEngine.Resources.FindObjectsOfTypeAll<QuantumLightningObject>();
				foreach(var lightning in lightnings) {
					var lightningRenderer = lightning.GetComponentInChildren<ParticleSystemRenderer>();

					if (newLightningMaterial == null) {
						newLightningMaterial = lightningRenderer.material;
						newLightningMaterial.color = new Color(0.6466f, 0.5322f, 1.2129f, 1f);
					}

					lightningRenderer.material = newLightningMaterial;

					// TODO: delete the light?

				}
			};
		}

		public void HandleEyeScene()
		{
			// TODO: move this to a patch on the vessel teleporter so it only appears once the player's on the surface

			// surface volume radius 450
			var surfaceSectorGO = GameObject.Find("EyeOfTheUniverse_Body/Sector_EyeOfTheUniverse/SixthPlanet_Root/Sector_EyeSurface");
			var trigger = surfaceSectorGO.AddComponent<SphereCollider>();
			trigger.isTrigger = true;
			trigger.radius = 450;
			surfaceSectorGO.AddComponent<EyeAmbientLightAdder>();

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