using AOSharp.Common.GameData;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOSharp.Navigator
{
    public class ZoneBorderLink : PlayfieldLink
    {
        [JsonConverter(typeof(Vector3ListConverter))]
        public List<Vector3> TransitionSpots;

        public ZoneBorderLink(PlayfieldId dstId, List<Vector3> transitionSpots) : base(dstId)
        {
            TransitionSpots = transitionSpots;
        }
    }
}
