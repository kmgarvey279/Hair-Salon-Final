using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;

namespace HairSalon.Controllers
{
  public class StylistsController : Controller
  {
    [HttpGet("/stylists")]
    public ActionResult Index()
    {
      List<Stylist> allStylists = Stylist.GetAll();
      return View(allStylists);
    }

    [HttpGet("/stylists/new")]
    public ActionResult New()
    {
      return View();
    }

    [HttpPost("/stylists")]
    public ActionResult Create(string stylistName)
    {
      Stylist newStylist = new Stylist(stylistName);
      newStylist.Save();
      List<Stylist> allStylists = Stylist.GetAll();
      return View("Index", allStylists);
    }

    [HttpGet("/stylists/{stylistId}")]
    public ActionResult Show(int stylistId)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Stylist selectedStylist = Stylist.Find(stylistId);
      List<Client> stylistClients = selectedStylist.GetClients();
      List<Specialty> stylistSpecialties = selectedStylist.GetSpecialties();
      List<Specialty> allSpecialties = Specialty.GetAll();
      model.Add("stylist", selectedStylist);
      model.Add("clients", stylistClients);
      model.Add("thisSpecialties", stylistSpecialties);
      model.Add("allSpecialties", allSpecialties);
      return View(model);
    }

    [HttpPost("/stylists/{stylistId}")]
    public ActionResult Create(int stylistId, string clientName, string clientPhoneNumber, string clientEmail, string clientNotes)
    {
      Client newClient = new Client(clientName, clientPhoneNumber, clientEmail, clientNotes, stylistId);
      newClient.Save();
      Stylist foundStylist = Stylist.Find(stylistId);
      Dictionary<string, object> model = new Dictionary<string, object>();
      List<Client> stylistClients = foundStylist.GetClients();
      List<Specialty> thisSpecialties = foundStylist.GetSpecialties();
      List<Specialty> allSpecialties = Specialty.GetAll();
      model.Add("clients", stylistClients);
      model.Add("stylist", foundStylist);
      model.Add("thisSpecialties", thisSpecialties);
      model.Add("allSpecialties", allSpecialties);
      return View("Show", model);
    }

    [HttpPost("/stylists/{stylistId}/specialties/new")]
    public ActionResult AddSpecialty(int stylistId, int specialtyId)
    {
      Stylist stylist = Stylist.Find(stylistId);
      Specialty specialty = Specialty.Find(specialtyId);
      stylist.AddSpecialty(specialty);
      return RedirectToAction("Show", new { id = stylistId });
    }

    [HttpGet("/stylists/{stylistId}/edit")]
    public ActionResult Edit(int stylistId)
    {
      Stylist stylist = Stylist.Find(stylistId);
      return View(stylist);
    }

    [HttpPost("/stylists/{stylistId}/edit")]
    public ActionResult Update(int stylistId, string newName)
    {
      Stylist stylist = Stylist.Find(stylistId);
      stylist.Edit(newName);
      return RedirectToAction("Index");
    }

    [HttpGet("/stylists/{stylistId}/delete")]
    public ActionResult Delete(int stylistId)
    {
      Stylist stylist = Stylist.Find(stylistId);
      stylist.Delete();
      return RedirectToAction("Index");
    }

    [HttpGet("/stylists/delete")]
    public ActionResult DeleteAll()
    {
      Stylist.DeleteAll();
      return RedirectToAction("Index");
    }

  }
}
