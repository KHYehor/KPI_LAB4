using System;
using Xunit;
using Xunit.Abstractions;
using IIG.PasswordHashingUtils;
using IIG.CoSFE.DatabaseUtils;
namespace KPI_LAB4.TestPasswordHashing_AuthDataBase
{
    public class TestIntegration : IDisposable
    {
        private static string server = @"127.0.0.1";
        private static string database = @"IIG.CoSWE.AuthDB";
        private static bool isTrusted = false;
        private static string login1 = @"sa";
        private static string login2 = "login2";
        private static string login3 = "login3";
        private static string password1 = @"Password1234password";
        private static string password2 = "password2";
        private static string password3 = "password3";
        private static int connectionTimeOut = 10;
        private ITestOutputHelper output;

        public TestIntegration(ITestOutputHelper output)
        {
            PasswordHasher.Init("salt", 23432);
            this.output = output;
        }

        public void Dispose()
        {
        }

        // 1. Check if password hashes the same way
        // 2. Check status of adding credentials
        // 3. Check if credentials were saved
        // 4. Check status
        // 5. Check if credentials were updated
        // 6. Check status
        // 7. Check if credentials were deleted
        [Fact]
        public void TestSuccessfulCase()
        {
            string password1Hashed = PasswordHasher.GetHash(password1);
            string password2Hashed = PasswordHasher.GetHash(password2);
            string password3Hashed = PasswordHasher.GetHash(password3);
            // 1. Check if password hashes the same way
            Assert.Equal(password1Hashed, PasswordHasher.GetHash(password1));
            AuthDatabaseUtils connection = new AuthDatabaseUtils(server, database, isTrusted, login1, password1, connectionTimeOut);
            bool status1 = connection.AddCredentials(login2, password2Hashed);
            // 2. Check status of adding credentials
            Assert.True(status1);
            bool status2 = connection.CheckCredentials(login2, password2Hashed);
            // 3. Check if credentials were saved
            Assert.True(status2);
            bool status3 = connection.UpdateCredentials(login2, password2Hashed, login3, password3Hashed);
            // 4. Check status
            Assert.True(status3);
            bool status4 = connection.CheckCredentials(login3, password3Hashed);
            // 5. Check if credentials were updated
            Assert.True(status4);
            bool status5 = connection.DeleteCredentials(login3, password3Hashed);
            // 6. Check status
            Assert.True(status5);
            bool status6 = connection.CheckCredentials(login3, password3Hashed);
            // 7. Check if credentials were deleted
            Assert.False(status6);
        }

    }
}
