﻿using System;
using System.Text;
using System.Runtime.InteropServices;
using AOSharp.Common.Unmanaged.DataTypes;
using AOSharp.Common.GameData;

namespace AOSharp.Common.Unmanaged.Imports
{
    public class View_c
    {
        [DllImport("GUI.dll", EntryPoint = "??0View@@QAE@ABVRect@@ABVString@@II@Z", CallingConvention = CallingConvention.ThisCall)]
        internal static extern unsafe IntPtr Constructor(IntPtr pThis, Rect* rect, IntPtr pName, int unk1, int unk2);

        [DllImport("GUI.dll", EntryPoint = "??1View@@UAE@XZ", CallingConvention = CallingConvention.ThisCall)]
        public static extern int Deconstructor(IntPtr pThis);

        [DllImport("GUI.dll", EntryPoint = "?AddChild@View@@UAEXPAV1@_N@Z", CallingConvention = CallingConvention.ThisCall)]
        public static extern void AddChild(IntPtr pThis, IntPtr pView, bool assignTabOrder);

        [DllImport("GUI.dll", EntryPoint = "?RemoveChild@View@@QAEXPAV1@@Z", CallingConvention = CallingConvention.ThisCall)]
        public static extern void RemoveChild(IntPtr pThis, IntPtr pView);

        [DllImport("GUI.dll", EntryPoint = "?FindChild@View@@QAEPAV1@PBD_N@Z", CallingConvention = CallingConvention.ThisCall)]
        public static extern IntPtr FindChild(IntPtr pThis, [MarshalAs(UnmanagedType.LPStr)] string name, bool unk);

        [DllImport("GUI.dll", EntryPoint = "?SetBorders@View@@QAEXMMMM@Z", CallingConvention = CallingConvention.ThisCall)]
        public static extern void SetBorders(IntPtr pThis, float minX, float minY, float maxX, float maxY);

        [DllImport("GUI.dll", EntryPoint = "?GetPreferredSize@View@@UBE?AVPoint@@_N@Z", CallingConvention = CallingConvention.ThisCall)]
        public static extern IntPtr GetPreferredSize(IntPtr pThis, ref Vector2 preferredSize, bool unk);

        [DllImport("GUI.dll", EntryPoint = "?CalculatePreferredSize@View@@UBE?AVPoint@@_N@Z", CallingConvention = CallingConvention.ThisCall)]
        public static extern IntPtr CalculatePreferredSize(IntPtr pThis, ref Vector2 preferredSize, bool unk);

        [DllImport("GUI.dll", EntryPoint = "?ResizeTo@View@@UAEXABVPoint@@@Z", CallingConvention = CallingConvention.ThisCall)]
        public static extern void ResizeTo(IntPtr pThis, ref Vector2 size);

        [DllImport("GUI.dll", EntryPoint = "?LimitMaxSize@View@@QAEXABVPoint@@@Z", CallingConvention = CallingConvention.ThisCall)]
        public static extern void LimitMaxSize(IntPtr pThis, ref Vector2 size);

        [DllImport("GUI.dll", EntryPoint = "?SetFrame@View@@UAEXABVRect@@_N@Z", CallingConvention = CallingConvention.ThisCall)]
        public static extern unsafe void SetFrame(IntPtr pThis, Rect* rect, bool unk);

        [DllImport("GUI.dll", EntryPoint = "?Show@View@@QAEX_N0@Z", CallingConvention = CallingConvention.ThisCall)]
        public static extern void Show(IntPtr pThis, bool visible, bool unk);

        [DllImport("GUI.dll", EntryPoint = "?SetLayoutNode@View@@QAEXPAVLayoutNode@@@Z", CallingConvention = CallingConvention.ThisCall)]
        public static extern void SetLayoutNode(IntPtr pThis, IntPtr pLayoutNode);


        public static unsafe IntPtr Create(Rect rect, string name, int unk1, int unk2)
        {
            StdString nameStr = StdString.Create(name);
            IntPtr pView = Constructor(MSVCR100.New(0x128), &rect, nameStr.Pointer, unk1, unk2);

            return pView;
        }
    }
}
