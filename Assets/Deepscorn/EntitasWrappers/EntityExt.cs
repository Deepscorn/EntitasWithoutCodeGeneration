/**
 * Created by Mikhail Tokarev (Deepscorn) on 15/07/17
 */
using Entitas;

namespace Deepscorn.EntitasWrappers
{
    public static class EntityExt
    {
        public static TValue GetValue<TValue>(this Entity entity, IComp<ISingleValueComponent<TValue>, object> compData)
        {
            ISingleValueComponent<TValue> component = (ISingleValueComponent<TValue>)entity.GetComponent(compData.EntitasData.Id);
            return component.Value;
        }

        public static T Get<T>(this Entity entity, IComp<T, object> compData)
        {
            return (T)entity.GetComponent(compData.EntitasData.Id);
        }

        public static bool Has(this Entity entity, IComp compData)
        {
            return entity.HasComponent(compData.EntitasData.Id);
        }

        public static TEditor Add<TComponent, TEditor>(this Entity entity, IComp<TComponent, TEditor> compData)
            where TComponent : IComponent, new()
            where TEditor : IAddComponentEditor<TComponent>
        {
            TComponent comp = entity.CreateComponent<TComponent>(compData.EntitasData.Id);
            compData.Editor.SetActionAdd(entity, compData.EntitasData.Id, comp);
            return compData.Editor;
        }

        /*public static Entity Add<TComponent, TValue>(this Entity entity, IComp<TComponent, SingleValueAddComponent<TValue>.Editor> compData, TValue value)
            where TComponent : SingleValueAddComponent<TValue>, new()
        {
            TComponent comp = entity.CreateComponent<TComponent>(compData.EntitasData.Id);
            compData.Editor.SetActionAdd(entity, compData.EntitasData.Id, comp);
            return compData.Editor.Apply(value);
        }*/

        public static Entity Add<TComponent, TValue>(this Entity entity, IComp<TComponent, SingleValueComponent<TValue>.Editor> compData, TValue value)
            where TComponent : SingleValueComponent<TValue>, new()
        {
            TComponent comp = entity.CreateComponent<TComponent>(compData.EntitasData.Id);
            compData.Editor.SetActionAdd(entity, compData.EntitasData.Id, comp);
            return compData.Editor.Apply(value);
        }

        public static TEditor Replace<TComponent, TEditor>(this Entity entity, IComp<TComponent, TEditor> compData) 
            where TComponent : IComponent, new()
            where TEditor : IComponentEditor<TComponent>
        {
            TComponent comp = entity.CreateComponent<TComponent>(compData.EntitasData.Id);
            compData.Editor.SetActionReplace(entity, compData.EntitasData.Id, comp);
            return compData.Editor;
        }

        public static Entity Replace<TComponent, TValue>(this Entity entity, IComp<TComponent, SingleValueComponent<TValue>.Editor> compData, TValue value)
            where TComponent : SingleValueComponent<TValue>, new()
        {
            TComponent comp = entity.CreateComponent<TComponent>(compData.EntitasData.Id);
            compData.Editor.SetActionReplace(entity, compData.EntitasData.Id, comp);
            return compData.Editor.Apply(value);
        }

        public static Entity Remove(this Entity entity, IComp<object> compData)
        {
            return entity.RemoveComponent(compData.EntitasData.Id);
        }

        public static Entity SetFlag<TComponent>(this Entity entity, IFlagComp<TComponent> compData, bool val = true)
            where TComponent : IComponent, new()
        {
            bool curVal = entity.HasComponent(compData.EntitasData.Id);
            if (curVal != val)
            {
                if (val)
                {
                    // TODO singleton instead of pooling
                    TComponent comp = entity.CreateComponent<TComponent>(compData.EntitasData.Id);
                    entity.AddComponent(compData.EntitasData.Id, comp);
                }
                else
                {
                    entity.RemoveComponent(compData.EntitasData.Id);
                }
            }
            return entity;
        }
    }
}
