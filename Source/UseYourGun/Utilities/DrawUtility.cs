using System;
using System.Collections.Generic;
using System.Linq;
using HugsLib.Settings;
using RimWorld;
using UnityEngine;
using Verse;
using Verse.Sound;

namespace UseYourGun.Utilities
{
	// Token: 0x02000009 RID: 9
	internal class DrawUtility
	{
		// Token: 0x06000017 RID: 23 RVA: 0x00002684 File Offset: 0x00000884
		private static void drawBackground(Rect rect, Color background)
		{
			Color color = GUI.color;
			GUI.color = background;
			GUI.DrawTexture(rect, TexUI.FastFillTex);
			GUI.color = color;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000026B4 File Offset: 0x000008B4
		private static void DrawLabel(string labelText, Rect textRect, float offset)
		{
			float num = Text.CalcHeight(labelText, textRect.width);
			num -= 2f;
			Rect rect = new Rect(textRect.x, textRect.yMin - num + offset, textRect.width, num);
			GUI.DrawTexture(rect, TexUI.GrayTextBG);
			GUI.color = Color.white;
			Text.Anchor = TextAnchor.UpperCenter;
			Widgets.Label(rect, labelText);
			Text.Anchor = 0;
			GUI.color = Color.white;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002734 File Offset: 0x00000934
		private static Color getColor(ThingDef weapon)
		{
			bool flag = weapon.graphicData != null;
			Color result;
			if (flag)
			{
				result = weapon.graphicData.color;
			}
			else
			{
				result = Color.white;
			}
			return result;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002768 File Offset: 0x00000968
		private static bool DrawIconForWeapon(ThingDef weapon, KeyValuePair<string, WeaponRecord> item, Rect contentRect, Vector2 iconOffset, int buttonID)
		{
			Texture2D uiIcon = weapon.uiIcon;
			Color color = DrawUtility.getColor(weapon);
			Color color2 = DrawUtility.getColor(weapon);
			Graphic graphic = null;
			bool flag = weapon.graphicData != null && weapon.graphicData.Graphic != null;
			if (flag)
			{
				Graphic graphic2 = weapon.graphicData.Graphic;
				graphic = weapon.graphicData.Graphic.GetColoredVersion(graphic2.Shader, color, color2);
			}
			Rect rect = new Rect(contentRect.x + iconOffset.x, contentRect.y + iconOffset.y, 32f, 32f);
			bool flag2 = !contentRect.Contains(rect);
			bool result;
			if (flag2)
			{
				result = false;
			}
			else
			{
				string label = weapon.label;
				TooltipHandler.TipRegion(rect, label);
				MouseoverSounds.DoRegion(rect, SoundDefOf.Mouseover_Command);
				bool flag3 = Mouse.IsOver(rect);
				if (flag3)
				{
					GUI.color = DrawUtility.iconMouseOverColor;
					GUI.DrawTexture(rect, ContentFinder<Texture2D>.Get("square", true));
				}
				else
				{
					bool isException = item.Value.isException;
					if (isException)
					{
						GUI.color = DrawUtility.iconMouseOverColor;
						GUI.DrawTexture(rect, ContentFinder<Texture2D>.Get("square", true));
					}
					else
					{
						GUI.color = DrawUtility.iconBaseColor;
						GUI.DrawTexture(rect, ContentFinder<Texture2D>.Get("square", true));
					}
				}
				bool flag4 = !weapon.uiIconPath.NullOrEmpty();
				Texture texture;
				if (flag4)
				{
					texture = weapon.uiIcon;
				}
				else
				{
					bool flag5 = graphic != null;
					if (flag5)
					{
						texture = graphic.MatSingle.mainTexture;
					}
					else
					{
						texture = new Texture2D((int)graphic.drawSize.x, (int)graphic.drawSize.y);
					}
				}
				GUI.color = color;
				GUI.DrawTexture(rect, texture);
				GUI.color = Color.white;
				bool flag6 = Widgets.ButtonInvisible(rect, true);
				if (flag6)
				{
					Event.current.button = buttonID;
					result = true;
				}
				else
				{
					result = false;
				}
			}
			return result;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x0000294C File Offset: 0x00000B4C
		public static bool CustomDrawer_Filter(Rect rect, SettingHandle<float> slider, bool def_isPercentage, float def_min, float def_max, Color background)
		{
			DrawUtility.drawBackground(rect, background);
			int num = 50;
			Rect rect2 = new Rect(rect);
			rect2.width -= (float)num;
			Rect rect3 = new Rect(rect);
			rect3.width = (float)num;
			rect3.position = new Vector2(rect2.position.x + rect2.width + 5f, rect2.position.y + 4f);
			rect2 = GenUI.ContractedBy(rect2, 2f);
			if (def_isPercentage)
			{
				Widgets.Label(rect3, Mathf.Round(slider.Value * 100f).ToString("F0") + "%");
			}
			else
			{
				Widgets.Label(rect3, slider.Value.ToString("F2"));
			}
			float num2 = Widgets.HorizontalSlider(rect2, slider.Value, def_min, def_max, true, null, null, null, -1f);
			bool result = false;
			bool flag = slider.Value != num2;
			if (flag)
			{
				result = true;
			}
			slider.Value = num2;
			return result;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002A6C File Offset: 0x00000C6C
		public static bool CustomDrawer_Tabs(Rect rect, SettingHandle<string> selected, string[] defaultValues)
		{
			int num = 140;
			int num2 = 0;
			bool result = false;
			foreach (string text in defaultValues)
			{
				Rect rect2 = new Rect(rect);
				rect2.width = (float)num;
				rect2.position = new Vector2(rect2.position.x + (float)num2, rect2.position.y);
				Color color = GUI.color;
				bool flag = text == selected.Value;
				bool flag2 = flag;
				if (flag2)
				{
					GUI.color = DrawUtility.SelectedOptionColor;
				}
				bool flag3 = Widgets.ButtonText(rect2, text, true, false, true);
				bool flag4 = flag;
				if (flag4)
				{
					GUI.color = color;
				}
				bool flag5 = flag3;
				if (flag5)
				{
					bool flag6 = selected.Value != text;
					if (flag6)
					{
						selected.Value = text;
					}
					else
					{
						selected.Value = "none";
					}
					result = true;
				}
				num2 += num;
			}
			return result;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002B70 File Offset: 0x00000D70
		internal static void filterWeapons(ref SettingHandle<DictWeaponRecordHandler> setting, List<ThingDef> allWeapons, SettingHandle<float> filter = null)
		{
			bool flag = setting.Value == null;
			if (flag)
			{
				setting.Value = new DictWeaponRecordHandler();
			}
			Dictionary<string, WeaponRecord> dictionary = new Dictionary<string, WeaponRecord>();
			foreach (ThingDef thingDef in allWeapons)
			{
				bool flag2 = false;
				bool flag3 = filter != null;
				if (flag3)
				{
					float statValueAbstract = thingDef.GetStatValueAbstract(StatDefOf.Mass, null);
					flag2 = (statValueAbstract >= filter.Value);
				}
				WeaponRecord weaponRecord = null;
				bool flag4 = setting.Value.InnerList.TryGetValue(thingDef.defName, out weaponRecord);
				bool flag5 = flag4 && weaponRecord.isException;
				if (flag5)
				{
					dictionary.Add(thingDef.defName, weaponRecord);
				}
				else
				{
					DefModExtension_SettingDefaults modExtension;
					bool flag6 = (modExtension = thingDef.GetModExtension<DefModExtension_SettingDefaults>()) != null && modExtension.weaponForbidden;
					flag2 = ((filter == null) ? flag6 : flag2);
					dictionary.Add(thingDef.defName, new WeaponRecord(flag2, false, thingDef.label));
				}
			}
			dictionary = (from d in dictionary
			orderby d.Value.label
			select d).ToDictionary((KeyValuePair<string, WeaponRecord> d) => d.Key, (KeyValuePair<string, WeaponRecord> d) => d.Value);
			setting.Value.InnerList = dictionary;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002D0C File Offset: 0x00000F0C
		internal static bool CustomDrawer_MatchingWeapons_active(Rect wholeRect, SettingHandle<DictWeaponRecordHandler> setting, List<ThingDef> allWeapons, SettingHandle<float> filter = null, string yesText = "Light weapon", string noText = "Heavy weapon")
		{
			DrawUtility.drawBackground(wholeRect, DrawUtility.background);
			GUI.color = Color.white;
			Rect rect = new Rect(wholeRect);
			rect.width = rect.width;
			rect.height = wholeRect.height - 20f + 2f;
			rect.position = new Vector2(rect.position.x - 275f, rect.position.y);
			Rect rect2 = new Rect(wholeRect);
			rect2.width = rect2.width;
			rect.height = wholeRect.height - 20f + 2f;
			rect2.position = new Vector2(rect2.position.x - 275f + rect.width, rect2.position.y);
			DrawUtility.DrawLabel(yesText, rect, 20f);
			DrawUtility.DrawLabel(noText, rect2, 20f);
			rect.position = new Vector2(rect.position.x, rect.position.y + 20f);
			rect2.position = new Vector2(rect2.position.x, rect2.position.y + 20f);
			int num = (int)(rect.width / 33f);
			bool flag = false;
			int num2 = 0;
			DrawUtility.filterWeapons(ref setting, allWeapons, filter);
			Dictionary<string, WeaponRecord> innerList = setting.Value.InnerList;
			foreach (KeyValuePair<string, WeaponRecord> keyValuePair in innerList)
			{
				bool isSelected = keyValuePair.Value.isSelected;
				if (isSelected)
				{
					num2++;
				}
			}
			int num3 = Math.Max(num2 / num, (innerList.Count - num2) / num) + 1;
			setting.CustomDrawerHeight = (float)num3 * 32f + (float)num3 * 1f + 20f;
			Dictionary<string, ThingDef> dictionary = allWeapons.ToDictionary((ThingDef o) => o.defName, (ThingDef o) => o);
			int num4 = 0;
			int num5 = 0;
			foreach (KeyValuePair<string, WeaponRecord> item in innerList)
			{
				Rect contentRect = item.Value.isSelected ? rect2 : rect;
				int num6 = item.Value.isSelected ? num5 : num4;
				bool isSelected2 = item.Value.isSelected;
				if (isSelected2)
				{
					num5++;
				}
				else
				{
					num4++;
				}
				int num7 = num6 % num;
				int num8 = num6 / num;
				ThingDef weapon = null;
				bool flag2 = dictionary.TryGetValue(item.Key, out weapon);
				bool flag3 = false;
				bool flag4 = flag2;
				if (flag4)
				{
					flag3 = DrawUtility.DrawIconForWeapon(weapon, item, contentRect, new Vector2(32f * (float)num7 + (float)num7 * 1f, 32f * (float)num8 + (float)num8 * 1f), num6);
				}
				bool flag5 = flag3;
				if (flag5)
				{
					flag = true;
					item.Value.isSelected = !item.Value.isSelected;
					item.Value.isException = !item.Value.isException;
				}
			}
			bool flag6 = flag;
			if (flag6)
			{
				setting.Value.InnerList = innerList;
			}
			return flag;
		}

		// Token: 0x0400000D RID: 13
		private const float ContentPadding = 5f;

		// Token: 0x0400000E RID: 14
		private const float IconSize = 32f;

		// Token: 0x0400000F RID: 15
		private const float IconGap = 1f;

		// Token: 0x04000010 RID: 16
		private const float TextMargin = 20f;

		// Token: 0x04000011 RID: 17
		private const float BottomMargin = 2f;

		// Token: 0x04000012 RID: 18
		private static readonly Color iconBaseColor = new Color(0.5f, 0.5f, 0.5f, 1f);

		// Token: 0x04000013 RID: 19
		private static readonly Color iconMouseOverColor = new Color(0.6f, 0.6f, 0.4f, 1f);

		// Token: 0x04000014 RID: 20
		private static Color background = new Color(0.5f, 0f, 0f, 0.1f);

		// Token: 0x04000015 RID: 21
		private static readonly Color SelectedOptionColor = new Color(0.5f, 1f, 0.5f, 1f);

		// Token: 0x04000016 RID: 22
		private static readonly Color constGrey = new Color(0.8f, 0.8f, 0.8f, 1f);

		// Token: 0x04000017 RID: 23
		private static Color exceptionBackground = new Color(0f, 0.5f, 0f, 0.1f);
	}
}
