using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Mini_bot_sushi
{
    public class UserBase
    {
        public enum Time : byte
        {
            morning,
            afternoon,
            evening,
            night
        }
        private Time timeOfDay;
        public Time TimeOfDay
        {
            get
            {
                TimeSpan timeNow = DateTime.Now.TimeOfDay;
                int hourNow = timeNow.Hours;

                if (hourNow >= 9 && hourNow < 12)
                {
                    timeOfDay = Time.morning;
                }

                else if (hourNow >= 12 && hourNow < 18)
                {
                    timeOfDay = Time.afternoon;
                }

                else if (hourNow >= 18 && hourNow < 22)
                {
                    timeOfDay = Time.evening;
                }

                else timeOfDay = Time.night;
                return timeOfDay;
            }
            set
            {
                timeOfDay = value;
            }
        }

        private protected List<User> userBase = new List<User>();

        public void AddUserInfo(User newUser)
        {
            userBase.Add(new User()
            {
                Name = newUser.Name,
                Address = newUser.Address ?? "unknown",
                Email = newUser.Email ?? "unknown",
            });
        }

        public void PrintUserBase()
        {
            foreach (var x in userBase)
            {
                Console.WriteLine($"{x.Name} {x.Address} {x.Email}");
            }
        }

        public string StringUserOrder(User user)
        {
            string stringOrder;
            //Console.WriteLine($"Your order:");
            stringOrder = $"Your order, {user.Name}:\n";
            stringOrder += $"Your address: {user.Address}\n";
            stringOrder += $"Your Email: {user.Email}\n";
            foreach (var x in user.Order.Distinct())
            {
                //Console.WriteLine($"{x.NameOfSushi} - {user.Order.Where(y => y == x).Count()} units");
                stringOrder = stringOrder + $"{x.NameOfSushi} - {user.Order.Where(y => y == x).Count()} units\n";
            }
            double tempSum = 0;
            foreach (var x in user.Order)
            {
                tempSum = tempSum + x.Price;
            }
            //Console.WriteLine($"Your final check: {tempSum:#.##} BYR\n");
            stringOrder = stringOrder + $"Your final check: {tempSum:#.##} BYR\n";
            return stringOrder;
        }

        public void UpdateUserInfo(User user)
        {
            if (userBase.Exists(x => x.Name.Equals(user.Name)))
            {
                User userForUpdate = userBase.Find(x => x.Name.Equals(user.Name));
                userForUpdate.Address = user.Address ?? "unknown";
                userForUpdate.Email = user.Email ?? "unknown";
            }
        }

        public void SendEmail(User user, string bodyMail)
        {
            using (MailMessage newMail = new MailMessage("Sushi Bar <mgpp@tut.by>", user.Email))
            {
                newMail.Subject = "Order";
                newMail.Body = bodyMail;
                newMail.IsBodyHtml = false;
                using (SmtpClient sc = new SmtpClient("smtp.yandex.ru", 25))
                {
                    sc.EnableSsl = true;
                    sc.DeliveryMethod = SmtpDeliveryMethod.Network;
                    sc.UseDefaultCredentials = false;
                    sc.Credentials = new NetworkCredential("mgpp@tut.by", "password");
                    sc.Send(newMail);
                }
            }
        }
    }
}
