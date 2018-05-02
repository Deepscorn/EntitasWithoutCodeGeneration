/**
 * Created by Mikhail Tokarev (Deepscorn) on 15/07/17
 */
using System;
using System.Collections.Generic;
using System.Reflection;
using Entitas;

namespace Deepscorn.EntitasWrappers
{
    // TODO move to separate file
    public class EntitasData
    {
        public static EntitasDataSummary Summary
        {
            get
            {
                if (summary == null)
                {
                    if (createStarted)
                    {
                        throw new InvalidOperationException("recursion");
                    }
                    CreateSummaryIfNotCreated();
                }
                return summary;
            }
        }
        private static EntitasDataSummary summary;
        private static bool createStarted;

        public static void CreateSummaryIfNotCreated()
        {
            if (!createStarted)
            {
                createStarted = true;
                summary = new EntitasDataSummary(typeof(Comps));
            }
        }

        public int Id { get; private set; }
        public IMatcher Match { get; private set; }
        private readonly string name;
        private readonly Type type;

        public static EntitasData Create<T>()
        {
            return new EntitasData(typeof(T));
        }

        private EntitasData(Type componentType)
        {
            type = componentType;
            string name = type.Name;
            int lastIndexOf = name.LastIndexOf("Component");
            this.name = (lastIndexOf == -1) ? name : name.Substring(0, lastIndexOf);
            CreateSummaryIfNotCreated();
        }

        public class EntitasDataSummary
        {
            public readonly string[] ComponentNames;
            public readonly Type[] ComponentTypes;

            internal EntitasDataSummary(Type containingType)
            {
                FieldInfo[] fields = containingType.GetFields();
                ComponentNames = new string[fields.Length];
                ComponentTypes = new Type[fields.Length];
                IList<IComp> comps = new List<IComp>();
                foreach (FieldInfo field in fields)
                {
                    IComp comp = (IComp) field.GetValue(null); // force jit call constructor on each component
                    comps.Add(comp);
                }
                for (int i = 0; i < fields.Length; ++i)
                {
                    EntitasData data = comps[i].EntitasData;
                    data.Id = i;
                    ComponentNames[i] = data.name;
                    ComponentTypes[i] = data.type;
                    var matcher = (Matcher) Matcher.AllOf(i);
                    matcher.componentNames = ComponentNames;
                    data.Match = matcher;
                }
            }
        }
    }
}
