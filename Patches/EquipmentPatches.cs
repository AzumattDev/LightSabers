/*using System;
using HarmonyLib;
using LightSabers.Scripts;
using Object = UnityEngine.Object;

namespace LightSabers.Patches;

[HarmonyPatch(typeof(Humanoid), nameof(Humanoid.EquipItem))]
static class Humanoid_EquipItem_Patch
{
    static void Postfix(Humanoid __instance, ItemDrop.ItemData item, bool triggerEquipEffects = true)
    {
        if (__instance.IsPlayer())
        {
            // If item contains lightsaber, set saber blade active
            if (item.m_shared.m_name == "$lightsaber_blue")
            {
                if (__instance.GetRightItem() == null) return;
                LightSabers.LSLogger.LogError("Equipped lightsaber");
                item.m_dropPrefab.GetComponentInChildren<Saber>().SaberActive = true;
                item.m_dropPrefab.GetComponentInChildren<Saber>()._blades[0].ForceSetActive(true);
            }
            __instance.SetupEquipment();
        }
    }
}

[HarmonyPatch(typeof(Humanoid), nameof(Humanoid.UnequipItem))]
static class HumanoidUnEquipItemPatch
{
    static void Postfix(ref Humanoid __instance, ItemDrop.ItemData? item,
        bool triggerEquipEffects = true)
    {
        if (!__instance.IsPlayer()) return;
        
        if (__instance.GetRightItem() == null) return;
        if (item?.m_shared.m_name == "$lightsaber_blue")
        {
            LightSabers.LSLogger.LogError("UnEquipped lightsaber");
            if (__instance.GetRightItem().m_dropPrefab.GetComponentInChildren<Saber>().SaberActive == false)
            {
                return;
            }

            item.m_dropPrefab.GetComponentInChildren<Saber>().SaberActive = false;
            item.m_dropPrefab.GetComponentInChildren<Saber>()._blades[0].ForceSetActive(false);

            
        }
        __instance.UpdateEquipmentStatusEffects();
    }
}*/