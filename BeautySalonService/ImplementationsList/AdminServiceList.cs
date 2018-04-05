using BeautySalonModel;
using BeautySalonService.BindingModels;
using BeautySalonService.Interfaces;
using BeautySalonService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonService.ImplementationsList
{
   public class AdminServiceList:IAdminService
    {
        private DataListSingleton source;

        public AdminServiceList()
        {
            source = DataListSingleton.GetInstance();
        }

        public List<AdminViewModel> GetList()
        {
            List<AdminViewModel> result = source.Admins
                .Select(rec => new AdminViewModel
                {
                    id = rec.id,
                    adminFirstName = rec.adminFirstName,
                    adminSecondName=rec.adminSecondName
                })
                .ToList();
            return result;
        }

        public AdminViewModel GetElement(int id)
        {
            Admin element = source.Admins.FirstOrDefault(rec => rec.id == id);
            if (element != null)
            {
                return new AdminViewModel
                {
                    id = element.id,
                    adminFirstName = element.adminFirstName,
                    adminSecondName=element.adminSecondName
                };
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(AdminBindingModel model)
        {
            Admin element = source.Admins.FirstOrDefault(rec => rec.adminFirstName == model.adminFirstName && rec.adminSecondName==model.adminSecondName);
            if (element != null)
            {
                throw new Exception("Уже есть сотрудник с таким ФИО");
            }
            int maxId = source.Admins.Count > 0 ? source.Admins.Max(rec => rec.id) : 0;
            source.Admins.Add(new Admin
            {
                id = maxId + 1,
                adminFirstName = model.adminFirstName,
                adminSecondName=model.adminSecondName
            });
        }

        public void UpdElement(AdminBindingModel model)
        {
            Admin element = source.Admins.FirstOrDefault(rec =>
                                        rec.adminFirstName == model.adminFirstName && rec.adminSecondName == model.adminSecondName && rec.id != model.id);
            if (element != null)
            {
                throw new Exception("Уже есть сотрудник с таким ФИО");
            }
            element = source.Admins.FirstOrDefault(rec => rec.id == model.id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.adminFirstName = model.adminFirstName;
            element.adminSecondName = model.adminSecondName;
        }

        public void DelElement(int id)
        {
            Admin element = source.Admins.FirstOrDefault(rec => rec.id == id);
            if (element != null)
            {
                source.Admins.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}
