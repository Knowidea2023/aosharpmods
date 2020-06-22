﻿using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Linq;
using AOSharp.Common.GameData;
using AOSharp.Common.Unmanaged.Imports;
using AOSharp.Core.Combat;
using SmokeLounge.AOtomation.Messaging.Messages.N3Messages;
using AOSharp.Core.GameData;

namespace AOSharp.Core
{
    public class Buff : DummyItem
    {
        public readonly Identity Owner;
        public readonly Identity Identity;
        public float RemainingTime => GetCurrentTime();
        public float TotalTime => GetTotalTime();

        internal Buff(Identity owner, Identity identity) : base(identity)
        {
            Owner = owner;
            Identity = identity;
        }

        private unsafe float GetCurrentTime()
        {
            IntPtr pEngine = N3Engine_t.GetInstance();

            if (pEngine == IntPtr.Zero)
                return 0;

            fixed (Identity* pIdentity = &Identity)
                fixed (Identity* pOwner = &Owner) {
                return N3EngineClientAnarchy_t.GetBuffCurrentTime(pEngine, pIdentity, pOwner) / 100;
            }
        }

        private unsafe float GetTotalTime()
        {
            IntPtr pEngine = N3Engine_t.GetInstance();

            if (pEngine == IntPtr.Zero)
                return 0;

            fixed (Identity* pIdentity = &Identity)
                fixed (Identity* pOwner = &Owner) {
                return N3EngineClientAnarchy_t.GetBuffTotalTime(pEngine, pIdentity, pOwner) / 100f;
            }
        }
    }
}