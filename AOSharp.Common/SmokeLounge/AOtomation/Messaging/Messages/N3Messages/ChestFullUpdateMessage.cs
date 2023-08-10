﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ChestFullUpdateMessage.cs" company="SmokeLounge">
//   Copyright © 2013 SmokeLounge.
//   This program is free software. It comes without any warranty, to
//   the extent permitted by applicable law. You can redistribute it
//   and/or modify it under the terms of the Do What The Fuck You Want
//   To Public License, Version 2, as published by Sam Hocevar. See
//   http://www.wtfpl.net/ for more details.
// </copyright>
// <summary>
//   Defines the ChestFullUpdateMessage type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SmokeLounge.AOtomation.Messaging.Messages.N3Messages
{
    using AOSharp.Common.GameData;
    using SmokeLounge.AOtomation.Messaging.GameData;
    using SmokeLounge.AOtomation.Messaging.Serialization;
    using SmokeLounge.AOtomation.Messaging.Serialization.MappingAttributes;

    [AoContract((int)N3MessageType.ChestFullUpdate)]
    public class ChestFullUpdateMessage : N3Message
    {
        #region Constructors and Destructors

        public ChestFullUpdateMessage()
        {
            this.N3MessageType = N3MessageType.ChestFullUpdate;
        }

        [AoMember(0)]
        public int Unknown1 { get; set; }

        [AoMember(1)]
        public Identity Owner { get; set; }

        [AoMember(2)]
        public int PlayfieldId { get; set; }

        [AoMember(3)]
        public Identity StateMachine { get; set; }

        [AoMember(4)]
        public short Unknown5 { get; set; }

        [AoMember(5, SerializeSize = ArraySizeType.X3F1)]
        public GameTuple<Stat,int>[] Stats { get; set; }

        [AoMember(6)]
        public int Unknown6 { get; set; }

        [AoMember(7)]
        public int Unknown7 { get; set; }

        [AoMember(8)]
        public int Unknown8 { get; set; }
       
        [AoMember(9, SerializeSize = ArraySizeType.X3F1)]
        public int[] UnknownArray { get; set; }

        #endregion
    }
}