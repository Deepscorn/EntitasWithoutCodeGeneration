/**
 * Created by Mikhail Tokarev (Deepscorn) on 15/07/17
 */
using System;
using Entitas;

namespace Deepscorn.EntitasWrappers
{
    // TODO remove
    // when you don't need Replace() for some component (e.g. Input)
    // use that editor. Replace() wount compile. Add() Add() will crash (no need to get weird with race of inputs, yeah?)
    public interface IAddComponentEditor<T>
    {
        void SetActionAdd(Entity entity, int componentId, T component);
    }

    public interface IComponentEditor<T>
        : IAddComponentEditor<T>
    {
        void SetActionReplace(Entity entity, int componentId, T component);
    }

    public abstract class AbstractComponentEditor<T>
        where T : IComponent
    {
        protected T component;

        private Entity entity;
        private int componentId;
        private Action applyAction;

        public void SetActionAdd(Entity nEntity, int nComponentId, T nComponent)
        {
            entity = nEntity;
            componentId = nComponentId;
            component = nComponent;
            applyAction = ApplyAdd;
        }

        public void SetActionReplace(Entity nEntity, int nComponentId, T nComponent)
        {
            entity = nEntity;
            componentId = nComponentId;
            component = nComponent;
            applyAction = ApplyReplace;
        }

        protected Entity Apply()
        {
            applyAction();
            Entity result = entity;
            entity = null;
            component = default(T);
            return result;
        }

        private void ApplyAdd()
        {
            entity.AddComponent(componentId, component);
        }

        private void ApplyReplace()
        {
            entity.ReplaceComponent(componentId, component);
        }
    }
    
    /*public class AddComponentEditor<T> : AbstractComponentEditor<T>, IAddComponentEditor<T>
        where T : IComponent
    {
    }*/
    
    public class ComponentEditor<T> : AbstractComponentEditor<T>, IComponentEditor<T> 
        where T : IComponent
    {
    }
}
