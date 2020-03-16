using System;
using System.Collections.Generic;
using System.Linq;
using Verse;

namespace UseYourGun.Utilities
{
	// Token: 0x0200000A RID: 10
	internal static class WeaponUtility
	{
		// Token: 0x06000021 RID: 33 RVA: 0x000031B4 File Offset: 0x000013B4
		public static List<ThingDef> getAllWeapons()
		{
			List<ThingDef> list = new List<ThingDef>();

			Predicate<ThingDef> isWeapon = (ThingDef td) => td.equipmentType == EquipmentType.Primary && !td.weaponTags.NullOrEmpty<string>() && !td.destroyOnDrop;
			IEnumerable<ThingDef> allDefs = DefDatabase<ThingDef>.AllDefs;
            list = allDefs.ToList().FindAll(isWeapon);
			//Func<ThingDef, bool> searcher;
			//Func<ThingDef, bool> predicate;
			//if ((predicate = <>9__1) == null)
			//{
			//	predicate = (searcher = ((ThingDef td) => isWeapon(td)));
			//}
			//foreach (ThingDef item in allDefs.Where(predicate))
			//{
			//	list.Add(item);
			//}
			return list;
		}
	}
}
