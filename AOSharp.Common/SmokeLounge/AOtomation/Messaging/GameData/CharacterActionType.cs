﻿namespace SmokeLounge.AOtomation.Messaging.GameData
{
    public enum CharacterActionType
    {
        TeamRequest = 0x1A,
        CastNano = 0x13,
        TeamRequestReply = 0x1C,
        TeamKickMember = 0x16,
        LeaveTeam = 0x18,
        TeamMemberLeft = 0x20,
        AcceptTeamRequest = 0x23,
        RemoveFriendlyNano = 0x41,
        UseItemOnItem = 0x51,
        StandUp = 0x57,
        Unknown3 = 0x61,
        SetNanoDuration = 0x62,
        Death = 0x63,
        InfoRequest = 0x69,
        FinishNanoCasting = 0x6B,
        InterruptNanoCasting = 0x6C,
        DeleteItem = 0x70,
        Logout = 0x78,
        StopLogout = 0x7A,
        Equip = 0x83,
        SpecialUnavailable = 0x84,
        Die = 0x98,
        StartedSneaking = 0xA2,
        StartSneak = 0xA3,
        SpecialAvailable = 0xA4,
        SpecialUsed = 0xAA,
        Search = 0x66,
        DisableXP = 0xA5,
        ChangeVisualFlag = 0xA6,
        ChangeAnimationAndStance = 0xA7,
        ShipInvite = 0xBA,
        TrainPerk = 0xBB,
        UploadNano = 0xCC,
        TradeskillSourceChanged = 0xDC,
        TradeskillTargetChanged = 0xDD,
        TradeskillBuildPressed = 0xDE,
        TradeskillSource = 0xDF,
        TradeskillTarget = 0xE0,
        TradeskillNotValid = 0xE1,
        TradeskillOutOfRange = 0xE2,
        TradeskillRequirement = 0xE3,
        TradeskillResult = 0xE4,
        TransferLeader = 0x19,
        TeamRequestInvite = 0x1A,
        Split = 0x22,
        DuelUpdate = 0x106,
        TeamRequestResponse = 0x23,
        SplitItem = 0x34,
        QueuePerk = 0x50,
        UsePerk = 0xB3,
        PerkAvailable = 0xCE,
        PerkUnavailable = 0xCF,
        JoinBattlestationQueue = 0xFD,
        LeaveBattlestationQueue = 0xFF,
        Inspect = 0x105,
    }
}