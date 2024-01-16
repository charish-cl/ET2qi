using System.Collections.Generic;

namespace ET
{
    [ComponentOf(typeof(Scene))]
    [ChildType(typeof(UnitCache))]
    public class UnitCacheComponent : Entity,IAwake,IDestroy
    {
        //key:UnitCache的类型名 在Awake时初始化
        public Dictionary<string, UnitCache> UnitCaches = new Dictionary<string, UnitCache>();
        //把所有实现IUnitCache的类型名缓存起来,在Awake时初始化
        public List<string> UnitCacheKeyList = new List<string>();

    }
}