﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OrgServerMessageType.cs" company="SmokeLounge">
//   Copyright © 2013 SmokeLounge.
//   This program is free software. It comes without any warranty, to
//   the extent permitted by applicable law. You can redistribute it
//   and/or modify it under the terms of the Do What The Fuck You Want
//   To Public License, Version 2, as published by Sam Hocevar. See
//   http://www.wtfpl.net/ for more details.
// </copyright>
// <summary>
//   Defines the OrgServerMessageType type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SmokeLounge.AOtomation.Messaging.Messages.N3Messages
{
    public enum OrgServerMessageType : byte
    {
        OrgContract = 0x01,
        OrgInfo = 0x02, 
        OrgInvite = 0x05
    }
}