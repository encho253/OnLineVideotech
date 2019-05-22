﻿using Microsoft.AspNetCore.Mvc;
using OnLineVideotech.Services.Admin.Models;
using OnLineVideotech.Services.Interfaces;
using OnLineVideotech.Web.Areas.Admin.Models.Genres;
using OnLineVideotech.Web.Infrastructure.Extensions;
using System;
using System.Threading.Tasks;

namespace OnLineVideotech.Web.Areas.Admin.Controllers
{
    public class GenreController : BaseAdminController
    {
        private readonly IGenreService genreService;

        public GenreController(IGenreService genreService)
        {
            this.genreService = genreService;
        }  

        public async Task<IActionResult> Add()
        {
            AddGenreViewModel model = new AddGenreViewModel();
            model.Genres = await this.genreService.GetAllGenres();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddGenreViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await this.genreService.Create(model.Name);

            TempData.AddSuccessMessage($"Genre '{model.Name}' successfully created !");

            return RedirectToAction(nameof(Add));
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            GenreServiceModel genre = await this.genreService.FindGenre(id);
            EditGenreViewModel genreViewModel = new EditGenreViewModel
            {
                Id = genre.Id,
                Name = genre.Name
            };

            return View(genreViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditGenreViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            GenreServiceModel genreServiceModel = new GenreServiceModel
            {
                Id = model.Id,
                Name = model.Name
            };

            await this.genreService.UpdateGenre(genreServiceModel);

            TempData.AddSuccessMessage($"Genre '{model.Name}' successfully updated !");

            return RedirectToAction(nameof(Add));
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            GenreServiceModel genre = await this.genreService.FindGenre(id);
            EditGenreViewModel genreViewModel = new EditGenreViewModel
            {
                Id = genre.Id,
                Name = genre.Name
            };

            return View(genreViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EditGenreViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            GenreServiceModel genreServiceModel = new GenreServiceModel
            {
                Id = model.Id,
                Name = model.Name
            };

            await this.genreService.Delete(genreServiceModel);

            TempData.AddSuccessMessage($"Genre '{model.Name}' successfully deleted !");


            return RedirectToAction(nameof(Add));
        }
    }
}