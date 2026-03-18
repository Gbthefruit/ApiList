using ApiList.Context;

namespace ApiList.Repositories {
    public class UnitOfWork : IUnitOfWork {

        private ITarefasRepository? _tarefasRepo;
        private IProgressoRepository? _progressoRepo;
        readonly TarefaDbContext _context;

        public UnitOfWork(TarefaDbContext context) { _context = context; }

        public ITarefasRepository TarefasRepository {

            // verifica se ja existe uma instancia, caso nao exista, cria uma nova.
            get {
                return _tarefasRepo = _tarefasRepo ?? new TarefasRepository(_context);
            }
        }

        public IProgressoRepository ProgressoRepository {

            get {
                return _progressoRepo = _progressoRepo ?? new ProgressoRepository(_context);
            }
        }

        public void Commit() {
            _context.SaveChanges();
        }

        public void Dispose() {
            _context.Dispose();
        }
    }
}
