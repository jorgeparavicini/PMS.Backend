using PMS.Backend.Features.Registration.Controllers;

namespace PMS.Backend.Test;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        // Arrange
        // Act
        // Assert
        var controller = new RegistrationController();
        Assert.NotNull(controller);
    }
}