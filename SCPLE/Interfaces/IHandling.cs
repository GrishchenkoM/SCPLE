using System;

namespace Scple.Interfaces
{
    public interface IHandling
    {
        void Handling(Exception ex);
        void Error(Exception ex, int row);
    }
}
