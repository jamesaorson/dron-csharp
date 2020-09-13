using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using DRON.Lex;
using DRON.Parse;

namespace DRON
{
    public static class Dron
    {
        #region Public

        #region Static Methods
        public static DronNode Parse([NotNull] string dronString)
            => ParseAsync(dronString).Result;
        
        public async static Task<DronNode> ParseAsync([NotNull] string dronString)
            => await ParseAsync(
                new MemoryStream(
                    Encoding.UTF8.GetBytes(dronString ?? "")
                )
            );

        public static DronNode Parse([NotNull] Stream stream)
            => ParseAsync(stream).Result;
        
        public async static Task<DronNode> ParseAsync([NotNull] Stream stream)
        {
            return await Task.Run(() =>
            {
                var lexer = new Lexer();
                var parser = new Parser();
                return parser.Parse(lexer.Lex(stream));
            });
        }
        #endregion

        #endregion
    }
}