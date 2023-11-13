using System;
using System.Runtime.InteropServices;
using AOSharp.Common.GameData;
using AOSharp.Common.Unmanaged.DataTypes;

namespace AOSharp.Common.Unmanaged.Imports
{
    public class N3InterfaceModule_t
    {
        [DllImport("Interfaces.dll", EntryPoint = "?GetInstance@N3InterfaceModule_t@@SAPAV1@XZ", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr GetInstance();

        [DllImport("Interfaces.dll", EntryPoint = "?GetClientInst@N3InterfaceModule_t@@QBEIXZ", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetClientInst();

        [DllImport("Interfaces.dll", EntryPoint = "?N3Msg_GetPFName@N3InterfaceModule_t@@QBEPBDI@Z", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr GetPFName(int pfId);

        [DllImport("Interfaces.dll", EntryPoint = "?N3Msg_CastNanoSpell@N3InterfaceModule_t@@QBEXABVIdentity_t@@0@Z", CallingConvention = CallingConvention.ThisCall)]
        public unsafe static extern void CastNanoSpell(IntPtr pThis, Identity* nano, Identity target);

        [DllImport("Interfaces.dll", EntryPoint = "?N3Msg_GetPerkProgress@N3InterfaceModule_t@@QBEMI@Z", CallingConvention = CallingConvention.ThisCall)]
        public static extern float GetPerkProgress(IntPtr pThis, uint perkId);

        public unsafe delegate void DCastNanoSpell(IntPtr pThis, Identity* nanoIdentity, Identity targetIdentity);

        [return: MarshalAs(UnmanagedType.U1)]
        [DllImport("Interfaces.dll", EntryPoint = "?N3Msg_GetCompletedPersonalResearchGoals@N3InterfaceModule_t@@QAEXAAV?$vector@IV?$allocator@I@std@@@std@@@Z", CallingConvention = CallingConvention.ThisCall)]
        public static extern bool GetCompletedPersonalResearchGoals(IntPtr pThis, ref StdStructVector vector);

        [return: MarshalAs(UnmanagedType.U1)]
        [DllImport("Interfaces.dll", EntryPoint = "?N3Msg_PersonalResearchGoals@N3InterfaceModule_t@@QAEXAAV?$vector@U?$pair@I_N@std@@V?$allocator@U?$pair@I_N@std@@@2@@std@@@Z", CallingConvention = CallingConvention.ThisCall)]
        public static extern bool GetPersonalResearchGoals(IntPtr pThis, ref StdStructVector vector);
    }
}
