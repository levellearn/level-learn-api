using LevelLearn.Service.Services.Institucional;
using Moq.AutoMock;
using System;
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
            return AutoMocker.CreateInstance<TurmaService>();
        }


        public void Dispose()
        {

        }
    }

}
