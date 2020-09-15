using DRON.Lex;
using DRON.Parse;
using DRON.Deserialization;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace DRON
{
    public static class Dron
    {
        #region Public

        #region Static Methods
        public static T Deserialize<T>([NotNull] string dronString)
            where T : new()
            => DeserializeAsync<T>(dronString).Result;
        
        public async static Task<T> DeserializeAsync<T>([NotNull] string dronString)
            where T : new()
            => await DeserializeAsync<T>(
                new MemoryStream(
                    Encoding.UTF8.GetBytes(dronString ?? "")
                )
            );

        public static T Deserialize<T>([NotNull] Stream stream)
            where T : new()
            => DeserializeAsync<T>(stream).Result;
        
        public async static Task<T> DeserializeAsync<T>([NotNull] Stream stream)
            where T : new()
        {
            var node = await Task.Run(() =>
                {
                    var lexer = new Lexer();
                    var parser = new Parser();
                    return parser.Parse(lexer.Lex(stream));
                }
            );
            return Deserializer.Deserialize<T>(node);
        }
        #endregion

        #endregion
    }
}