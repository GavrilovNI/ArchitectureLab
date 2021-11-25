﻿using Microsoft.AspNetCore.Mvc;
using Web.Data;
using Web.Data.Models;
using Web.Data.Repositories;

namespace Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly DataContext _dataContext = new DataContext();

        public IActionResult Index()
        {

            List<Product> products = new ProductRepository(_dataContext).GetAllAsList();
            //products.Add(new Product("Nvidia RTX 3070", 100500, "It's heater", 2, "/img/rtx3070.png"));
            //products.Add(new Product("Watermelon", 47, "Nam, Nam", 4519, "/img/watermelon.png"));



            return View(products);
        }


    }
}
