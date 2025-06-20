using APITask_Framework.Models.Request;
using APITask_Framework.Utils;
using APITask_Tests.Base;
using System.Net;

namespace APITask_Tests.Tests
{
    public class UsersEndpointTests : BaseTest
    {
        [Test]
        [Category("Users")]
        [Description("Verify that the API returns a list of users.")]
        public void GetUsers_ShouldReturnUserList()
        {
            var response = UsersEndpoints.GetUsers(1);

            Assert.That(response.IsSuccessful, Is.True, "API request call failed.");
            Assert.That(response.Data, Is.Not.Null);
            Assert.That(response.Data.Data, Is.Not.Empty, "The response body with Users is empty.");

            var firstUser = response.Data.Data.First();
            TestContext.WriteLine("Extracting the first user details from the list");
            TestContext.WriteLine($"User ID: {firstUser.Id}, Email: {firstUser.Email}");

            var sortedUsers = response.Data.Data.OrderBy(u => u.First_Name).ToList();

            TestContext.WriteLine("All Users sorted alphabetically by First Name:");
            foreach (var user in sortedUsers)
            {
                TestContext.WriteLine($"{user.First_Name} {user.Last_Name} - {user.Email}");
            }
        }


        [Test]
        [Category("Users")]
        [Description("Verify that the API returns a specific user by ID.")]
        public void GetUserById_ShouldReturnCorrectUser()
        {
            var listResponse = UsersEndpoints.GetUsers(1);
            var expectedUser = listResponse.Data.Data.First();

            var response = UsersEndpoints.GetUserById(expectedUser.Id);

            Assert.That(response.IsSuccessful, Is.True, "API request call failed.");
            Assert.That(response.Data, Is.Not.Null);
            Assert.That(response.Data.Data.Id, Is.EqualTo(expectedUser.Id));
            Assert.That(response.Data.Data.Email, Is.EqualTo(expectedUser.Email));
            Assert.That(response.Data.Data.First_Name, Is.EqualTo(expectedUser.First_Name));
            Assert.That(response.Data.Data.Last_Name, Is.EqualTo(expectedUser.Last_Name));

            Assert.That(response.Data.Support, Is.Not.Null);
            Assert.That(response.Data.Support.Url, Is.Not.Empty);
            Assert.That(response.Data.Support.Text, Is.Not.Empty);
            TestContext.WriteLine("User details retrieved successfully:");
            TestContext.WriteLine($"User ID: {response.Data.Data.Id}, Email: {response.Data.Data.Email}");
        }

        [Test]
        [Category("Users")]
        [Description("Verify that the API returns 404 for invalid user ID.")]
        public void GetUserByInvalidId_ShouldReturn404()
        {
            int invalidUserId = Generator.GenerateRandomId();
            var response = UsersEndpoints.GetUserById(invalidUserId);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
            Assert.That(response.Data.Data, Is.Null, $"Expected no user data with id: {invalidUserId}");
            TestContext.WriteLine($"No User data with ID: {invalidUserId}");
            TestContext.WriteLine($"Response Content: {response.Content}");
        }

        [Test]
        [Category("Users")]
        [Description("Verify that the API can create a new user.")]
        public void CreateUser_ShouldReturn201AndCorrectData()
        {
            var newUser = new CreateUserRequest
            {
                Name = Generator.GenerateRandomString(5),
                Job = Generator.GenerateRandomString(5)
            };

            var response = UsersEndpoints.CreateUser(newUser);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created), "Expected 201 Created.");
            Assert.IsNotNull(response.Data, "Response data should not be null.");
            Assert.That(response.Data.Name, Is.EqualTo(newUser.Name));
            Assert.That(response.Data.Job, Is.EqualTo(newUser.Job));
            Assert.That(response.Data.Id, Is.Not.Empty, "User ID should not be empty.");
            Assert.That(response.Data.CreatedAt, Is.Not.Empty, "CreatedAt should not be empty.");

            TestContext.WriteLine($"Created User: {response.Data.Name}, Job: {response.Data.Job}, ID: {response.Data.Id}");
        }

        [Test]
        [Category("Users")]
        [Description("Verify that the API can delete an existing user.")]
        public void DeleteUser_ShouldReturn204NoContent()
        {
            var newUser = new CreateUserRequest
            {
                Name = Generator.GenerateRandomString(5),
                Job = Generator.GenerateRandomString(5)
            };

            var createResponse = UsersEndpoints.CreateUser(newUser);
            var userId = createResponse.Data.Id;

            Assert.That(createResponse.StatusCode, Is.EqualTo(HttpStatusCode.Created));

            var deleteResponse = UsersEndpoints.DeleteUser(userId);

            Assert.That(deleteResponse.StatusCode, Is.EqualTo(HttpStatusCode.NoContent),"Expected 204 No Content on deletion.");
            TestContext.WriteLine($"User with ID: {userId} was created successfully.");
            TestContext.WriteLine($"User with ID: {userId} deleted successfully.");
        }
    }
}
