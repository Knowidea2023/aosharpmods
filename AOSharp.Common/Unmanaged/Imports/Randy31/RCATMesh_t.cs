using AOSharp.Common.GameData;
using System;
using System.Runtime.InteropServices;

namespace AOSharp.Common.Unmanaged.Imports
{
    public class RCATMesh_t
    {
        [DllImport("Randy31.dll", EntryPoint = "?GetBoneMatrix@RCATMesh_t@@QAE_NPBDAAVTMatrix4_t@@@Z", CallingConvention = CallingConvention.ThisCall)]
        public static extern IntPtr GetBoneMatrix(IntPtr pThis, [MarshalAs(UnmanagedType.LPStr)] string name, ref Matrix4x4 matrix);
    }
}
