using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalon.Models;
using System.Collections.Generic;
using System;

namespace HairSalon.Tests
{
  [TestClass]
  public class SpecialtyTest : IDisposable
  {
    public SpecialtyTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=kevin_garvey_test;";
    }

    public void Dispose()
    {
      Specialty.ClearAll();
    }

    [TestMethod]
    public void SpecialtyConstructor_CreatesInstanceOfSpecialty_Specialty()
    {
      Specialty newSpecialty = new Specialty("dye");
      Assert.AreEqual(typeof(Specialty), newSpecialty.GetType());
    }

    [TestMethod]
    public void GetName_ReturnsName_String()
    {
      Specialty newSpecialty = new Specialty("Dye");
      string result = newSpecialty.GetName();
    }

    [TestMethod]
    public void GetAll_SpecialtiesStartsEmpty_SpecialtyList()
    {
      int result = Specialty.GetAll().Count;
      Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void GetAll_ReturnsAllSpecialtyObjects_SpecialtyList()
    {
      Specialty newSpecialty1 = new Specialty("dye");
      newSpecialty1.Save();
      Specialty newSpecialty2 = new Specialty("short hair");
      newSpecialty2.Save();
      List<Specialty> newList = new List<Specialty> { newSpecialty1, newSpecialty2 };
      List<Specialty> result = Specialty.GetAll();
      CollectionAssert.AreEqual(newList, result);
    }

    [TestMethod]
    public void Save_SavesSpecialtyToDatabase_Specialty()
    {
      Specialty newSpecialty = new Specialty("dye");
      newSpecialty.Save();
      List<Specialty> result = Specialty.GetAll();
      List<Specialty> newList = new List<Specialty>{newSpecialty};
      CollectionAssert.AreEqual(newList, result);
    }

    [TestMethod]
    public void Save_DatabaseAssignsIdToSpecialty_Id()
    {
      Specialty newSpecialty = new Specialty("dye");
      newSpecialty.Save();
      Specialty savedSpecialty = Specialty.GetAll()[0];
      int result = savedSpecialty.GetId();
      int newId = newSpecialty.GetId();
      Assert.AreEqual(newId, result);
    }

    [TestMethod]
    public void Equals_ReturnsTrueIfNamesAreTheSame_Specialty()
    {
      Specialty firstSpecialty = new Specialty("dye");
      Specialty secondSpecialty = new Specialty("dye");
      Console.WriteLine("test");
      Assert.AreEqual(firstSpecialty, secondSpecialty);
    }

    [TestMethod]
    public void Find_ReturnsSpecialtyInDatabase_Specialty()
    {
      Specialty newSpecialty =  new Specialty("dye");
      newSpecialty.Save();
      Specialty foundSpecialty = Specialty.Find(newSpecialty.GetId());
      Assert.AreEqual(newSpecialty, foundSpecialty);
    }

    [TestMethod]
    public void GetStylists_ReturnsAllSpecialtyStylists_StylistList()
    {
      Specialty testSpecialty = new Specialty("dye");
      testSpecialty.Save();
      Stylist testStylist1 = new Stylist("Mo");
      testStylist1.Save();
      Stylist testStylist2 = new Stylist("Jo");
      testStylist2.Save();
      testSpecialty.AddStylist(testStylist1);
      List<Stylist> result = testSpecialty.GetStylists();
      List<Stylist> testList = new List<Stylist> {testStylist1};
      CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void AddStylist_AddsStylistToSpecialty_StylistList()
    {
      Specialty testSpecialty = new Specialty("dye");
      testSpecialty.Save();
      Stylist testStylist = new Stylist("Mo");
      testStylist.Save();
      testSpecialty.AddStylist(testStylist);
      List<Stylist> result = testSpecialty.GetStylists();
      List<Stylist> testList = new List<Stylist>{testStylist};
      CollectionAssert.AreEqual(testList, result);
    }
  }
}
