﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GameTuple.cs" company="SmokeLounge">
//   Copyright © 2013 SmokeLounge.
//   This program is free software. It comes without any warranty, to
//   the extent permitted by applicable law. You can redistribute it
//   and/or modify it under the terms of the Do What The Fuck You Want
//   To Public License, Version 2, as published by Sam Hocevar. See
//   http://www.wtfpl.net/ for more details.
// </copyright>
// <summary>
//   Defines the GameTuple type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SmokeLounge.AOtomation.Messaging.GameData
{
    using SmokeLounge.AOtomation.Messaging.Serialization.MappingAttributes;

    public class OrgContractSlot
    {
        #region AoMember Properties

        [AoMember(0)]
        public int Slot { get; set; }

        [AoMember(1)]
        public int Unknown1 { get; set; } //6356993 Inactive? / 2162689 Active?

        [AoMember(2)]
        public int Unknown2 { get; set; }
       
        [AoMember(3)]
        public int Unknown3 { get; set; }
      
        [AoMember(4)]
        public int LowId { get; set; }
      
        [AoMember(5)]
        public int HighId { get; set; }
        
        [AoMember(6)]
        public int Ql { get; set; }

        [AoMember(7)]
        public int Unknown4 { get; set; }

        #endregion
    }
}