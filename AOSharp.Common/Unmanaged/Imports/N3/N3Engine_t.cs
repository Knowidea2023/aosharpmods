using System;
using System.Runtime.InteropServices;

namespace AOSharp.Common.Unmanaged.Imports
{
    public class N3Engine_t
    {
        [DllImport("N3.dll", EntryPoint = "?GetInstance@n3Engine_t@@SAPAV1@XZ", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr GetInstance();

        [DllImport("N3.dll", EntryPoint = "?GetRoot@n3Engine_t@@QAEAAVn3Root_t@@XZ", CallingConvention = CallingConvention.ThisCall)]
        public static extern IntPtr GetRoot(IntPtr pThis);
    }
}
