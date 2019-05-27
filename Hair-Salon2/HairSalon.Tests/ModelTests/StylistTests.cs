using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalon.Models;
using System.Collections.Generic;
using System;
using MySql.Data.MySqlClient;

namespace HairSalon.Tests
{
  [TestClass]
  public class StylistTest : IDisposable
  {
    public StylistTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=kevin_garvey_test;";
    }

    public void Dispose()
    {
      Stylist.ClearAll();
    }

    [TestMethod]
    public void StylistConstructor_CreatesInstanceOfStylist_Stylist()
    {
      Stylist newStylist = new Stylist("Test Stylist");
      Assert.AreEqual(typeof(Stylist), newStylist.GetType());
    }

    [TestMethod]
    public void GetName_ReturnsName_String()
    {
      string name = "Test Stylist";
      Stylist newStylist = new Stylist(name);
      string result = newStylist.GetName();
      Assert.AreEqual(name, result);
    }

    // [TestMethod]
    // public void GetId_ReturnsStylistId_Int()
    // {
    //   string name = "Test Stylist";
    //   Stylist newStylist = new Stylist(name);
    //   int result = newStylist.GetId();
    //   Assert.AreEqual(1, result);
    // }

    [TestMethod]
    public void GetAll_ReturnsAllStylistObjects_StylistList()
    {
      //Arrange
      string name01 = "Test Stylist1";
      string name02 = "Test Stylist2";
      Stylist newStylist1 = new Stylist(name01);
      newStylist1.Save();
      Stylist newStylist2 = new Stylist(name02);
      newStylist2.Save();
      List<Stylist> newList = new List<Stylist> { newStylist1, newStylist2 };
      List<Stylist> result = Stylist.GetAll();
      CollectionAssert.AreEqual(newList, result);
    }

    // [TestMethod]
    // public void Find_ReturnsCorrectStylist_Stylist()
    // {
    //   string name01 = "Test Stylist1";
    //   string name02 = "Test Stylist2";
    //   Stylist newStylist1 = new Stylist(name01);
    //   newStylist1.Save();
    //   Stylist newStylist2 = new Stylist(name02);
    //   newStylist2.Save();
    //   Stylist result = Stylist.Find(2);
    //   Assert.AreEqual(newStylist2, result);
    // }

    [TestMethod]
    public void GetClients_ReturnsEmptyClientList_ClientList()
    {
      string name = "Test Stylist";
      Stylist newStylist = new Stylist(name);
      newStylist.Save();
      List<Client> newList = new List<Client> { };
      List<Client> result = newStylist.GetClients();
      CollectionAssert.AreEqual(newList, result);
    }

    // [TestMethod]
    // public void AddClient_AssociatesClientWithStylist_ClientList()
    // {
    //   string clientName = "Test Client";
    //   Client newClient = new Client(clientName, 1);
    //   newClient.Save();
    //   List<Client> newList = new List<Client> { newClient };
    //   string stylistName = "Test Stylist";
    //   Stylist newStylist = new Stylist(stylistName);
    //   newStylist.Save();
    //   newStylist.AddClient(newClient);
    //   List<Client> result = newStylist.GetClients();
    //   CollectionAssert.AreEqual(newList, result);
    // }

    public static void ClearAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM stylists;";
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    [TestMethod]
    public void GetAll_StylistsEmptyAtFirst_StylistList()
    {
      int result = Stylist.GetAll().Count;
      Assert.AreEqual(0, result);
    }

    // [TestMethod]
    // public void Equals_ReturnsTrueIfNamesAreTheSame_Stylist()
    // {
    //   Stylist firstStylist = new Stylist("Test Stylist1");
    //   firstStylist.Save();
    //   Stylist secondStylist = new Stylist("Test Stylist2");
    //   secondStylist.Save();
    //   Assert.AreEqual(firstStylist, secondStylist);
    // }

    [TestMethod]
    public void Save_SavesStylistToDatabase_StylistList()
    {
      Stylist testStylist = new Stylist("Test Stylist");
      testStylist.Save();
      List<Stylist> result = Stylist.GetAll();
      List<Stylist> testList = new List<Stylist>{testStylist};
      CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void Save_DatabaseAssignsIdToStylist_Id()
    {
      Stylist testStylist = new Stylist("Test Stylist");
      testStylist.Save();
      Stylist savedStylist = Stylist.GetAll()[0];
      int result = savedStylist.GetId();
      int testId = testStylist.GetId();
      Assert.AreEqual(testId, result);
    }

    [TestMethod]
    public void Find_ReturnsStylistInDatabase_Stylist()
    {
      Stylist testStylist = new Stylist("Test Stylist");
      testStylist.Save();
      Stylist foundStylist = Stylist.Find(testStylist.GetId());
      Assert.AreEqual(testStylist, foundStylist);
    }

  }
}
