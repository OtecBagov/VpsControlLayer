using VpsControlLayer.Application.Commands;
using Xunit;

namespace VpsControlTests.CliCommandTests;

public class DeployCommandHandlerTests
{
    [Fact]
    public void Handle_ExecutesWithoutException()
    {
        // Arrange
        var handler = new DeployCommandHandler();
        
        // Act & Assert
        var exception = Record.Exception(() => handler.Handle());
        
        Assert.Null(exception);
    }
}