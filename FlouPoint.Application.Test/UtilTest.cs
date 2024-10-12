namespace FlouPoint.Test
{
    using global::Application.Result;
    using FluentAssertions;

    public static class UtilTest<T>
    {
        public static void Assert(Task<OperationResult<T>> result)
        {
            result.Should().NotBeNull();
            result.Id.Should().NotBe(0);
            result.Status.Should().Be(TaskStatus.RanToCompletion);
            result.Exception.Should().BeNull();
            result.AsyncState.Should().BeNull();
            result.Result.Should().NotBeNull();
        }
    }
}
