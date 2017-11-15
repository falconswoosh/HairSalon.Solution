using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using HairSalon.Models;

namespace HairSalon.Tests
{
  [TestClass]
  public class StylistTests : IDisposable
  {
        public StylistTests()
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=alan_falcon_test;";
        }
        public void Dispose()
        {
          Client.DeleteAll();
          Stylist.DeleteAll();
        }
        [TestMethod]
        public void GetAll_StylistsEmptyAtFirst_0()
        {
         //Arrange, Act
         int result = Stylist.GetAll().Count;

         //Assert
         Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void Equals_ReturnsTrueForSameName_Stylist()
        {
          //Arrange, Act
          Stylist firstStylist = new Stylist("Barry");
          Stylist secondStylist = new Stylist("Barry");

          //Assert
          Assert.AreEqual(firstStylist.GetName(), secondStylist.GetName());
        }

        [TestMethod]
        public void Save_SavesStylistToDatabase_StylistList()
        {
          //Arrange
          Stylist testStylist = new Stylist("Wendy",1);
          testStylist.Save();

          //Act
          List<Stylist> result = Stylist.GetAll();
          List<Stylist> testList = new List<Stylist>{testStylist};

          //Assert
          CollectionAssert.AreEqual(testList, result);
        }

        [TestMethod]
        public void Save_DatabaseAssignsIdToStylist_Id()
        {
         //Arrange
         Stylist testStylist = new Stylist("Barry",1);
         testStylist.Save();

         //Act
         Stylist savedStylist = Stylist.GetAll()[0];

         int result = savedStylist.GetId();
         int testId = testStylist.GetId();

         //Assert
         Assert.AreEqual(testId, result);
        }


        [TestMethod]
        public void Find_FindsStylistInDatabase_Stylist()
        {
          //Arrange
          Stylist testStylist = new Stylist("Wendy",1);
          testStylist.Save();

          //Act
          Stylist foundStylist = Stylist.Find(testStylist.GetId());

          //Assert
          Assert.AreEqual(testStylist, foundStylist);
        }
        [TestMethod]
        public void UpdateStylist_UpdateAStylist__0()
        {
          //Arrange
          string addStylist = "Mary";
          Stylist testStylist = new Stylist(addStylist,1);
          testStylist.Save();

          //Act
          string savedStylistName = testStylist.GetName();
          string localStylistName = addStylist;


          //Assert
          Assert.AreEqual(localStylistName,savedStylistName);
        }
  }
}
