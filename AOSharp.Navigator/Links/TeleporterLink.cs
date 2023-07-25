using AOSharp.Common.GameData;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOSharp.Navigator
{
    public class TeleporterLink : PlayfieldLink
    {
        [JsonConverter(typeof(Vector3Converter))]
        public Vector3 TeleporterPos;

        public TeleporterLink(PlayfieldId dstId, Vector3 teleporterPos) : base(dstId)
        {
            TeleporterPos = teleporterPos;
        }
    }
}
