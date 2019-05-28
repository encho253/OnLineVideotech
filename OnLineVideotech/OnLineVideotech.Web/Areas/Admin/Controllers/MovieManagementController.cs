﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnLineVideotech.Data.Models;
using OnLineVideotech.Services.Admin.Interfaces;
using OnLineVideotech.Services.Admin.ServiceModels;
using OnLineVideotech.Web.Areas.Admin.Models;
using OnLineVideotech.Web.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnLineVideotech.Web.Areas.Admin.Controllers
{
    public class MovieManagementController : BaseAdminController
    {
        private readonly IMovieManagementService movies;
        private readonly UserManager<User> userManager;
        private readonly IRoleService roleService;
        private readonly IGenreService genreService;

        public MovieManagementController(
            IMovieManagementService movies,
            UserManager<User> userManager,
            IRoleService roleService,
            IGenreService genreService)
        {
            this.movies = movies;
            this.userManager = userManager;
            this.roleService = roleService;
            this.genreService = genreService;
        }

        public async Task<IActionResult> CreateMovie()
        {
            IEnumerable<Role> roles = await roleService.GetAllRoles();
            IEnumerable<GenreServiceModel> genres = await genreService.GetAllGenres();

            AddMovieViewModel model = new AddMovieViewModel();
            model.Prices = new List<PriceServiceModel>();
            model.Genres = new List<GenreServiceModel>();

            foreach (Role role in roles)
            {
                PriceServiceModel priceModel = new PriceServiceModel();
                priceModel.Role = role;
                model.Prices.Add(priceModel);
            }

            foreach (GenreServiceModel genre in genres)
            {
                GenreServiceModel genreModel = new GenreServiceModel();
                genreModel.Name = genre.Name;
                genreModel.Id = genre.Id;
                model.Genres.Add(genreModel);
            }

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMovie(AddMovieViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }
             
           await this.movies.Create(
               model.Name,
               model.Year,
               model.Rating,
               model.VideoPath,
               model.PosterPath,
               model.TrailerPath,
               model.Summary,
               model.Prices,
               model.Genres);

            TempData.AddSuccessMessage($"Movie '{model.Name}' successfully created");

            return RedirectToAction(nameof(CreateMovie));
        }

        public async Task<IActionResult> EditMovie(Guid id)
        {
            MovieAdminServiceModel model = await this.movies.FindMovie(id);
            return this.View(model);
        }

        //[HttpPost]
        //public async Task<IActionResult> EditMovie()
        //{
        //    return this.View();
        //}
    }
}