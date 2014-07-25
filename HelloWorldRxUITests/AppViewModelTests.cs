using System;
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

        [TestMethod]
        public void TestCommand()
        {
            var viewModel = new AppViewModel();
            bool called = false;
            viewModel.ContinueCommand.Subscribe(_ => called = true);
            viewModel.ContinueCommand.Execute(null);
            Assert.IsTrue(called, "Command action not called");
        }
    }
}
