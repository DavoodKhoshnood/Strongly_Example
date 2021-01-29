using Strongly_Myself.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Strongly_Myself.Controllers
{
    public class HomeController : Controller
    {
        EF_DBEntities db = new EF_DBEntities();
        // GET: Home
        public ActionResult Index()
        {
            return View(db.People.Select(p => new PersonViewModel()
            {
                ID = p.ID,
                Name = p.Name,
                Email = p.Email,
                Phone = p.Phone,
                CarModel = p.PersonCar.CarModel,
                CarPlaque = p.PersonCar.CarPlaque
            }));
        }
        public ActionResult Detail(int id)
        {
            return View(db.People.Where(p=> p.ID==id).Select(p=> new PersonViewModel()
            {
                ID = p.ID,
                Name = p.Name,
                Email = p.Email,
                Phone = p.Phone,
                CarModel = p.PersonCar.CarModel,
                CarPlaque = p.PersonCar.CarPlaque
            }).First());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(PersonViewModel person)
        {
            People p = new People()
            {
                Name = person.Name,
                Email = person.Email,
                Phone = person.Phone
            };
            db.People.Add(p);

            PersonCar pc = new PersonCar()
            {
                PersonID = person.ID,
                CarModel = person.CarModel,
                CarPlaque = person.CarPlaque
            };
            db.PersonCar.Add(pc);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            return View(db.People.Where(p => p.ID == id).Select(p => new PersonViewModel()
            {
                ID = p.ID,
                Name = p.Name,
                Email = p.Email,
                Phone = p.Phone,
                CarModel = p.PersonCar.CarModel,
                CarPlaque = p.PersonCar.CarPlaque
            }).First());
        }

        [HttpPost]
        public ActionResult Edit(PersonViewModel person)
        {
            People p = db.People.Find(person.ID);
                 
            p.Name = person.Name;
            p.Email = person.Email;
            p.Phone = person.Phone;
        
            PersonCar cp = db.PersonCar.Find(person.ID);
            if (cp == null)
            {
                PersonCar cpNew = new PersonCar()
                {
                    PersonID = person.ID,
                    CarModel = person.CarModel,
                    CarPlaque = person.CarPlaque
                };
                db.PersonCar.Add(cpNew);
            }
            else
            {
                cp.PersonID = person.ID;
                cp.CarModel = person.CarModel;
                cp.CarPlaque = person.CarPlaque;
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var p = db.People.Find(id);
            var cp = db.PersonCar.Find(id);

            db.PersonCar.Remove(cp);
            db.People.Remove(p);

            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}