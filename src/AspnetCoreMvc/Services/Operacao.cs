namespace AspnetCoreMvc.Services;

public class Operacao : IOperacao
{
    public Operacao() : this(Guid.NewGuid())
    {
    }
    
    public Operacao(Guid id)
    {
        OperacaoId = id;
    }
    
    public Guid OperacaoId { get; private set; }
}