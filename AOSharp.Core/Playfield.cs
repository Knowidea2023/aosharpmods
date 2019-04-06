﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using AOSharp.Common.GameData;
using AOSharp.Core.GameData;

namespace AOSharp.Core
{
    public static class Playfield
    {
        ///<summary>
        ///Playfield Identity
        ///</summary>
        public static Identity Identity => GetIdentity();

        ///<summary>
        ///Are mechs allowed on the playfield
        ///</summary>
        public static bool AllowsVehicles => AreVehiclesAllowed();

        ///<summary>
        ///Is Shadowlands playfield
        ///</summary>
        public static bool IsShadowlands => IsShadowlandPF();

        ///<summary>
        ///Is BattleStation
        ///</summary>
        public static bool IsBattleStation => IsBattleStationPF();

        ///<summary>
        ///Is playfield a dungeon
        ///</summary>
        public static bool IsDungeon => IsDungeonPF();

        ///<summary>
        ///Playfield name
        ///</summary>
        public static string Name => GetName();

        //TODO: Convert to use n3Playfield_t::GetPlayfieldDynels() to remove dependencies on hard-coded offsets
        internal unsafe static List<IntPtr> GetPlayfieldDynels()
        {
            IntPtr pPlayfield = N3EngineClient_t.GetPlayfield();

            if (pPlayfield == IntPtr.Zero)
                return new List<IntPtr>();

            return (*(Playfield_MemStruct*)pPlayfield).Dynels.ToList();
        }

        private unsafe static Identity GetIdentity()
        {
            IntPtr pPlayfield = N3EngineClient_t.GetPlayfield();

            //TODO: throw playfield not initialized exception?
            if (pPlayfield == IntPtr.Zero)
                return Identity.None;

            return *N3Playfield_t.GetIdentity(pPlayfield);
        }

        private unsafe static string GetName()
        {
            IntPtr pPlayfield = N3EngineClient_t.GetPlayfield();

            //TODO: throw playfield not initialized exception?
            if (pPlayfield == IntPtr.Zero)
                return String.Empty;

            return Marshal.PtrToStringAnsi(N3Playfield_t.GetName(pPlayfield));
        }

        private static bool AreVehiclesAllowed()
        {
            IntPtr pPlayfield = N3EngineClient_t.GetPlayfield();

            //TODO: throw playfield not initialized exception?
            if (pPlayfield == IntPtr.Zero)
                return false;

            return N3PlayfieldAnarchy_t.AreVehiclesAllowed(pPlayfield);
        }

        private static bool IsShadowlandPF()
        {
            IntPtr pPlayfield = N3EngineClient_t.GetPlayfield();

            //TODO: throw playfield not initialized exception?
            if (pPlayfield == IntPtr.Zero)
                return false;

            return N3PlayfieldAnarchy_t.IsShadowlandPF(pPlayfield);
        }

        private static bool IsDungeonPF()
        {
            IntPtr pPlayfield = N3EngineClient_t.GetPlayfield();

            //TODO: throw playfield not initialized exception?
            if (pPlayfield == IntPtr.Zero)
                return false;

            return N3Playfield_t.IsDungeon(pPlayfield);
        }

        private static bool IsBattleStationPF()
        {
            IntPtr pPlayfield = N3EngineClient_t.GetPlayfield();

            //TODO: throw playfield not initialized exception?
            if (pPlayfield == IntPtr.Zero)
                return false;

            return N3Playfield_t.IsBattleStation(pPlayfield);
        }

        [StructLayout(LayoutKind.Explicit, Pack = 0)]
        private unsafe struct Playfield_MemStruct
        {
            [FieldOffset(0x30)]
            public StdObjVector Dynels;
        }
    }
}
