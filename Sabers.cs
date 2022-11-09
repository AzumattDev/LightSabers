using System;
using System.Collections.Generic;
using System.IO;
using BepInEx;
using HarmonyLib;
using System.Reflection;
using BepInEx.Configuration;
using BepInEx.Logging;
using ItemManager;
using KeyManager;
using ServerSync;
using UnityEngine;

namespace LightSabers
{
    [BepInPlugin(ModGUID, ModName, ModVersion)]
    [KeyManager.VerifyKey("Azumatt/LightSabers", LicenseMode.DedicatedServer)]
    public class LightSabers : BaseUnityPlugin
    {
        internal const string ModName = "LightSabers";
        internal const string ModVersion = "1.2.0";
        internal const string Author = "Azumatt";
        private const string ModGUID = Author + "." + ModName;
        private static string ConfigFileName = ModGUID + ".cfg";
        private static string ConfigFileFullPath = Paths.ConfigPath + Path.DirectorySeparatorChar + ConfigFileName;
        private readonly Harmony _harmony = new(ModGUID);
        internal static string ConnectionError = "";
        internal static readonly ManualLogSource LSLogger = BepInEx.Logging.Logger.CreateLogSource(ModName);
        private ConfigFile _localizationFile = null!;

        /* Items */
        // SaberGreen;
        // SaberRed;
        // SaberDark;
        // SaberBlue;
        // SaberPurple;
        // SaberPink;
        // SaberOrange;

        /* Crystals */
        // SaberCrystal_Green;
        // SaberCrystal_Red;
        // SaberCrystal_Dark;
        // SaberCrystal_Blue;
        // SaberCrystal_Purple;
        // SaberCrystal_Pink;
        // SaberCrystal_Orange;

        private readonly ConfigSync configSync = new(ModName)
            { DisplayName = ModName, CurrentVersion = ModVersion, MinimumRequiredVersion = ModVersion };

        private readonly Dictionary<string, ConfigEntry<string>> m_localizedStrings =
            new();


        public void Awake()
        {
            _serverConfigLocked = Config.Bind("1 - General", "Force Server Config", true, "Force Server Config");
            _ = configSync.AddLockingConfigEntry(_serverConfigLocked);

            /* Add SFX */
            PrefabManager.RegisterPrefab(PrefabManager.RegisterAssetBundle("lightsabers"), "sfx_saber_swing");
            PrefabManager.RegisterPrefab(PrefabManager.RegisterAssetBundle("lightsabers"), "sfx_saber_hit");

            /* Add SaberCrystals */
            Item SaberCrystal_Green = new Item("lightsabers", "SaberCrystal_Green");
            SaberCrystal_Green.Crafting.Add(CraftingTable.Forge, 3);
            SaberCrystal_Green.RequiredItems.Add("Crystal", 50);
            SaberCrystal_Green.CraftAmount = 1;

            Item SaberCrystal_Red = new Item("lightsabers", "SaberCrystal_Red");
            SaberCrystal_Red.Crafting.Add(CraftingTable.Forge, 3);
            SaberCrystal_Red.RequiredItems.Add("Crystal", 50);
            SaberCrystal_Red.CraftAmount = 1;


            Item SaberCrystal_Dark = new Item("lightsabers", "SaberCrystal_Dark");
            SaberCrystal_Dark.Crafting.Add(CraftingTable.Forge, 3);
            SaberCrystal_Dark.RequiredItems.Add("Crystal", 50);
            SaberCrystal_Dark.CraftAmount = 1;


            Item SaberCrystal_Blue = new Item("lightsabers", "SaberCrystal_Blue");
            SaberCrystal_Blue.Crafting.Add(CraftingTable.Forge, 3);
            SaberCrystal_Blue.RequiredItems.Add("Crystal", 50);
            SaberCrystal_Blue.CraftAmount = 1;


            Item SaberCrystal_Purple = new Item("lightsabers", "SaberCrystal_Purple");
            SaberCrystal_Purple.Crafting.Add(CraftingTable.Forge, 3);
            SaberCrystal_Purple.RequiredItems.Add("Crystal", 50);
            SaberCrystal_Purple.CraftAmount = 1;


            Item SaberCrystal_Pink = new Item("lightsabers", "SaberCrystal_Pink");
            SaberCrystal_Pink.Crafting.Add(CraftingTable.Forge, 3);
            SaberCrystal_Pink.RequiredItems.Add("Crystal", 50);
            SaberCrystal_Pink.CraftAmount = 1;


            Item SaberCrystal_Orange = new Item("lightsabers", "SaberCrystal_Orange");
            SaberCrystal_Orange.Crafting.Add(CraftingTable.Forge, 3);
            SaberCrystal_Orange.RequiredItems.Add("Crystal", 50);
            SaberCrystal_Orange.CraftAmount = 1;


            Item SaberRed = new Item("lightsabers", "LightSaber_Red");
            SaberRed.Crafting.Add(CraftingTable.Forge, 3);
            SaberRed.RequiredItems.Add("SaberCrystal_Red", 20);
            SaberRed.RequiredItems.Add("Silver", 40);
            SaberRed.RequiredUpgradeItems.Add("Iron",
                20); // Upgrade requirements are per item, even if you craft two at the same time
            SaberRed.RequiredUpgradeItems.Add("Silver",
                10); // 10 Silver: You need 10 silver for level 2, 20 silver for level 3, 30 silver for level 4
            SaberRed.CraftAmount = 1;

            Item SaberGreen = new Item("lightsabers", "LightSaber_Green");
            SaberGreen.Crafting.Add(CraftingTable.Forge, 3);
            SaberGreen.RequiredItems.Add("SaberCrystal_Green", 20);
            SaberGreen.RequiredItems.Add("Silver", 40);
            SaberGreen.RequiredUpgradeItems.Add("Iron",
                20); // Upgrade requirements are per item, even if you craft two at the same time
            SaberGreen.RequiredUpgradeItems.Add("Silver",
                10); // 10 Silver: You need 10 silver for level 2, 20 silver for level 3, 30 silver for level 4
            SaberGreen.CraftAmount = 1;


            Item SaberDark = new Item("lightsabers", "LightSaber_Dark");
            SaberDark.Crafting.Add(CraftingTable.Forge, 3);
            SaberDark.RequiredItems.Add("SaberCrystal_Dark", 20);
            SaberDark.RequiredItems.Add("Silver", 40);
            SaberDark.RequiredUpgradeItems.Add("Iron",
                20); // Upgrade requirements are per item, even if you craft two at the same time
            SaberDark.RequiredUpgradeItems.Add("Silver",
                10); // 10 Silver: You need 10 silver for level 2, 20 silver for level 3, 30 silver for level 4
            SaberDark.CraftAmount = 1;

            Item SaberBlue = new Item("lightsabers", "LightSaber_Blue");
            SaberBlue.Crafting.Add(CraftingTable.Forge, 3);
            SaberBlue.RequiredItems.Add("SaberCrystal_Blue", 20);
            SaberBlue.RequiredItems.Add("Silver", 40);
            SaberBlue.RequiredUpgradeItems.Add("Iron",
                20); // Upgrade requirements are per item, even if you craft two at the same time
            SaberBlue.RequiredUpgradeItems.Add("Silver",
                10); // 10 Silver: You need 10 silver for level 2, 20 silver for level 3, 30 silver for level 4
            SaberBlue.CraftAmount = 1;

            /*Item SaberBlue = new Item("azu_lightsabers", "LightSaber_Blue");
            SaberBlue.Crafting.Add(CraftingTable.Forge, 3);
            SaberBlue.RequiredItems.Add("SaberCrystal_Blue", 20);
            SaberBlue.RequiredItems.Add("Silver", 40);
            SaberBlue.RequiredUpgradeItems.Add("Iron",
                20); // Upgrade requirements are per item, even if you craft two at the same time
            SaberBlue.RequiredUpgradeItems.Add("Silver",
                10); // 10 Silver: You need 10 silver for level 2, 20 silver for level 3, 30 silver for level 4
            SaberBlue.CraftAmount = 1;
            SaberBlue.Snapshot();*/

            Item SaberPurple = new Item("lightsabers", "LightSaber_Purple");
            SaberPurple.Crafting.Add(CraftingTable.Forge, 3);
            SaberPurple.RequiredItems.Add("SaberCrystal_Purple", 20);
            SaberPurple.RequiredItems.Add("Silver", 40);
            SaberPurple.RequiredUpgradeItems
                .Add("Iron", 20); // Upgrade requirements are per item, even if you craft two at the same time
            SaberPurple.RequiredUpgradeItems.Add("Silver",
                10); // 10 Silver: You need 10 silver for level 2, 20 silver for level 3, 30 silver for level 4
            SaberPurple.CraftAmount = 1;

            Item SaberPink = new Item("lightsabers", "LightSaber_Pink");
            SaberPink.Crafting.Add(CraftingTable.Forge, 3);
            SaberPink.RequiredItems.Add("SaberCrystal_Pink", 20);
            SaberPink.RequiredItems.Add("Silver", 40);
            SaberPink.RequiredUpgradeItems.Add("Iron",
                20); // Upgrade requirements are per item, even if you craft two at the same time
            SaberPink.RequiredUpgradeItems.Add("Silver",
                10); // 10 Silver: You need 10 silver for level 2, 20 silver for level 3, 30 silver for level 4
            SaberPink.CraftAmount = 1;

            Item SaberOrange = new Item("lightsabers", "LightSaber_Orange");
            SaberOrange.Crafting.Add(CraftingTable.Forge, 3);
            SaberOrange.RequiredItems.Add("SaberCrystal_Orange", 20);
            SaberOrange.RequiredItems.Add("Silver", 40);
            SaberOrange.RequiredUpgradeItems
                .Add("Iron", 20); // Upgrade requirements are per item, even if you craft two at the same time
            SaberOrange.RequiredUpgradeItems.Add("Silver",
                10); // 10 Silver: You need 10 silver for level 2, 20 silver for level 3, 30 silver for level 4
            SaberOrange.CraftAmount = 1;


            _localizationFile =
                new ConfigFile(
                    Path.Combine(Path.GetDirectoryName(Config.ConfigFilePath) ?? throw new InvalidOperationException(),
                        ModGUID + ".Localization.cfg"), false);
            Assembly assembly = Assembly.GetExecutingAssembly();
            _harmony.PatchAll(assembly);
            SetupWatcher();
            Localize();
        }

        public void OnDestroy()
        {
            Config.Save();
        }

        private void SetupWatcher()
        {
            FileSystemWatcher watcher = new(Paths.ConfigPath, ConfigFileName);
            watcher.Changed += ReadConfigValues;
            watcher.Created += ReadConfigValues;
            watcher.Renamed += ReadConfigValues;
            watcher.IncludeSubdirectories = true;
            watcher.SynchronizingObject = ThreadingHelper.SynchronizingObject;
            watcher.EnableRaisingEvents = true;
        }

        private void ReadConfigValues(object sender, FileSystemEventArgs e)
        {
            if (!File.Exists(ConfigFileFullPath)) return;
            try
            {
                LSLogger.LogDebug("ReadConfigValues called");
                Config.Reload();
            }
            catch
            {
                LSLogger.LogError($"There was an issue loading your {ConfigFileName}");
                LSLogger.LogError("Please check your config entries for spelling and format!");
            }
        }

        [HarmonyPatch(typeof(ZNetScene), nameof(ZNetScene.Awake))]
        private static class Sabers_SFXFix
        {
            private static void Postfix()
            {
                /* Tell the SFX to respect audio mixer of the game, this keeps your ears intact. Sometimes custom audio comes in at a much higher volume
                 than what it sounds like in Unity. */
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

        [HarmonyPatch(typeof(Player), nameof(Player.Awake))]
        public class PatchPlayerAwake
        {
            [HarmonyPostfix]
            private static void PlayerAwake()
            {
                if (ZNetScene.instance && KeyManager.KeyManager.CheckAllowed() != State.Verified)
                {
                    Application.Quit();
                }
            }
        }

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
            ConfigEntry<string>? configEntry = _localizationFile.Bind(langSection, key, val);
            Localization.instance.AddWord(key, configEntry.Value);
            m_localizedStrings.Add(key, configEntry);

            return $"${key}";
        }


        #region ConfigOptions

        private static ConfigEntry<bool>? _serverConfigLocked = null!;

        private ConfigEntry<T> config<T>(string group, string name, T value, ConfigDescription description,
            bool synchronizedSetting = true)
        {
            ConfigDescription extendedDescription =
                new(
                    description.Description +
                    (synchronizedSetting ? " [Synced with Server]" : " [Not Synced with Server]"),
                    description.AcceptableValues, description.Tags);
            ConfigEntry<T> configEntry = Config.Bind(group, name, value, extendedDescription);
            //var configEntry = Config.Bind(group, name, value, description);

            SyncedConfigEntry<T> syncedConfigEntry = configSync.AddConfigEntry(configEntry);
            syncedConfigEntry.SynchronizedConfig = synchronizedSetting;

            return configEntry;
        }

        private ConfigEntry<T> config<T>(string group, string name, T value, string description,
            bool synchronizedSetting = true)
        {
            return config(group, name, value, new ConfigDescription(description), synchronizedSetting);
        }

        private class ConfigurationManagerAttributes
        {
            public bool? Browsable = false;
        }

        #endregion
    }
}