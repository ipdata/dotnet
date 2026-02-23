using System.Net;
using FluentAssertions;
using IPData.Exceptions;
using IPData.Exceptions.Factory;
using Xunit;

namespace IPData.Tests.Exceptions.Factory
{
    public class ApiExceptionFactoryTests
    {
        [Fact]
        public void Create_WhenCalledWithBadRequest_ShouldReturnBadRequestException()
        {
            // Arrange
            var sut = new ApiExceptionFactory();

            // Act
            var actual = sut.Create(HttpStatusCode.BadRequest, string.Empty);

            // Assert
            actual.Should().BeOfType<BadRequestException>();
        }

        [Fact]
        public void Create_WhenCalledWithUnauthorized_ShouldReturnUnauthorizedException()
        {
            // Arrange
            var sut = new ApiExceptionFactory();

            // Act
            var actual = sut.Create(HttpStatusCode.Unauthorized, string.Empty);

            // Assert
            actual.Should().BeOfType<UnauthorizedException>();
        }

        [Fact]
        public void Create_WhenCalledWithForbidden_ShouldReturnForbiddenException()
        {
            // Arrange
            var sut = new ApiExceptionFactory();

            // Act
            var actual = sut.Create(HttpStatusCode.Forbidden, string.Empty);

            // Assert
            actual.Should().BeOfType<ForbiddenException>();
        }

        [Fact]
        public void Create_WhenCalled_ShouldReturnApiException()
        {
            // Arrange
            var sut = new ApiExceptionFactory();

            // Act
            var actual = sut.Create(HttpStatusCode.InternalServerError, string.Empty);

            // Assert
            actual.Should().BeOfType<ApiException>();
        }
    }
}
