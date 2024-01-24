// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NanoEffect.cs" company="SmokeLounge">
//   Copyright © 2013 SmokeLounge.
//   This program is free software. It comes without any warranty, to
//   the extent permitted by applicable law. You can redistribute it
//   and/or modify it under the terms of the Do What The Fuck You Want
//   To Public License, Version 2, as published by Sam Hocevar. See
//   http://www.wtfpl.net/ for more details.
// </copyright>
// <summary>
//   Defines the NanoEffect type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using AOSharp.Common.GameData;

namespace SmokeLounge.AOtomation.Messaging.GameData
{
    public class InspectSlotInfo
    {
        public int Unk { get; set; }
        public EquipSlot EquipSlot { get; set; }
        public int Unk2 { get; set; }
        public Identity UniqueIdentity { get; set; }
        public int LowId { get; set; }
        public int HighId { get; set; }
        public int Ql { get; set; }
    }
}