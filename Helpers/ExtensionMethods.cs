using System;
using System.Collections.Generic;
using System.Linq;
using DevOne.Security.Cryptography.BCrypt;
using MyPersonalPlannerBackend.Model;

namespace MyPersonalPlannerBackend.Helpers
{
    public static class ExtensionMethods
    {
        public static IEnumerable<User> WithoutPasswords(this IEnumerable<User> users)
        {
            return users.Select(x => x.WithoutPassword());
        }

        public static User WithoutPassword(this User user)
        {
            user.Password = null;
            return user;
        }

        public static String HashPassword(this String plainPassword)
        {
            var salt = BCryptHelper.GenerateSalt(6);
            var hashedPassword = BCryptHelper.HashPassword(plainPassword, salt);
            return hashedPassword;
        }

    }
}
