/**
 * Created by Mikhail Tokarev (Deepscorn) on 15/07/17
 */
using Entitas;

namespace Deepscorn.EntitasWrappers
{
    // TODO separate files

    public interface ISingleValueComponent<TValue> : IComponent
    {
        TValue Value { get; }
    }

    public interface ISingleValueEditor<TValue>
    {
        Entity Apply(TValue value);
    }

    public class SingleValueComponent<TValue> : ISingleValueComponent<TValue>
    {
        // TODO make backing field explicit - to reduce boilerplate in PositionComponent
        public TValue Value { get; private set; }

        public class Editor : ComponentEditor<SingleValueComponent<TValue>>,
            ISingleValueEditor<TValue>
        {
            public Entity Apply(TValue value)
            {
                component.Value = value;
                return Apply();
            }
        }
    }

    /*public class SingleValueAddComponent<TValue> : ISingleValueComponent<TValue>
    {
        public TValue Value { get; private set; }

        public class Editor : AddComponentEditor<SingleValueAddComponent<TValue>>,
            ISingleValueEditor<TValue>
        {
            public Entity Apply(TValue value)
            {
                component.Value = value;
                return Apply();
            }
        }
    }*/
}
