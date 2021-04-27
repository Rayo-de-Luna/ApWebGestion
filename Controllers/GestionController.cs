﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using AWGestion.Models;

namespace AWGestion.Controllers
{
    [Authorize]
    public class GestionController : Controller
    {
        private GestionContext _context;
        
        public GestionController(GestionContext context)
        {
            _context = context;
        }
        
        // GET
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
    }
}