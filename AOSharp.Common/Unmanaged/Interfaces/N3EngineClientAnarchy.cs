﻿using System;
using AOSharp.Common.GameData;
using AOSharp.Common.Helpers;
using AOSharp.Common.Unmanaged.Imports;

namespace AOSharp.Common.Unmanaged.Interfaces
{
    public class N3EngineClientAnarchy
    {
        public static void DebugSpellListToChat(Identity identity, int unk, int spellList)
        {
            IntPtr pEngine = N3Engine_t.GetInstance();

            if (pEngine == IntPtr.Zero)
                return;

            N3EngineClientAnarchy_t.DebugSpellListToChat(pEngine, unk, ref identity, spellList);
        }

        public static string GetName(Identity identity)
        {
            IntPtr pEngine = N3Engine_t.GetInstance();

            if (pEngine == IntPtr.Zero)
                return null;
            
            Identity garbage = new Identity();
            
            return Utils.UnsafePointerToString(N3EngineClientAnarchy_t.GetName(pEngine, ref identity, ref garbage));
        }

        public static bool HasPerk(int perkId)
        {
            IntPtr pEngine = N3Engine_t.GetInstance();

            if (pEngine == IntPtr.Zero)
                throw new NullReferenceException("Could not get N3Engine instance");

            return N3EngineClientAnarchy_t.HasPerk(pEngine, perkId);
        }
        
        public static void UseItem(Identity identity, bool unknown = false)
        {
            IntPtr pEngine = N3Engine_t.GetInstance();

            if (pEngine != IntPtr.Zero)
            {
                N3EngineClientAnarchy_t.UseItem(pEngine, ref identity, unknown);
            }
        }
        
        public static void UseItemOnItem(Identity source, Identity target)
        {
            IntPtr pEngine = N3Engine_t.GetInstance();

            if (pEngine != IntPtr.Zero)
            {
                N3EngineClientAnarchy_t.UseItemOnItem(pEngine, ref source, ref target);
            }
        }
        
        public static void UseItemOnCharacter(Identity source, Identity target)
        {
            IntPtr pEngine = N3Engine_t.GetInstance();

            if (pEngine != IntPtr.Zero)
            {
                N3EngineClientAnarchy_t.UseItemOnCharacter(pEngine, ref source, ref target);
            }
        }

        public static bool GetQuestWorldPos(Identity mission, out Identity playfield, out Vector3 universePos, out Vector3 zonePos)
        {
            playfield = Identity.None;
            universePos = Vector3.Zero;
            zonePos = Vector3.Zero;
            IntPtr pEngine = N3Engine_t.GetInstance();

            if (pEngine == IntPtr.Zero)
                return false;

            return N3EngineClientAnarchy_t.GetQuestWorldPos(pEngine, ref mission, ref playfield, ref universePos, ref zonePos);
        }

        public static int GetNumberOfFreeInventorySlots()
        {
            IntPtr pEngine = N3Engine_t.GetInstance();

            if (pEngine == IntPtr.Zero)
                return 0;

            return N3EngineClientAnarchy_t.GetNumberOfFreeInventorySlots(pEngine);
        }

        public static void SetStat(Stat stat, int value)
        {
            IntPtr pEngine = N3Engine_t.GetInstance();

            if (pEngine != IntPtr.Zero)
                N3EngineClientAnarchy_t.SetStat(pEngine, value, stat);
        }
    }
}
