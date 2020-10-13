using LevelLearn.Domain.Repositories.Institucional;
using LevelLearn.Domain.UnityOfWorks;
using LevelLearn.Service.Services.Institucional;
using Moq;
using Moq.AutoMock;
using System;
using System.Threading.Tasks;
using Xunit;

namespace LevelLearn.XUnitTest.Fixtures
{
    [CollectionDefinition(nameof(TurmaAutoMockerCollection))]
    public class TurmaAutoMockerCollection: ICollectionFixture<TurmaTestFixture> { }

    public class TurmaTestFixture : IDisposable
    {
        public AutoMocker AutoMocker { get; private set; }

        public TurmaTestFixture()
        {
            AutoMocker = new AutoMocker();
        }

        public TurmaService ObterTurmaService()
        {
            var turmaService = AutoMocker.CreateInstance<TurmaService>();
            AutoMocker.GetMock<IUnitOfWork>().Setup(uow => uow.CommitAsync()).Returns(Task.FromResult(true));
            AutoMocker.GetMock<IUnitOfWork>().Setup(uow => uow.Commit()).Returns(true);
            return turmaService;
        }

        public Mock<ITurmaRepository> SetupTurmaRepositoryUoW()
        {
            var turmaRepositoryMock = new Mock<ITurmaRepository>();
            AutoMocker.GetMock<IUnitOfWork>().Setup(uow => uow.Turmas).Returns(turmaRepositoryMock.Object);
            return turmaRepositoryMock;
        }

        public Mock<ICursoRepository> SetupCursoRepositoryUoW()
        {
            var cursoRepository = new Mock<ICursoRepository>();
            AutoMocker.GetMock<IUnitOfWork>().Setup(uow => uow.Cursos).Returns(cursoRepository.Object);
            return cursoRepository;
        }


        public void Dispose()
        {

        }
    }

}
