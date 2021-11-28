using System;
using System.Collections.Generic;
using System.IO;
using BepInEx;
using HarmonyLib;
using System.Reflection;
using BepInEx.Configuration;
using BepInEx.Logging;
using ItemManager;
using UnityEngine;

namespace LightSabers
{
    [BepInPlugin(ModGUID, ModName, ModVersion)]
    public class LightSabers : BaseUnityPlugin
    {
        internal const string ModName = "LightSabers";
        internal const string ModVersion = "1.0.0";
        private const string ModGUID = "odinplus.LightSabers";
        private readonly Harmony _harmony = new(ModGUID);
        internal static readonly ManualLogSource LSLogger = BepInEx.Logging.Logger.CreateLogSource(ModName);
        private ConfigFile localizationFile;

        /* Items */
        private static Item SaberGreen;
        private static Item SaberRed;
        private static Item SaberDark;
        private static Item SaberBlue;
        private static Item SaberPurple;
        private static Item SaberPink;
        private static Item SaberOrange;

        /* Crystals */
        private static Item SaberCrystal_Green;
        private static Item SaberCrystal_Red;
        private static Item SaberCrystal_Dark;
        private static Item SaberCrystal_Blue;
        private static Item SaberCrystal_Purple;
        private static Item SaberCrystal_Pink;
        private static Item SaberCrystal_Orange;


        private readonly Dictionary<string, ConfigEntry<string>> m_localizedStrings =
            new();


        public void Awake()
        {
            /* Add SFX */
            PrefabManager.RegisterPrefab(PrefabManager.RegisterAssetBundle("lightsabers"), "sfx_saber_swing");
            PrefabManager.RegisterPrefab(PrefabManager.RegisterAssetBundle("lightsabers"), "sfx_saber_hit");
            /* Add SaberCrystals */
            SaberCrystal_Green = new Item("lightsabers", "SaberCrystal_Green");
            SaberCrystal_Green.Crafting.Add(CraftingTable.Forge, 3);
            SaberCrystal_Green.RequiredItems.Add("Crystal", 50);
            SaberCrystal_Green.CraftAmount = 1;

            SaberCrystal_Red = new Item("lightsabers", "SaberCrystal_Red");
            SaberCrystal_Red.Crafting.Add(CraftingTable.Forge, 3);
            SaberCrystal_Red.RequiredItems.Add("Crystal", 50);
            SaberCrystal_Red.CraftAmount = 1;


            SaberCrystal_Dark = new Item("lightsabers", "SaberCrystal_Dark");
            SaberCrystal_Dark.Crafting.Add(CraftingTable.Forge, 3);
            SaberCrystal_Dark.RequiredItems.Add("Crystal", 50);
            SaberCrystal_Dark.CraftAmount = 1;


            SaberCrystal_Blue = new Item("lightsabers", "SaberCrystal_Blue");
            SaberCrystal_Blue.Crafting.Add(CraftingTable.Forge, 3);
            SaberCrystal_Blue.RequiredItems.Add("Crystal", 50);
            SaberCrystal_Blue.CraftAmount = 1;


            SaberCrystal_Purple = new Item("lightsabers", "SaberCrystal_Purple");
            SaberCrystal_Purple.Crafting.Add(CraftingTable.Forge, 3);
            SaberCrystal_Purple.RequiredItems.Add("Crystal", 50);
            SaberCrystal_Purple.CraftAmount = 1;


            SaberCrystal_Pink = new Item("lightsabers", "SaberCrystal_Pink");
            SaberCrystal_Pink.Crafting.Add(CraftingTable.Forge, 3);
            SaberCrystal_Pink.RequiredItems.Add("Crystal", 50);
            SaberCrystal_Pink.CraftAmount = 1;


            SaberCrystal_Orange = new Item("lightsabers", "SaberCrystal_Orange");
            SaberCrystal_Orange.Crafting.Add(CraftingTable.Forge, 3);
            SaberCrystal_Orange.RequiredItems.Add("Crystal", 50);
            SaberCrystal_Orange.CraftAmount = 1;


            SaberRed = new Item("lightsabers", "LightSaber_Red");
            SaberRed.Crafting.Add(CraftingTable.Forge, 3);
            SaberRed.RequiredItems.Add("SaberCrystal_Red", 20);
            SaberRed.RequiredItems.Add("Silver", 40);
            SaberRed.RequiredUpgradeItems.Add("Iron",
                20); // Upgrade requirements are per item, even if you craft two at the same time
            SaberRed.RequiredUpgradeItems.Add("Silver",
                10); // 10 Silver: You need 10 silver for level 2, 20 silver for level 3, 30 silver for level 4
            SaberRed.CraftAmount = 1;

            SaberGreen = new Item("lightsabers", "LightSaber_Green");
            SaberGreen.Crafting.Add(CraftingTable.Forge, 3);
            SaberGreen.RequiredItems.Add("SaberCrystal_Green", 20);
            SaberGreen.RequiredItems.Add("Silver", 40);
            SaberGreen.RequiredUpgradeItems.Add("Iron",
                20); // Upgrade requirements are per item, even if you craft two at the same time
            SaberGreen.RequiredUpgradeItems.Add("Silver",
                10); // 10 Silver: You need 10 silver for level 2, 20 silver for level 3, 30 silver for level 4
            SaberGreen.CraftAmount = 1;

            SaberDark = new Item("lightsabers", "LightSaber_Dark");
            SaberDark.Crafting.Add(CraftingTable.Forge, 3);
            SaberDark.RequiredItems.Add("SaberCrystal_Dark", 20);
            SaberDark.RequiredItems.Add("Silver", 40);
            SaberDark.RequiredUpgradeItems.Add("Iron",
                20); // Upgrade requirements are per item, even if you craft two at the same time
            SaberDark.RequiredUpgradeItems.Add("Silver",
                10); // 10 Silver: You need 10 silver for level 2, 20 silver for level 3, 30 silver for level 4
            SaberDark.CraftAmount = 1;

            SaberBlue = new Item("lightsabers", "LightSaber_Blue");
            SaberBlue.Crafting.Add(CraftingTable.Forge, 3);
            SaberBlue.RequiredItems.Add("SaberCrystal_Blue", 20);
            SaberBlue.RequiredItems.Add("Silver", 40);
            SaberBlue.RequiredUpgradeItems.Add("Iron",
                20); // Upgrade requirements are per item, even if you craft two at the same time
            SaberBlue.RequiredUpgradeItems.Add("Silver",
                10); // 10 Silver: You need 10 silver for level 2, 20 silver for level 3, 30 silver for level 4
            SaberBlue.CraftAmount = 1;

            SaberPurple = new Item("lightsabers", "LightSaber_Purple");
            SaberPurple.Crafting.Add(CraftingTable.Forge, 3);
            SaberPurple.RequiredItems.Add("SaberCrystal_Purple", 20);
            SaberPurple.RequiredItems.Add("Silver", 40);
            SaberPurple.RequiredUpgradeItems
                .Add("Iron", 20); // Upgrade requirements are per item, even if you craft two at the same time
            SaberPurple.RequiredUpgradeItems.Add("Silver",
                10); // 10 Silver: You need 10 silver for level 2, 20 silver for level 3, 30 silver for level 4
            SaberPurple.CraftAmount = 1;

            SaberPink = new Item("lightsabers", "LightSaber_Pink");
            SaberPink.Crafting.Add(CraftingTable.Forge, 3);
            SaberPink.RequiredItems.Add("SaberCrystal_Pink", 20);
            SaberPink.RequiredItems.Add("Silver", 40);
            SaberPink.RequiredUpgradeItems.Add("Iron",
                20); // Upgrade requirements are per item, even if you craft two at the same time
            SaberPink.RequiredUpgradeItems.Add("Silver",
                10); // 10 Silver: You need 10 silver for level 2, 20 silver for level 3, 30 silver for level 4
            SaberPink.CraftAmount = 1;


            SaberOrange = new Item("lightsabers", "LightSaber_Orange");
            SaberOrange.Crafting.Add(CraftingTable.Forge, 3);
            SaberOrange.RequiredItems.Add("SaberCrystal_Orange", 20);
            SaberOrange.RequiredItems.Add("Silver", 40);
            SaberOrange.RequiredUpgradeItems
                .Add("Iron", 20); // Upgrade requirements are per item, even if you craft two at the same time
            SaberOrange.RequiredUpgradeItems.Add("Silver",
                10); // 10 Silver: You need 10 silver for level 2, 20 silver for level 3, 30 silver for level 4
            SaberOrange.CraftAmount = 1;
            
            /* Damage */
            /*baseDamage = Config.Bind("Damage", "Base Damage", 500, "");
            baseBlunt = Config.Bind("Damage", "Base Blunt Damage", 0, "");
            baseSlash = Config.Bind("Damage", "Base Slash Damage", 0, "");
            basePierce = Config.Bind("Damage", "Base Pierce Damage", 0, "");
            baseChop = Config.Bind("Damage", "Base Chop Damage", 0, "");
            basePickaxe = Config.Bind("Damage", "Base Pickaxe Damage", 0, "");
            baseFire = Config.Bind("Damage", "Base Fire Damage", 500, "");
            baseFrost = Config.Bind("Damage", "Base Frost Damage", 0, "");
            baseLightning = Config.Bind("Damage", "Base Lightning Damage", 500, "");
            basePoison = Config.Bind("Damage", "Base Poison Damage", 0, "");
            baseSpirit = Config.Bind("Damage", "Base Spirit Damage", 0, "");
            baseDamagePerPerLevel = Config.Bind("Damage", "Base Damage Per Level", 50, "");
            baseBluntPerLevel = Config.Bind("Damage", "Base Blunt Damage Per Level", 100, "");
            baseSlashPerLevel = Config.Bind("Damage", "Base Slash Damage Per Level", 0, "");
            basePiercePerLevel = Config.Bind("Damage", "Base Pierce Damage Per Level", 0, "");
            baseChopPerLevel = Config.Bind("Damage", "Base Chop Damage Per Level", 0, "");
            basePickaxePerLevel = Config.Bind("Damage", "Base Pickaxe Damage Per Level", 0, "");
            baseFirePerLevel = Config.Bind("Damage", "Base Fire Damage Per Level", 50, "");
            baseFrostPerLevel = Config.Bind("Damage", "Base Frost Damage Per Level", 0, "");
            baseLightningPerLevel = Config.Bind("Damage", "Base Lightning Damage Per Level", 200, "");
            basePoisonPerLevel = Config.Bind("Damage", "Base Poison Damage Per Level", 0, "");
            baseSpiritPerLevel = Config.Bind("Damage", "Base Spirit Damage Per Level", 0, "");
            baseAttackForce = Config.Bind("Damage", "Base Attack Force (a.k.a Knockback)", 200, "");
            baseBlockPower = Config.Bind("Damage", "Base Block Power", 500, "");
            baseParryForce = Config.Bind("Damage", "Base Parry Force", 20, "");
            baseBackstab = Config.Bind("Damage", "Base Backstab Bonus", 3, "");
            baseBackstab = Config.Bind("Durability", "Base Durability", 800, "");*/


            localizationFile =
                new ConfigFile(
                    Path.Combine(Path.GetDirectoryName(Config.ConfigFilePath) ?? throw new InvalidOperationException(),
                        ModGUID.ToLower() + ".Localization.cfg"), false);
            Assembly assembly = Assembly.GetExecutingAssembly();
            _harmony.PatchAll(assembly);
            Localize();
        }

        private void OnDestroy()
        {
            _harmony?.UnpatchSelf();
        }

        [HarmonyPatch(typeof(ZNetScene), nameof(ZNetScene.Awake))]
        private static class Sabers_SFXFix
        {
            private static void Postfix()
            {
                /* Tell the SFX to respect audio mixer of the game */
                GameObject saberhit = ZNetScene.instance.GetPrefab("sfx_saber_hit");
                GameObject saberswing = ZNetScene.instance.GetPrefab("sfx_saber_swing");
                
                try
                {
                    saberhit.GetComponentInChildren<AudioSource>().outputAudioMixerGroup =
                        AudioMan.instance.m_ambientMixer;
                    saberswing.GetComponentInChildren<AudioSource>().outputAudioMixerGroup =
                        AudioMan.instance.m_ambientMixer;
                }
                catch
                {
                    LSLogger.LogError(
                        "AudioMan.instance.m_ambientMixer could not be assigned on outputAudioMixerGroup of saber sfx");
                }
            }
        }


        /*[HarmonyPatch(typeof(ZNet), nameof(ZNet.OnNewConnection))]
        private static class Sabers_SyncConfigDamage
        {
            private static void Postfix()
            {
                InitDamageValues(SaberGreen.Prefab.GetComponent<ItemDrop>());
                InitDamageValues(SaberRed.Prefab.GetComponent<ItemDrop>());
                InitDamageValues(SaberDark.Prefab.GetComponent<ItemDrop>());
                InitDamageValues(SaberBlue.Prefab.GetComponent<ItemDrop>());
                InitDamageValues(SaberPurple.Prefab.GetComponent<ItemDrop>());
                InitDamageValues(SaberPink.Prefab.GetComponent<ItemDrop>());
                InitDamageValues(SaberOrange.Prefab.GetComponent<ItemDrop>());
            }
        }

        private static void InitDamageValues(ItemDrop item)
        {
            ItemDrop? itmdrop = item;
            LSLogger.LogDebug($"Setting damage values for {itmdrop.gameObject.name}");
            itmdrop.m_itemData.m_shared.m_damages.m_damage = baseDamage.Value;
            itmdrop.m_itemData.m_shared.m_toolTier = baseBlunt.Value;
            itmdrop.m_itemData.m_shared.m_damages.m_blunt = baseBlunt.Value;
            itmdrop.m_itemData.m_shared.m_damages.m_slash = baseSlash.Value;
            itmdrop.m_itemData.m_shared.m_damages.m_pierce = basePierce.Value;
            itmdrop.m_itemData.m_shared.m_damages.m_chop = baseChop.Value;
            itmdrop.m_itemData.m_shared.m_damages.m_pickaxe = basePickaxe.Value;
            itmdrop.m_itemData.m_shared.m_damages.m_fire = baseFire.Value;
            itmdrop.m_itemData.m_shared.m_damages.m_frost = baseFrost.Value;
            itmdrop.m_itemData.m_shared.m_damages.m_lightning = baseLightning.Value;
            itmdrop.m_itemData.m_shared.m_damages.m_poison = basePoison.Value;
            itmdrop.m_itemData.m_shared.m_damages.m_spirit = baseSpirit.Value;
            itmdrop.m_itemData.m_shared.m_damagesPerLevel.m_damage = baseDamagePerPerLevel.Value;
            itmdrop.m_itemData.m_shared.m_damagesPerLevel.m_blunt = baseBluntPerLevel.Value;
            itmdrop.m_itemData.m_shared.m_damagesPerLevel.m_slash = baseSlashPerLevel.Value;
            itmdrop.m_itemData.m_shared.m_damagesPerLevel.m_pierce = basePiercePerLevel.Value;
            itmdrop.m_itemData.m_shared.m_damagesPerLevel.m_chop = baseChopPerLevel.Value;
            itmdrop.m_itemData.m_shared.m_damagesPerLevel.m_pickaxe = basePickaxePerLevel.Value;
            itmdrop.m_itemData.m_shared.m_damagesPerLevel.m_fire = baseFirePerLevel.Value;
            itmdrop.m_itemData.m_shared.m_damagesPerLevel.m_frost = baseFrostPerLevel.Value;
            itmdrop.m_itemData.m_shared.m_damagesPerLevel.m_lightning = baseLightningPerLevel.Value;
            itmdrop.m_itemData.m_shared.m_damagesPerLevel.m_poison = basePoisonPerLevel.Value;
            itmdrop.m_itemData.m_shared.m_damagesPerLevel.m_spirit = baseSpiritPerLevel.Value;
            itmdrop.m_itemData.m_shared.m_attackForce = baseAttackForce.Value;
            itmdrop.m_itemData.m_shared.m_blockPower = baseBlockPower.Value;
            itmdrop.m_itemData.m_shared.m_deflectionForce = baseParryForce.Value;
            itmdrop.m_itemData.m_shared.m_backstabBonus = baseBackstab.Value;
            itmdrop.m_itemData.m_durability = baseDurability.Value;
        }*/

        private void Localize()
        {
            LocalizeWord("lightsaber_blue", "Blue Lightsaber");
            LocalizeWord("lightsaber_blue_description",
                "A weapon of power imbued with a blue glow");
            LocalizeWord("lightsaber_green", "Green Lightsaber");
            LocalizeWord("lightsaber_green_description",
                "A weapon of power imbued with a green glow");
            LocalizeWord("lightsaber_red", "Red Lightsaber");
            LocalizeWord("lightsaber_red_description",
                "A weapon of power imbued with a red glow");
            LocalizeWord("lightsaber_dark", "Dark Saber");
            LocalizeWord("lightsaber_dark_description",
                "A weapon of power imbued with a dark glow");
            LocalizeWord("lightsaber_purple", "Purple Lightsaber");
            LocalizeWord("lightsaber_purple_description",
                "A weapon of power imbued with a purple glow");
            LocalizeWord("lightsaber_pink", "Pink Lightsaber");
            LocalizeWord("lightsaber_pink_description",
                "A weapon of power imbued with a pink glow");
            LocalizeWord("lightsaber_orange", "Orange Lightsaber");
            LocalizeWord("lightsaber_orange_description",
                "A weapon of power imbued with a orange glow");

            LocalizeWord("item_sabercrystal_blue", "Blue Saber Crystal");
            LocalizeWord("item_sabercrystal_green", "Green Saber Crystal");
            LocalizeWord("item_sabercrystal_dark", "Dark Saber Crystal");
            LocalizeWord("item_sabercrystal_red", "Red Saber Crystal");
            LocalizeWord("item_sabercrystal_pink", "Pink Saber Crystal");
            LocalizeWord("item_sabercrystal_purple", "Purple Saber Crystal");
            LocalizeWord("item_sabercrystal_orange", "Orange Saber Crystal");

            LocalizeWord("item_sabercrystal_blue_description", "Blue Crystal imbued with power from another world.");
            LocalizeWord("item_sabercrystal_green_description", "Green Crystal imbued with power from another world.");
            LocalizeWord("item_sabercrystal_dark_description", "Dark Crystal imbued with power from another world.");
            LocalizeWord("item_sabercrystal_red_description", "Red Crystal imbued with power from another world.");
            LocalizeWord("item_sabercrystal_pink_description", "Pink Crystal imbued with power from another world.");
            LocalizeWord("item_sabercrystal_purple_description",
                "Purple Crystal imbued with power from another world.");
            LocalizeWord("item_sabercrystal_orange_description",
                "Orange Crystal imbued with power from another world.");
        }

        private string LocalizeWord(string key, string val)
        {
            if (m_localizedStrings.ContainsKey(key)) return $"${key}";
            Localization? loc = Localization.instance;
            string? langSection = loc.GetSelectedLanguage();
            ConfigEntry<string>? configEntry = localizationFile.Bind(langSection, key, val);
            Localization.instance.AddWord(key, configEntry.Value);
            m_localizedStrings.Add(key, configEntry);

            return $"${key}";
        }


        #region Configs

        /* Damage */
        /*private static ConfigEntry<int> baseDamage;
        private static ConfigEntry<int> baseBlunt;
        private static ConfigEntry<int> baseSlash;
        private static ConfigEntry<int> basePierce;
        private static ConfigEntry<int> baseChop;
        private static ConfigEntry<int> basePickaxe;
        private static ConfigEntry<int> baseFire;
        private static ConfigEntry<int> baseFrost;
        private static ConfigEntry<int> baseLightning;
        private static ConfigEntry<int> basePoison;
        private static ConfigEntry<int> baseSpirit;
        private static ConfigEntry<int> baseDamagePerPerLevel;
        private static ConfigEntry<int> baseBluntPerLevel;
        private static ConfigEntry<int> baseSlashPerLevel;
        private static ConfigEntry<int> basePiercePerLevel;
        private static ConfigEntry<int> baseChopPerLevel;
        private static ConfigEntry<int> basePickaxePerLevel;
        private static ConfigEntry<int> baseFirePerLevel;
        private static ConfigEntry<int> baseFrostPerLevel;
        private static ConfigEntry<int> baseLightningPerLevel;
        private static ConfigEntry<int> basePoisonPerLevel;
        private static ConfigEntry<int> baseSpiritPerLevel;
        private static ConfigEntry<int> baseAttackForce;
        private static ConfigEntry<int> baseBlockPower; // block power
        private static ConfigEntry<int> baseParryForce; // parry force
        public static ConfigEntry<int> baseKnockbackForce; // knockback force
        private static ConfigEntry<int> baseBackstab; // backstab
        private static ConfigEntry<int> baseDurability;*/

        #endregion
    }
}