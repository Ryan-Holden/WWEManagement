using Moq;
using WWEManagement.Data.Models;
using WWEManagement.Data.Repositories;
using WWEManagement.Web.Services;
using WWEManagement.Web.ViewModels;

namespace WWEManagement.Tests.Services;

public class WrestlerServiceTests
{
    [Fact]
    public void Create_SetsEmployeeJobTitleToWrestler()
    {
        Mock<IWrestlerRepository> repositoryMock =
            new Mock<IWrestlerRepository>();

        Employee? capturedEmployee = null;
        Wrestler? capturedWrestler = null;

        repositoryMock
            .Setup(r => r.CreateWithEmployee(
                It.IsAny<Employee>(),
                It.IsAny<Wrestler>()))
            .Callback<Employee, Wrestler>(
                (employee, wrestler) =>
                {
                    capturedEmployee = employee;
                    capturedWrestler = wrestler;
                })
            .Returns(42);

        WrestlerService service =
            new WrestlerService(
                repositoryMock.Object);

        CreateWrestlerViewModel model =
            new CreateWrestlerViewModel
            {
                FirstName = "Dwayne",
                LastName = "Johnson",
                HireDate = new DateTime(
                    1996,
                    1,
                    1),
                RingName = "The Rock",
                WeightClass = "Heavyweight",
                DebutDate = new DateTime(
                    1996,
                    11,
                    17),
                IsActive = true
            };

        int result =
            service.Create(model);

        Assert.Equal(42, result);

        Assert.NotNull(capturedEmployee);
        Assert.NotNull(capturedWrestler);

        Assert.Equal(
            "Wrestler",
            capturedEmployee.JobTitle);

        Assert.Equal(
            "Dwayne",
            capturedEmployee.FirstName);

        Assert.Equal(
            "The Rock",
            capturedWrestler.RingName);

        repositoryMock.Verify(
            r => r.CreateWithEmployee(
                It.IsAny<Employee>(),
                It.IsAny<Wrestler>()),
            Times.Once);
    }

    [Fact]
    public void Delete_CallsRepositoryDelete()
    {
        Mock<IWrestlerRepository> repositoryMock =
            new Mock<IWrestlerRepository>();

        repositoryMock
            .Setup(r =>
                r.DeleteWithEmployee(5))
            .Returns(true);

        WrestlerService service =
            new WrestlerService(
                repositoryMock.Object);

        bool result =
            service.Delete(5);

        Assert.True(result);

        repositoryMock.Verify(
            r =>
                r.DeleteWithEmployee(5),
            Times.Once);
    }

    [Fact]
    public void Update_ReturnsFalse_WhenWrestlerDoesNotExist()
    {
        Mock<IWrestlerRepository> repositoryMock =
            new Mock<IWrestlerRepository>();

        repositoryMock
            .Setup(r =>
                r.GetDetailsById(999))
            .Returns(
                (WrestlerDetails?)null);

        WrestlerService service =
            new WrestlerService(
                repositoryMock.Object);

        EditWrestlerViewModel model =
            new EditWrestlerViewModel
            {
                WrestlerId = 999,
                FirstName = "Missing",
                LastName = "Person",
                RingName = "Missing Wrestler",
                HireDate = DateTime.Today,
                IsActive = true
            };

        bool result =
            service.Update(model);

        Assert.False(result);

        repositoryMock.Verify(
            r => r.UpdateWithEmployee(
                It.IsAny<int>(),
                It.IsAny<Employee>(),
                It.IsAny<Wrestler>()),
            Times.Never);
    }
}