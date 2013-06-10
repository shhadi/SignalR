using System;

namespace Microsoft.AspNet.SignalR.Client
{
    [Flags]
    public enum TraceLevels
    {
        None = 0,
        Messages = 1,
        Events = 2,
        StateChanges = 4,
        All = Messages | Events | StateChanges
    }

#if WINDOWS_PHONE7
    public static class TraceLevelsExtensions
    {
        public static bool HasFlag(this TraceLevels @this, TraceLevels traceLevels)
        {
            return ((int)@this & (int)traceLevels) == (int)traceLevels;
        }
    }
#endif
}
