using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using HairSalon.Models;

namespace HairSalon.Controllers
{
    public class HomeController : Controller
    {
      [HttpGet("/")] //Similar as example, with exception this returns list of stylists.
      public ActionResult Index()
      {
        List<Stylist> stylists = Stylist.GetAll();
        return View(stylists);
      }
      [HttpPost("/stylist")]  //Post-Categories
      public ActionResult AddStylist()
      {
        Stylist newStylist = new Stylist(Request.Form["stylistName"]);
        List<Stylist> allStylists = Stylist.GetAll();
        newStylist.Save();
        return View("Stylist", allStylists);
      }
      [HttpGet("/stylist/new")] //Get-Categories-new
      public ActionResult StylistForm()
      {
        return View();
      }
      [HttpGet("/stylist/{id}")] //Get-Categories-Id. Seems to view stylists and perhaps clients
      public ActionResult StylistDetail(int id)
      {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Stylist selectedStylist = Stylist.Find(id);
        List<Client> stylistClients = selectedStylist.GetClients();
        model.Add("stylist", selectedStylist);
        model.Add("client", stylistClients);
        return View(model);
      }
      [HttpGet("/stylist/{id}/client")] //MISSING from RESTful example - should diplay stylist's clients
      public ActionResult AddClient(int id)
      {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Stylist selectedStylist = Stylist.Find(id);
        List<Client> stylistClients = selectedStylist.GetClients();
        model.Add("stylist", selectedStylist);
        model.Add("client", stylistClients);
        return View(model);
      }
      [HttpPost("/stylist/{id}/client/new")] //Get-Categories-Id-Client-New.
      public ActionResult NewClient(int id)
      {
        int stylistId = int.Parse(Request.Form["stylist-Id"]);
        Client newClient = new Client(Request.Form["clientName"], stylistId);
        newClient.Save();
        return Redirect("/stylist/" + stylistId);
      }
      [HttpGet("/stylist/{sid}/client/delete/{cid}")] //Get-Categories-sid-Client-cid
      public ActionResult Delete(int sid, int cid)
      {
        Client.DeleteClient(cid);
        return Redirect("/stylist/"+sid);
      }
      [HttpGet("/stylist/{sid}/client/update/{cid}")] //MISSING from RESTful - client update
      public ActionResult Update(int sid, int cid)
      {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Stylist selectedStylist = Stylist.Find(sid);
        Client selectedClient = Client.Find(cid);
        model.Add("stylist", selectedStylist);
        model.Add("client", selectedClient);
        return View(model);
      }
      [HttpPost("/stylist/{sid}/client/update")] //MISSING from RESTful - client update
      public ActionResult UpdateProccess(int id)
      {
        int clientId = int.Parse(Request.Form["client-id"]);
        Client updatedClient = Client.Find(clientId);
        updatedClient.UpdateClient(Request.Form["newName"]);
        return Redirect("/stylist/"+id);
      }

    }
}
