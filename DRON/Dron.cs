using DRON.Lex;
using DRON.Parse;
using DRON.Deserialization;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using DRON.Serialization;

namespace DRON
{
    public static class Dron
    {
        #region Public

        #region Static Methods
        public static T Deserialize<T>([NotNull] string dronString)
            where T : class, new()
            => DeserializeAsync<T>(dronString).Result;
        
        public async static Task<T> DeserializeAsync<T>([NotNull] string dronString)
            where T : class, new()
            => await DeserializeAsync<T>(
                new MemoryStream(
                    Encoding.UTF8.GetBytes(dronString ?? "")
                )
            );

        public static T Deserialize<T>([NotNull] Stream stream)
            where T : class, new()
            => DeserializeAsync<T>(stream).Result;
        
        public async static Task<T> DeserializeAsync<T>([NotNull] Stream stream)
            where T : class, new()
        {
            var node = await Task.Run(() =>
                {
                    var lexer = new Lexer();
                    var parser = new Parser();
                    return parser.Parse(lexer.Lex(stream));
                }
            );
            return new Deserializer().Deserialize<T>(node);
        }

        public static string Serialize<T>(T value)
            where T : class, new()
        {
            var serializer = new Serializer();
            return serializer.ToDronSourceString(
                serializer.Serialize<T>(value)
            );
        }
        #endregion

        #endregion
    }
}