using System;
using System.Collections.Generic;
using System.Linq;
using HugsLib.Settings;

namespace UseYourGun
{
	// Token: 0x02000004 RID: 4
	internal class DictWeaponRecordHandler : SettingHandleConvertible
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000004 RID: 4 RVA: 0x00002144 File Offset: 0x00000344
		// (set) Token: 0x06000005 RID: 5 RVA: 0x0000215C File Offset: 0x0000035C
		public Dictionary<string, WeaponRecord> InnerList
		{
			get
			{
				return this.inner;
			}
			set
			{
				this.inner = value;
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002168 File Offset: 0x00000368
		public override void FromString(string settingValue)
		{
			this.inner = new Dictionary<string, WeaponRecord>();
			bool flag = !settingValue.Equals(string.Empty);
			if (flag)
			{
				foreach (string text in settingValue.Split(new char[]
				{
					'|'
				}))
				{
					string[] source = text.Split(new char[]
					{
						','
					});
					bool flag2 = source.Count<string>() < 4;
					if (flag2)
					{
						this.inner.Add(text.Split(new char[]
						{
							','
						})[0], new WeaponRecord(Convert.ToBoolean(text.Split(new char[]
						{
							','
						})[1]), Convert.ToBoolean(text.Split(new char[]
						{
							','
						})[2]), ""));
					}
					else
					{
						this.inner.Add(text.Split(new char[]
						{
							','
						})[0], new WeaponRecord(Convert.ToBoolean(text.Split(new char[]
						{
							','
						})[1]), Convert.ToBoolean(text.Split(new char[]
						{
							','
						})[2]), text.Split(new char[]
						{
							','
						})[3]));
					}
				}
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000022B0 File Offset: 0x000004B0
		public override string ToString()
		{
			List<string> list = new List<string>();
			foreach (KeyValuePair<string, WeaponRecord> keyValuePair in this.inner)
			{
				list.Add(keyValuePair.Key + "," + keyValuePair.Value.ToString());
			}
			return (this.inner != null) ? string.Join("|", list.ToArray()) : "";
		}

		// Token: 0x04000002 RID: 2
		public Dictionary<string, WeaponRecord> inner = new Dictionary<string, WeaponRecord>();
	}
}
