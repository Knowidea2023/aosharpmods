using AOSharp.Common.GameData;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOSharp.Navigator
{
    public class FixerGridTerminalLink : UseOnTerminalLink
    {
        public FixerGridTerminalLink(Vector3 terminalPos) : base(PlayfieldId.FixerGrid, "Data Receptacle", "Enter The Grid", terminalPos)
        {
        }
    }
}
