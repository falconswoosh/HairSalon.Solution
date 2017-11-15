using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using HairSalon.Models;

namespace HairSalon.Controllers
{
    public class HomeController : Controller
    {
      [HttpGet("/")]
      public ActionResult Index()
      {
        List<Stylist> stylists = Stylist.GetAll();
        return View(stylists);
      }
      [HttpPost("/stylist")]
      public ActionResult StylistPost()
      {
        Stylist newStylist = new Stylist(Request.Form["stylistName"]);
        List<Stylist> allStylists = Stylist.GetAll();
        newStylist.Save();
        return View("StylistDetail",newStylist);
      }
      [HttpGet("/stylist/clients")]
      public ActionResult ClientsAll()
      {
        List<Client> allclients = Client.GetAll();
        return View(allclients);
      }
      [HttpGet("/stylist/clients/delete")]
      public ActionResult ClientsDeleteAll()
      {
        Client.DeleteAll();
        return Redirect("/stylist/clients");
      }
      [HttpGet("/stylist/new")]
      public ActionResult StylistForm()
      {
        return View();
      }
      [HttpGet("/stylist/{id}")]
      public ActionResult StylistDetail(int id)
      {
        Dictionary<string, object> model = new Dictionary<string, object> {};
        Stylist selectedStylist = Stylist.Find(id);
        List<Client> stylistClients = selectedStylist.GetClients();
        model.Add("stylist", selectedStylist);
        model.Add("clients", stylistClients);
        return View(model);
      }
      [HttpPost("/stylist/{id}/client")]
      public ActionResult ClientPost(int id)
      {
        Client newClient = new Client(Request.Form["name"], id);
        newClient.Save();
        Dictionary<string, object> model = new Dictionary<string, object> {};
        Stylist selectedStylist = Stylist.Find(id);
        List<Client> stylistClients = Client.GetAllStylistsClients(id);
        model.Add("client", newClient);
        model.Add("stylist", selectedStylist);
        model.Add("clients", stylistClients);
        return View("StylistDetail",model);
      }
      [HttpGet("/stylist/{id}/client/new")]
      public ActionResult ClientForm(int id)
      {
        Stylist selectedStylist = Stylist.Find(id);
        return View(selectedStylist);
      }
      [HttpGet("/stylist/{sid}/client/{cid}/delete")]
      public ActionResult Delete(int sid, int cid)
      {
        Client.DeleteClient(cid);
        return Redirect("/stylist/"+sid);
      }

      [HttpGet("/stylist/{sid}/client/{cid}/update")]
      public ActionResult UpdateClient(int sid, int cid)
      {
        Dictionary<string, object> model = new Dictionary<string, object> {};
        Stylist selectedStylist = Stylist.Find(sid);
        Client selectedClient = Client.Find(cid);
        model.Add("stylist", selectedStylist);
        model.Add("client", selectedClient);
        return View(model);
      }
      [HttpPost("/stylist/{sid}/client/{cid}/update")]
      public ActionResult UpdateClientPost(int sid, int cid)
      {
        Client selectedClient = Client.Find(cid);
        selectedClient.UpdateClient(Request.Form["name"],selectedClient.GetStylistId());

        Dictionary<string, object> model = new Dictionary<string, object> {};
        model.Add("client", selectedClient);
        Stylist selectedStylist = Stylist.Find(sid);
        List<Client> stylistClients = Client.GetAllStylistsClients(sid);
        model.Add("stylist", selectedStylist);
        model.Add("clients", stylistClients);
        return View("StylistDetail",model);
      }
      [HttpGet("/stylist/{sid}/update")]
      public ActionResult UpdateStylist(int sid)
      {
        Stylist thisStylist = Stylist.Find(sid);
        return View(thisStylist);
      }

      [HttpPost("/stylist/{sid}/update")]
      public ActionResult UpdateStylistPost(int sid)
      {
        Stylist updatedStylist = Stylist.Find(sid);
        updatedStylist.UpdateStylist(Request.Form["name"], sid);
        return Redirect("/stylist/"+sid);
      }
    }
}
