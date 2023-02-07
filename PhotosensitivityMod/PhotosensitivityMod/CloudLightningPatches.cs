using HarmonyLib;
using UnityEngine;

namespace PhotosensitivityMod
{
	[HarmonyPatch]
	public static class CloudLightningPatches
	{
		[HarmonyPostfix]
		[HarmonyPatch(typeof(CloudLightningGenerator), nameof(CloudLightningGenerator.Awake))]
		[HarmonyPatch(typeof(CloudLightning), nameof(CloudLightning.Awake))]
		public static void CloudLightning_Awake(Component __instance)
		{
			Delay.FireOnNextUpdate(() => GameObject.Destroy(__instance.gameObject));
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
