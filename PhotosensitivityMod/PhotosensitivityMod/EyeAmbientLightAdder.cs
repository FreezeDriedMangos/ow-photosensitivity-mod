using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace PhotosensitivityMod
{
	public class EyeAmbientLightAdder : MonoBehaviour
	{
		private static bool lightBuilt = false;

		public void OnTriggerEnter(Collider c) {
			if (c.attachedRigidbody != Locator.GetPlayerBody()._rigidbody) return;

			if (lightBuilt) return;
			lightBuilt = true;

			PhotosensitivityMod.Instance.ModHelper.Console.WriteLine("BUILDING AMBIENT LIGHT!");

			// reduce dynamic range of lightning strikes
			// add ambient light to the eye surface
			// range 150, intensity 0.6
			var ambientLightParent = GameObject.Find("EyeOfTheUniverse_Body/Sector_EyeOfTheUniverse/SixthPlanet_Root/Sector_EyeSurface/Lighting_EyeSurface/");
			var ambientLight = new GameObject("Ambient Light");
			ambientLight.transform.parent = ambientLightParent.transform;
			ambientLight.transform.localPosition = Vector3.zero;
			var light = ambientLight.AddComponent<Light>();
			light.range = 150;
			light.intensity = 0.6f;
		}
	}
}
