using System;

namespace audatex.br.audabridge2.infrastructure.exception
{    
    [Serializable]
    public class IntegracaoException : Exception
    {        
        protected IntegracaoException()
            : base() { }

        public IntegracaoException(string message) 
            : base(message) { }

        public IntegracaoException(string message, Exception ex)
            : base(message, ex) { }

        public IntegracaoException(int idIntegracao, Exception ex)
            : base(string.Concat("ID INTEGRAÇÃO: ", idIntegracao), ex) { }
    }
}
