using System.Diagnostics.CodeAnalysis;

public static class ListExtensions
{
    public static bool TryGet<T>(this List<T> list,Predicate<T> condition, [MaybeNullWhen(false)] out T item)
    {
        item = list.Find(condition);
        return item != null;
    }

    public static bool TryGetAndDo<T>(this List<T> list, Predicate<T> condition, Action<T> action)
    {
        if (!list.TryGet(condition, out var item)) return false;
        
        action(item);
        return true;

    }
}