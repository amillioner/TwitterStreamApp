using System;

namespace Twitter.StreamApp.Stream.Stream.Interfaces
{
    public interface IStreamServiceManager
    {
        IStreamService Get(Type service);
    }
}