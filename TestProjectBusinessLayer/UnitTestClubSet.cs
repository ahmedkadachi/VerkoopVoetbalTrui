using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Exceptions;
using BusinessLayer.Model;
using Xunit;

namespace TestProjectBusinessLayer
{
    public class UnitTestClubSet
    {
        [Fact]
        public void Test_ctor_noId_Valid()
        {
            ClubSet club = new ClubSet(true, 1);

            Assert.True(club.Thuis);
            Assert.Equal(1, club.Versie);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void Test_Ctor_noId_Versie_Invalid(int versie)
        {
            Assert.Throws<ClubSetException>(() => new ClubSet(true, versie));
        }
    }
}
