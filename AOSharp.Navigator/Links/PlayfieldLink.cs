using AOSharp.Common.GameData;
using JsonKnownTypes;
using Newtonsoft.Json;

namespace AOSharp.Navigator
{
    [JsonConverter(typeof(JsonKnownTypesConverter<PlayfieldLink>))]
    [JsonKnownType(typeof(TerminalLink), "TerminalLink")]
    public class PlayfieldLink : NavigatorTask
    {
        protected PlayfieldLink(PlayfieldId dstId) : base(dstId)
        {
        }
    }
}
