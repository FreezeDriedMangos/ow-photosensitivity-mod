using HarmonyLib;
using UnityEngine;

namespace PhotosensitivityMod
{
	[HarmonyPatch]
	public static class CloudLightningPatches
	{
		[HarmonyPostfix]
		[HarmonyPatch(typeof(CloudLightningGenerator), nameof(CloudLightningGenerator.Awake))]
		public static void EyeLightningGenerator_Awake(EyeLightningGenerator __instance)
		{
			// this slows down the animation
			var durationScale = 3f;
			var durationMin = 3f;
			__instance._lightDuration = new Range
			(
				Mathf.Max(durationMin, __instance._lightDuration.min * durationScale),
				Mathf.Max(durationMin, __instance._lightDuration.max * durationScale)
			);

			for (int i = 0; i < __instance._lightRandomAnimSettings.Length; i++)
            {
				// this prevents the sudden appearance of full brightness lightning
				__instance._lightRandomAnimSettings[i].intensityScale = new AnimationCurve(new Keyframe(0, 0), new Keyframe(0.5f, 0.5f), new Keyframe(1, 0));
				
				// this adjusts the flicker harshness
				var oldKeys = __instance._lightRandomAnimSettings[i].radiusScale.keys;
				Keyframe[] newRadiusKeyframes = new Keyframe[oldKeys.Length];
				for (int j = 0; j < oldKeys.Length; j++)
				{
					newRadiusKeyframes[j] = new Keyframe(oldKeys[j].time, oldKeys[j].value);
				}
				__instance._lightRandomAnimSettings[i].radiusScale = new AnimationCurve(newRadiusKeyframes);
			}
		}

		[HarmonyPrefix]
		[HarmonyPatch(typeof(CloudLightning), nameof(CloudLightning.Awake))]
		public static bool CloudLightning_Awake(CloudLightning __instance)
		{
			// This doesn't do anything most likely, but I have it here just in case a CloudLightning that wasn't spawned by a CloudLightningGenerator exists somewhere 
			__instance._lightLength *= 3f;
			__instance._lightLength = Mathf.Max(3f, __instance._lightLength);

			return true;
		}

		[HarmonyPostfix]
		[HarmonyPatch(typeof(HeatLightningController), nameof(HeatLightningController.Start))]
		public static void HeatLightningController_Start(HeatLightningController __instance)
		{
			__instance._maxflashDuration = 5f;
			__instance._minflashDuration = 4f;
		}
	}
}
