using AOSharp.Common.GameData;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOSharp.Navigator
{
    public class UseOnTerminalLink : TerminalLink
    {
        public string ItemName;

        public UseOnTerminalLink(PlayfieldId dstId, string itemName, string terminalName, Vector3 terminalPos) : base(dstId, terminalName, terminalPos)
        {
            ItemName = itemName;
        }
    }
}
