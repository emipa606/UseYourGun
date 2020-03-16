using System;
using UseYourGun;
using Verse;

// Token: 0x02000003 RID: 3
internal static class RangePatch
{
	// Token: 0x06000003 RID: 3 RVA: 0x000020B8 File Offset: 0x000002B8
	public static void Patch_Postfix(ref bool __result, ref Thing caster)
	{
		bool flag = caster is Pawn;
		if (flag)
		{
			Pawn pawn = caster as Pawn;
			bool flag2 = pawn.equipment != null && pawn.equipment.Primary != null;
			if (flag2)
			{
				WeaponRecord weaponRecord;
				bool flag3 = Base.weaponForbidder.Value.InnerList.TryGetValue(pawn.equipment.Primary.def.defName, out weaponRecord);
				bool flag4 = flag3 && weaponRecord.isSelected;
				if (!flag4)
				{
					__result = true;
				}
			}
		}
	}
}
