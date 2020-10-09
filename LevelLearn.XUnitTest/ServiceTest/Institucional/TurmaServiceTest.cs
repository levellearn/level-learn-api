using LevelLearn.Domain.Entities.Institucional;
using LevelLearn.Domain.Repositories.Institucional;
using LevelLearn.Domain.UnityOfWorks;
using LevelLearn.Resource;
using LevelLearn.Service.Response;
using LevelLearn.Service.Services.Institucional;
using LevelLearn.XUnitTest.DomainTest;
using Moq;
using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace LevelLearn.XUnitTest.ServiceTest.Institucional
{
    public class TurmaServiceTest
    {
        //AAA Arrange, Act, Assert

        //Nomenclatura 
        //MetodoTestado_EstadoEmTeste_ComportamentoEsperado
        //ObjetoEmTeste_MetodoComportamentoEmTeste_ComportamentoEsperado

        public TurmaServiceTest()
        {

        }

        [Trait("Categoria", "Turmas")]
        [Fact]
        public void TurmaService_ObterTurma_DeveEncontrarTurma()
        {
            var turma = FakerTest.CriarTurmaPadrao();
            var turmaId = turma.Id;
            var sharedResource = new Mock<ISharedResource>();
            var uow = new Mock<IUnitOfWork>();
            var turmaRepository = new Mock<ITurmaRepository>();
            uow.Setup(uow => uow.Turmas).Returns(turmaRepository.Object);
            turmaRepository.Setup(p => p.GetAsync(turmaId, true)).Returns(Task.FromResult(turma));

            var turmaService = new TurmaService(uow.Object, sharedResource.Object);

            ResultadoService<Turma> resultadoService = turmaService.ObterTurma(turmaId).Result;

            uow.Verify(r => r.Turmas.GetAsync(turmaId, true), Times.Once);
            Assert.Equal(HttpStatusCode.OK, (HttpStatusCode)resultadoService.StatusCode);
        }

    }
}
