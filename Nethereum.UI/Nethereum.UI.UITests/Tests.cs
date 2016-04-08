using NUnit.Framework;
using Xamarin.UITest;

namespace Nethereum.UI.UITests
{
    [TestFixture(Platform.Android, Category = "Andorid")]
    [TestFixture(Platform.iOS, Category = "iOS")]
    public class Tests
    {
        IApp app;
        Platform platform;

        public Tests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
        }

        [Test]
        public void AppLaunches()
        {
            // Assert
            var results = app.Query(c => c.Marked("MainPageTitleLbl"));
            Assert.AreEqual("Hello Xamarin!", results[0].Text);
        }
    }
}

