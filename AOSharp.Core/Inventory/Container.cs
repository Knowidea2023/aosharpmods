using AOSharp.Common.GameData;
using AOSharp.Core.GameData;
using AOSharp.Common.Unmanaged.Imports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AOSharp.Core.Inventory
{
    public unsafe class Container : ItemHolder
    {
        public override List<Item> Items => Inventory.GetContainerItems(Identity);
        public bool IsOpen => GetIsOpen();
        public readonly Identity Identity;

        internal Container(Identity identity)
        {
            Identity = identity;
        }

        private bool GetIsOpen()
        {
            IntPtr pEngine = N3Engine_t.GetInstance();

            if (pEngine == IntPtr.Zero)
                return false;

            Identity identity = Identity;
            IntPtr pItems = N3EngineClientAnarchy_t.GetInventoryVec(pEngine, ref identity);

            return pItems != IntPtr.Zero;
        }
    }
}
