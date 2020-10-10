using LevelLearn.Domain.Entities.Institucional;
using LevelLearn.Domain.Repositories.Institucional;
using LevelLearn.Domain.UnityOfWorks;
using LevelLearn.Service.Response;
using LevelLearn.Service.Services.Institucional;
using LevelLearn.XUnitTest.DomainTest;
using LevelLearn.XUnitTest.Fixtures;
using Moq;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace LevelLearn.XUnitTest.ServiceTest.Institucional
{
    [Collection(nameof(TurmaAutoMockerCollection))]
    public class TurmaServiceTest
    {
        //AAA Arrange, Act, Assert

        //Nomenclatura 
        //MetodoTestado_EstadoEmTeste_ComportamentoEsperado
        //ObjetoEmTeste_MetodoComportamentoEmTeste_ComportamentoEsperado

        private readonly TurmaTestFixture _turmaFixture;
        private readonly TurmaService _turmaService;
        public TurmaServiceTest(TurmaTestFixture turmaFixture)
        {
            _turmaFixture = turmaFixture;
            _turmaService = turmaFixture.ObterTurmaService();
        }

        [Trait("Categoria", "Turmas")]
        [Fact]
        public void TurmaService_ObterTurma_DeveEncontrarTurma()
        {
            var turma = FakerTest.CriarTurmaPadrao();
            var turmaId = turma.Id;
            
            var turmaRepositoryMock = new Mock<ITurmaRepository>();
            _turmaFixture.AutoMocker.GetMock<IUnitOfWork>().Setup(uow => uow.Turmas).Returns(turmaRepositoryMock.Object);
            turmaRepositoryMock.Setup(p => p.GetAsync(turmaId, true)).Returns(Task.FromResult(turma));

            ResultadoService<Turma> resultadoService = _turmaService.ObterTurma(turmaId).Result;

            _turmaFixture.AutoMocker.GetMock<IUnitOfWork>().Verify(r => r.Turmas.GetAsync(turmaId, true), Times.Once);
            Assert.Equal(HttpStatusCode.OK, (HttpStatusCode)resultadoService.StatusCode);
        }

    }
}
