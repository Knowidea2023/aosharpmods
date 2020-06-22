﻿using System;
using System.Runtime.InteropServices;
using AOSharp.Common.GameData;

namespace AOSharp.Common.Unmanaged.Imports
{
    public class N3PlayfieldAnarchy_t
    {
        [DllImport("Gamecode.dll", EntryPoint = "?AreVehiclesAllowed@PlayfieldAnarchy_t@@QBE_NXZ", CallingConvention = CallingConvention.ThisCall)]
        public static extern bool AreVehiclesAllowed(IntPtr pThis);

        [DllImport("Gamecode.dll", EntryPoint = "?IsShadowlandPF@PlayfieldAnarchy_t@@QBE_NXZ", CallingConvention = CallingConvention.ThisCall)]
        public static extern bool IsShadowlandPF(IntPtr pThis);
    }
}