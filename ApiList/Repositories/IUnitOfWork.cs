namespace ApiList.Repositories {
    public interface IUnitOfWork {

        ITarefasRepository TarefasRepository { get; }
        IProgressoRepository ProgressoRepository { get; }

        void Commit();
    }
}
