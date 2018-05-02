/**
 * Created by Mikhail Tokarev (Deepscorn) on 15/07/17
 */
using Entitas;

namespace Deepscorn.EntitasWrappers
{
    public static class PoolExt
    {
        public static Group GetGroup(this Pool pool, IComp compData)
        {
            return pool.GetGroup(compData.EntitasData.Match);
        }
    }
}
