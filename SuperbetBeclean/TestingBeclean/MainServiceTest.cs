using System.Data.SqlClient;
using SuperbetBeclean.Model;
using SuperbetBeclean.Services;

namespace TestingBeclean.MainServiceTests
{
    [Apartment(ApartmentState.STA)]
    public class MainServiceTests
    {
        private IMainService mainService;
        private User userToday;
        private User userFiveDaysFromNow;
        private User userPlayerOneMock;
        private readonly string connectionString = "Data Source= DESKTOP-F6HM4JS; Initial Catalog = Team42; Integrated Security = True;";
        private SqlConnection connection;
        private readonly string iconPath = "C:\\Users\\danla\\Source\\Repos\\UBB-SE-2024-Team-42-Part-2\\assets\\demo_avatar.jpg";

        [SetUp]
        public void Setup()
        {
            mainService = new MainService();
            userToday = new (1, "player1", 1, 1, "path", 10000, 500, 10, 200, 11, 10, DateTime.Now.Date.AddDays(-1));
            userFiveDaysFromNow = new (2, "player2", 1, 1, "path", 10000, 500, 10, 200, 11, 10, DateTime.Now.Date.AddDays(5));
            userPlayerOneMock = new (1, "NewUsername", 1, 1, iconPath, 1, 1005500, 200, 201, 10, 10, DateTime.Now.Date);
            connection = new SqlConnection(connectionString);
            connection.Open();
        }

        [TearDown]
        public void TearDown()
        {
            connection.Dispose();
            connection.Close();
        }

        [TestCase(0)]
        public void OccupiedIntern_ClickingOnInternTable_ReturnsTrue(int expectedValue)
        {
            int occupiedIntern = mainService.OccupiedIntern();
            Assert.That(occupiedIntern, Is.EqualTo(expectedValue));
        }

        [TestCase(0)]
        public void OccupiedJunior_ClickingOnInternTable_ReturnsTrue(int expectedValue)
        {
            int occupiedIntern = mainService.OccupiedJunior();
            Assert.That(occupiedIntern, Is.EqualTo(expectedValue));
        }

        [TestCase(0)]
        public void OccupiedSenior_ClickingOnInternTable_ReturnsTrue(int expectedValue)
        {
            int occupiedIntern = mainService.OccupiedSenior();
            Assert.That(occupiedIntern, Is.EqualTo(expectedValue));
        }

        [Test]
        public void NewUserLogin_UserGainsOneMoreStreak_ReturnsTrue()
        {
            int userTodayStreak = userToday.UserStreak;
            mainService.NewUserLogin(userToday);
            Assert.That(userToday.UserStreak, Is.EqualTo(userTodayStreak + 1));
        }

        [Test]
        public void NewUserLogin_UserLosesAllStreaks_ReturnsTrue()
        {
            mainService.NewUserLogin(userFiveDaysFromNow);
            Assert.That(userFiveDaysFromNow.UserStreak, Is.EqualTo(1));
        }

        [Test]
        public void FetchUser_UserIdIsValid_ReturnsTrue()
        {
            User user = mainService.FetchUser(connection, "NewUsername");
            int userChips = 1005700;
            int userStreak = 201;
            int userHandsPlayed = 10;
            int userLevel = 10;

            Assert.That(user.UserChips, Is.EqualTo(userChips));
            Assert.That(user.UserStreak, Is.EqualTo(userStreak));
            Assert.That(user.UserHandsPlayed, Is.EqualTo(userHandsPlayed));
            Assert.That(user.UserLevel, Is.EqualTo(userLevel));
        }

        [Test]
        public void FetchUser_UserIdIsNotValid_ReturnsTrue()
        {
            User user = mainService.FetchUser(connection, "asdasd");
            Assert.That(user, Is.EqualTo(null));
        }

        [Test]
        public void FetchUser_UserTitleIsNull_ReturnsTrue()
        {
            User user = mainService.FetchUser(connection, "player8");
            int userChips = 10;
            int userStreak = 0;
            int userHandsPlayed = 0;
            int userLevel = 1;

            Assert.That(user.UserChips, Is.EqualTo(userChips));
            Assert.That(user.UserStreak, Is.EqualTo(userStreak));
            Assert.That(user.UserHandsPlayed, Is.EqualTo(userHandsPlayed));
            Assert.That(user.UserLevel, Is.EqualTo(userLevel));
        }

        [Test]
        public void FetchUser_UserDateIsNull_ReturnsTrue()
        {
            User user = mainService.FetchUser(connection, "player9");
            int userChips = 0;
            int userStreak = 0;
            int userHandsPlayed = 0;
            int userLevel = 1;

            Assert.That(user.UserChips, Is.EqualTo(userChips));
            Assert.That(user.UserStreak, Is.EqualTo(userStreak));
            Assert.That(user.UserHandsPlayed, Is.EqualTo(userHandsPlayed));
            Assert.That(user.UserLevel, Is.EqualTo(userLevel));
        }
    }
}