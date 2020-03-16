using System;
using UnityEngine;

namespace UseYourGun
{
	// Token: 0x02000006 RID: 6
	public static class Extensions
	{
		// Token: 0x0600000A RID: 10 RVA: 0x00002374 File Offset: 0x00000574
		public static bool Contains(this Rect rect, Rect otherRect)
		{
			bool flag = !rect.Contains(new Vector2(otherRect.xMin, otherRect.yMin));
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = !rect.Contains(new Vector2(otherRect.xMin, otherRect.yMax));
				if (flag2)
				{
					result = false;
				}
				else
				{
					bool flag3 = !rect.Contains(new Vector2(otherRect.xMax, otherRect.yMax));
					if (flag3)
					{
						result = false;
					}
					else
					{
						bool flag4 = !rect.Contains(new Vector2(otherRect.xMax, otherRect.yMin));
						result = !flag4;
					}
				}
			}
			return result;
		}
	}
}
