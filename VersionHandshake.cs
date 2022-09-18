﻿using System;
using System.Collections.Generic;
using HarmonyLib;

namespace LightSabers
{
    [HarmonyPatch(typeof(ZNet), nameof(ZNet.OnNewConnection))]
    public static class RegisterAndCheckVersion
    {
        private static void Prefix(ZNetPeer peer, ref ZNet __instance)
        {
            // Register version check call
            LightSabers.LSLogger.LogDebug("Registering version RPC handler");
            peer.m_rpc.Register($"{LightSabers.ModName}_VersionCheck",
                new Action<ZRpc, ZPackage>(RpcHandlers.RPC_DaedricSet_Version));

            // Make calls to check versions
            LightSabers.LSLogger.LogInfo("Invoking version check");
            ZPackage zpackage = new();
            zpackage.Write(LightSabers.ModVersion);
            peer.m_rpc.Invoke($"{LightSabers.ModName}_VersionCheck", zpackage);
        }
    }

    [HarmonyPatch(typeof(ZNet), nameof(ZNet.RPC_PeerInfo))]
    public static class VerifyClient
    {
        private static bool Prefix(ZRpc rpc, ZPackage pkg, ref ZNet __instance)
        {
            if (!__instance.IsServer() || RpcHandlers.ValidatedPeers.Contains(rpc)) return true;
            // Disconnect peer if they didn't send mod version at all
            LightSabers.LSLogger.LogWarning(
                $"Peer ({rpc.m_socket.GetHostName()}) never sent version or couldn't due to previous disconnect, disconnecting");
            rpc.Invoke("Error", 3);
            return false; // Prevent calling underlying method
        }

        private static void Postfix(ZNet __instance)
        {
            ZRoutedRpc.instance.InvokeRoutedRPC(ZRoutedRpc.instance.GetServerPeerID(), "RequestAdminSync",
                new ZPackage());
        }
    }

    [HarmonyPatch(typeof(FejdStartup), nameof(FejdStartup.ShowConnectError))]
    public class ShowConnectionError
    {
        private static void Postfix(FejdStartup __instance)
        {
            if (__instance.m_connectionFailedPanel.activeSelf)
                __instance.m_connectionFailedError.text += "\n" + LightSabers.ConnectionError;
        }
    }

    [HarmonyPatch(typeof(ZNet), nameof(ZNet.Disconnect))]
    public static class RemoveDisconnectedPeerFromVerified
    {
        private static void Prefix(ZNetPeer peer, ref ZNet __instance)
        {
            if (!__instance.IsServer()) return;
            // Remove peer from validated list
            LightSabers.LSLogger.LogInfo(
                $"Peer ({peer.m_rpc.m_socket.GetHostName()}) disconnected, removing from validated list");
            _ = RpcHandlers.ValidatedPeers.Remove(peer.m_rpc);
        }
    }

    public static class RpcHandlers
    {
        public static readonly List<ZRpc> ValidatedPeers = new();

        public static void RPC_DaedricSet_Version(ZRpc rpc, ZPackage pkg)
        {
            string? version = pkg.ReadString();
            LightSabers.LSLogger.LogInfo("Version check, local: " +
                                                      LightSabers.ModVersion +
                                                      ",  remote: " + version);
            if (version != LightSabers.ModVersion)
            {
                LightSabers.ConnectionError =
                    $"{LightSabers.ModName} Installed: {LightSabers.ModVersion}\n Needed: {version}";
                if (!ZNet.instance.IsServer()) return;
                // Different versions - force disconnect client from server
                LightSabers.LSLogger.LogWarning(
                    $"Peer ({rpc.m_socket.GetHostName()}) has incompatible version, disconnecting");
                rpc.Invoke("Error", 3);
            }
            else
            {
                if (!ZNet.instance.IsServer())
                {
                    // Enable mod on client if versions match
                    LightSabers.LSLogger.LogInfo("Received same version from server!");
                }
                else
                {
                    // Add client to validated list
                    LightSabers.LSLogger.LogInfo(
                        $"Adding peer ({rpc.m_socket.GetHostName()}) to validated list");
                    ValidatedPeers.Add(rpc);
                }
            }
        }
    }
}