namespace AutoGlass.Domain.Interfaces.Services
{
    public interface IServiceEmail
    {
        bool Enviar(string destinatario, string assunto, string corpoMensagem);
    }
}
