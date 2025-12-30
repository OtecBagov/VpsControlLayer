using VpsControlLayer.Application.Commands;
using Xunit;

namespace VpsControlTests.CliCommandTests;

public class StatusCommandHandlerTests
{
    [Fact]
    public void Handle_ExecutesWithoutException()
    {
        // Arrange
        var handler = new StatusCommandHandler();
        
        // Act & Assert
        var exception = Record.Exception(() => handler.Handle());
        
        Assert.Null(exception);
    }
}