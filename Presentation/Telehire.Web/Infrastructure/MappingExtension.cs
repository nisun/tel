using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Telehire.Data.Domain;
using Telehire.Web.Models;

namespace Telehire.Web.Infrastructure
{
    public static class MappingExtension
    {
        #region AdminRegisterUserModel

        public static PersonalInformation ToEntity(this AdminRegisterUserModel model)
        {
            return Mapper.Map<AdminRegisterUserModel, PersonalInformation>(model);
        }

        public static AdminRegisterUserModel ToModel(this PersonalInformation entity)
        {
            return Mapper.Map<PersonalInformation, AdminRegisterUserModel>(entity);
        }

        public static PersonalInformation ToEntity(this AdminRegisterUserModel model, PersonalInformation destination)
        {
            return Mapper.Map(model, destination);
        }

        #endregion


    }
}