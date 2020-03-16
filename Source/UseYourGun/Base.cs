using System;
using System.Collections.Generic;
using HugsLib;
using HugsLib.Settings;
using UnityEngine;
using UseYourGun.Utilities;
using Verse;

namespace UseYourGun
{
    // Token: 0x02000007 RID: 7
    public class Base : ModBase
    {
        // Token: 0x17000002 RID: 2
        // (get) Token: 0x0600000B RID: 11 RVA: 0x00002420 File Offset: 0x00000620
        public override string ModIdentifier
        {
            get
            {
                return "UseYourGun";
            }
        }

        // Token: 0x17000003 RID: 3
        // (get) Token: 0x0600000C RID: 12 RVA: 0x00002437 File Offset: 0x00000637
        // (set) Token: 0x0600000D RID: 13 RVA: 0x0000243E File Offset: 0x0000063E
        public static Base Instance { get; private set; }

        // Token: 0x0600000E RID: 14 RVA: 0x00002446 File Offset: 0x00000646
        public Base()
        {
            Base.Instance = this;
        }

        // Token: 0x0600000F RID: 15 RVA: 0x00002470 File Offset: 0x00000670
        public override void DefsLoaded()
        {
            this.allWeapons = WeaponUtility.getAllWeapons();
            if (Prefs.DevMode) Log.Message($"Found {allWeapons.Count} weapons in the database.");
            Base.tabsHandler = base.Settings.GetHandle<string>("tabs", Translator.Translate("UYG_Tabs_Title"), "", "none", null, null);
            Base.tabsHandler.CustomDrawer = ((Rect rect) => DrawUtility.CustomDrawer_Tabs(rect, Base.tabsHandler, this.tabNames));
            Base.weaponForbidder = base.Settings.GetHandle<DictWeaponRecordHandler>("weaponForbidder_new", null, Translator.Translate("UYG_WeaponForbidder_Description"), null, null, null);
            Base.weaponForbidder.VisibilityPredicate = (() => Base.tabsHandler.Value == this.tabNames[0]);
            Base.weaponForbidder.CustomDrawer = ((Rect rect) => DrawUtility.CustomDrawer_MatchingWeapons_active(rect, Base.weaponForbidder, this.allWeapons, null, Translator.Translate("UYG_Allow"), Translator.Translate("UYG_Forbid")));
            DrawUtility.filterWeapons(ref Base.weaponForbidder, this.allWeapons, null);
        }

        // Token: 0x06000010 RID: 16 RVA: 0x0000252E File Offset: 0x0000072E
        internal void ResetForbidden()
        {
            Base.weaponForbidder.Value = null;
            DrawUtility.filterWeapons(ref Base.weaponForbidder, this.allWeapons, null);
        }

        // Token: 0x04000005 RID: 5
        internal static SettingHandle<DictWeaponRecordHandler> weaponForbidder;

        // Token: 0x04000006 RID: 6
        internal static SettingHandle<string> tabsHandler;

        // Token: 0x04000007 RID: 7
        private List<ThingDef> allWeapons;

        // Token: 0x04000008 RID: 8
        private static Color highlight1 = new Color(0.5f, 0f, 0f, 0.1f);

        // Token: 0x04000009 RID: 9
        private string[] tabNames = new string[]
        {
            Translator.Translate("UYG_tab1")
        };
    }
}
