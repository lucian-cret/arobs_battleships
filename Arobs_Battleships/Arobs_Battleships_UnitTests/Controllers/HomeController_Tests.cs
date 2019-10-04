using Arobs_Battleships.Controllers;
using Arobs_Battleships.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Arobs_Battleships_UnitTests.Controllers
{
    public class HomeController_Tests
    {
        private readonly Mock<ILogger<HomeController>> _mockedLogger = new Mock<ILogger<HomeController>>();
        private readonly HomeController _controller;
        public HomeController_Tests()
        {
            _controller = new HomeController(_mockedLogger.Object);
        }

        [Fact]
        public void Post_ModelStateInvalid_ReturnsIndexView()
        {
            _controller.ModelState.AddModelError("errorKey", "error message");
            var result = _controller.Post(Mock.Of<GridConfigurationViewModel>()) as ViewResult;

            Assert.NotNull(result);
            Assert.Equal("Index", result.ViewName);
        }

        [Fact]
        public void Post_CreateTwoShips_Success()
        {
            var model = new GridConfigurationViewModel
            {
                Rows = 10,
                Columns = 10,
                Battleships = 2
            };
            var result = _controller.Post(model) as ViewResult;
            var viewModel = result.ViewData.Model as HomeViewModel;

            Assert.NotNull(result);
            Assert.Equal("Grid", result.ViewName);
            Assert.Equal(2, viewModel.Grid.Ships.Count);
        }
    }
}
