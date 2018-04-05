using BeautySalonModel;
using BeautySalonService.BindingModels;
using BeautySalonService.Interfaces;
using BeautySalonService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonService.ImplementationsBD
{
    public class AdminServiceBD:IAdminService
    {
        private AbstractDataBaseContext context;

        public AdminServiceBD(AbstractDataBaseContext context)
        {
            this.context = context;
        }

        public List<AdminViewModel> GetList()
        {
            List<AdminViewModel> result = context.Admins
                .Select(rec => new AdminViewModel
                {
                    id = rec.id,
                    adminFirstName = rec.adminFirstName,
                    adminSecondName = rec.adminSecondName
                })
                .ToList();
            return result;
        }

        public AdminViewModel GetElement(int id)
        {
            Admin element = context.Admins.FirstOrDefault(rec => rec.id == id);
            if (element != null)
            {
                return new AdminViewModel
                {
                    id = element.id,
                    adminFirstName = element.adminFirstName,
                    adminSecondName = element.adminSecondName
                };
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(AdminBindingModel model)
        {
            Admin element = context.Admins.FirstOrDefault(rec => rec.adminFirstName == model.adminFirstName && rec.adminSecondName == model.adminSecondName);
            if (element != null)
            {
                throw new Exception("Уже есть сотрудник с таким ФИО");
            }
            context.Admins.Add(new Admin
            {
                adminFirstName = model.adminFirstName,
                adminSecondName = model.adminSecondName
            });
            context.SaveChanges();
        }

        public void UpdElement(AdminBindingModel model)
        {
            Admin element = context.Admins.FirstOrDefault(rec =>
                                        rec.adminFirstName == model.adminFirstName && rec.adminSecondName == model.adminSecondName && rec.id != model.id);
            if (element != null)
            {
                throw new Exception("Уже есть сотрудник с таким ФИО");
            }
            element = context.Admins.FirstOrDefault(rec => rec.id == model.id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.adminFirstName = model.adminFirstName;
            element.adminSecondName = model.adminSecondName;
            context.SaveChanges();
        }

        public void DelElement(int id)
        {
            Admin element = context.Admins.FirstOrDefault(rec => rec.id == id);
            if (element != null)
            {
                context.Admins.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}
