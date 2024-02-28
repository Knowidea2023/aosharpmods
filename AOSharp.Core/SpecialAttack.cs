using System;
using System.Collections.Generic;
using System.Linq;
using AOSharp.Common.GameData;
using AOSharp.Common.Unmanaged.Imports;

namespace AOSharp.Core
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

        private const double ATTACK_DELAY_BUFFER = 0.5;
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

            //Why is this inverted lol?
            return !N3EngineClientAnarchy_t.IsSecondarySpecialAttackAvailable(pEngine, _stat);
        }

        public unsafe bool IsInRange(Dynel target)
        {
            const EquipSlot MainHand = EquipSlot.Weap_RightHand;
            const EquipSlot OffHand = EquipSlot.Weap_LeftHand;

            Dictionary<EquipSlot, WeaponItem> weapons = DynelManager.LocalPlayer.Weapons;

            SpecialAttack specialAttack = this == Backstab ? SpecialAttack.SneakAttack : this;

            if (weapons.Count > 0 && _stat != Stat.Brawl)
            {
                if (weapons.ContainsKey(MainHand) && weapons[MainHand].SpecialAttacks.Contains(specialAttack))
                    return weapons[MainHand].IsDynelInRange(target);
                else if (weapons.ContainsKey(OffHand) && weapons[OffHand].SpecialAttacks.Contains(specialAttack))
                    return weapons[OffHand].IsDynelInRange(target);
                else
                    return false;
            }
            else
            {
                IntPtr pWeaponHolder = DynelManager.LocalPlayer.pWeaponHolder;
                IntPtr dummyWeapon = WeaponHolder_t.GetDummyWeapon(pWeaponHolder, _stat);

                if (dummyWeapon == null)
                    return false;

                IntPtr pdummyWeaponUnk = *(IntPtr*)(dummyWeapon + 0xE4);

                return WeaponHolder_t.IsDynelInWeaponRange(pWeaponHolder, pdummyWeaponUnk, target.Pointer);
            }
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

            bool successful = N3EngineClientAnarchy_t.SecondarySpecialAttack(pEngine, ref target, _stat);

            if (successful)
                _nextAttack = Time.NormalTime + ATTACK_DELAY_BUFFER;

            return successful;
        }

        public override string ToString()
        {
            return _stat.ToString();
        }
    }
}
