using AOSharp.Common.GameData;
using org.critterai.nav;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOSharp.Navigator.BT
{
    public class NavigatorContext
    {
        internal AONavigator Navigator;

        internal Queue<NavigatorTask> Tasks = new Queue<NavigatorTask>();

        internal Dictionary<PlayfieldId, Navmesh> NavmeshCache = new Dictionary<PlayfieldId, Navmesh>();

        public NavigatorContext(AONavigator navigator)
        {
            Navigator = navigator;
        }
    }
}
