using System;

namespace AOSharp.Common.GameData
{
    [Flags]
    public enum CharacterFlags
    {
        None = 0x00,
        Unknown = 0x01,
        Unknown1 = 0x08,
        Unknown2 = 0x40,
        //Tower = 0x184,
        PetTower = 0x200,
        Unknown4 = 0x800,
        Unknown5 = 0x1000,
        Tower = 0x20000,
        CollideWithStatels = 0x80000,
        Unknown7 = 0x100000,
        HasItemsForSale = 0x200000,
        HasVisibleName = 0x400000,
        HasBlueName = 0x800000,
        Pet = 0x8000000,
        Unknown8 = 0x20000000,
    }

    public enum NpcClan
    {
        EngineerAttackPet = 95,
        MPHealPets = 96,
        MPAttackPets = 97,
        MPMezzPets = 98,
        ShadowMutants = 150
    }
    public enum NpcFamily
    {
        AttackPet = 100001,
        HealPet = 110001,
        SupportPet = 120001,
        Vendor = 11001,
        GuardsA = 70001,
        GuardsB = 70002
    }

}
