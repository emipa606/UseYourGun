using System;
using System.Reflection;
using HarmonyLib;
using Verse;

// Token: 0x02000002 RID: 2
[StaticConstructorOnStartup]
internal class Main
{
	// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
	static Main()
	{
		MethodInfo methodInfo = AccessTools.Method(typeof(VerbUtility), "AllowAdjacentShot", null, null);
		HarmonyMethod harmonyMethod = new HarmonyMethod(typeof(RangePatch).GetMethod("Patch_Postfix"));
		Main.harmony.Patch(methodInfo, null, harmonyMethod, null);
	}

	// Token: 0x04000001 RID: 1
	public static Harmony harmony = new Harmony("com.UYG.patch");
}
