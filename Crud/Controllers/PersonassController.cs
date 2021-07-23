using Crud.Data;
using Crud.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crud.Controllers
{[Authorize]
    public class PersonassController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public PersonassController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;

        }

        [Authorize (Roles ="Rey,Peon")]
        public IActionResult Index()
        {
            List<Persona> personas = new List<Persona>();
            personas = _applicationDbContext.Persona.ToList();

            return View(personas);
        }
        [Authorize(Roles = "Rey,Peon")]
        public IActionResult Details(int id) 
        {
            if (id ==0)
                return RedirectToAction("Index");
            Persona persona = _applicationDbContext.Persona.Where(D => D.Codigo == id).FirstOrDefault();
            if (persona==null)
                return RedirectToAction("Index");
            return View(persona);
        }
        [Authorize(Roles = "Rey")]
        public IActionResult Create()
        {
         

            return View();
        }
        [Authorize(Roles = "Peon")]
        [HttpPost]
        public IActionResult Create(Persona persona)
        {
           
            try
            {
                persona.Estado = 1;
                _applicationDbContext.Add(persona);
                _applicationDbContext.SaveChanges();
            }
            catch (Exception)
            {

                return View(persona);
            }
           
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Rey")]
        public IActionResult Edit(int id) 
        {
            if (id == 0)
            {
                return RedirectToAction("Index");
            }
            Persona persona = _applicationDbContext.Persona.Where(D => D.Codigo == id).FirstOrDefault();

            if (persona == null)
                return RedirectToActionPermanent("Index");
                return View(persona);
        }
        [Authorize(Roles = "Rey")]
        [HttpPost]
        public IActionResult Edit(int id,Persona persona)
        {
            if (id != persona.Codigo)
                return RedirectToAction("Index");
            try
            {
                persona.Estado = 1;
                _applicationDbContext.Update(persona);
                _applicationDbContext.SaveChanges();
            }
            catch (Exception)
            {

                return View(persona);
            }

            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Rey")]
        public IActionResult Delete(int id)
        {
            if (id == 0)
            
                return RedirectToAction("Index");
            Persona persona = _applicationDbContext.Persona.Where(D => D.Codigo == id).FirstOrDefault();
            try
            {
                _applicationDbContext.Remove(persona);
                _applicationDbContext.SaveChanges();
            }
            catch (Exception ex)
            {

                return RedirectToActionPermanent("Index");
            }
          
                return RedirectToActionPermanent("Index");
           
        }
        [Authorize(Roles = "Rey")]
        public IActionResult Desactivar(int id)
        {
            if (id == 0)

                return RedirectToAction("Index");
            Persona persona = _applicationDbContext.Persona.Where(D => D.Codigo == id).FirstOrDefault();
            try
            {
                persona.Estado = 0;
                _applicationDbContext.Update(persona);
                _applicationDbContext.SaveChanges();
            }
            catch (Exception ex)
            {

                return RedirectToActionPermanent("Index");
            }

            return RedirectToActionPermanent("Index");

        }
        [Authorize(Roles = "Rey")]
        public IActionResult Activar(int id)
        {
            if (id == 0)

                return RedirectToAction("Index");
            Persona persona = _applicationDbContext.Persona.Where(D => D.Codigo == id).FirstOrDefault();
            try
            {
                persona.Estado = 1;
                _applicationDbContext.Update(persona);
                _applicationDbContext.SaveChanges();
            }
            catch (Exception ex)
            {

                return RedirectToActionPermanent("Index");
            }

            return RedirectToActionPermanent("Index");

        }
    }

}
