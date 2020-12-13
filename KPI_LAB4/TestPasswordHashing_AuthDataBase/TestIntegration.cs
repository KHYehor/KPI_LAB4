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
        private static bool isTrusted = true;
        private static string login1 = @"coswe";
        private static string login2 = "login2";
        private static string login3 = "login3";
        private static string password1 = @"L}EjpfCgru9X@GLj";
        private static string password2 = "password2";
        private static string password3 = "password3";
        private static int connectionTimeOut = 15;

        private readonly ITestOutputHelper output;

        public TestIntegration(ITestOutputHelper output)
        {
            PasswordHasher.Init("salt", 23432);
            this.output = output;
        }

        public void Dispose()
        {
        }

        [Fact]
        public void TestSuccessfulCase()
        {
            string password = PasswordHasher.GetHash("password");
            // 1. Check if password hashes the same way
            Assert.Equal(password, PasswordHasher.GetHash("password"));
            AuthDatabaseUtils connection = new AuthDatabaseUtils(server, database, isTrusted, login1, password1, connectionTimeOut);
            // 2.
            output.WriteLine(connection.getStatus().ToString());
            // Assert.True(connection.getStatus());
            bool status1 = connection.AddCredentials(login2, password2);
            output.WriteLine(status1.ToString());
            // 3.Check status of adding credentials
            //Assert.True(status1);
            //bool status2 = connection.CheckCredentials(login2, password2);
            //Assert.True(status2);
            //bool status3 = connection.UpdateCredentials(login2, password2, login3, password3);
            //Assert.True(status3);
            //bool status4 = connection.CheckCredentials(login3, password3);
            //Assert.True(status4);
            //bool status5 = connection.DeleteCredentials(login3, password3);
            //Assert.True(status5);
            //bool status6 = connection.CheckCredentials(login3, password3);
            //Assert.False(status6);
        }

    }
}
