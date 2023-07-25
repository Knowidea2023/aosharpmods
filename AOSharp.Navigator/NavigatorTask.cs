using AOSharp.Common.GameData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOSharp.Navigator
{
    public class NavigatorTask
    {
        public readonly PlayfieldId DstId;

        protected NavigatorTask(PlayfieldId dstId)
        {
            DstId = dstId;
        }
    }
}
