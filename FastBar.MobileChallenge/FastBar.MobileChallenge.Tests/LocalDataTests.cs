using System.IO;
using System.Reflection;
using FastBar.MobileChallenge.Services;
using FluentAssertions;
using NUnit.Framework;
using SQLite.Net;
using SQLite.Net.Platform.Win32;

namespace FastBar.MobileChallenge.Test
{
    [TestFixture]
    public class LocalDataTests
    {
        [SetUp]
        public void SetUp()
        {
            // SQLite binary should be in the bin folder
            var nativeInteropPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase)
                ?.Replace("file:\\", "");

            // Random temp file
            var path = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());

            // Use the Win32 sqlite platform implementation
            var connection = new SQLiteConnection(new SQLitePlatformWin32(nativeInteropPath), path);
            _sut = new LocalDataService(connection);
        }

        [TearDown]
        public void TearDown()
        {
            _sut.Dispose();
        }

        private LocalDataService _sut;

        [Test]
        public void ShouldCreateTableQueries()
        {
            _sut.Events.Should().NotBeNull();
        }
    }
}