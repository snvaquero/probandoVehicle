using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vehicles.API.Data.Entities;
using Vehicles.API.Models;

namespace Vehicles.API.Helpers
{
    public class ConverterHelper : IConverterHelper
    {
        public User ToUser(UserViewModel model, Guid imageId, bool isNew)
        {
            return new User
            {
                Id = isNew ? Guid.NewGuid().ToString() : model.Id,
                ImageId = imageId,
                Address = model.Address,
                Document = model.Document,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                UserName = model.UserName,
                DocumentType = model.DocumentType,
                UserType = model.UserType,
                Vehicles = model.Vehicles
            };
        }

        public UserViewModel ToUserViewModel(User user)
        {
            return new UserViewModel
            {
                Id = user.Id,
                ImageId = user.ImageId,
                Address = user.Address,
                Document = user.Document,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                UserName = user.UserName,
                DocumentType = user.DocumentType,
                UserType = user.UserType,
                Vehicles = user.Vehicles
            };
        }
    }
}
