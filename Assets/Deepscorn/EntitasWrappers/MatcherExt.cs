/**
 * Created by Mikhail Tokarev (Deepscorn) on 15/07/17
 */
using System.Linq;
using Deepscorn.EntitasWrappers;

namespace Entitas
{
    public static class MatcherExt
    {
        public static INoneOfMatcher NoneOf(this IAllOfMatcher matcher, params IComp[] compDatas)
        {
            return matcher.NoneOf(compDatas.Select(compData => compData.EntitasData.Match).ToArray());
        }

        public static INoneOfMatcher NoneOf(this IAnyOfMatcher matcher, params IComp[] compDatas)
        {
            return matcher.NoneOf(compDatas.Select(compData => compData.EntitasData.Match).ToArray());
        }
    }

    public partial class Matcher
    {
        // TODO use AllOf instead everywhere
        // reason: chaining works on IAllOfMatcher, not on regular matcher:
        // GameMatcher
        // .AllOf(GameMatcher.Position)
        // .AnyOf(GameMatcher.Health, GameMatcher.Interactive)
        // .NoneOf(GameMatcher.Animating);
        // source: https://github.com/sschmid/Entitas-CSharp/wiki/The-Basics
        public static IMatcher Of(IComp compData)
        {
            return compData.EntitasData.Match;
        }

        // TODO deny less with 2 parameters if AllOf is useless for <= 1 params
        public static IAllOfMatcher AllOf(params IComp[] compDatas)
        {
            return AllOf(compDatas.Select(compData => compData.EntitasData.Match).ToArray());
        }

        public static IAnyOfMatcher AnyOf(params IComp[] compDatas)
        {
            return AnyOf(compDatas.Select(compData => compData.EntitasData.Match).ToArray());
        }
    }
}
