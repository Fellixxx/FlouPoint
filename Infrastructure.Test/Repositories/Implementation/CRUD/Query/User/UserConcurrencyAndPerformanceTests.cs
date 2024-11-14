namespace Infrastructure.Test.Repositories.Implementation.CRUD.User
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Linq;
    using System.Diagnostics;
    using Domain.Entities;
    using Application.Result;

    [TestClass]
    public class UserConcurrencyAndPerformanceTests : SetupTest
    {
        [TestMethod]
        public async Task CreateUser_WithConcurrentTasks_UnderHighLoad_ShouldSucceed()
        {
            const int numberOfUsers = 1000;
            const int expectedTimeLimitMilliseconds = 5000;  // Maximum time allowed for all operations to complete
            var tasks = new List<Task<Operation<string>>>();
            var stopwatch = Stopwatch.StartNew();

            for (int i = 0; i < numberOfUsers; i++)
            {
                var newUser = new User
                {
                    Id = Guid.NewGuid().ToString(),  // Ensure a unique ID for each user
                    Name = $"User {i}",
                    Email = $"user{i}@example.com",
                    Password = "Password123!"
                };
                tasks.Add(_userCreate.Create(newUser));
            }

            var results = await Task.WhenAll(tasks);
            stopwatch.Stop();

            // Verify that all user creation tasks were successful
            Assert.IsTrue(results.All(result => result.IsSuccessful), "Not all user creation operations were successful.");

            // Check that the operation completed within the expected time limit
            var milliseconds = stopwatch.ElapsedMilliseconds;
            Assert.IsTrue(milliseconds < expectedTimeLimitMilliseconds,
                $"Expected completion within {expectedTimeLimitMilliseconds} ms, but took {milliseconds} ms.");

            Debug.WriteLine($"Time taken to create {numberOfUsers} users with concurrent tasks: {milliseconds} ms");
        }

        [TestMethod]
        public async Task CreateUser_WithBatchInsert_UnderHighLoad_ShouldSucceed()
        {
            const int numberOfUsers = 1000;
            const int expectedTimeLimitMilliseconds = 300;
            var stopwatch = Stopwatch.StartNew();

            for (int i = 0; i < numberOfUsers; i++)
            {
                var newUser = new User
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = $"User {i}",
                    Email = $"user{i}@example.com",
                    Password = "Password123!"
                };
                _dbContext.Users.Add(newUser); // Add without calling SaveChanges each time
            }

            await _dbContext.SaveChangesAsync(); // Commit all users in a single operation
            stopwatch.Stop();
            // Check that the operation completed within the expected time limit
            var milliseconds = stopwatch.ElapsedMilliseconds;
            Assert.IsTrue(milliseconds < expectedTimeLimitMilliseconds,
                $"Expected completion within {expectedTimeLimitMilliseconds} ms, but took {milliseconds} ms.");

            Debug.WriteLine($"Time taken to create {numberOfUsers} users with concurrent tasks: {milliseconds} ms");
            Debug.WriteLine($"Time taken to create {numberOfUsers} users with batch insert: {stopwatch.ElapsedMilliseconds} ms");
        }


        [TestMethod]
        public async Task ReadUsers_UnderHighLoad_ShouldReturnCorrectCount()
        {
            const int numberOfUsers = 500;

            // Create users with GUID-based IDs
            for (int i = 0; i < numberOfUsers; i++)
            {
                var newUser = new User
                {
                    Id = Guid.NewGuid().ToString(),  // Assign a unique GUID string ID
                    Name = $"User {i}",
                    Email = $"user{i}@example.com",
                    Password = "Password123!"
                };
                await _userCreate.Create(newUser);
            }

            // Perform read operations in parallel to simulate high load
            var tasks = Enumerable.Range(0, numberOfUsers).Select(_ => _userReadFilter.ReadFilter(u => u.Active));
            var stopwatch = Stopwatch.StartNew();

            var results = await Task.WhenAll(tasks);
            stopwatch.Stop();

            // Verify that all read operations were successful
            Assert.IsTrue(results.All(result => result.IsSuccessful), "Not all user read operations were successful.");

            Debug.WriteLine($"Time taken to read {numberOfUsers} users: {stopwatch.ElapsedMilliseconds} ms");
        }


        [TestMethod]
        public async Task UpdateUser_UnderHighLoad_ShouldSucceed()
        {
            const int numberOfUsers = 300;
            var tasks = new List<Task<Operation<bool>>>();
            var userIds = new List<string>();  // List to store generated GUID IDs

            // Create users with GUID-based IDs and store each ID
            for (int i = 0; i < numberOfUsers; i++)
            {
                var newUserId = Guid.NewGuid().ToString();
                userIds.Add(newUserId);  // Save the GUID ID for later use in updates

                var newUser = new User
                {
                    Id = newUserId,
                    Name = $"User {i}",
                    Email = $"user{i}@example.com",
                    Password = "Password123!"
                };
                await _userCreate.Create(newUser);
            }

            // Use saved GUID IDs to update each user
            foreach (var i in Enumerable.Range(0, numberOfUsers))
            {
                var updatedUser = new User
                {
                    Id = userIds[i],  // Reuse the stored GUID ID
                    Name = $"Updated User {i}",
                    Email = $"updateduser{i}@example.com",
                    Password = "Password123!"
                };
                tasks.Add(_userUpdate.Update(updatedUser));
            }

            var stopwatch = Stopwatch.StartNew();
            var results = await Task.WhenAll(tasks);
            stopwatch.Stop();

            // Verify that all update operations were successful
            Assert.IsTrue(results.All(result => result.IsSuccessful), "Not all user update operations were successful.");

            Debug.WriteLine($"Time taken to update {numberOfUsers} users: {stopwatch.ElapsedMilliseconds} ms");
        }


        [TestMethod]
        public async Task DeleteUser_UnderHighLoad_ShouldSucceed()
        {
            const int numberOfUsers = 200;
            var tasks = new List<Task<Operation<bool>>>();
            var userIds = new List<string>();  // List to store generated GUID IDs

            // Create users with GUID-based IDs and store each ID
            for (int i = 0; i < numberOfUsers; i++)
            {
                var newUserId = Guid.NewGuid().ToString();
                userIds.Add(newUserId);  // Save the GUID ID for later deletion

                var newUser = new User
                {
                    Id = newUserId,
                    Name = $"User {i}",
                    Email = $"user{i}@example.com",
                    Password = "Password123!"
                };
                await _userCreate.Create(newUser);
            }

            // Use saved GUID IDs to delete each user
            foreach (var userId in userIds)
            {
                tasks.Add(_userDelete.Delete(userId));  // Use the stored GUID ID for deletion
            }

            var stopwatch = Stopwatch.StartNew();
            var results = await Task.WhenAll(tasks);
            stopwatch.Stop();

            // Verify that all delete operations were successful
            Assert.IsTrue(results.All(result => result.IsSuccessful), "Not all user delete operations were successful.");

            Debug.WriteLine($"Time taken to delete {numberOfUsers} users: {stopwatch.ElapsedMilliseconds} ms");
        }


        [TestMethod]
        public async Task BulkUpdate_Users_ShouldMaintainDataIntegrity()
        {
            const int numberOfUsers = 100;
            const int expectedTimeLimitMilliseconds = 1;
            var guids = new List<string>();
            var tasks = new List<Task<Operation<bool>>>();

            // Create users with GUID-based IDs
            for (int i = 0; i < numberOfUsers; i++)
            {
                var userName = $"User {i}";
                var name = userName.Length>50? userName.Substring(0,49) : userName;
                var newUser = new User
                {
                    Id = Guid.NewGuid().ToString(),  // Assign a unique GUID string ID
                    Name = name,
                    Email = $"user{i}@example.com",
                    Password = "Password123!"
                };
                await _userCreate.Create(newUser);
                guids.Add(newUser.Id);
            }

            // Update each user
            foreach (var userId in guids)
            {
                var userName = $"Bulk Updated User {userId}";
                var name = userName.Length>50 ? userName.Substring(0, 49) : userName;
                var updatedUser = new User
                {
                    Id = userId,  // Ensure ID is a GUID string
                    Name = name,
                    Email = $"bulkupdateduser{userId}@example.com",
                    Password = "Password123!"
                };
                tasks.Add(_userUpdate.Update(updatedUser));
            }

            var stopwatch = Stopwatch.StartNew();
            var results = await Task.WhenAll(tasks);
            stopwatch.Stop();

            // Verify that all update operations were successful
            Assert.IsTrue(results.All(result => result.IsSuccessful), "Not all user update operations were successful.");

            // Check that the number of updated users is correct
            var updatedUsers = await _userReadFilter.ReadFilter(u => u.Name.StartsWith("Bulk Updated"));
            Assert.AreEqual(numberOfUsers, updatedUsers.Data.Count(), "The number of successfully updated users does not match the expected count.");
            var milliseconds = stopwatch.ElapsedMilliseconds;
            var errorMessage = $"Expected completion within {expectedTimeLimitMilliseconds} ms, but took {milliseconds} ms.";
            Assert.IsTrue(milliseconds < expectedTimeLimitMilliseconds,errorMessage);
            Debug.WriteLine($"Time taken for bulk updating {numberOfUsers} users: {milliseconds} ms");
        }
    }
}
