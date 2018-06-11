using BeautySalonService.BindingModel;
using BeautySalonService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonService.Interfaces
{
   public interface IMessageInfoService
    {
        List<MessageInfoViewModel> GetList();
        MessageInfoViewModel GetElement(int id);
        void AddElement(MessageInfoBindingModel model);
    }
}
