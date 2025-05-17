using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University.Models;

namespace University.Test
{
    public class PrepodavatelTest
    {
        [Fact]
        public void isValidPrepodavatelFIO_Иванов_Иван_Иванович_True()
        {
            var prepodavatel = new Prepodavateli
            {
                Lastname = "Иванов",
                Name = "Иван",
                Surname = "Иванович"
            };

            var result = prepodavatel.isValidFIO();

            Assert.True(result);
        }
    }
}
