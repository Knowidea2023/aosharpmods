using AOSharp.Common.GameData;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOSharp.Navigator
{
    public class TerminalLink : PlayfieldLink
    {
        public string TerminalName;

        [JsonConverter(typeof(Vector3Converter))]
        public Vector3 TerminalPos;

        public TerminalLink(PlayfieldId dstId, string terminalName, Vector3 terminalPos) : base(dstId)
        {
            TerminalName = terminalName;
            TerminalPos = terminalPos;
        }
    }
}
