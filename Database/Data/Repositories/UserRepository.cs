﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Areas.Identity.Data;
using Web.Data;
using Web.Data.Repositories;

namespace Database.Data.Repositories
{
    public class UserRepository : Repository
    {
        protected UserRepository(DataContext context) : base(context)
        {
        }

        protected DbSet<User> Users => Context.Users;
        protected DbSet<IdentityRole> Roles => Context.Roles;
        protected DbSet<IdentityUserRole<string>> UserRoles => Context.UserRoles;

        private IdentityRole AdminRole => Roles.Where(r => r.NormalizedName == "ADMIN").First();

        public IQueryable<User> GetAll()
        {
            return Users.AsQueryable();
        }

        public User? GetById(string id)
        {
            return Users.Where(u => u.Id == id).FirstOrDefault(u => true, null);
        }

        public User? GetByEmail(string email)
        {
            return Users.Where(u => u.NormalizedEmail == email.ToUpper()).FirstOrDefault(u => true, null);
        }

        public void SetAdmin(User user)
        {
            if(IsAdmin(user) == false)
            {
                UserRoles.Add(new IdentityUserRole<string>() { UserId = user.Id, RoleId = AdminRole.Id });
                Context.SaveChanges();
            }
        }

        public void SetNotAdmin(User user)
        {
            if (IsAdmin(user))
            {
                UserRoles.Remove(UserRoles.Where(r => r.UserId == user.Id && r.RoleId == AdminRole.Id).First());
                Context.SaveChanges();
            }
        }

        public bool IsAdmin(User user)
        {
            bool isAdmin = UserRoles.Where(r => r.UserId == user.Id && r.RoleId == AdminRole.Id).Count() > 0;
            return isAdmin;
        }

        public void Remove(User user)
        {
            Users.Remove(user);
            Context.SaveChanges();
        }
    }
}
