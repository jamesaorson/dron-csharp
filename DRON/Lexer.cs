using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;
using DRON.Tokens;

namespace DRON
{
    public class Lexer
    {
        #region Public

        #region Constructors
        public Lexer(string dronString)
        {
            _currentStreamValue = -1;
            _stream = new MemoryStream(
                Encoding.UTF8.GetBytes(dronString ?? "")
            );
        }

        public Lexer([NotNull] Stream stream)
        {
            if (stream is null)
            {
                throw new System.ArgumentNullException("'stream' argument cannot be null");
            }

            _stream = stream;
        }
        #endregion

        #region Member Methods
        public IEnumerable<Token> Lex()
        {
            if (_stream.Length == 0)
            {
                yield break;
            }
            
            _tokenString = "";
            _currentStreamValue = _stream.ReadByte();
            while (_currentStreamValue != -1)
            {
                _tokenString += (byte)_currentStreamValue;
                _currentToken = new ColonToken();

                yield return _currentToken;
                _currentStreamValue = _stream.ReadByte();
            }
            if (_tokenString.Trim().Length == 0)
            {
                yield break;
            }
            yield return new CloseBraceToken();
        }
        #endregion

        #endregion

        #region Protected

        #region Members
        protected int _currentStreamValue { get; set; }
        protected string _tokenString { get; set; }
        protected Token _currentToken { get; set; }
        protected Stream _stream { get; set; }
        #endregion

        #endregion

        ~Lexer()
        {
            if (_stream is not null)
            {
                _stream.Dispose();
            }
        }
    }
}