using BeautySalonModel;
using BeautySalonService.BindingModels;
using BeautySalonService.Interfaces;
using BeautySalonService.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BeautySalonService.ImplementationsBD
{
    public class DeliveryServiceBD:IDeliveryService
    {
        private AbstractDataBaseContext context;

        public DeliveryServiceBD(AbstractDataBaseContext context)
        {
            this.context = context;
        }
        public void SendEmail(string mailAddress, string subject, string text)
        {
            MailMessage objMailMessage = new MailMessage();
            SmtpClient objSmtpClient = null;
            
            context.MessageInfos.Add(new MessageInfo
            {
                MessageId = mailAddress,
                FromMailAddress = ConfigurationManager.AppSettings["MailLogin"],
                Subject = subject,
                Body = text,
                DateDelivery = DateTime.Now,
                buyerId = null
            });

            context.SaveChanges();

            try
            {
                objMailMessage.From = new MailAddress(ConfigurationManager.AppSettings["MailLogin"]);
                objMailMessage.To.Add(new MailAddress(mailAddress));
                objMailMessage.Subject = subject;
                objMailMessage.Body = text;
                objMailMessage.SubjectEncoding = System.Text.Encoding.UTF8;
                objMailMessage.BodyEncoding = System.Text.Encoding.UTF8;
                objMailMessage.Attachments.Add(new Attachment("D:\\deliverys.xls"));

                objSmtpClient = new SmtpClient("smtp.gmail.com", 587);
                objSmtpClient.UseDefaultCredentials = false;
                objSmtpClient.EnableSsl = true;
                objSmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                objSmtpClient.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["MailLogin"],
                    ConfigurationManager.AppSettings["MailPassword"]);

                objSmtpClient.Send(objMailMessage);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objMailMessage = null;
                objSmtpClient = null;
            }
        }
        public List<DeliveryViewModel> GetList()
        {
            List<DeliveryViewModel> result = context.Deliverys
                .Select(rec => new DeliveryViewModel
                {
                    id = rec.id,
                    Date=rec.Date,
                    deliveryResource = context.DeliveryResources
                            .Where(recPC => recPC.deliveryId == rec.id)
                            .Select(recPC => new DeliveryResourceViewModel
                            {
                                
                                id = recPC.id,
                                count = recPC.count,
                                price = recPC.resource.price
                            })
                            .ToList()
                })
                .ToList();
            return result;
        }

        public DeliveryViewModel GetElement(int id)
        {
            Delivery element = context.Deliverys.FirstOrDefault(rec => rec.id == id);
            if (element != null)
            {
                return new DeliveryViewModel
                {
                    id = element.id,
                    Date=element.Date,
                    deliveryResource = context.DeliveryResources
                            .Where(recPC => recPC.deliveryId == element.id)
                            .Select(recPC => new DeliveryResourceViewModel
                            {
                                id = recPC.id,
                                count = recPC.count,
                                price = recPC.resource.price
                            }).ToList()
                };
            }
            throw new Exception("Элемент не найден");
        }

        public int AddElement(DeliveryBindingModel model)
        {
            Delivery element = context.Deliverys.FirstOrDefault(rec => rec.name == model.name);
            if (element != null)
            {
                throw new Exception("Уже есть доставка с таким названием");
            }
            element = new Delivery
            {
                Date = DateTime.Now,
                name = model.name
            };
            context.Deliverys.Add(element);
            try
            {
                context.SaveChanges();
                return element.id;
            }
            catch (DbUpdateException e) {
                var sb = new StringBuilder();
                sb.AppendLine($"DbUpdateException error details - {e?.InnerException?.InnerException?.Message}");
                foreach (var eve in e.Entries) {
                    sb.AppendLine($"Entity of type {eve.Entity.GetType().Name} in state {eve.State} could not be updated");
                }
                Console.Write(sb.ToString());
                throw;
            }
        }

        public void UpdElement(DeliveryBindingModel model)
        {
            Delivery element = context.Deliverys.FirstOrDefault(rec => rec.name == model.name &&
                                        rec.id != model.id);
            if (element != null)
            {
                throw new Exception("Уже есть доставка с таким id");
            }
            element = context.Deliverys.FirstOrDefault(rec => rec.id == model.id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.Date = DateTime.Now;
            element.name = model.name;
            context.SaveChanges();
        }

        public void DelElement(int id)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Delivery element = context.Deliverys.FirstOrDefault(rec => rec.id == id);
                    if (element != null)
                    {
                        context.DeliveryResources.RemoveRange(
                                            context.DeliveryResources.Where(rec => rec.deliveryId == id));
                        context.Deliverys.Remove(element);
                        context.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("Элемент не найден");
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}
