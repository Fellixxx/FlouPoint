namespace FlouPoint.Test
{
    using global::Application.Result;
    using FluentAssertions;

    /// <summary>
    /// The UtilTest class provides utility methods for testing Task<OperationResult<T>> objects.
    /// This class is generic and works with any type T.
    /// </summary>
    public static class UtilTest<T>
    {
        /// <summary>
        /// Asserts various conditions on the provided Task<OperationResult<T>> object to ensure it meets expected outcomes.
        /// </summary>
        /// <param name = "result">The Task<OperationResult<T>> object that represents an asynchronous operation with an expected result of type T.</param>
        public static void Assert(Task<OperationResult<T>> result)
        {
            // Asserts that the task result is not null
            result.Should().NotBeNull();
            // Asserts that the operation result ID is not 0, assuming 0 is not a valid ID
            result.Id.Should().NotBe(0);
            // Asserts that the task has successfully completed
            result.Status.Should().Be(TaskStatus.RanToCompletion);
            // Asserts that the task did not encounter any exceptions
            result.Exception.Should().BeNull();
            // Asserts that there is no asynchronous state object associated with this task
            result.AsyncState.Should().BeNull();
            // Asserts that the actual result of the task is not null
            result.Result.Should().NotBeNull();
        }
    }
}