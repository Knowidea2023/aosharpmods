using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AOSharp.Common.GameData;
using SmokeLounge.AOtomation.Messaging.GameData;
using SmokeLounge.AOtomation.Messaging.Messages.N3Messages;

namespace AOSharp.Core
{
    public class Battlestation
    {
        public static EventHandler<BattlestationInviteEventArgs> Invited;

        public static void JoinQueue(Side side)
        {
            Network.Send(new CharacterActionMessage
            {
                Action = CharacterActionType.JoinBattlestationQueue,
                Target = DynelManager.LocalPlayer.Identity,
                Parameter2 = (int)side
            });
        }

        public static void LeaveQueue()
        {
            Network.Send(new CharacterActionMessage
            {
                Action = CharacterActionType.LeaveBattlestationQueue,
                Target = DynelManager.LocalPlayer.Identity
            });
        }

        public static void AcceptInvite(Identity battlestationIdentity)
        {
            Network.Send(new AcceptBSInviteMessage
            {
                UnkIdentity = battlestationIdentity,
                UnkByte = 1
            });
        }

        internal static void OnBattlestationInvite(Identity battlestationIdentity)
        {
            Invited?.Invoke(null, new BattlestationInviteEventArgs(battlestationIdentity));
        }

        public enum Side
        {
            Red = 0,
            Blue = 1
        }
    }

    public class BattlestationInviteEventArgs : EventArgs
    {
        public Identity BattlestationIdentity { get; }

        public BattlestationInviteEventArgs(Identity battlestationIdentity)
        {
            BattlestationIdentity = battlestationIdentity;
        }

        public void Accept()
        {
            Battlestation.AcceptInvite(BattlestationIdentity);
        }
    }
}
