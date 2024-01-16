using System;

namespace ET
{
    public class Other2UnitCache_AddOrUpdateUnitHandler : AMActorRpcHandler<Scene, Other2UnitCache_AddOrUpdateUnit,UnitCache2Other_AddOrUpdateUnit>
    {
        protected override async ETTask Run(Scene scene, Other2UnitCache_AddOrUpdateUnit request, UnitCache2Other_AddOrUpdateUnit response, Action reply)
        {
            UpdateUnitCacheAsync(scene,request,response).Coroutine();
            //不等待，直接回复
            reply();
            await ETTask.CompletedTask;
        }

        private async ETTask UpdateUnitCacheAsync(Scene scene, Other2UnitCache_AddOrUpdateUnit request, UnitCache2Other_AddOrUpdateUnit response)
        {
            UnitCacheComponent unitCacheComponent = scene.GetComponent<UnitCacheComponent>();
            //使用后清空，塞入到对象池复用
            using ( ListComponent<Entity> entityList = ListComponent<Entity>.Create())
            {
                //请求的所有entity都添加到entityList
                for (int index = 0; index < request.EntityTypes.Count; ++index)
                {
                    Type type = Game.EventSystem.GetType(request.EntityTypes[index]);
                    //反序列化
                    Entity entity = (Entity)MongoHelper.FromBson(type, request.EntityBytes[index]);
                    entityList.Add(entity);
                }
                //添加到缓存
                await unitCacheComponent.AddOrUpdate(request.UnitId, entityList);
            }
        }
        
        
    }
}