using System.Collections.Generic;

namespace missingEngine
{
    public interface IMissingEngine
    {
        IEnumerable<string> GetMissing();
    }
}
