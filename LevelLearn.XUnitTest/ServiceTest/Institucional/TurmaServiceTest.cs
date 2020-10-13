using FluentValidation.Results;
using LevelLearn.Domain.Entities.Institucional;
using LevelLearn.Domain.Repositories.Institucional;
using LevelLearn.Domain.UnityOfWorks;
using LevelLearn.Domain.Utils.Comum;
using LevelLearn.Service.Response;
using LevelLearn.Service.Services.Institucional;
using LevelLearn.XUnitTest.DomainTest;
using LevelLearn.XUnitTest.Fixtures;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

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
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly TurmaService _turmaService;
        public TurmaServiceTest(TurmaTestFixture turmaFixture, ITestOutputHelper testOutputHelper)
        {
            _turmaFixture = turmaFixture;
            _testOutputHelper = testOutputHelper;
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
            _testOutputHelper.WriteLine($"Deve passar pelo Get em TurmaRepository. Erros encontrados na validação de turma: {turma.ResultadoValidacao.Errors.Count} {string.Join(", ",turma.ResultadoValidacao.Errors)}");
        }

        [Trait("Categoria", "Turmas")]
        [Fact]
        public void TurmaService_TurmasProfessorPorCurso_DeveEncontrarTurmasDoProfessor()
        {
            var turmasFake = FakerTest.CriarListaTurmaFake(5);
            var cursoId = turmasFake.First().CursoId;
            var professorId = turmasFake.First().ProfessorId;
            var filtroPag = new FiltroPaginacao();

            var turmaRepositoryMock = new Mock<ITurmaRepository>();
            _turmaFixture.AutoMocker.GetMock<IUnitOfWork>().Setup(uow => uow.Turmas).Returns(turmaRepositoryMock.Object);
            turmaRepositoryMock.Setup(p => p.TurmasProfessorPorCurso(cursoId, professorId, filtroPag)).Returns(Task.FromResult(turmasFake));

            ResultadoService<IEnumerable<Turma>> resultadoService = _turmaService.TurmasProfessorPorCurso(cursoId, professorId, filtroPag).Result;

            _turmaFixture.AutoMocker.GetMock<IUnitOfWork>().Verify(r => r.Turmas.TurmasProfessorPorCurso(cursoId, professorId, filtroPag), Times.Once);
            IEnumerable<ValidationFailure> errosValidacao = turmasFake.SelectMany(p => p.ResultadoValidacao.Errors);
            _testOutputHelper.WriteLine($"Erros encontrados na validação de turma: {errosValidacao.Count()} {string.Join(", ", errosValidacao)}");
        }

    }
}
