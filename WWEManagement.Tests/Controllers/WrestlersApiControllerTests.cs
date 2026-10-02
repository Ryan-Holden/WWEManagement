using Microsoft.AspNetCore.Mvc;
using Moq;
using WWEManagement.Data.Models;
using WWEManagement.Web.Controllers.Api;
using WWEManagement.Web.Dtos;
using WWEManagement.Web.Services;

namespace WWEManagement.Tests.Controllers;

public class WrestlersApiControllerTests
{
    [Fact]
    public void GetById_ReturnsNotFound_WhenWrestlerDoesNotExist()
    {
        Mock<IWrestlerService> serviceMock =
            new Mock<IWrestlerService>();

        serviceMock
            .Setup(s => s.GetById(999))
            .Returns((WrestlerDetails?)null);

        WrestlersApiController controller =
            new WrestlersApiController(
                serviceMock.Object);

        ActionResult<WrestlerDetails> result =
            controller.GetById(999);

        Assert.IsType<NotFoundResult>(
            result.Result);
    }

    [Fact]
    public void GetById_ReturnsOk_WhenWrestlerExists()
    {
        Mock<IWrestlerService> serviceMock =
            new Mock<IWrestlerService>();

        WrestlerDetails wrestler =
            new WrestlerDetails
            {
                WrestlerId = 2,
                EmployeeId = 1,
                FirstName = "Cody",
                LastName = "Rhodes",
                JobTitle = "Wrestler",
                HireDate = new DateTime(2019, 1, 1),
                RingName = "Cody Rhodes",
                WeightClass = "Heavyweight",
                IsActive = true
            };

        serviceMock
            .Setup(s => s.GetById(2))
            .Returns(wrestler);

        WrestlersApiController controller =
            new WrestlersApiController(
                serviceMock.Object);

        ActionResult<WrestlerDetails> result =
            controller.GetById(2);

        OkObjectResult okResult =
            Assert.IsType<OkObjectResult>(
                result.Result);

        WrestlerDetails returned =
            Assert.IsType<WrestlerDetails>(
                okResult.Value);

        Assert.Equal(
            "Cody Rhodes",
            returned.RingName);
    }

    [Fact]
    public void Create_ReturnsCreatedAtAction()
    {
        Mock<IWrestlerService> serviceMock =
            new Mock<IWrestlerService>();

        WrestlerRequestDto request =
            new WrestlerRequestDto
            {
                FirstName = "Test",
                LastName = "Wrestler",
                HireDate = new DateTime(2026, 10, 1),
                RingName = "Test Ring Name",
                WeightClass = "Heavyweight",
                DebutDate = new DateTime(2026, 10, 1),
                IsActive = true
            };

        WrestlerDetails createdWrestler =
            new WrestlerDetails
            {
                WrestlerId = 10,
                EmployeeId = 14,
                FirstName = "Test",
                LastName = "Wrestler",
                JobTitle = "Wrestler",
                HireDate = new DateTime(2026, 10, 1),
                RingName = "Test Ring Name",
                WeightClass = "Heavyweight",
                DebutDate = new DateTime(2026, 10, 1),
                IsActive = true
            };

        serviceMock
            .Setup(s => s.Create(request))
            .Returns(10);

        serviceMock
            .Setup(s => s.GetById(10))
            .Returns(createdWrestler);

        WrestlersApiController controller =
            new WrestlersApiController(
                serviceMock.Object);

        ActionResult<WrestlerDetails> result =
            controller.Create(request);

        CreatedAtActionResult createdResult =
            Assert.IsType<CreatedAtActionResult>(
                result.Result);

        Assert.Equal(
            nameof(WrestlersApiController.GetById),
            createdResult.ActionName);

        WrestlerDetails returned =
            Assert.IsType<WrestlerDetails>(
                createdResult.Value);

        Assert.Equal(10, returned.WrestlerId);
    }

    [Fact]
    public void Update_ReturnsNotFound_WhenWrestlerDoesNotExist()
    {
        Mock<IWrestlerService> serviceMock =
            new Mock<IWrestlerService>();

        WrestlerRequestDto request =
            new WrestlerRequestDto
            {
                FirstName = "Missing",
                LastName = "Wrestler",
                HireDate = DateTime.Today,
                RingName = "Missing",
                IsActive = true
            };

        serviceMock
            .Setup(s => s.Update(999, request))
            .Returns(false);

        WrestlersApiController controller =
            new WrestlersApiController(
                serviceMock.Object);

        IActionResult result =
            controller.Update(
                999,
                request);

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public void Delete_ReturnsNoContent_WhenDeleteSucceeds()
    {
        Mock<IWrestlerService> serviceMock =
            new Mock<IWrestlerService>();

        serviceMock
            .Setup(s => s.Delete(10))
            .Returns(true);

        WrestlersApiController controller =
            new WrestlersApiController(
                serviceMock.Object);

        IActionResult result =
            controller.Delete(10);

        Assert.IsType<NoContentResult>(result);

        serviceMock.Verify(
            s => s.Delete(10),
            Times.Once);
    }
}