using HelloWorldRxUI;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HelloWorldRxUITests
{
    [TestClass]
    public class AppViewModelTests
    {
        [TestMethod]
        public void TestGreeting()
        {
            var viewModel = new AppViewModel();
            viewModel.Name = "Foo";
            Assert.IsTrue(viewModel.Greeting.Equals("Hello Foo"), "Greeting is {0} but should be 'Hello Foo'", viewModel.Greeting);
        }
    }
}
