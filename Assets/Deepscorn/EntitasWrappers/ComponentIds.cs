/**
 * Created by Mikhail Tokarev (Deepscorn) on 15/07/17
 */
using Deepscorn.EntitasWrappers;

public static class ComponentIds
{
    public static int TotalComponents { get { return EntitasData.Summary.ComponentNames.Length; } }

    public static string[] componentNames { get { return EntitasData.Summary.ComponentNames; } }

    public static System.Type[] componentTypes { get { return EntitasData.Summary.ComponentTypes; } }
}
