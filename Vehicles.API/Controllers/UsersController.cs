using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vehicles.API.Data;
using Vehicles.API.Data.Entities;
using Vehicles.API.Helpers;
using Vehicles.API.Models;
using Vehicles.Common.Enums;

namespace Vehicles.API.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly DataContext _context;
        private readonly IBlobHelper _blobHelper;
        private readonly IConverterHelper _converterHelper;
        private readonly ICombosHelper _combosHelper;
        private readonly IUserHelper _userHelper;

        public UsersController(DataContext context, IBlobHelper blobHelper, IConverterHelper converterHelper, ICombosHelper combosHelper, IUserHelper userHelper)
        {
            _context = context;
            _blobHelper = blobHelper;
            _converterHelper = converterHelper;
            _combosHelper = combosHelper;
            _userHelper = userHelper;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.Include(u => u.DocumentType).Include(u => u.Vehicles).ToListAsync());
        }

        public IActionResult Create()
        {
            UserViewModel model = new UserViewModel
            {
                DocumentTypes = _combosHelper.GetComboDocumentTypes()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                Guid imageId = Guid.Empty;

                if (model.ImageFile != null)
                {
                    imageId = await _blobHelper.UploadBlobAsync(model.ImageFile, "users");
                }

                User user = await _converterHelper.ToUserAsync(model, imageId, true);
                user.UserType = UserType.User;
                user.UserName = model.Email;
                await _userHelper.AddUserAsync(user, "123456");
                await _userHelper.AddUserToRoleAsync(user, user.UserType.ToString());
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            Guid userId = Guid.Parse(id);
            User user = await _userHelper.GetUserAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            await _userHelper.DeleteUserAsync(user);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            Guid userId = Guid.Parse(id);
            User user = await _userHelper.GetUserAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            UserViewModel model = _converterHelper.ToUserViewModel(user);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                Guid imageId = model.ImageId;

                if (model.ImageFile != null)
                {
                    imageId = await _blobHelper.UploadBlobAsync(model.ImageFile, "users");
                }

                User user = await _converterHelper.ToUserAsync(model, imageId, false);
                await _userHelper.UpdateUser(user);
                return RedirectToAction(nameof(Index));
            }

            model.DocumentTypes = _combosHelper.GetComboDocumentTypes();
            return View(model);
        }

        public async Task<IActionResult> Details(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            User user = await _context.Users
                .Include(x => x.DocumentType)
                .Include(x => x.Vehicles)
                .ThenInclude(x => x.Brand)
                .Include(x => x.Vehicles)
                .ThenInclude(x => x.VehicleType)
                .Include(x => x.Vehicles)
                .ThenInclude(x => x.VehiclePhotos)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        public async Task<IActionResult> AddVehicle(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            User user = await _context.Users
                .Include(x => x.Vehicles)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            VehicleViewModel model = new VehicleViewModel
            {
                UserId = user.Id,
                Brands = _combosHelper.GetComboBrands(),
                VehicleTypes = _combosHelper.GetComboVehicleTypes()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddVehicle(VehicleViewModel vehicleViewModel)
        {
            User user = await _context.Users
                .Include(x => x.Vehicles)
                .FirstOrDefaultAsync(x => x.Id == vehicleViewModel.UserId);
            if (user == null)
            {
                return NotFound();
            }

            try
            {
                Guid imageId = Guid.Empty;

                if (vehicleViewModel.ImageFile != null)
                {
                    imageId = await _blobHelper.UploadBlobAsync(vehicleViewModel.ImageFile, "vehicles");
                }

                Vehicle vehicle = await _converterHelper.ToVehicleAsync(vehicleViewModel, true);
                if (vehicle.VehiclePhotos == null)
                {
                    vehicle.VehiclePhotos = new List<VehiclePhoto>();
                }

                vehicle.VehiclePhotos.Add(new VehiclePhoto
                {
                    ImageId = imageId
                });

                user.Vehicles.Add(vehicle);
                _context.Update(user);
                await _context.SaveChangesAsync();

                return RedirectToAction($"{nameof(Index)}");
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                {
                    ModelState.AddModelError(string.Empty, "Ya existe un vehículo con esa placa.");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                }
            }
            catch (Exception exception)
            {
                ModelState.AddModelError(string.Empty, exception.Message);
            }

            vehicleViewModel.Brands = _combosHelper.GetComboBrands();
            vehicleViewModel.VehicleTypes = _combosHelper.GetComboVehicleTypes();
            return View(vehicleViewModel);
        }
    }
}
