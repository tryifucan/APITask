using APITask_Framework.Endpoints;

namespace APITask_Tests.Base
{
    public class BaseTest
    {
        protected UsersEndpoints UsersEndpoints;

        [SetUp]
        public void SetUp()
        {
            UsersEndpoints = new UsersEndpoints();
        }
    }
}
