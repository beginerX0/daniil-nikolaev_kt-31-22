using Xunit;
using University;
using University.Models;

namespace University.Tests
{
    public class PrepodavateliTest
    {
        [Fact]
        public void isValidFIO_Иванов_Иван_Иванович_True()
        {
            var testPrepodavatel = new Prepodavateli
            {
                Lastname = "Иванов",
                Name = "Иван",
                Surname = "Иванович"
            };
            var result = testPrepodavatel.isValidFIO();
            Assert.True(result);
        }
    }
}