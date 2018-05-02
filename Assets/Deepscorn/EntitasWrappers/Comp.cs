/**
 * Created by Mikhail Tokarev (Deepscorn) on 15/07/17
 */
// TODO refactor
namespace Deepscorn.EntitasWrappers
{
    public interface IComp
    {
        EntitasData EntitasData { get; }
    }

    public interface IFlagComp<out TComponent> : IComp { }

    public interface IComp<out TEditor> : IComp
    {
        TEditor Editor { get; }
    }

    public interface IComp<out TComponent, out TEditor> : IComp<TEditor>
    {
    }

    public class Comp<TComponentType> : IFlagComp<TComponentType>
    {
        public EntitasData EntitasData { get; private set; }

        public Comp()
        {
            EntitasData = EntitasData.Create<TComponentType>();
        }
    }

    public class Comp<TComponentType, TEditorType> : Comp<TComponentType>, IComp<TComponentType, TEditorType>
        where TEditorType : new()
    {
        public TEditorType Editor { get; private set; }

        public Comp()
        {
            Editor = new TEditorType();
        }
    }
}
