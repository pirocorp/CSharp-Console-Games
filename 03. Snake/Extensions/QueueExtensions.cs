namespace Snake.Extensions;

using System;
using System.Collections.Generic;

public static class QueueExtensions
{
    public static void ForeEach<T>(this Queue<T> queue, Action<T> action)
    {
        foreach (var element in queue)
        {
            action(element);
        }
    }
}
