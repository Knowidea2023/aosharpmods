using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmokeLounge.AOtomation.Messaging.GameData;
using SmokeLounge.AOtomation.Messaging.Messages.N3Messages;

namespace AOSharp.Core
{
    public class Battlestation
    {
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

        public enum Side
        {
            Red = 0,
            Blue = 1
        }
    }
}
