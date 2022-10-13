﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HealthDamageMessage.cs" company="SmokeLounge">
//   Copyright © 2013 SmokeLounge.
//   This program is free software. It comes without any warranty, to
//   the extent permitted by applicable law. You can redistribute it
//   and/or modify it under the terms of the Do What The Fuck You Want
//   To Public License, Version 2, as published by Sam Hocevar. See
//   http://www.wtfpl.net/ for more details.
// </copyright>
// <summary>
//   Defines the HealthDamageMessage type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using AOSharp.Common.GameData;

namespace SmokeLounge.AOtomation.Messaging.Messages.N3Messages
{
    using SmokeLounge.AOtomation.Messaging.GameData;
    using SmokeLounge.AOtomation.Messaging.Serialization.MappingAttributes;

    [AoContract((int)N3MessageType.HealthDamage)]
    public class HealthDamageMessage : N3Message
    {
        #region Constructors and Destructors

        public HealthDamageMessage()
        {
            this.N3MessageType = N3MessageType.HealthDamage;
        }

        #endregion

        #region AoMember Properties

        [AoMember(0)]
        public int TargetHp { get; set; }

        [AoMember(1)]
        public int Amount { get; set; }

        [AoMember(2)]
        public Stat Stat { get; set; }

        [AoMember(3)]
        public int Unk1 { get; set; }

        [AoMember(4)]
        public Identity Target { get; set; }

        [AoMember(5)]
        public int Unk2 { get; set; }
        #endregion
    }
}