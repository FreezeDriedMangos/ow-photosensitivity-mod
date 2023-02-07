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
			Delay.FireOnNextUpdate(() => { 
				for (int i = 0; i < __instance._lightRandomAnimSettings.Length; i++)
                {
					//__instance._lightRandomAnimSettings[i].radiusScale = new AnimationCurve(new Keyframe(0, 1), new Keyframe(1, 1));
					__instance._lightRandomAnimSettings[i].intensityScale = new AnimationCurve(new Keyframe(0, 0), new Keyframe(0.5f, 1), new Keyframe(1, 0));
				}
			});
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
