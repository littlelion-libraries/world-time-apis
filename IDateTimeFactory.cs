using System;
using System.Collections.Generic;

namespace WorldTimeAPIs
{
    public interface IDateTimeFactory
    {
        IEnumerator<DateTime?> CreateDateTimeAsync();
    }
}