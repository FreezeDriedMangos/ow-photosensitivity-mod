using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotosensitivityMod
{
	public static class Delay
	{
		public static void FireOnNextUpdate(Action action) => PhotosensitivityMod.Instance.ModHelper.Events.Unity.FireOnNextUpdate(action);
	}
}
