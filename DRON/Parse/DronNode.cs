using System.Diagnostics.CodeAnalysis;

namespace DRON.Parse
{
    public abstract class DronNode
    {
        #region Protected

        #region Static Methods
        protected static string TrimQuotes([NotNull] string s)
        {
            if (s[0] == '"')
            {
                s = s.Substring(1);
                if (s[s.Length - 1] == '"')
                {
                    s = s.Substring(0, s.Length - 1);
                }
            }
            return s;
        }
        #endregion

        #endregion
    }
}
