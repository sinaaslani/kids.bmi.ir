using System.Collections.Generic;
using BMIBranch.ServiceProxy.BMIBranchServiceRef;

namespace BMIBranch.ServiceProxy
{
    public static class BMIBranchProvider
    {
        private static readonly Dictionary<int, Unit> BranchCache = new Dictionary<int, Unit>();
        public static Unit FindUnits(int UnitId)
        {
            if (BranchCache.ContainsKey(UnitId))
                return BranchCache[UnitId];

            using (var c = new UnitsHandlerSRVSoapClient())
            {
                var u = c.GetUnit(UnitId);
                if (u == null)
                    return null;

                BranchCache.Add(u.UnitId, u);

                if (BranchCache.Count > 500)
                    BranchCache.Clear();

                return u;
            }
        }
    }
}
