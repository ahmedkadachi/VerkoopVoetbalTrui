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
    public class UnitTestClub
    {
        [Fact]
        public void Test_ctor_noId_Valid()
        {
            Club club = new Club("premier ligue", "United");

            Assert.Equal("premier ligue", club.Competitie);
            Assert.Equal("United", club.Ploegnaam);
        }

        [Fact]
        public void Test_Ctor_noId_Competitie_Invalid()
        {
            Assert.Throws<ClubException>(() => new Club("", "United"));
        }
        [Fact]
        public void Test_Ctor_noId_Ploegnaam_Invalid()
        {
            Assert.Throws<ClubException>(() => new Club("premier ligue", ""));
        }


    }
}
