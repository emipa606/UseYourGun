using System;

namespace UseYourGun
{
	// Token: 0x02000008 RID: 8
	public class WeaponRecord
	{
		// Token: 0x06000015 RID: 21 RVA: 0x000025F8 File Offset: 0x000007F8
		public WeaponRecord(bool isSelected, bool isException, string label)
		{
			this.isException = isException;
			this.isSelected = isSelected;
			this.label = label;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002630 File Offset: 0x00000830
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				this.isSelected.ToString(),
				",",
				this.isException.ToString(),
				",",
				this.label
			});
		}

		// Token: 0x0400000A RID: 10
		public bool isSelected = false;

		// Token: 0x0400000B RID: 11
		public bool isException = false;

		// Token: 0x0400000C RID: 12
		public string label = "";
	}
}
