using System;

namespace ApiCatalogoJogos.Exceptions {
    public class NotRegisteredGameException : Exception {
        public NotRegisteredGameException() : base("Este jogo não está cadastrado") {}
    }
}