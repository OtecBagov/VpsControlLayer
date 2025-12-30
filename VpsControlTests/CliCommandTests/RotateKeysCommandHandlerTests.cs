using VpsControlLayer.Application.Commands;
using Xunit;

namespace VpsControlTests.CliCommandTests;

public class RotateKeysCommandHandlerTests
{
    [Fact]
    public void Handle_ExecutesWithoutException()
    {
        // Arrange
        var handler = new RotateKeysCommandHandler();
        
        // Act & Assert
        var exception = Record.Exception(() => handler.Handle());
        
        Assert.Null(exception);
    }
}