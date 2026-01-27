namespace AspnetCoreMvc.Services;

public class OperacaoService
{
    public OperacaoService(
        IOperacaoTransient transient,
        IOperacaoScoped scoped,
        IOperacaoSingleton singleton,
        IOperacaoSingletonInstance singletonInstance)
    {
        Transient = transient;
        Scoped = scoped;
        Singleton = singleton;
        SingletonInstance = singletonInstance;
    }
    
    public IOperacaoTransient Transient { get; set; }
    public IOperacaoScoped Scoped { get; set; }
    public IOperacaoSingleton Singleton { get; set; }
    public IOperacaoSingletonInstance SingletonInstance { get; set; }
}