﻿using System;
using AOSharp.Common.GameData;

namespace AOSharp.Core.GameData
{
    public class SpecialAttack
    {
        public static SpecialAttack FastAttack = new SpecialAttack(Stat.FastAttack);
        public static SpecialAttack Brawl = new SpecialAttack(Stat.Brawl);
        public static SpecialAttack Dimach = new SpecialAttack(Stat.Dimach);
        public static SpecialAttack Burst = new SpecialAttack(Stat.Burst);
        public static SpecialAttack FullAuto = new SpecialAttack(Stat.FullAuto);
        public static SpecialAttack FlingShot = new SpecialAttack(Stat.FlingShot);
        public static SpecialAttack Backstab = new SpecialAttack(Stat.Backstab);
        public static SpecialAttack SneakAttack = new SpecialAttack(Stat.SneakAttack);
        public static SpecialAttack AimedShot = new SpecialAttack(Stat.AimedShot);

        private const double ATTACK_DELAY_BUFFER = 0.1;
        private double _nextAttack = Time.NormalTime;
        private Stat _stat;

        protected SpecialAttack(Stat stat)
        {
            _stat = stat;
        }

        public bool IsAvailable()
        {
            if (Time.NormalTime < _nextAttack)
                return false;

            IntPtr pEngine = N3Engine_t.GetInstance();

            if (pEngine == IntPtr.Zero)
                return false;

            // For some reason bool wouldn't work here..
            // It's also inverted...
            return N3EngineClientAnarchy_t.IsSecondarySpecialAttackAvailable(pEngine, _stat) == 0;
        }

        public bool UseOn(Dynel target)
        {
            return UseOn(target.Identity);
        }

        public unsafe bool UseOn(Identity target)
        {
            IntPtr pEngine = N3Engine_t.GetInstance();

            if (pEngine == IntPtr.Zero)
                return false;

            bool successful = N3EngineClientAnarchy_t.SecondarySpecialAttack(pEngine, &target, _stat);

            if (successful)
                _nextAttack = Time.NormalTime + ATTACK_DELAY_BUFFER;

            return successful;
        }
    }
}
