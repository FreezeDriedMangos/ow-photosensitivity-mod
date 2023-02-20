using HarmonyLib;
using UnityEngine;

namespace PhotosensitivityMod
{
	[HarmonyPatch]
	public static class EyeLightningSwapTriggerPatches
	{
		[HarmonyPrefix]
		[HarmonyPatch(typeof(EyeLightningSwapTrigger), nameof(EyeLightningSwapTrigger.OnEntry))]
		public static bool EyeLightningSwapTrigger_OnEntry(EyeLightningSwapTrigger __instance, GameObject hitObj)
		{
			if (hitObj.CompareTag("PlayerDetector"))
			{
				for (int i = 0; i < __instance._lightningGenerators.Length; i++)
				{
					__instance._lightningGenerators[i].enabled = false;
				}
				for (int j = 0; j < __instance._quantumLightningObjects.Length; j++)
				{
					__instance._quantumLightningObjects[j].SetActivation(active: true);
					__instance._quantumLightningObjects[j]._flashDuration *= 2f;
				}
				//_ambientLight.FadeTo(0f, 5f);
				//_cloudEdgeLight.FadeTo(0f, 5f);
			}

			return false;
		}
	}
}
