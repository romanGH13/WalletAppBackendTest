using Microsoft.EntityFrameworkCore;
using WalletAppBackend.Entities;
using WalletAppBackend.Models;
using WalletAppBackend.Repositories;

namespace WalletAppBackend.Services
{
    public class UserService : IUserService
    {
        public static readonly decimal MaxLimit = 1500;

        private IRepository<User> _userRepository;

        public UserService(IRepository<User> userRepository)
        {
            this._userRepository = userRepository;
        }

        public async Task<User> AddUser(string userName)
        {
            Random r = new Random();
            int balance = r.Next(0, 1500);

            User user = new User() { UserName = userName, CardBalance = balance };

            User resultUser = _userRepository.Add(user);

            await _userRepository.SaveChangesAsync();

            return resultUser;
        }

        public Task<User?> GetById(int id)
        {
            return _userRepository.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public PagedResponse<User> GetUsers(int page, int pageSize)
        {
            var query = _userRepository.GetQueryable();

            var result = _userRepository.GetPaged(query, page, pageSize);

            return result;
        }

        public string CalculateDailyPoints()
        {
            DateTime currentDay = DateTime.UtcNow;
            DateTime pointerDayOfYear = new DateTime().AddYears(currentDay.Year - 1);

            double dayliPoints = 0;

            double dayBeforeYesterdayPoints = 0;
            double yesterdayPoints = 0;

            // for the first 60 days we take 0 points
            while (pointerDayOfYear.DayOfYear <= currentDay.DayOfYear)
            {
                // check if it's new season of year
                if (pointerDayOfYear.Day == 1 && (pointerDayOfYear.Month == 3 || pointerDayOfYear.Month == 6 || pointerDayOfYear.Month == 9 || pointerDayOfYear.Month == 12))
                {
                    dayliPoints = 1;
                }
                else if (pointerDayOfYear.Day == 2 && (pointerDayOfYear.Month == 3 || pointerDayOfYear.Month == 6 || pointerDayOfYear.Month == 9 || pointerDayOfYear.Month == 12))
                {
                    dayliPoints = 3;
                }
                else
                {
                    dayliPoints = dayBeforeYesterdayPoints * 0.6 + yesterdayPoints;
                }

                pointerDayOfYear = pointerDayOfYear.AddDays(1);

                dayBeforeYesterdayPoints = yesterdayPoints;
                yesterdayPoints = dayliPoints;

            }

            return ConvertNumber(Convert.ToInt32(dayliPoints));
        }

        public string ConvertNumber(int num)
        {
            if (num >= 1000)
                return string.Concat(num / 1000, "k");
            else
                return num.ToString();
        }

    }
}
