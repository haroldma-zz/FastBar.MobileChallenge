﻿namespace Portable.FluentRest.Deserializers
{
    public interface IDeserializer
    {
        T Deserialize<T>(string text);
    }
}