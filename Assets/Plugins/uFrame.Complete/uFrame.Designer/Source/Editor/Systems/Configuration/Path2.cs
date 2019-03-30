using System.IO;
using System.Linq;

namespace uFrame.Editor.Configurations
{
    public static class Path2
    {
        public static string Combine(params string[] paths)
        {
            var result = paths.First();
            foreach (var item in paths.Skip(1))
            {
                result = Path.Combine(result, item);
            }
            return result;
        }
    }
}