using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using System.Linq;
using HairSalon.Models;

namespace HairSalon.Tests
{
  [TestClass]
  public class ClientTests : IDisposable
  {
        public ClientTests()
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=alan_falcon_test;";
        }
        public void Dispose()
        {
          Client.DeleteAll();
          Stylist.DeleteAll();
        }
        [TestMethod]
        public void GetAll_ClientsEmptyAtFirst_0()
        {
         //Arrange, Act
         int result = Client.GetAll().Count;

         //Assert
         Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void Equals_ReturnsTrueForSameName_Client()
        {
          //Arrange, Act
          Client firstClient = new Client("Barry",1);
          Client secondClient = new Client("Barry",1);

          //Assert
          Assert.AreEqual(firstClient.GetName(), secondClient.GetName());
        }

        [TestMethod]
        public void Save_SavesClientToDatabase_ClientList()
        {
          //Arrange
          Client testClient = new Client("Wendy",1);
          testClient.Save();

          //Act
          List<Client> result = Client.GetAll();
          List<Client> testList = new List<Client>{testClient};

          //Assert
          CollectionAssert.AreEqual(testList, result);
        }

        [TestMethod]
        public void Save_DatabaseAssignsIdToClient_Id()
        {
         //Arrange
         Client testClient = new Client("Barry",1);
         testClient.Save();

         //Act
         Client savedClient = Client.GetAll()[0];

         int result = savedClient.GetId();
         int testId = testClient.GetId();

         //Assert
         Assert.AreEqual(testId, result);
        }

        [TestMethod]
        public void Find_FindsClientInDatabase_Client()
        {
          //Arrange
          Client testClient = new Client("Wendy",1);
          testClient.Save();

          //Act
          Client foundClient = Client.Find(testClient.GetId());

          //Assert
          Assert.AreEqual(testClient, foundClient);
        }
        [TestMethod]
        public void GetAllStylistsClients_FindsAStylistsClients_Id()
        {
          //Arrange
          Stylist testStylist = new Stylist("Mary");
          testStylist.Save();

          List<Client> listofTestClients = new List<Client> ();

          string clientName1 = "Barry";
          int stylistId1 = 1;
          int clientId1 = 1;

          Client newClient1 = new Client(clientName1, stylistId1);
          Client newClient1List = new Client(clientName1,stylistId1,clientId1);
          listofTestClients.Add(newClient1List);
          newClient1.Save();

          string clientName2 = "Wendy";
          int stylistId2 = 1;
          int clientId2 = 2;

          Client newClient2 = new Client(clientName2, stylistId2);
          Client newClient2List = new Client(clientName2,stylistId2,clientId2);
          listofTestClients.Add(newClient2List);
          newClient2.Save();

          //Act
          int testSid = testStylist.GetId();

          List<Client> stylistsClients = Client.GetAllStylistsClients(testSid);

          //Assert
          CollectionAssert.AreEqual(stylistsClients,listofTestClients);
        }
        [TestMethod]
        public void UpdateClient_UpdateAClient_0()
        {
          //Arrange
          string addClient = "Mary";
          Client testClient = new Client(addClient,1);
          testClient.Save();

          //Act
          string savedClientName = testClient.GetName();
          // string testClientID = testClient.GetId();
          string localClientName = addClient;


          //Assert
          Assert.AreEqual(localClientName,savedClientName);
        }
        [TestMethod]
        public void DeleteClient_DeleteAClient_Id()
        {
          //Arrange
          int beforeClientCount = Client.GetAll().Count;

          string addClient = "Mary";
          Client testClient = new Client(addClient,1);
          testClient.Save();
          //Act
          Client.DeleteClient(testClient.GetId());
          int afterClientCount = Client.GetAll().Count;

          //Assert
          Assert.AreEqual(beforeClientCount,afterClientCount);
        }
        [TestMethod]
        public void DeleteAll_DeleteAllClients_0()
        {
          //Arrange
          int beforeClientCount = Client.GetAll().Count;

          string addClient1 = "Mary";
          Client testClient1 = new Client(addClient1,1);
          testClient1.Save();
          string addClient2 = "James";
          Client testClient2 = new Client(addClient2,1);
          testClient2.Save();
          string addClient3 = "Mindy";
          Client testClient3 = new Client(addClient3,1);
          testClient3.Save();
          //Act
          Client.DeleteAll();
          int afterClientCount = Client.GetAll().Count;

          //Assert
          Assert.AreEqual(beforeClientCount,afterClientCount);
         }
  }
}
