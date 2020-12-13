using System;
using Xunit;
using Xunit.Abstractions;
using IIG.BinaryFlag;
using IIG.FileWorker;

namespace KPI_LAB4.TestBinaryFlag_FileWorker
{
    public class TestIntegration : IDisposable
    {
        private static string AbsolutePath = "/Applications/github/KPI_LAB4/KPI_LAB4/TestBinaryFlag_FileWorker";

        private static string InitDirectory = "initDirectory";
        private static string InitFile = "initFile.txt";
        private static string InitAbsolutePath = AbsolutePath + "/" + InitDirectory + "/" + InitFile;

        private static string TestDirectory = "testDirectory";
        private static string TestFile = "testFile.txt";

        private MultipleBinaryFlag _MBF;
        private ulong SIZE = 5;

        public TestIntegration()
        {
            _MBF = new MultipleBinaryFlag(SIZE);
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }


        // 1. Create directory
        // 2. Copy file
        // 3. Get file name via BaseFileWorker
        // 4. Get file text via BaseFileWorker
        // 5. Get MultipleBinaryFlag flag (true)
        // 6. Reset Flag to false and get status
        // 7. Set Flag to true and get status
        // 8. Write 1st flag to file and read it
        // 9. Write 2nd flag to file and read it
        // 10. Write 3rd flag to file and read it

        [Fact]
        public void TestSaveBinaryFlagToFileSuccess()
        {
            string targetPath = BaseFileWorker.MkDir(TestDirectory);
            // 1. Test if dir was crated
            Assert.NotNull(targetPath);
            bool status1 = BaseFileWorker.TryCopy(InitAbsolutePath, targetPath + "/" + TestFile, true, 10);
            // 2. Test if file was copied and previous dir was created
            Assert.True(status1);
            string fileName = BaseFileWorker.GetFileName(targetPath + "/" + TestFile);
            // 3. Test if file system if it was created
            Assert.Equal(TestFile, fileName);
            string fileText = BaseFileWorker.ReadAll(targetPath + "/" + TestFile);
            // 4. Test if file was read right
            Assert.Equal("some test text", fileText);
            bool flag1 = _MBF.GetFlag();
            // 5. Test flag status
            Assert.True(flag1);
            _MBF.ResetFlag(SIZE - 2);
            bool flag2 = _MBF.GetFlag();
            // 6. Test flag status after reset
            Assert.False(flag2);
            _MBF.SetFlag(SIZE - 2);
            bool flag3 = _MBF.GetFlag();
            // 7. Test flag status after set
            Assert.True(flag3);
            bool status2 = BaseFileWorker.TryWrite(flag1.ToString(), targetPath + "/" + TestFile, 10);
            // Check if previous operation was successful
            Assert.True(status2);
            string[] lines1 = BaseFileWorker.ReadLines(targetPath + "/" + TestFile);
            // 8. Check if writing to file was successful
            Assert.Equal(lines1[0], true.ToString());
            bool status3 = BaseFileWorker.Write(flag2.ToString(), targetPath + "/" + TestFile);
            // Check if previous operation was successful
            Assert.True(status3);
            string[] lines2 = BaseFileWorker.ReadLines(targetPath + "/" + TestFile);
            // 9. Check if writing to file was successful
            Assert.Equal(lines2[0], false.ToString());
            bool status4 = BaseFileWorker.Write(flag3.ToString(), targetPath + "/" + TestFile);
            // Check if previous operation was successful
            Assert.True(status4);
            string[] lines3 = BaseFileWorker.ReadLines(targetPath + "/" + TestFile);
            // 10. Check if writing to file was successful
            Assert.Equal(lines3[0], true.ToString());
        }
    }
}
