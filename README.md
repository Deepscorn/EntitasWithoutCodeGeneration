# EntitasWithoutCodeGeneration
Library to use entitas without code generation

# How it works
It works thanks to generics, lookup table and reflection for initializing indexes. My intent was to keep original performance. It has drawback, it has a reflection step to initialize indexes (see EntitasData.cs). But it is done only once, the first time you use api.

# Workflow

You don't need to generate any code. 
1. You need to register each new component in lookup table. Just add line to file, you'll be using to say which component. For example, I name that file Comps.cs.
2. And you need to add an editor to each component for Add/Replace to work. For single values you can inherit SingleValueComponent to not write boilerplate. No need to write anything special for flag components.

Examples:

```csharp

public class AgeComponent : SingleValueComponent<float> {}
  
public class DestroyingComponent : IComponent {} // it's a flag, so it is simple
  
public class PositionComponent : IComponent
{
    public float x;
    public float y;

    public class Editor : ComponentEditor<PositionComponent>
    {
        public Entity Apply(float x, float y)
        {
            component.x = x;
            component.y = y;
            return Apply();
        }
    }
}

public static class Comps
{
    public static readonly IComp<AgeComponent, AgeComponent.Editor> Age = new Comp<AgeComponent, AgeComponent.Editor>();
    public static readonly IComp<PositionComponent, PositionComponent.Editor> Position = new Comp<PositionComponent, PositionComponent.Editor>();

    public static readonly IFlagComp<DestroyingComponent> Destroying = new Comp<DestroyingComponent>(); // flag is registered as IFlagComp
}
```

# Syntax

This is how you can work with Entitas without generated code. It's not such sexy, but I tried to make it as short and clean as possible.

```csharp

PositionComponent position = entity.Get(Comps.Position);
float x = position.x;

float age = entity.Get(Comps.Age).Value;
age = entity.GetValue(Comps.Age); // shorthand for ISingleValueComponent

entity.Add(Comps.Age, 2); // add AgeComponent with Value = 2
entity.Add(Comps.Position).Apply(0, 3); // add PositionComponent with x = 0, y = 3. If you know how to make it look more sexy, please, make an issue/pr

entity.Replace(Comps.Age, 5); // replace AgeComponent with Value = 5
entity.Replace(Comps.Position).Apply(5, 2); // replace PositionComponent with values x = 5, y = 2

entity.Remove(Comps.Age);

entity.SetFlag(Comps.Destroying, true); // it adds DestroyingComponent internally if not added
entity.SetFlag(Comps.Destroying, false); // it removes DestroyingComponent internally if exist

entity.Has(Comps.Destroying); // check if entity has component

```

## Notes
Entitas 0.31.1 was used when creating wrappers. If you want more recent version - report an issue/make pull request.
