﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="QuestAction.cs" company="SmokeLounge">
//   Copyright © 2013 SmokeLounge.
//   This program is free software. It comes without any warranty, to
//   the extent permitted by applicable law. You can redistribute it
//   and/or modify it under the terms of the Do What The Fuck You Want
//   To Public License, Version 2, as published by Sam Hocevar. See
//   http://www.wtfpl.net/ for more details.
// </copyright>
// <summary>
//   Defines the QuestAction type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SmokeLounge.AOtomation.Messaging.GameData
{
    using AOSharp.Common.GameData;
    using SmokeLounge.AOtomation.Messaging.Serialization.MappingAttributes;

    public class QuestActionInfo
    {
        #region AoMember Properties

        [AoMember(0)]
        public int Version { get; set; }

        [AoMember(1)]
        public Identity Action { get; set; }

        [AoMember(2)]
        public Identity UnknownId1 { get; set; }

        [AoMember(3)]
        public Identity UnknownId2 { get; set; }

        [AoMember(4)]
        public Identity UnknownId3 { get; set; }

        [AoMember(5)]
        public Identity UnknownId4 { get; set; }

        [AoMember(6)]
        public float Unknown1 { get; set; }

        [AoMember(7)]
        public float Unknown2 { get; set; }

        [AoMember(8)]
        public float Unknown3 { get; set; }

        [AoMember(9)]
        public float Unknown4 { get; set; }
     
        [AoMember(10)]
        public Identity UnknownId5 { get; set; }

        [AoMember(11)]
        public float Unknown5 { get; set; }

        [AoMember(12)]
        public float Unknown6 { get; set; }

        [AoMember(13)]
        public float Unknown7 { get; set; }

        [AoMember(14)]
        public float Unknown8 { get; set; }

        [AoMember(15)]
        public Identity UnknownId6 { get; set; }

        [AoMember(16, SerializeSize = Serialization.ArraySizeType.NoSerialization, FixedSizeLength = 4, IsFixedSize = true)]
        public string UnknownHash1 { get; set; }

        [AoMember(17)]
        public int Unknown9 { get; set; }
      
        [AoMember(18)]
        public Identity UnknownId7 { get; set; }

        [AoMember(19)]
        public Identity PlayfieldId { get; set; }

        [AoMember(20)]
        public int Unknown10 { get; set; }

        [AoMember(21)]
        public int Unknown11 { get; set; }
       
        [AoMember(22)]
        public Vector3 Position { get; set; }
        #endregion
    }
}